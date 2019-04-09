namespace SimAirport.Modding.Base
{
	public abstract class BaseMod {
		//Basic information
		/// <summary>
		/// Name of your mod.
		/// </summary>
		public abstract string Name { get; }

		/// <summary>
		/// Description of your mod.
		/// </summary>
		public abstract string Description { get; }

		/// <summary>
		/// You.
		/// </summary>
		public abstract string Author { get; }

		/// <summary>
		/// Called once per frame, use to update data that must be updated each fram.
		/// Use wisely as heavy load slows down the game!
		/// </summary>
		public abstract void OnTick();
	}
}