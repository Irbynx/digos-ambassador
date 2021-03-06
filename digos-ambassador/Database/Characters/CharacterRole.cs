//
//  CharacterRole.cs
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

using DIGOS.Ambassador.Database.Interfaces;
using DIGOS.Ambassador.Database.ServerInfo;

namespace DIGOS.Ambassador.Database.Characters
{
    /// <summary>
    /// Represents a role associated with a character, similar to a nickname.
    /// </summary>
    public class CharacterRole : IEFEntity
    {
        /// <inheritdoc />
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the server that the role is on.
        /// </summary>
        public Server Server { get; set; }

        /// <summary>
        /// Gets or sets the role ID, taken from Discord.
        /// </summary>
        public long DiscordID { get; set; }

        /// <summary>
        /// Gets or sets the access conditions of the role.
        /// </summary>
        public RoleAccess Access { get; set; }
    }
}
