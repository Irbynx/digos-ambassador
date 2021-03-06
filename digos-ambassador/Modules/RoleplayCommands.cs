﻿//
//  RoleplayCommands.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Affero General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Affero General Public License for more details.
//
//  You should have received a copy of the GNU Affero General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DIGOS.Ambassador.Database;
using DIGOS.Ambassador.Database.Roleplaying;
using DIGOS.Ambassador.Extensions;
using DIGOS.Ambassador.Permissions;
using DIGOS.Ambassador.Permissions.Preconditions;
using DIGOS.Ambassador.Services;
using DIGOS.Ambassador.Services.Exporters;
using DIGOS.Ambassador.TypeReaders;

using Discord;
using Discord.Commands;
using Discord.Net;
using Humanizer;
using JetBrains.Annotations;
using static Discord.Commands.ContextType;
using static Discord.Commands.RunMode;
using PermissionTarget = DIGOS.Ambassador.Permissions.PermissionTarget;

#pragma warning disable SA1615 // Disable "Element return value should be documented" due to TPL tasks

namespace DIGOS.Ambassador.Modules
{
    /// <summary>
    /// Commands for interacting with and managing channel roleplays.
    /// </summary>
    [UsedImplicitly]
    [Alias("roleplay", "rp")]
    [Group("roleplay")]
    [Summary("Commands for interacting with and managing channel roleplays.")]
    [Remarks
    (
        "Parameters which take a roleplay can be specified in two ways - by just the name, which will search your " +
        "roleplays, and by mention and name, which will search the given user's roleplays. For example,\n" +
        "\n" +
        "Your roleplay: ipsum\n" +
        "Another user's roleplay: @DIGOS Ambassador:ipsum\n" +
        "\n" +
        "You can also substitute any roleplay name for \"current\", and the active roleplay will be used instead."
    )]
    public class RoleplayCommands : ModuleBase<SocketCommandContext>
    {
        [ProvidesContext]
        private readonly GlobalInfoContext Database;
        private readonly RoleplayService Roleplays;

        private readonly UserFeedbackService Feedback;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleplayCommands"/> class.
        /// </summary>
        /// <param name="database">A database context from the context pool.</param>
        /// <param name="roleplays">The roleplay service.</param>
        /// <param name="feedback">The user feedback service.</param>
        public RoleplayCommands(GlobalInfoContext database, RoleplayService roleplays, UserFeedbackService feedback)
        {
            this.Database = database;
            this.Roleplays = roleplays;
            this.Feedback = feedback;
        }

        /// <summary>
        /// Shows information about the current.
        /// </summary>
        [UsedImplicitly]
        [Alias("show", "info")]
        [Command("show", RunMode = Async)]
        [Summary("Shows information about the current roleplay.")]
        [RequireContext(Guild)]
        public async Task ShowRoleplayAsync()
        {
            var getCurrentRoleplayResult = await this.Roleplays.GetActiveRoleplayAsync(this.Database, this.Context);
            if (!getCurrentRoleplayResult.IsSuccess)
            {
                await this.Feedback.SendErrorAsync(this.Context, getCurrentRoleplayResult.ErrorReason);
                return;
            }

            var roleplay = getCurrentRoleplayResult.Entity;
            var eb = CreateRoleplayInfoEmbed(roleplay);
            await this.Feedback.SendEmbedAsync(this.Context.Channel, eb);
        }

        /// <summary>
        /// Shows information about the named roleplay owned by the specified user.
        /// </summary>
        /// <param name="roleplay">The roleplay.</param>
        [UsedImplicitly]
        [Alias("show", "info")]
        [Command("show", RunMode = Async)]
        [Summary("Shows information about the specified roleplay.")]
        [RequireContext(Guild)]
        public async Task ShowRoleplayAsync([NotNull] Roleplay roleplay)
        {
            var eb = CreateRoleplayInfoEmbed(roleplay);
            await this.Feedback.SendEmbedAsync(this.Context.Channel, eb);
        }

