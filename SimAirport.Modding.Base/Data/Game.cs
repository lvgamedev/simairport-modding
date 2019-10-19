using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Data {
	public class Game {
		public static Game Instance { get; private set; }

		public Game(Func<GameState> internalGetState, Func<int> internalGetMoneyBalance, Action<int> internalSetMoneyBalance) {
			if (Instance != null)
				throw new InvalidOperationException("Tried to create 2nd " + GetType().Name);

			Instance = this;

			InternalGetState = internalGetState;
			InternalGetMoneyBalance = internalGetMoneyBalance;
			InternalSetMoneyBalance = internalSetMoneyBalance;
		}

		public GameState GetState() => InternalGetState.Invoke();
		private readonly Func<GameState> InternalGetState;

		public int MoneyBalance {
			get => InternalGetMoneyBalance.Invoke();
			set => InternalSetMoneyBalance.Invoke(value);
		}

		private readonly Func<int> InternalGetMoneyBalance;
		private readonly Action<int> InternalSetMoneyBalance;
	}
}