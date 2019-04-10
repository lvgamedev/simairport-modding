using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Base.Settings {
	public abstract class SettingBase {}

    public abstract class SettingBase<T> : SettingBase where T : class {
		/// <summary>
		/// Name of the variable.
		/// </summary>
		public abstract string Name { get; set; }

		/// <summary>
		/// Placeholder is used if Value is empty.
		/// </summary>
		public abstract T Placeholder { get; set; }

		/// <summary>
		/// Current value, will be set on game load and when user changes variable.
		/// </summary>
		public abstract T Value { get; set; }

		/// <summary>
		/// ValueChanged event, fires when value is changed.
		/// </summary>
		public abstract Action<T> OnValueChanged { get; set; }
	}
}