        [NotNull]
        private Embed CreateRoleplayInfoEmbed([NotNull] Roleplay roleplay)
        {
            var eb = this.Feedback.CreateEmbedBase();

            eb.WithAuthor(this.Context.Client.GetUser((ulong)roleplay.Owner.DiscordID));
            eb.WithTitle(roleplay.Name);
            eb.WithDescription(roleplay.Summary);

            eb.AddField("Currently", $"{(roleplay.IsActive ? "Active" : "Inactive")}", true);
            eb.AddField("Channel", MentionUtils.MentionChannel(this.Context.Channel.Id), true);

            eb.AddField("NSFW", roleplay.IsNSFW ? "Yes" : "No");
            eb.AddField("Public", roleplay.IsPublic ? "Yes" : "No", true);

            var joinedUsers = roleplay.JoinedUsers.Select(p => this.Context.Client.GetUser((ulong)p.User.DiscordID));
            var joinedMentions = joinedUsers.Select(u => u.Mention);

            var participantList = joinedMentions.Humanize();
            participantList = string.IsNullOrEmpty(participantList) ? "None" : participantList;

            eb.AddField("Participants", $"{participantList}");

            return eb.Build();
        }

        /// <summary>
        /// Lists the roleplays that the given user owns.
        /// </summary>
        /// <param name="discordUser">The user to show the roleplays of.</param>
        [UsedImplicitly]
        [Alias("list-owned", "list")]
        [Command("list-owned", RunMode = Async)]
        [Summary("Lists the roleplays that the given user owns.")]
        [RequireContext(Guild)]
        public async Task ListOwnedRoleplaysAsync([CanBeNull] IUser discordUser = null)
        {
            discordUser = discordUser ?? this.Context.Message.Author;

            var eb = this.Feedback.CreateEmbedBase();
            eb.WithAuthor(discordUser);
            eb.WithTitle("Your roleplays");

            var roleplays = this.Roleplays.GetUserRoleplays(this.Database, discordUser, this.Context.Guild);

            foreach (var roleplay in roleplays)
            {
                eb.AddField(roleplay.Name, roleplay.Summary);
            }

            if (eb.Fields.Count <= 0)
            {
                eb.WithDescription("You don't have any roleplays.");
            }

            await this.Feedback.SendEmbedAsync(this.Context.Channel, eb.Build());
        }

        /// <summary>
        /// Creates a new roleplay with the specified name.
        /// </summary>
        /// <param name="roleplayName">The user-unique name of the roleplay.</param>
        /// <param name="roleplaySummary">A summary of the roleplay.</param>
        /// <param name="isNSFW">Whether or not the roleplay is NSFW.</param>
        /// <param name="isPublic">Whether or not the roleplay is public.</param>
        [UsedImplicitly]
        [Command("create", RunMode = Async)]
        [Summary("Creates a new roleplay with the specified name.")]
        [RequireContext(Guild)]
        [RequirePermission(Permission.CreateRoleplay)]
        public async Task CreateRoleplayAsync
        (
            [NotNull] string roleplayName,
            [NotNull] string roleplaySummary = "No summary set.",
            bool isNSFW = false,
            bool isPublic = true
        )
        {
            var result = await this.Roleplays.CreateRoleplayAsync(this.Database, this.Context, roleplayName, roleplaySummary, isNSFW, isPublic);
            if (!result.IsSuccess)
            {
                await this.Feedback.SendErrorAsync(this.Context, result.ErrorReason);
                return;
            }

            await this.Feedback.SendConfirmationAsync(this.Context, $"Roleplay \"{result.Entity.Name}\" created.");
        }

        /// <summary>
        /// Deletes the specified roleplay.
        /// </summary>
        /// <param name="roleplay">The roleplay.</param>
        [UsedImplicitly]
        [Command("delete", RunMode = Async)]
        [Summary("Deletes the specified roleplay.")]
        [RequireContext(Guild)]
        [RequirePermission(Permission.DeleteRoleplay)]
        public async Task DeleteRoleplayAsync
        (
            [NotNull]
            [RequireEntityOwnerOrPermission(Permission.DeleteRoleplay, PermissionTarget.Other)]
            Roleplay roleplay
        )
        {
            this.Database.Attach(roleplay);

            this.Database.Roleplays.Remove(roleplay);
            await this.Database.SaveChangesAsync();

            await this.Feedback.SendConfirmationAsync(this.Context, $"Roleplay \"{roleplay.Name}\" deleted.");
        }

