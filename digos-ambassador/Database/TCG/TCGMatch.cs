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
using DIGOS.Ambassador.Database.Interfaces;

namespace DIGOS.Ambassador.Database.TCG
{
    /// <summary>
    /// Contains information about an ongoing match
    /// </summary>
    public class TCGMatch : IEFEntity
    {
        /// <inheritdoc />
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the list that contains all match participants
        /// </summary>
        public List<TCGMatchParticipant> Participants;

        /// <summary>
        /// Gets or sets the ruleset being used in the match
        /// </summary>
        public TCGRuleset Ruleset;
        
        /// <summary>
        /// Gets or sets the list of the characters that already acted this turn
        /// </summary>
        public List<TCGCardCharacterEntity> ActedCharacters;

        /// <summary>
        /// Gets or sets the active character that is acting right now
        /// </summary>
        public TCGCardCharacterEntity ActiveCharacter;

        /// <summary>
        /// Gets or sets the card that the active character played
        /// </summary>
        public TCGCardEntity PlayedCard;

        /// <summary>
        /// Gets or sets the main target enemy for the currently acting character
        /// </summary>
        public TCGCardCharacterEntity TargetFoe;

        /// <summary>
        /// Gets or sets the main target ally for the currently acting character
        /// </summary>
        public TCGCardCharacterEntity TargetAlly;
        
        // Add more possible targets when needed
    }
}