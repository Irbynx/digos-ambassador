﻿//
//  UserKink.cs
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

using System.ComponentModel.DataAnnotations;
using DIGOS.Ambassador.Database.Interfaces;
using DIGOS.Ambassador.Database.Kinks;
using JetBrains.Annotations;

namespace DIGOS.Ambassador.Database.Users
{
    /// <summary>
    /// Represents a user's kink, along with their preference for it.
    /// </summary>
    public class UserKink : IEFEntity
    {
        /// <inheritdoc />
        public long ID { get; set; }

        /// <summary>
        /// Gets or sets the kink.
        /// </summary>
        [Required]
        public Kink Kink { get; set; }

        /// <summary>
        /// Gets or sets the user's preference for the kink.
        /// </summary>
        public KinkPreference Preference { get; set; }

        /// <summary>
        /// Creates a new <see cref="UserKink"/> from the given <see cref="Kink"/>.
        /// </summary>
        /// <param name="kink">The kink.</param>
        /// <returns>The user kink.</returns>
        [NotNull]
        public static UserKink CreateFrom(Kink kink)
        {
            return new UserKink
            {
                Kink = kink,
                Preference = KinkPreference.NoPreference
            };
        }
    }
}