        /// <summary>
        /// Joins the roleplay owned by the given person with the given name.
        /// </summary>
        /// <param name="roleplay">The roleplay.</param>
        [UsedImplicitly]
        [Command("join", RunMode = Async)]
        [Summary("Joins the roleplay owned by the given person with the given name.")]
        [RequireContext(Guild)]
        [RequirePermission(Permission.JoinRoleplay)]
        public async Task JoinRoleplayAsync([NotNull] Roleplay roleplay)
        {
            this.Database.Attach(roleplay);

            var addUserResult = await this.Roleplays.AddUserToRoleplayAsync(this.Database, this.Context, roleplay, this.Context.Message.Author);
            if (!addUserResult.IsSuccess)
            {
                await this.Feedback.SendErrorAsync(this.Context, addUserResult.ErrorReason);
                return;
            }

            var roleplayOwnerUser = this.Context.Guild.GetUser((ulong)roleplay.Owner.DiscordID);
            await this.Feedback.SendConfirmationAsync(this.Context, $"Joined {roleplayOwnerUser.Mention}'s roleplay \"{roleplay.Name}\"");
        }

        /// <summary>
        /// Invites the specified user to the given roleplay.
        /// </summary>
        /// <param name="playerToInvite">The player to invite.</param>
        /// <param name="roleplay">The roleplay.</param>
        [UsedImplicitly]
        [Command("invite", RunMode = Async)]
        [Summary("Invites the specified user to the given roleplay.")]
        [RequireContext(Guild)]
        [RequirePermission(Permission.EditRoleplay)]
        public async Task InvitePlayerAsync
        (
            [NotNull]
            IUser playerToInvite,
            [NotNull]
            [RequireEntityOwnerOrPermission(Permission.EditRoleplay, PermissionTarget.Other)]
            Roleplay roleplay
        )
        {
            this.Database.Attach(roleplay);

            var invitePlayerResult = await this.Roleplays.InviteUserAsync(this.Database, roleplay, playerToInvite);
            if (!invitePlayerResult.IsSuccess)
            {
                await this.Feedback.SendErrorAsync(this.Context, invitePlayerResult.ErrorReason);
                return;
            }

            await this.Feedback.SendConfirmationAsync(this.Context, $"Invited {playerToInvite.Mention} to {roleplay.Name}.");

            var userDMChannel = await playerToInvite.GetOrCreateDMChannelAsync();
            try
            {
                await userDMChannel.SendMessageAsync(
                    $"You've been invited to join {roleplay.Name}. Use \"!rp join {roleplay.Name}\" to join.");
            }
            catch (HttpException hex) when (hex.WasCausedByDMsNotAccepted())
            {
            }
            finally
            {
                await userDMChannel.CloseAsync();
            }
        }

        /// <summary>
        /// Leaves the roleplay owned by the given person with the given name.
        /// </summary>
        /// <param name="roleplay">The roleplay.</param>
        [UsedImplicitly]
        [Command("leave", RunMode = Async)]
        [Summary("Leaves the roleplay owned by the given person with the given name.")]
        [RequireContext(Guild)]
        public async Task LeaveRoleplayAsync([NotNull] Roleplay roleplay)
        {
            this.Database.Attach(roleplay);

            var removeUserResult = await this.Roleplays.RemoveUserFromRoleplayAsync(this.Database, this.Context, roleplay, this.Context.Message.Author);
            if (!removeUserResult.IsSuccess)
            {
                await this.Feedback.SendErrorAsync(this.Context, removeUserResult.ErrorReason);
                return;
            }

            var roleplayOwnerUser = this.Context.Guild.GetUser((ulong)roleplay.Owner.DiscordID);
            await this.Feedback.SendConfirmationAsync(this.Context, $"Left {roleplayOwnerUser.Mention}'s roleplay \"{roleplay.Name}\"");
        }

