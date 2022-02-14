using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Settings {
	/// <summary>
	/// Setting for a string, will be displayed as a textbox.
	/// </summary>
	public class StringSetting : SettingBase<string> {
		/// <inheritdoc />
		public override string Name { get; set; }

		/// <inheritdoc />
		public string Placeholder { get; set; }

		private string _value;
		/// <inheritdoc />
		public override string Value {
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
		public override Action<string> OnValueChanged { get; set; }
	}
}