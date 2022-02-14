using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SimAirport.Modding.Data {
	/// <summary>
	/// Access to in-game graphics
	/// </summary>
	public class Graphics {
		/// <summary>
		/// The current <see cref="Graphics"/> instance. Access this to use it's vars/functions
		/// </summary>
		public static Graphics Instance { get; private set; }

		public Graphics(Func<string, Sprite> internalGetSprite) {
			if (Instance != null)
				throw new InvalidOperationException("Tried to create 2nd " + GetType().Name);

			Instance = this;

			InternalGetSprite = internalGetSprite;
		}

		/// <summary>
		/// Get a sprite from the game or modded ones.
		/// </summary>
		/// <param name="name">Name of the sprite to get</param>
		public Sprite GetSprite(string name) => InternalGetSprite.Invoke(name);
		private readonly Func<string, Sprite> InternalGetSprite;
	}
}