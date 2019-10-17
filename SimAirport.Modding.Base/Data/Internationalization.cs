using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Data {
	public class Internationalization {
		public static Internationalization Instance { get; private set; }

		public Internationalization(Func<string, string, string> internalGet) {
			//Only one instance can be created, it'll be created by the game.
			if (Instance != null)
				throw new InvalidOperationException("Tried to create 2nd " + GetType().Name);

			Instance = this;

			InternalGet = internalGet;
		}

		/// <summary>
		/// Get a string from the current locale (includes everything from the game and any modded i18n)
		/// </summary>
		public void Get(string key, string defaultValue = "") => InternalGet.Invoke(key, defaultValue);
		private readonly Func<string, string, string> InternalGet;
	}
}