        /// <summary>
        /// Kicks the given user from the named roleplay.
        /// </summary>
        /// <param name="discordUser">The user to kick.</param>
        /// <param name="roleplay">The roleplay.</param>
        [UsedImplicitly]
        [Command("kick", RunMode = Async)]
        [Summary("Kicks the given user from the named roleplay.")]
        [RequireContext(Guild)]
        [RequirePermission(Permission.KickRoleplayMember)]
        public async Task KickRoleplayParticipantAsync
        (
            [NotNull]
            IUser discordUser,
            [NotNull]
            [RequireEntityOwnerOrPermission(Permission.KickRoleplayMember, PermissionTarget.Other)]
            Roleplay roleplay
        )
        {
            this.Database.Attach(roleplay);

            var kickUserResult = await this.Roleplays.KickUserFromRoleplayAsync(this.Database, this.Context, roleplay, discordUser);
            if (!kickUserResult.IsSuccess)
            {
                await this.Feedback.SendErrorAsync(this.Context, kickUserResult.ErrorReason);
                return;
            }

            var userDMChannel = await discordUser.GetOrCreateDMChannelAsync();
            try
            {
                await userDMChannel.SendMessageAsync
                (
                    $"You've been removed from the \"{roleplay.Name}\" by {this.Context.Message.Author.Username}."
                );
            }
            catch (HttpException hex) when (hex.WasCausedByDMsNotAccepted())
            {
            }
            finally
            {
                await userDMChannel.CloseAsync();
            }

            await this.Feedback.SendConfirmationAsync(this.Context, $"{discordUser.Mention} has been kicked from {roleplay.Name}.");
        }

        /// <summary>
        /// Makes the roleplay with the given name current in the current channel.
        /// </summary>
        /// <param name="roleplay">The roleplay.</param>
        [UsedImplicitly]
        [Command("make-current", RunMode = Async)]
        [Summary("Makes the roleplay with the given name current in the current channel.")]
        [RequireContext(Guild)]
        [RequirePermission(Permission.StartStopRoleplay)]
        public async Task MakeRoleplayCurrentAsync
        (
            [NotNull]
            [RequireEntityOwnerOrPermission(Permission.StartStopRoleplay, PermissionTarget.Other)]
            Roleplay roleplay
        )
        {
            this.Database.Attach(roleplay);

            var isNsfwChannel = this.Context.Channel is ITextChannel textChannel && textChannel.IsNsfw;
            if (roleplay.IsNSFW && !isNsfwChannel)
            {
                await this.Feedback.SendErrorAsync(this.Context, "This channel is not marked as NSFW, while your roleplay is... naughty!");
                return;
            }

            roleplay.ActiveChannelID = (long)this.Context.Channel.Id;
            await this.Database.SaveChangesAsync();

            await this.Feedback.SendConfirmationAsync(this.Context, $"The roleplay \"{roleplay.Name}\" is now current in #{this.Context.Channel.Name}.");
        }

