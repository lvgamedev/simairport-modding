using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimAirport.Modding.Settings {
	public class SettingManager {
		private readonly Dictionary<string, SettingBase> settings;

		public SettingManager(Dictionary<string, SettingBase> loadedSettings) {
			settings = loadedSettings ?? new Dictionary<string, SettingBase>();
		}

		/// <summary>
		/// Adds a setting only if it wasn't yet added
		/// </summary>
		public void AddDefault(string key, SettingBase setting) {
			if (!settings.ContainsKey(key))
				settings.Add(key, setting);
		}

		public bool TryGetSetting<T>(string key, out T tSetting) where T : class {
			if (!settings.TryGetValue(key, out var setting)) {
				tSetting = default;
				return false;
			}

			if (setting.GetType() == typeof(T)) {
				tSetting = setting as T;
				return true;
			}

			tSetting = default;
			return false;
		}

		/// <summary>
		/// Gets the current value, if it doesn't exist returns the default value
		/// </summary>
		public T GetOrDefault<T>(string key, T defaultValue) where T : class {
			if (TryGetSetting<T>(key, out var tSetting))
				return tSetting;

			return defaultValue;
		}

		public Dictionary<string, SettingBase> GetAll() => settings;

		public bool UpdateValue<T>(string key, T value) {
			if (!settings.TryGetValue(key, out var setting))
				return false;

			if (setting.Type != typeof(T))
				return false;

			setting.SetValue(value);
			return true;
		}

		public bool TryGetValue<T>(string key, out T value) {
			if (!settings.TryGetValue(key, out var setting) || setting.Type != typeof(T)) {
				value = default;
				return false;
			}

			value = (T)setting.GetValue();
			return true;
		}

		public bool TryGetString(string key, out string value) {
			if (!settings.TryGetValue(key, out var setting) || !(setting is StringSetting s)) {
				value = null;
				return false;
			}

			value = s.Value;
			return true;
		}

		public bool TryGetFloat(string key, out float value) {
			if (!settings.TryGetValue(key, out var setting) || !(setting is SliderSetting s)) {
				value = 0f;
				return false;
			}

			value = s.Value;
			return true;
		}
	}
}