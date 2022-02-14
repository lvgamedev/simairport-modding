using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Data {
	public class Game {
		/// <summary>
		/// The current <see cref="Game"/> instance. Access this to use it's vars/functions
		/// </summary>
		public static Game Instance { get; private set; }

		public Game(Func<GameState> internalGetState, Func<double> internalGetMoneyBalance, Action<double> internalSetMoneyBalance) {
			if (Instance != null)
				throw new InvalidOperationException("Tried to create 2nd " + GetType().Name);

			Instance = this;

			InternalGetState = internalGetState;
			InternalGetMoneyBalance = internalGetMoneyBalance;
			InternalSetMoneyBalance = internalSetMoneyBalance;
		}

		/// <summary>
		/// Returns the current <see cref="GameState">game state</see>.
		/// </summary>
		public GameState GetState() => InternalGetState.Invoke();
		private readonly Func<GameState> InternalGetState;

		/// <summary>
		/// Access to the game's money balance.
		/// </summary>
		public double MoneyBalance {
			get => InternalGetMoneyBalance.Invoke();
			set => InternalSetMoneyBalance.Invoke(value);
		}

		private readonly Func<double> InternalGetMoneyBalance;
		private readonly Action<double> InternalSetMoneyBalance;
	}
}