using Newtonsoft.Json;
using System;

namespace SimAirport.Modding.Settings {
	/// <summary>
	/// Setting for a float value, will be displayed as slider.
	/// </summary>
	public class SliderSetting : SettingBase<float> {
		public override string Name { get; set; }

		public float Minimum { get; set; }

		public float Maximum { get; set; } = 1f;

		public int Stepping { get; set; }

		/// <summary>
		/// The format to use when displaying the current number on the right side of a Slider.
		/// </summary>
		public string ValueFormat { get; set; } = "N";

		private float _value;
		public override float Value {
			get => _value;
			set {
				if (value != _value) {
					OnValueChanged?.Invoke(value);
					_value = value;
				}
			}
		}

		[JsonIgnore]
		public override Action<float> OnValueChanged { get; set; }
	}
}