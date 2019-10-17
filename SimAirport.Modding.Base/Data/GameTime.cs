using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Data {
	/// <summary>
	/// Gives access to GameTime
	/// </summary>
	public class GameTime {
		public static GameTime Instance { get; private set; }

		public GameTime(Func<double> internalTotalGameSeconds, Func<TimeSpan> internalCurrentGameTime) {
			//Only one instance can be created, it'll be created by the game.
			if (Instance != null)
				throw new InvalidOperationException("Tried to create 2nd " + GetType().Name);

			Instance = this;

			InternalTotalGameSeconds = internalTotalGameSeconds;
			InternalCurrentGameTime = internalCurrentGameTime;
		}

		/// <summary>
		/// Returns the total time this game has been running in in game seconds
		/// </summary>
		public double TotalGameSeconds => InternalTotalGameSeconds.Invoke();
		private readonly Func<double> InternalTotalGameSeconds;

		/// <summary>
		/// Returns the current in-game time and date.
		/// </summary>
		public TimeSpan CurrentGameTime => InternalCurrentGameTime();
		private readonly Func<TimeSpan> InternalCurrentGameTime;
	}
}