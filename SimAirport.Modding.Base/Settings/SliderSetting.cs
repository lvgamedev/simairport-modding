using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Settings {
	/// <summary>
	/// Setting for a float value, will be displayed as slider.
	/// </summary>
	public class SliderSetting : SettingBase<float> {
		public override string Name { get; set; }

		public float Minimum { get; set; }

		public float Maximum { get; set; } = 1f;

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