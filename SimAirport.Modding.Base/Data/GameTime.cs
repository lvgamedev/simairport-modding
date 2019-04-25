using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Base.Data {
	/// <summary>
	/// Gives access to GameTime
	/// </summary>
	public class GameTime {
		public static GameTime Instance { get; private set; }

		public GameTime(Func<double> internalTotalGameSeconds) {
			//Only one instance can be created, it'll be created by the game.
			if (Instance != null)
				throw new InvalidOperationException("Tried to create 2nd " + GetType().Name);

			Instance = this;

			InternalTotalGameSeconds = internalTotalGameSeconds;
		}

		/// <summary>
		/// Returns the total time this game has been running in in game seconds
		/// </summary>
		public double TotalGameSeconds => InternalTotalGameSeconds.Invoke();
		private readonly Func<double> InternalTotalGameSeconds;
	}
}