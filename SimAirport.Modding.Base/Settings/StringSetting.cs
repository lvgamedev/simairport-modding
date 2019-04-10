using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Base.Settings {
	public class StringSetting : SettingBase<string> {
		public override string Name { get; set; }

		public override string Placeholder { get; set; }

		private string _value;
		public override string Value {
			get => _value;
			set {
				if (value != _value) {
					OnValueChanged(value);
					_value = value;
				}
			}
		}

		public override Action<string> OnValueChanged { get; set; }
	}
}