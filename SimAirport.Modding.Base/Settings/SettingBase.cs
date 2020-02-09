using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Settings {
	public abstract class SettingBase {
		public abstract Type Type { get; }

		public abstract void SetValue(object newValue);

		public abstract object GetValue();

		/// <summary>
		/// Order by which the game sorts your settings.
		/// Ordered by decending, higher order equals a higher position in the UI.
		/// </summary>
		public int SortOrder { get; set; }
	}

    public abstract class SettingBase<T> : SettingBase {
		public override Type Type => typeof(T);

		public override void SetValue(object newValue) {
			if (!(newValue is T newTValue))
				throw new ArgumentException($"Cannot assign value of type {newValue.GetType()} to setting of type {Type}");

			Value = newTValue;
		}

		public override object GetValue() => Value;

		/// <summary>
		/// Name of the variable.
		/// </summary>
		public abstract string Name { get; set; }

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