        /// <summary>
        /// Starts the roleplay with the given name.
        /// </summary>
        /// <param name="roleplay">The roleplay.</param>
        [UsedImplicitly]
        [Command("start", RunMode = Async)]
        [Summary("Starts the roleplay with the given name.")]
        [RequireContext(Guild)]
        [RequirePermission(Permission.StartStopRoleplay)]
        public async Task StartRoleplayAsync
        (
            [NotNull]
            [RequireEntityOwnerOrPermission(Permission.StartStopRoleplay, PermissionTarget.Other)]
            Roleplay roleplay
        )
        {
            this.Database.Attach(roleplay);

            var isNsfwChannel = this.Context.Channel is ITextChannel textChannel && textChannel.IsNsfw;
            if (roleplay.IsNSFW && !isNsfwChannel)
            {
                await this.Feedback.SendErrorAsync(this.Context, "This channel is not marked as NSFW, while your roleplay is... naughty!");
                return;
            }

            if (await this.Roleplays.HasActiveRoleplayAsync(this.Database, this.Context.Channel))
            {
                await this.Feedback.SendWarningAsync(this.Context, "There's already a roleplay active in this channel.");

                var currentRoleplayResult = await this.Roleplays.GetActiveRoleplayAsync(this.Database, this.Context);
                if (!currentRoleplayResult.IsSuccess)
                {
                    await this.Feedback.SendErrorAsync(this.Context, currentRoleplayResult.ErrorReason);
                    return;
                }

                var currentRoleplay = currentRoleplayResult.Entity;
                var timeOfLastMessage = currentRoleplay.Messages.Last().Timestamp;
                var currentTime = DateTimeOffset.Now;
                if (timeOfLastMessage < currentTime.AddHours(-4))
                {
                    await this.Feedback.SendConfirmationAsync(this.Context, "However, that roleplay has been inactive for over four hours.");
                    currentRoleplay.IsActive = false;
                }
                else
                {
                    return;
                }
            }

            if (roleplay.ActiveChannelID != (long)this.Context.Channel.Id)
            {
                roleplay.ActiveChannelID = (long)this.Context.Channel.Id;
            }

            roleplay.IsActive = true;
            await this.Database.SaveChangesAsync();

            var joinedUsers = roleplay.JoinedUsers.Select(p => this.Context.Client.GetUser((ulong)p.User.DiscordID));
            var joinedMentions = joinedUsers.Select(u => u.Mention);

            var participantList = joinedMentions.Humanize();
            await this.Feedback.SendConfirmationAsync(this.Context, $"The roleplay \"{roleplay.Name}\" is now active in {MentionUtils.MentionChannel(this.Context.Channel.Id)}.");
            await this.Context.Channel.SendMessageAsync($"Calling {participantList}!");
        }

        /// <summary>
        /// Stops the given roleplay.
        /// </summary>
        /// <param name="roleplay">The roleplay.</param>
        [UsedImplicitly]
        [Command("stop", RunMode = Async)]
        [Summary("Stops the given roleplay.")]
        [RequireContext(Guild)]
        [RequirePermission(Permission.StartStopRoleplay)]
        public async Task StopRoleplayAsync
        (
            [NotNull]
            [RequireEntityOwnerOrPermission(Permission.StartStopRoleplay, PermissionTarget.Other)]
            Roleplay roleplay
        )
        {
            this.Database.Attach(roleplay);

            roleplay.IsActive = false;
            await this.Database.SaveChangesAsync();

            await this.Feedback.SendConfirmationAsync(this.Context, $"The roleplay \"{roleplay.Name}\" has been stopped.");
        }

        /// <summary>
        /// Includes previous messages into the roleplay, starting at the given time.
        /// </summary>
        /// <param name="roleplay">The roleplay.</param>
        /// <param name="startMessage">The earliest message to start adding from.</param>
        /// <param name="finalMessage">The final message in the range.</param>
        [UsedImplicitly]
        [Command("include-previous", RunMode = Async)]
        [Summary("Includes previous messages into the roleplay, starting at the given message.")]
        [RequireContext(Guild)]
        [RequirePermission(Permission.EditRoleplay)]
        public async Task IncludePreviousMessagesAsync
        (
            [NotNull]
            [RequireEntityOwnerOrPermission(Permission.EditRoleplay, PermissionTarget.Other)]
            Roleplay roleplay,
            [OverrideTypeReader(typeof(UncachedMessageTypeReader<IMessage>))]
            IMessage startMessage,
            [CanBeNull]
            [OverrideTypeReader(typeof(UncachedMessageTypeReader<IMessage>))]
            IMessage finalMessage = null
        )
        {
            finalMessage = finalMessage ?? this.Context.Message;

            this.Database.Attach(roleplay);

            int addedOrUpdatedMessageCount = 0;

            var latestMessage = startMessage;
            while (latestMessage.Timestamp < finalMessage.Timestamp)
            {
                var messages = (await this.Context.Channel.GetMessagesAsync(latestMessage, Direction.After).FlattenAsync()).OrderBy(m => m.Timestamp).ToList();
                latestMessage = messages.Last();

                foreach (var message in messages)
                {
                    // Jump out if we've passed the final message
                    if (message.Timestamp > finalMessage.Timestamp)
                    {
                        break;
                    }

                    var modifyResult = await this.Roleplays.AddToOrUpdateMessageInRoleplayAsync(this.Database, roleplay, message);
                    if (modifyResult.IsSuccess)
                    {
                        ++addedOrUpdatedMessageCount;
                    }
                }
            }

            await this.Feedback.SendConfirmationAsync(this.Context, $"{addedOrUpdatedMessageCount} messages added to \"{roleplay.Name}\".");
        }

