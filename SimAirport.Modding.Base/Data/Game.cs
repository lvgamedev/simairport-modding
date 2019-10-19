using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Data {
	public class Game {
		public static Game Instance { get; private set; }

		public Game(Func<GameState> internalGetState, Func<double> internalGetMoneyBalance, Action<double> internalSetMoneyBalance) {
			if (Instance != null)
				throw new InvalidOperationException("Tried to create 2nd " + GetType().Name);

			Instance = this;

			InternalGetState = internalGetState;
			InternalGetMoneyBalance = internalGetMoneyBalance;
			InternalSetMoneyBalance = internalSetMoneyBalance;
		}

		public GameState GetState() => InternalGetState.Invoke();
		private readonly Func<GameState> InternalGetState;

		public double MoneyBalance {
			get => InternalGetMoneyBalance.Invoke();
			set => InternalSetMoneyBalance.Invoke(value);
		}

		private readonly Func<double> InternalGetMoneyBalance;
		private readonly Action<double> InternalSetMoneyBalance;
	}
}