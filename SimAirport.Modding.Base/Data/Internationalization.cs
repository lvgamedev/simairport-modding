using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Data {
	/// <summary>
	/// Access to the game-managed i18n translation library.
	/// </summary>
	public class Internationalization {
		/// <summary>
		/// The current <see cref="Internationalization"/> instance. Access this to use it's vars/functions
		/// </summary>
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
		/// <param name="key">Translation key</param>
		/// <param name="defaultValue">Default value, optional.</param>
		public string Get(string key, string defaultValue = "") => InternalGet.Invoke(key, defaultValue);
		private readonly Func<string, string, string> InternalGet;
	}
}