        /// <summary>
        /// Transfers ownership of the named roleplay to the specified user.
        /// </summary>
        /// <param name="newOwner">The new owner.</param>
        /// <param name="roleplay">The roleplay.</param>
        [UsedImplicitly]
        [Command("transfer-ownership", RunMode = Async)]
        [Summary("Transfers ownership of the named roleplay to the specified user.")]
        [RequireContext(Guild)]
        [RequirePermission(Permission.TransferRoleplay)]
        public async Task TransferRoleplayOwnershipAsync
        (
            [NotNull] IUser newOwner,
            [NotNull]
            [RequireEntityOwnerOrPermission(Permission.TransferRoleplay, PermissionTarget.Other)]
            Roleplay roleplay
        )
        {
            this.Database.Attach(roleplay);

            var transferResult = await this.Roleplays.TransferRoleplayOwnershipAsync(this.Database, newOwner, roleplay, this.Context.Guild);
            if (!transferResult.IsSuccess)
            {
                await this.Feedback.SendErrorAsync(this.Context, transferResult.ErrorReason);
                return;
            }

            await this.Feedback.SendConfirmationAsync(this.Context, "Roleplay ownership transferred.");
        }

        /// <summary>
        /// Exports the named roleplay owned by the given user, sending you a file with the contents.
        /// </summary>
        /// <param name="roleplay">The roleplay.</param>
        /// <param name="format">The export format.</param>
        [UsedImplicitly]
        [Command("export", RunMode = Async)]
        [Summary(" Exports the named roleplay owned by the given user, sending you a file with the contents.")]
        [RequireContext(Guild)]
        [RequirePermission(Permission.ReplayRoleplay)]
        public async Task ExportRoleplayAsync
        (
            [NotNull]
            Roleplay roleplay,
            [OverrideTypeReader(typeof(HumanizerEnumTypeReader<ExportFormat>))]
            ExportFormat format = ExportFormat.PDF
        )
        {
            IRoleplayExporter exporter;
            switch (format)
            {
                case ExportFormat.PDF:
                {
                    exporter = new PDFRoleplayExporter(this.Context);
                    break;
                }
                case ExportFormat.Plaintext:
                {
                    exporter = new PlaintextRoleplayExporter(this.Context);
                    break;
                }
                default:
                {
                    await this.Feedback.SendErrorAsync(this.Context, "That export format hasn't been implemented yet.");
                    return;
                }
            }

            await this.Feedback.SendConfirmationAsync(this.Context, "Compiling the roleplay...");
            using (var output = await exporter.ExportAsync(roleplay))
            {
                await this.Context.Channel.SendFileAsync(output.Data, $"{output.Title}.{output.Format.GetFileExtension()}");
            }
        }

