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
using System.ComponentModel.DataAnnotations;
using DIGOS.Ambassador.Database.Interfaces;
using DIGOS.Ambassador.Database.Users;
using JetBrains.Annotations;

namespace DIGOS.Ambassador.Database.TCG
{
    /// <summary>
    /// Contains information about a participant of a match
    /// </summary>
    public class TCGMatchParticipant : IEFEntity
    {
        /// <inheritdoc />
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the match that the user is a part of.
        /// </summary>
        [Required]
        public TCGMatch Match { get; set; }

        /// <summary>
        /// Gets or sets the user that is part of the match. If null, the participant is "AI".
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the list of cards that comprise the "hand" of the participant
        /// </summary>
        public List<TCGCardEntity> Hand { get; set; }

        /// <summary>
        /// Gets or sets the list of cards that comprise the "draw pile" of the participant
        /// </summary>
        public List<TCGCardEntity> DrawPile { get; set; }

        /// <summary>
        /// Gets or sets the list of cards that comprise the "discard pile" of the participant
        /// </summary>
        public List<TCGCardEntity> DiscardPile { get; set; }
        
        /// <summary>
        /// Gets or sets the list of active character cards
        /// </summary>
        public List<TCGCardCharacterEntity> Characters { get; set; }

        /// <summary>
        /// Gets or sets the side the participant is on
        /// </summary>
        public string Side; 

        /// <summary>
        /// Initializes a new instance of the <see cref="TCGMatchParticipant"/> class.
        /// </summary>
        public TCGMatchParticipant()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TCGMatchParticipant"/> class.
        /// </summary>
        /// <param name="match">The match that the user is participating in.</param>
        /// <param name="user">The user that is participating in the match.</param>
        public TCGMatchParticipant([NotNull] TCGMatch match, [NotNull] User user)
        {
            this.Match = match;
            this.User = user;
        }
    }
}