using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimAirport.Modding.Settings {
	public class DropdownSetting : SettingBase<int> {
		/// <inheritdoc />
		public override string Name { get; set; }

		private int _value;

		/// <summary>
		/// Index of the selected option in the Dropdown.
		/// </summary>
		public override int Value {
			get => _value;
			set {
				if (value != _value) {
					OnValueChanged?.Invoke(value);
					_value = value;
				}
			}
		}

		/// <summary>
		/// I18n keys for your selectable options.
		/// </summary>
		public List<string> Options { get; set; }

		/// <inheritdoc />
		[JsonIgnore]
		public override Action<int> OnValueChanged { get; set; }
	}
}