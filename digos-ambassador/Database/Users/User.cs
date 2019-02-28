﻿//
//  User.cs
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

using System.Collections.Generic;
using DIGOS.Ambassador.Database.Characters;
using DIGOS.Ambassador.Database.Interfaces;
using DIGOS.Ambassador.Database.TCG;
using JetBrains.Annotations;

namespace DIGOS.Ambassador.Database.Users
{
    /// <summary>
    /// Represents globally accessible information about a user.
    /// </summary>
    public class User : IEFEntity
    {
        /// <inheritdoc />
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the Discord ID of the user.
        /// </summary>
        public long DiscordID { get; set; }

        /// <summary>
        /// Gets or sets the class of the user within the DIGOS 'verse.
        /// </summary>
        public UserClass Class { get; set; }

        /// <summary>
        /// Gets or sets the biography of the user. This contains useful information that the users provide themselves.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the current timezone of the user. This is an hour offset ( + or - ) to UTC/GMT.
        /// </summary>
        [CanBeNull]
        public int? Timezone { get; set; }

        /// <summary>
        /// Gets or sets the user's default character.
        /// </summary>
        public Character DefaultCharacter { get; set; }

        /// <summary>
        /// Gets or sets the characters that the user owns.
        /// </summary>
        public List<Character> Characters { get; set; }

        /// <summary>
        /// Gets or sets the kinks or fetishes of a user, as well as their preferences for each.
        /// </summary>
        public List<UserKink> Kinks { get; set; }

        /// <summary>
        /// Gets or sets the currently ongoing match the user takes part in
        /// </summary>
        public TCGMatch CurrentMatch { get; set; }

        /// <summary>
        /// Gets or sets the decks that this player owns
        /// </summary>
        public List<TCGDeck> Decks { get; set; }
    }
}
