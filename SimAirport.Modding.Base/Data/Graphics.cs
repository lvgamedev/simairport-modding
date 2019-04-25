using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SimAirport.Modding.Base.Data {
	/// <summary>
	/// Access to in-game graphics
	/// </summary>
	public class Graphics {
		public static Graphics Instance { get; private set; }

		public Graphics(Func<string, Sprite> internalGetSprite) {
			if (Instance != null)
				throw new InvalidOperationException("Tried to create 2nd " + GetType().Name);

			Instance = this;

			InternalGetSprite = internalGetSprite;
		}

		public Sprite GetSprite(string name) => InternalGetSprite.Invoke(name);
		private readonly Func<string, Sprite> InternalGetSprite;
	}
}