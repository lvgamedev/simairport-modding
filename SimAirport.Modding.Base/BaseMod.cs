using System.Collections.Generic;

namespace SimAirport.Modding.Base
{
	public abstract class BaseMod {
		//Basic information
		/// <summary>
		/// Name of your mod.
		/// </summary>
		public abstract string Name { get; }

		/// <summary>
		/// This is the name the game uses to identify your mod, use something unique to you like
		/// sa.yourname.yourmod to not conflict with other mods.
		/// </summary>
		public abstract string InternalName { get; }

		/// <summary>
		/// Description of your mod.
		/// </summary>
		public abstract string Description { get; }

		/// <summary>
		/// You.
		/// </summary>
		public abstract string Author { get; }

		/// <summary>
		/// A list of settings. Settings will be saved/loaded automatically.
		/// This list will be filled by the game, add to it, overwrite means all saved settings will
		/// be deleted aswell.
		/// </summary>
		public abstract List<Settings.SettingBase> Settings { get; set; }

		/// <summary>
		/// Called once per frame, use to update data that must be updated each fram.
		/// Use wisely as heavy load slows down the game!
		/// </summary>
		public abstract void OnTick();
	}
}