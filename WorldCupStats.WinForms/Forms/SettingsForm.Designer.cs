namespace WorldCupStats.WinForms.Forms
{
	partial class SettingsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
			rbType = new GroupBox();
			rbTypeWomen = new RadioButton();
			rbTypeMen = new RadioButton();
			rbLang = new GroupBox();
			rbLangHr = new RadioButton();
			rbLangEn = new RadioButton();
			btnSave = new Button();
			btnCancel = new Button();
			rbType.SuspendLayout();
			rbLang.SuspendLayout();
			SuspendLayout();
			// 
			// rbType
			// 
			resources.ApplyResources(rbType, "rbType");
			rbType.Controls.Add(rbTypeWomen);
			rbType.Controls.Add(rbTypeMen);
			rbType.Name = "rbType";
			rbType.TabStop = false;
			// 
			// rbTypeWomen
			// 
			resources.ApplyResources(rbTypeWomen, "rbTypeWomen");
			rbTypeWomen.Name = "rbTypeWomen";
			rbTypeWomen.UseVisualStyleBackColor = true;
			// 
			// rbTypeMen
			// 
			resources.ApplyResources(rbTypeMen, "rbTypeMen");
			rbTypeMen.Checked = true;
			rbTypeMen.Name = "rbTypeMen";
			rbTypeMen.TabStop = true;
			rbTypeMen.UseVisualStyleBackColor = true;
			// 
			// rbLang
			// 
			resources.ApplyResources(rbLang, "rbLang");
			rbLang.Controls.Add(rbLangHr);
			rbLang.Controls.Add(rbLangEn);
			rbLang.Name = "rbLang";
			rbLang.TabStop = false;
			// 
			// rbLangHr
			// 
			resources.ApplyResources(rbLangHr, "rbLangHr");
			rbLangHr.Name = "rbLangHr";
			rbLangHr.UseVisualStyleBackColor = true;
			// 
			// rbLangEn
			// 
			resources.ApplyResources(rbLangEn, "rbLangEn");
			rbLangEn.Checked = true;
			rbLangEn.Name = "rbLangEn";
			rbLangEn.TabStop = true;
			rbLangEn.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			resources.ApplyResources(btnSave, "btnSave");
			btnSave.Name = "btnSave";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// btnCancel
			// 
			resources.ApplyResources(btnCancel, "btnCancel");
			btnCancel.Name = "btnCancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// SettingsForm
			// 
			resources.ApplyResources(this, "$this");
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(btnCancel);
			Controls.Add(btnSave);
			Controls.Add(rbLang);
			Controls.Add(rbType);
			Name = "SettingsForm";
			Load += SettingsForm_Load;
			rbType.ResumeLayout(false);
			rbType.PerformLayout();
			rbLang.ResumeLayout(false);
			rbLang.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private GroupBox rbType;
		private RadioButton rbTypeWomen;
		private RadioButton rbTypeMen;
		private GroupBox rbLang;
		private RadioButton rbLangHr;
		private RadioButton rbLangEn;
		private Button btnSave;
		private Button btnCancel;
	}
}