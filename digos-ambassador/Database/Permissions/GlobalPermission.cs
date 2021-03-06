﻿//
//  GlobalPermission.cs
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

using System.Diagnostics.CodeAnalysis;
using DIGOS.Ambassador.Database.Interfaces;
using DIGOS.Ambassador.Permissions;
using JetBrains.Annotations;

namespace DIGOS.Ambassador.Database.Permissions
{
    /// <summary>
    /// Represents a globally granted permission.
    /// </summary>
    public class GlobalPermission : IPermission<GlobalPermission>, IEFEntity
    {
        /// <inheritdoc />
        public long ID { get; set; }

        /// <inheritdoc />
        public long UserDiscordID { get; set; }

        /// <inheritdoc />
        public Permission Permission { get; set; }

        /// <inheritdoc />
        public PermissionTarget Target { get; set; }

        /// <inheritdoc />
        public bool Equals(GlobalPermission other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return
                this.Permission == other.Permission &&
                this.Target == other.Target &&
                this.UserDiscordID == other.UserDiscordID;
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "ArrangeThisQualifier", Justification = "Used for explicit differentiation between compared objects.")]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((GlobalPermission)obj);
        }

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode", Justification = "Class is an entity.")]
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (int)this.Permission;
                hashCode = (hashCode * 397) ^ (int)this.Target;
                hashCode = (hashCode * 397) ^ this.UserDiscordID.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Compares the equality of two <see cref="GlobalPermission"/> objects.
        /// </summary>
        /// <param name="left">The first object.</param>
        /// <param name="right">The second object.</param>
        /// <returns>true if the objects are equal; otherwise, false.</returns>
        public static bool operator ==([CanBeNull] GlobalPermission left, [CanBeNull] GlobalPermission right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Compares the inequality of two <see cref="GlobalPermission"/> objects.
        /// </summary>
        /// <param name="left">The first object.</param>
        /// <param name="right">The second object.</param>
        /// <returns>true if the objects are equal; otherwise, false.</returns>
        public static bool operator !=([CanBeNull] GlobalPermission left, [CanBeNull] GlobalPermission right)
        {
            return !Equals(left, right);
        }
    }
}
