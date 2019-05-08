using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Data {
	public class Map {
		public static Map Instance { get; private set; }

		public Map(Action<int> internalSetMaxMapCellCount) {
			//Only one instance can be created, it'll be created by the game.
			if (Instance != null)
				throw new InvalidOperationException("Tried to create 2nd " + GetType().Name);

			Instance = this;

			InternalSetMaxMapCellCount = internalSetMaxMapCellCount;
		}

		/// <summary>
		/// Sets the amount of cells at which buying new land will be forbidden.
		/// </summary>
		public void SetMaxMapCellCount(int cellCount) => InternalSetMaxMapCellCount.Invoke(cellCount);
		private readonly Action<int> InternalSetMaxMapCellCount;
	}
}