        /// <summary>
        /// Replays the named roleplay owned by the given user to you.
        /// </summary>
        /// <param name="roleplay">The roleplay.</param>
        /// <param name="from">The time from which you want to replay,</param>
        /// <param name="to">The time until you want to replay.</param>
        [UsedImplicitly]
        [Command("replay", RunMode = Async)]
        [Summary("Replays the named roleplay owned by the given user to you.")]
        [RequireContext(Guild)]
        [RequirePermission(Permission.ReplayRoleplay)]
        public async Task ReplayRoleplayAsync
        (
            [NotNull] Roleplay roleplay,
            DateTimeOffset from = default,
            DateTimeOffset to = default
        )
        {
            if (from == default)
            {
                from = DateTimeOffset.MinValue;
            }

            if (to == default)
            {
                to = DateTimeOffset.Now;
            }

            var userDMChannel = await this.Context.Message.Author.GetOrCreateDMChannelAsync();
            var eb = CreateRoleplayInfoEmbed(roleplay);

            try
            {
                await userDMChannel.SendMessageAsync(string.Empty, false, eb);

                var messages = roleplay.Messages.Where
                (
                    m =>
                        m.Timestamp > from && m.Timestamp < to
                )
                .OrderBy(msg => msg.Timestamp).ToList();

                var timestampEmbed = this.Feedback.CreateFeedbackEmbed
                (
                    this.Context.User,
                    Color.DarkPurple,
                    $"Roleplay began at {messages.First().Timestamp.ToUniversalTime()}"
                );

                await userDMChannel.SendMessageAsync(string.Empty, false, timestampEmbed);

                if (messages.Count <= 0)
                {
                    await userDMChannel.SendMessageAsync("No messages found in the specified timeframe.");
                    return;
                }

                await this.Feedback.SendConfirmationAsync
                (
                    this.Context,
                    $"Replaying \"{roleplay.Name}\". Please check your private messages."
                );

                const int messageCharacterLimit = 2000;
                var sb = new StringBuilder(messageCharacterLimit);

                foreach (var message in messages)
                {
                    var newContent = $"**{message.AuthorNickname}** {message.Contents}\n";

                    if (sb.Length + newContent.Length >= messageCharacterLimit)
                    {
                        await userDMChannel.SendMessageAsync(sb.ToString());
                        await Task.Delay(TimeSpan.FromSeconds(2));

                        sb.Clear();
                        sb.AppendLine();
                    }

                    sb.Append(newContent);

                    if (message.ID == messages.Last().ID)
                    {
                        await userDMChannel.SendMessageAsync(sb.ToString());
                    }
                }
            }
            catch (HttpException hex) when (hex.WasCausedByDMsNotAccepted())
            {
                await this.Feedback.SendWarningAsync
                (
                    this.Context,
                    "I can't do that, since you don't accept DMs from non-friends on this server."
                );
            }
            finally
            {
                await userDMChannel.CloseAsync();
            }
        }

        /// <summary>
        /// Setter commands for roleplay properties.
        /// </summary>
        [UsedImplicitly]
        [Group("set")]
        public class SetCommands : ModuleBase<SocketCommandContext>
        {
            [ProvidesContext]
            private readonly GlobalInfoContext Database;
            private readonly RoleplayService Roleplays;

            private readonly UserFeedbackService Feedback;

            /// <summary>
            /// Initializes a new instance of the <see cref="SetCommands"/> class.
            /// </summary>
            /// <param name="database">A database context from the context pool.</param>
            /// <param name="roleplays">The roleplay service.</param>
            /// <param name="feedback">The user feedback service.</param>
            public SetCommands(GlobalInfoContext database, RoleplayService roleplays, UserFeedbackService feedback)
            {
                this.Database = database;
                this.Roleplays = roleplays;
                this.Feedback = feedback;
            }

            /// <summary>
            /// Sets the name of the named roleplay.
            /// </summary>
            /// <param name="newRoleplayName">The roleplay's new name.</param>
            /// <param name="roleplay">The roleplay.</param>
            [UsedImplicitly]
            [Command("name", RunMode = Async)]
            [Summary("Sets the new name of the named roleplay.")]
            [RequireContext(Guild)]
            [RequirePermission(Permission.EditRoleplay)]
            public async Task SetRoleplayNameAsync
            (
                [NotNull]
                string newRoleplayName,
                [NotNull]
                [RequireEntityOwnerOrPermission(Permission.EditRoleplay, PermissionTarget.Other)]
                Roleplay roleplay
            )
            {
                this.Database.Attach(roleplay);

                var result = await this.Roleplays.SetRoleplayNameAsync(this.Database, this.Context, roleplay, newRoleplayName);
                if (!result.IsSuccess)
                {
                    await this.Feedback.SendErrorAsync(this.Context, result.ErrorReason);
                    return;
                }

                await this.Feedback.SendConfirmationAsync(this.Context, "Roleplay name set.");
            }

