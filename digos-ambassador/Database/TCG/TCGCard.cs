//
//  TCGCard.cs
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
    /// Represents a card that can be used in the TCG.
    /// </summary>
    public class TCGCard : IEFEntity
    {
        /// <inheritdoc />
        public long ID { get; set; }
        
        /// <summary>
        /// Gets or sets the internal name of the card
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the pretty name of the card (shown to users)
        /// </summary>
        public string PrettyName { get; set; }
        
        /// <summary>
        /// Gets or sets the flavor text for the card
        /// </summary>
        public string FlavorText { get; set; }
        
        /// <summary>
        /// Gets or sets the power level of the card; used to balance out decks. Only useful for characters or some special case cards
        /// </summary>
        public long PowerLevel { get; set; }
        
        /// <summary>
        /// Gets or sets the AP cost of the card
        /// </summary>
        public long APCost { get; set; }
        
        /// <summary>
        /// Gets or sets whenever the card is a "creature" card. If true, at the start of the combat the card will be automatically added to the field from the deck of it's owner.
        /// </summary>
        public bool IsCreature { get; set; }
        
        /// <summary>
        /// Gets or sets the Agility (AGI) stat for the card; matters only for creatures. Influences AP gain per turn on 1:1 basis
        /// </summary>
        public long Agility { get; set; }
        
        /// <summary>
        /// Gets or sets the Intelligence (INT) stat for the card; matters only for creatures. Influences card draw per turn on 1:2 basis
        /// </summary>
        public long Intelligence { get; set; }
        
        /// <summary>
        /// Gets or sets the Strength (STR) stat for the card; matters only for creatures. Influences total HP of the card on 1:4 basis
        /// </summary>
        public long Strength { get; set; }
        
        /// <summary>
        /// Gets or sets the list of effects
        /// </summary>
        public List<TCGEffectElement> Effects { get; set; }
    }
}