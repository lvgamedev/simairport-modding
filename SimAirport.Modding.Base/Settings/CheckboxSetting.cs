using Newtonsoft.Json;
using System;

namespace SimAirport.Modding.Settings {
	public class CheckboxSetting : SettingBase<bool> {
		public override string Name { get; set; }

		private bool _value;
		public override bool Value {
			get => _value;
			set {
				if (value != _value) {
					OnValueChanged?.Invoke(value);
					_value = value;
				}
			}
		}

		[JsonIgnore]
		public override Action<bool> OnValueChanged { get; set; }
	}
}