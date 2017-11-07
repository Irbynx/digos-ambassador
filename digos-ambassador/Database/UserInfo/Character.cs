//
//  Character.cs
//
//  Author:
//       Jarl Gullberg <jarl.gullberg@gmail.com>
//
//  Copyright (c) 2017 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

namespace DIGOS.Ambassador.Database.UserInfo
{
	/// <summary>
	/// Represents a user's character.
	/// </summary>
	public class Character
	{
		/// <summary>
		/// Gets or sets the character's unique key.
		/// </summary>
		public uint CharacterID { get; set; }

		/// <summary>
		/// Gets or sets the user that owns this character.
		/// </summary>
		public User Owner { get; set; }

		/// <summary>
		/// Gets or sets the user-unique name of the character.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets a URL pointing to the character's avatar.
		/// </summary>
		public string Avatar { get; set; }

		/// <summary>
		/// Gets or sets the nickname that a user should have when playing as the character.
		/// </summary>
		public string Nickname { get; set; }

		/// <summary>
		/// Gets or sets the character summary.
		/// </summary>
		public string Summary { get; set; }

		/// <summary>
		/// Gets or sets the full description of the character.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the skin color
		/// </summary>
		public string SkinColor { get; set; }

		/// <summary>
		/// Gets or sets the skin pattern type
		/// </summary>
		public string SkinPatternType { get; set; }

		/// <summary>
		/// Gets or sets the skin pattern color
		/// </summary>
		public string SkinPatternColor { get; set; }

		/// <summary>
		/// Gets or sets the fur color
		/// </summary>
		public string FurColor { get; set; }

		/// <summary>
		/// Gets or sets the fur pattern type
		/// </summary>
		public string FurPatternType { get; set; }

		/// <summary>
		/// Gets or sets the fur pattern color
		/// </summary>
		public string FurPatternColor { get; set; }

		/// <summary>
		/// Gets or sets the scales color
		/// </summary>
		public string ScalesColor { get; set; }

		/// <summary>
		/// Gets or sets the scales pattern type
		/// </summary>
		public string ScalesPatternType { get; set; }

		/// <summary>
		/// Gets or sets the scales pattern color
		/// </summary>
		public string ScalesPatternColor { get; set; }

		/// <summary>
		/// Gets or sets the hair color
		/// </summary>
		public string HairColor { get; set; }

		/// <summary>
		/// Gets or sets the hair pattern type
		/// </summary>
		public string HairPatternType { get; set; }

		/// <summary>
		/// Gets or sets the hair pattern color
		/// </summary>
		public string HairPatternColor { get; set; }

		/// <summary>
		/// Gets or sets the hair style
		/// </summary>
		public string HairStyle { get; set; }

		/// <summary>
		/// Gets or sets the hair length
		/// </summary>
		public int HairLength { get; set; }

		/// <summary>
		/// Gets or sets the eye color
		/// </summary>
		public string EyeColor { get; set; }

		/// <summary>
		/// Gets or sets the head TF species
		/// </summary>
		public string HeadTF { get; set; }

		/// <summary>
		/// Gets or sets the body TF species
		/// </summary>
		public string BodyTF { get; set; }

		/// <summary>
		/// Gets or sets the tail TF species
		/// </summary>
		public string TailTF { get; set; }

		/// <summary>
		/// Gets or sets the arm TF species
		/// </summary>
		public string ArmsTF { get; set; }

		/// <summary>
		/// Gets or sets the legs TF species
		/// </summary>
		public string LegsTF { get; set; }

		/// <summary>
		/// Gets or sets the height
		/// </summary>
		public int Height { get; set; }

		/// <summary>
		/// Gets or sets the weight
		/// </summary>
		public int Weight { get; set; }

		/// <summary>
		/// Gets or sets the masculinity
		/// </summary>
		public int Masculinity { get; set; }

		/// <summary>
		/// Gets or sets the muscule
		/// </summary>
		public int Muscle { get; set; }

		/// <summary>
		/// Gets or sets the skin color (default)
		/// </summary>
		public string SkinColorDefault { get; set; }

		/// <summary>
		/// Gets or sets the skin pattern type (default)
		/// </summary>
		public string SkinPatternTypeDefault { get; set; }

		/// <summary>
		/// Gets or sets the skin pattern color (default)
		/// </summary>
		public string SkinPatternColorDefault { get; set; }

		/// <summary>
		/// Gets or sets the fur color (default)
		/// </summary>
		public string FurColorDefault { get; set; }

		/// <summary>
		/// Gets or sets the fur pattern type (default)
		/// </summary>
		public string FurPatternTypeDefault { get; set; }

		/// <summary>
		/// Gets or sets the fur pattern color (default)
		/// </summary>
		public string FurPatternColorDefault { get; set; }

		/// <summary>
		/// Gets or sets the scales color (default)
		/// </summary>
		public string ScalesColorDefault { get; set; }

		/// <summary>
		/// Gets or sets the scales pattern type (default)
		/// </summary>
		public string ScalesPatternTypeDefault { get; set; }

		/// <summary>
		/// Gets or sets the scales pattern color (default)
		/// </summary>
		public string ScalesPatternColorDefault { get; set; }

		/// <summary>
		/// Gets or sets the hair color (default)
		/// </summary>
		public string HairColorDefault { get; set; }

		/// <summary>
		/// Gets or sets the hair pattern type (default)
		/// </summary>
		public string HairPatternTypeDefault { get; set; }

		/// <summary>
		/// Gets or sets the hair pattern color (default)
		/// </summary>
		public string HairPatternColorDefault { get; set; }

		/// <summary>
		/// Gets or sets the hair style (default)
		/// </summary>
		public string HairStyleDefault { get; set; }

		/// <summary>
		/// Gets or sets the hair length (default)
		/// </summary>
		public int HairLengthDefault { get; set; }

		/// <summary>
		/// Gets or sets the eye color (default)
		/// </summary>
		public string EyeColorDefault { get; set; }

		/// <summary>
		/// Gets or sets the head TF species (default)
		/// </summary>
		public string HeadTFDefault { get; set; }

		/// <summary>
		/// Gets or sets the body TF species (default)
		/// </summary>
		public string BodyTFDefault { get; set; }

		/// <summary>
		/// Gets or sets the tail TF species (default)
		/// </summary>
		public string TailTFDefault { get; set; }

		/// <summary>
		/// Gets or sets the arm TF species (default)
		/// </summary>
		public string ArmsTFDefault { get; set; }

		/// <summary>
		/// Gets or sets the legs TF species (default)
		/// </summary>
		public string LegsTFDefault { get; set; }

		/// <summary>
		/// Gets or sets the height (default)
		/// </summary>
		public int HeightDefault { get; set; }

		/// <summary>
		/// Gets or sets the weight (default)
		/// </summary>
		public int WeightDefault { get; set; }

		/// <summary>
		/// Gets or sets the masculinity (default)
		/// </summary>
		public int MasculinityDefault { get; set; }

		/// <summary>
		/// Gets or sets the muscule (default)
		/// </summary>
		public int MuscleDefault { get; set; }
	}
}