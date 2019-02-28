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
    /// Represents an active creature during the match
    /// </summary>
    public class TCGCardCharacterEntity  : IEFEntity
    {
        /// <inheritdoc />
        public long ID { get; set; }
        
        /// <summary>
        /// Gets or sets the card that this CardEntity represents
        /// </summary>
        public TCGCard Card { get; set; }
        
        /// <summary>
        /// Gets or sets the participant that owns this card
        /// </summary>
        public TCGMatchParticipant Owner { get; set; }
        
        /// <summary>
        /// Gets or sets the health of this character
        /// </summary>
        public long HP { get; set; }
        
        /// <summary>
        /// Gets or sets the action points of this character
        /// </summary>
        public long AP { get; set; }
        
        /// <summary>
        /// Gets or sets the attached cards to this character
        /// </summary>
        public List<TCGCardEntity> AttachedCards { get; set; }
    }
}