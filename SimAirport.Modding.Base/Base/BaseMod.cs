using SimAirport.Modding.Data;
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
		/// Setting manager, created by the game after loading the mod.
		/// Will be set and filled with settings before running OnSettingsLoaded!
		/// </summary>
		public abstract Settings.SettingManager SettingManager { get; set; }

		/// <summary>
		/// Called when the mod is loaded.
		/// This may be called from the menu or in-game but will only be called once.
		/// </summary>
		/// <param name="state">The current game state</param>
		public virtual void OnLoad(GameState state) { }

		/// <summary>
		/// Called once per frame, use to update data that must be updated each fram.
		/// Only called in-game, never from menu.
		/// Use wisely as heavy load slows down the game!
		/// </summary>
		public abstract void OnTick();

		/// <summary>
		/// Executed after settings are loaded, you can add settings or change them at this point.
		/// </summary>
		public virtual void OnSettingsLoaded() { }
	}
}