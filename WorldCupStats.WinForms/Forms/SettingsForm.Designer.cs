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
			rbType.Controls.Add(rbTypeWomen);
			rbType.Controls.Add(rbTypeMen);
			rbType.Location = new Point(12, 12);
			rbType.Name = "rbType";
			rbType.Size = new Size(229, 62);
			rbType.TabIndex = 2;
			rbType.TabStop = false;
			rbType.Text = "Championship";
			// 
			// rbTypeWomen
			// 
			rbTypeWomen.AutoSize = true;
			rbTypeWomen.Location = new Point(138, 26);
			rbTypeWomen.Name = "rbTypeWomen";
			rbTypeWomen.Size = new Size(67, 19);
			rbTypeWomen.TabIndex = 1;
			rbTypeWomen.Text = "Women";
			rbTypeWomen.UseVisualStyleBackColor = true;
			// 
			// rbTypeMen
			// 
			rbTypeMen.AutoSize = true;
			rbTypeMen.Checked = true;
			rbTypeMen.Location = new Point(13, 26);
			rbTypeMen.Name = "rbTypeMen";
			rbTypeMen.Size = new Size(49, 19);
			rbTypeMen.TabIndex = 0;
			rbTypeMen.TabStop = true;
			rbTypeMen.Text = "Men";
			rbTypeMen.UseVisualStyleBackColor = true;
			// 
			// rbLang
			// 
			rbLang.Controls.Add(rbLangHr);
			rbLang.Controls.Add(rbLangEn);
			rbLang.Location = new Point(12, 80);
			rbLang.Name = "rbLang";
			rbLang.Size = new Size(229, 62);
			rbLang.TabIndex = 3;
			rbLang.TabStop = false;
			rbLang.Text = "Language";
			// 
			// rbLangHr
			// 
			rbLangHr.AutoSize = true;
			rbLangHr.Location = new Point(138, 26);
			rbLangHr.Name = "rbLangHr";
			rbLangHr.Size = new Size(68, 19);
			rbLangHr.TabIndex = 1;
			rbLangHr.Text = "Hrvatski";
			rbLangHr.UseVisualStyleBackColor = true;
			// 
			// rbLangEn
			// 
			rbLangEn.AutoSize = true;
			rbLangEn.Checked = true;
			rbLangEn.Location = new Point(13, 26);
			rbLangEn.Name = "rbLangEn";
			rbLangEn.Size = new Size(63, 19);
			rbLangEn.TabIndex = 0;
			rbLangEn.TabStop = true;
			rbLangEn.Text = "English";
			rbLangEn.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			btnSave.Location = new Point(84, 175);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(75, 23);
			btnSave.TabIndex = 4;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// btnCancel
			// 
			btnCancel.Location = new Point(165, 175);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(75, 23);
			btnCancel.TabIndex = 5;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// SettingsForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(257, 208);
			Controls.Add(btnCancel);
			Controls.Add(btnSave);
			Controls.Add(rbLang);
			Controls.Add(rbType);
			Name = "SettingsForm";
			Text = "Settings";
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