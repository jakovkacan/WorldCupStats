using System.Resources;
using WorldCupStats.Data.Interfaces;
using WorldCupStats.Data.Models;
using WorldCupStats.WinForms.Utils;

namespace WorldCupStats.WinForms.Forms
{
	public partial class SettingsForm : Form
	{
		private readonly ISettingsRepository _settings;
		private readonly ResourceManager _rm;
		public SettingsForm(ISettingsRepository settings)
		{
			_settings = settings;
			_isStartup = !_settings.IsInitialized();
			_rm = new ResourceManager("WorldCupStats.WinForms.Forms.SettingsForm", typeof(SettingsForm).Assembly);
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

			var result = MessageBoxUtils.ShowConfirmation(_rm.GetString("SaveConfirmation"), _rm.GetString("Confirmation"));

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
			if (_settings.LanguageChanged())
			{
				Application.Restart();
			}
			else
				Close();
		}

		private void btnCancel_Click(object sender, EventArgs e) => Close();
	}
}
