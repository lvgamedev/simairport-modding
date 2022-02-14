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
		/// <param name="key">Setting key to use</param>
		public void AddDefault(string key, SettingBase setting) {
			if (!settings.ContainsKey(key))
				settings.Add(key, setting);
		}

		/// <summary>
		/// Gets a specific <see cref="SettingBase">setting</see> (not Value).
		/// </summary>
		/// <param name="key">Setting key to use</param>
		/// <param name="tSetting">Reference to set</param>
		/// <typeparam name="T">The type of <see cref="SettingBase">setting</see> to return.</typeparam>
		/// <returns>`true` if the setting value existed and is the same type, `false` if not.</returns>
		public bool TryGetSetting<T>(string key, out T tSetting) where T : SettingBase {
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
		/// <param name="key">Setting key to use</param>
		/// <param name="defaultValue">Default value</param>
		/// <typeparam name="T">Any type that can be saved by `Json.Net`.</typeparam>
		/// <returns>`defaultValue` or the setting's value.</returns>
		public T GetOrDefault<T>(string key, T defaultValue) {
			if (TryGetValue<T>(key, out var tValue))
				return tValue;

			return defaultValue;
		}

		/// <summary>
		/// Remove the setting and it's data.
		/// </summary>
		/// <param name="key">Setting key to use</param>
		public void RemoveSetting(string key) => settings.Remove(key);

		/// <summary>
		/// Get the dictionary containing all of the settings.
		/// </summary>
		/// <returns>The private settings dictionary</returns>
		public Dictionary<string, SettingBase> GetAll() => settings;

		/// <summary>
		/// Update the setting's value.
		/// </summary>
		/// <param name="key">Setting key to use</param>
		/// <param name="value">The value to set the setting</param>
		/// <typeparam name="T">Any type that can be saved by `Json.Net`.</typeparam>
		/// <returns>`true` if the setting value existed and is the same type, `false` if not.</returns>
		public bool UpdateValue<T>(string key, T value) {
			if (!settings.TryGetValue(key, out var setting))
				return false;

			if (setting.Type != typeof(T))
				return false;

			setting.SetValue(value);
			return true;
		}

		/// <summary>
		/// Gets the current value, if it doesn't exist returns the default value
		/// </summary>
		/// <param name="key">Setting key to use</param>
		/// <param name="value">Reference to set</param>
		/// <typeparam name="T">Any type that can be saved by `Json.Net`.</typeparam>
		/// <returns>`true` if the setting value existed and is the same type, `false` if not.</returns>
		public bool TryGetValue<T>(string key, out T value) {
			if (!settings.TryGetValue(key, out var setting) || setting.Type != typeof(T)) {
				value = default;
				return false;
			}

			value = (T)setting.GetValue();
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key">Setting key to use</param>
		/// <param name="value"></param>
		/// <returns>`true` if the setting value existed, `false` if not.</returns>
		public bool TryGetString(string key, out string value) {
			if (!settings.TryGetValue(key, out var setting) || !(setting is StringSetting s)) {
				value = null;
				return false;
			}

			value = s.Value;
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key">Setting key to use</param>
		/// <param name="value"></param>
		/// <returns>`true` if the setting value existed, `false` if not.</returns>
		public bool TryGetFloat(string key, out float value) {
			if (!settings.TryGetValue(key, out var setting) || !(setting is SliderSetting s)) {
				value = 0f;
				return false;
			}

			value = s.Value;
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="key">Setting key to use</param>
		/// <param name="value"></param>
		/// <returns>`true` if the setting value existed, `false` if not.</returns>
		public bool TryGetBool(string key, out bool value) {
			if (!settings.TryGetValue(key, out var setting) || !(setting is CheckboxSetting s)) {
				value = false;
				return false;
			}

			value = s.Value;
			return true;
		}
	}
}