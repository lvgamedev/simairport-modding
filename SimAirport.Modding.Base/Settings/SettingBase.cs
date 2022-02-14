using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Settings {
	public abstract class SettingBase {
		/// <summary>
		/// The type we are currently holding
		/// </summary>
		/// <value>A type</value>
		public abstract Type Type { get; }

		/// <summary>
		/// Set this setting's value.
		/// </summary>
		/// <param name="newValue">New value to set</param>
		public abstract void SetValue(object newValue);

		/// <summary>
		/// Get this setting's value.
		/// </summary>
		/// <returns>Any value</returns>
		public abstract object GetValue();

		/// <summary>
		/// Order by which the game sorts your settings.
		/// Ordered by decending, higher order equals a higher position in the UI.
		/// </summary>
		public int SortOrder { get; set; }
	}

	/// <summary>
	/// Base setting type.
	/// </summary>
	/// <typeparam name="T">Any type that can be saved by `Json.Net`.</typeparam>
    public abstract class SettingBase<T> : SettingBase {
		/// <inheritdoc />
		public override Type Type => typeof(T);

		/// <inheritdoc />
		public override void SetValue(object newValue) {
			if (!(newValue is T newTValue))
				throw new ArgumentException($"Cannot assign value of type {newValue.GetType()} to setting of type {Type}");

			Value = newTValue;
		}

		/// <inheritdoc />
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
		/// 
		/// Example callback:
		/// ```cs
		/// OnValueChanged = delegate (val T) {
		/// 	this.MyMagicalFunction(val);	
		/// }
		/// ```
		/// </summary>
		public abstract Action<T> OnValueChanged { get; set; }
	}
}