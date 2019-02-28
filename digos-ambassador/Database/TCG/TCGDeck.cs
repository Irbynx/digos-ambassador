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

using System.Collections.Generic;
using Discord;
using DIGOS.Ambassador.Database.Interfaces;
using DIGOS.Ambassador.Database.Users;
using JetBrains.Annotations;

namespace DIGOS.Ambassador.Database.TCG
{
    /// <summary>
    /// Contains information about a player made deck
    /// </summary>
    public class TCGDeck: IOwnedNamedEntity, IServerEntity
    {
        /// <inheritdoc />
        public long ID { get; set; }

        /// <inheritdoc />
        public long ServerID { get; set; }

        /// <inheritdoc />
        public User Owner { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        [NotNull]
        public string EntityTypeDisplayName => nameof(TCGDeck);

        /// <inheritdoc />
        public bool IsOwner(User user)
        {
            return IsOwner(user.DiscordID);
        }

        /// <inheritdoc />
        public bool IsOwner(IUser user)
        {
            return IsOwner((long)user.Id);
        }

        /// <inheritdoc />
        public bool IsOwner(long userID)
        {
            return this.Owner.DiscordID == userID;
        }

        /// <summary>
        /// Returns or sets the list of the cards contained in this deck
        /// </summary>
        public List<TCGCard> Cards { get; set; }
    }
}