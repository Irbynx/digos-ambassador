//
//  TCGService.cs
//
//  Author:
//       Irbynx <irbynx@gmail.com>
//
//  Copyright (c) 2019 Irbynx
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
using Discord;
using Discord.Commands;
using Discord.Net.WebSockets;
using DIGOS.Ambassador.Database;
using DIGOS.Ambassador.Database.TCG;
using DIGOS.Ambassador.Database.Users;
using DIGOS.Ambassador.Extensions;
using JetBrains.Annotations;

namespace DIGOS.Ambassador.Services.TCG
{
    /// <summary>
    /// Acts as an interface for accessing and interacting with ongoing TCG games
    /// </summary>
    public class TCGService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TCGService"/> class.
        /// </summary>
        public TCGService()
        {
            
        }

        /// <summary>
        /// Selects a card for a specified user during their turn. The selection may be for a card to play or the target of that card, usually in sequence.
        /// </summary>
        /// <param name="db">The database where the roleplays are stored.</param>
        /// <param name="context">The context of the user.</param>
        /// <param name="targetID">The target ID string, containing either a numeral or a latin letter, specifying the target card out of their available selection.</param>
        /// <param name="user">The user that issued the selection.</param>
        /// <returns>An execution result which may or may not have succeeded.</returns>
        public async Task<ExecuteResult> ParseUserSelection
        (
            [NotNull] GlobalInfoContext db,
            [NotNull] SocketCommandContext context,
            [NotNull] string targetID,
            [NotNull] IUser user
        )
        {
            if (targetID.IsNullOrEmpty())
            {
                var errorMessage = "Target not selected!";

                return ExecuteResult.FromError(CommandError.Unsuccessful, errorMessage);
            }
            
            var retrievedUser = await db.GetOrRegisterUserAsync(user);

            if (!retrievedUser.IsSuccess)
            {
                return ExecuteResult.FromError(CommandError.ObjectNotFound, retrievedUser.ErrorReason);
            }

            if (retrievedUser.Entity.CurrentMatch is null)
            {
                var errorMessage = "You aren't a part of any match";

                return ExecuteResult.FromError(CommandError.Unsuccessful, errorMessage);
            }

            var participant = retrievedUser.Entity.CurrentMatch.Participants
                .Find( u => u.User.DiscordID == (long)user.Id);

            if (retrievedUser.Entity.CurrentMatch.ActiveCharacter.Owner != participant)
            {
                var errorMessage = "It's not your turn yet!";

                return ExecuteResult.FromError(CommandError.Unsuccessful, errorMessage);
            }

            int tgtIndex;
            bool isTgtIDNumber = int.TryParse(targetID, out tgtIndex);

            if (!isTgtIDNumber)
            {
                char convertedLetter = char.ToUpper(targetID.ToCharArray().First());
                if (convertedLetter < 'A' || convertedLetter > 'Z')
                {
                    var errorMessage = "You can only select cards with 1 to 9 numbers or a to z latin letters!";

                    return ExecuteResult.FromError(CommandError.Unsuccessful, errorMessage);
                }
                tgtIndex = convertedLetter - 64;
            }

            return await this.SelectCard(db, context, tgtIndex, participant);
        }
    }
}
