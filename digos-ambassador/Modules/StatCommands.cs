//
//  StatCommands.cs
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

using System.Linq;
using System.Threading.Tasks;

using DIGOS.Ambassador.Extensions;
using DIGOS.Ambassador.Pagination;
using DIGOS.Ambassador.Services;
using DIGOS.Ambassador.Services.Interactivity;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using JetBrains.Annotations;
using static Discord.Commands.ContextType;
using static Discord.Commands.RunMode;

#pragma warning disable SA1615 // Disable "Element return value should be documented" due to TPL tasks

namespace DIGOS.Ambassador.Modules
{
    /// <summary>
    /// Various statistics-related commands.
    /// </summary>
    [UsedImplicitly]
    [Group("stats")]
    [Summary("Various statistics-related commands.")]
    public class StatCommands : ModuleBase<SocketCommandContext>
    {
        private readonly UserFeedbackService Feedback;
        private readonly InteractivityService Interactivity;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatCommands"/> class.
        /// </summary>
        /// <param name="feedback">The feedback service.</param>
        /// <param name="interactivity">The interactivity service.</param>
        public StatCommands(UserFeedbackService feedback, InteractivityService interactivity)
        {
            this.Feedback = feedback;
            this.Interactivity = interactivity;
        }

        /// <summary>
        /// Displays statistics about the current guild.
        /// </summary>
        [UsedImplicitly]
        [Command("guild", RunMode = Async)]
        [Alias("guild", "server")]
        [Summary("Displays statistics about the current guild.")]
        [RequireContext(Guild)]
        public async Task ShowServerStatsAsync()
        {
            var guild = this.Context.Guild;

            var eb = CreateGuildInfoEmbed(guild);

            await this.Feedback.SendEmbedAsync(this.Context.Channel, eb.Build());
        }

        /// <summary>
        /// Displays statistics about all guilds the bot has joined.
        /// </summary>
        [UsedImplicitly]
        [Command("guilds", RunMode = Async)]
        [Alias("guilds", "servers")]
        [Summary("Displays statistics about all guilds the bot has joined.")]
        [RequireContext(DM)]
        [RequireOwner]
        public async Task ShowServersStatsAsync()
        {
            var guilds = this.Context.Client.Guilds;
            var pages = guilds.Select(CreateGuildInfoEmbed);

            var paginatedEmbed = new PaginatedEmbed().WithPages(pages);
            var paginatedMessage = new PaginatedMessage<EmbedBuilder, PaginatedEmbed>
            (
                this.Feedback,
                paginatedEmbed
            );

            await this.Interactivity.SendPrivateInteractiveMessageAndDeleteAsync
            (
                this.Context,
                this.Feedback,
                paginatedMessage
            );
        }

        /// <summary>
        /// Creates an embed with information about a guild.
        /// </summary>
        /// <param name="guild">The guild.</param>
        /// <returns>The embed.</returns>
        [NotNull]
        private EmbedBuilder CreateGuildInfoEmbed([NotNull] SocketGuild guild)
        {
            var eb = this.Feedback.CreateEmbedBase();

            if (!(guild.SplashUrl is null))
            {
                eb.WithThumbnailUrl(guild.SplashUrl);
            }
            else if (!(guild.IconUrl is null))
            {
                eb.WithThumbnailUrl(guild.IconUrl);
            }

            var authorBuilder = new EmbedAuthorBuilder().WithName(guild.Name);
            if (!(guild.IconUrl is null))
            {
                authorBuilder.WithIconUrl(guild.IconUrl);
            }

            eb.WithAuthor(authorBuilder);

            eb.AddField("Owner", guild.Owner.Mention);
            eb.AddField("Members", guild.MemberCount, true);

            eb.AddField("Created at", guild.CreatedAt);

            return eb;
        }
    }
}
