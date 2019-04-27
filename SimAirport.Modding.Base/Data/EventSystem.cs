using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Data {
	/// <summary>
	/// Used for Events
	/// </summary>
	public class EventSystem {
		public static EventSystem Instance { get; private set; }

		public EventSystem() {
			if (Instance != null)
				throw new InvalidOperationException("Tried to create 2nd " + GetType().Name);

			Instance = this;
		}

		/// <summary>
		/// UI level change, provides new level.
		/// </summary>
		public Action<int> OnLevelChanged;
	}
}