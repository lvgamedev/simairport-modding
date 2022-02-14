using Newtonsoft.Json;
using System;

namespace SimAirport.Modding.Settings {
	/// <summary>
	/// Setting for a float value, will be displayed as slider.
	/// </summary>
	public class SliderSetting : SettingBase<float> {
		/// <inheritdoc />
		public override string Name { get; set; }

		/// <summary>
		/// Minimum slier value.
		/// </summary>
		public float Minimum { get; set; }

		/// <summary>
		/// Maximmum slider value.
		/// </summary>
		public float Maximum { get; set; } = 1f;

		/// <summary>
		/// How much does the slider increase/movement
		/// </summary>
		public int Stepping { get; set; }

		/// <summary>
		/// The format to use when displaying the current number on the right side of a Slider.
		/// See [this](https://docs.microsoft.com/en-us/dotnet/api/system.single.tostring?view=netframework-4.7.1#system-single-tostring(system-string)) for more information.
		/// </summary>
		public string ValueFormat { get; set; } = "N";

		private float _value;
		
		/// <inheritdoc />
		public override float Value {
			get => _value;
			set {
				if (value != _value) {
					OnValueChanged?.Invoke(value);
					_value = value;
				}
			}
		}

		/// <inheritdoc />
		[JsonIgnore]
		public override Action<float> OnValueChanged { get; set; }
	}
}