            /// <summary>
            /// Sets the summary of the named roleplay.
            /// </summary>
            /// <param name="newRoleplaySummary">The roleplay's new summary.</param>
            /// <param name="roleplay">The roleplay</param>
            [UsedImplicitly]
            [Command("summary", RunMode = Async)]
            [Summary("Sets the summary of the named roleplay.")]
            [RequireContext(Guild)]
            [RequirePermission(Permission.EditRoleplay)]
            public async Task SetRoleplaySummaryAsync
            (
                [NotNull]
                string newRoleplaySummary,
                [NotNull]
                [RequireEntityOwnerOrPermission(Permission.EditRoleplay, PermissionTarget.Other)]
                Roleplay roleplay
            )
            {
                this.Database.Attach(roleplay);

                var result = await this.Roleplays.SetRoleplaySummaryAsync(this.Database, roleplay, newRoleplaySummary);
                if (!result.IsSuccess)
                {
                    await this.Feedback.SendErrorAsync(this.Context, result.ErrorReason);
                    return;
                }

                await this.Feedback.SendConfirmationAsync(this.Context, "Roleplay summary set.");
            }

            /// <summary>
            /// Sets a value indicating whether or not the named roleplay is NSFW. This restricts which channels it
            /// can be made active in.
            /// </summary>
            /// <param name="isNSFW">true if the roleplay is NSFW; otherwise, false.</param>
            /// <param name="roleplay">The roleplay.</param>
            [UsedImplicitly]
            [Command("nsfw", RunMode = Async)]
            [Summary("Sets a value indicating whether or not the named roleplay is NSFW. This restricts which channels it can be made active in.")]
            [RequireContext(Guild)]
            [RequirePermission(Permission.EditRoleplay)]
            public async Task SetRoleplayIsNSFW
            (
                bool isNSFW,
                [NotNull]
                [RequireEntityOwnerOrPermission(Permission.EditRoleplay, PermissionTarget.Other)]
                Roleplay roleplay
            )
            {
                this.Database.Attach(roleplay);

                var result = await this.Roleplays.SetRoleplayIsNSFWAsync(this.Database, roleplay, isNSFW);
                if (!result.IsSuccess)
                {
                    await this.Feedback.SendErrorAsync(this.Context, result.ErrorReason);
                    return;
                }

                await this.Feedback.SendConfirmationAsync(this.Context, $"Roleplay set to {(isNSFW ? "NSFW" : "SFW")}");
            }

            /// <summary>
            /// Sets a value indicating whether or not the named roleplay is publíc. This restricts replays to participants.
            /// </summary>
            /// <param name="isPublic">true if the roleplay is public; otherwise, false.</param>
            /// <param name="roleplay">The roleplay.</param>
            [UsedImplicitly]
            [Command("public", RunMode = Async)]
            [Summary("Sets a value indicating whether or not the named roleplay is public. This restricts replays to participants.")]
            [RequireContext(Guild)]
            [RequirePermission(Permission.EditRoleplay)]
            public async Task SetRoleplayIsPublic
            (
                bool isPublic,
                [NotNull]
                [RequireEntityOwnerOrPermission(Permission.EditRoleplay, PermissionTarget.Other)]
                Roleplay roleplay
            )
            {
                this.Database.Attach(roleplay);

                var result = await this.Roleplays.SetRoleplayIsPublicAsync(this.Database, roleplay, isPublic);
                if (!result.IsSuccess)
                {
                    await this.Feedback.SendErrorAsync(this.Context, result.ErrorReason);
                    return;
                }

                await this.Feedback.SendConfirmationAsync(this.Context, $"Roleplay set to {(isPublic ? "public" : "private")}");
            }
        }
    }
}
