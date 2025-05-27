using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;

namespace WorldCupStats.WinForms.Forms
{
	public partial class SettingsForm : Form
	{
		private readonly ISettingsRepository _settings;
		public SettingsForm(ISettingsRepository settings)
		{
			_settings = settings;
			_isStartup = !_settings.IsInitialized();
			InitializeComponent();
		}

		public bool SettingsSaved { get; set; } = false;
		private readonly bool _isStartup;
		private ChampionshipType _type;
		private Language _language;

		private void SettingsForm_Load(object sender, EventArgs e)
		{
			if (_isStartup)
			{
				_type = ChampionshipType.Men;
				_language = Language.EN;
			}
			else
			{
				_type = _settings.GetValue<ChampionshipType>();
				_language = _settings.GetValue<Language>();

				rbTypeMen.Checked = _type == ChampionshipType.Men;
				rbTypeWomen.Checked = _type == ChampionshipType.Women;
				rbLangEn.Checked = _language == Language.EN;
				rbLangHr.Checked = _language == Language.HR;
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			_type = rbTypeMen.Checked ? ChampionshipType.Men : ChampionshipType.Women;
			_language = rbLangEn.Checked ? Language.EN : Language.HR;

			var result = MessageBox.Show(
				"Are you sure you want to save the settings?",
				"Confirm Settings",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button1);

			if (result != DialogResult.Yes) return;

			if (_isStartup)
			{
				_settings.CreateSettingsFile(_type, _language);
			}
			else
			{
				_settings.SetValue(_type);
				_settings.SetValue(_language);
			}

			SettingsSaved = true;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
