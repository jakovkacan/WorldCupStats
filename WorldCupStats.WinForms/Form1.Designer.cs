﻿namespace WorldCupStats.WinForms
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			button1 = new Button();
			button2 = new Button();
			richTextBox1 = new RichTextBox();
			SuspendLayout();
			// 
			// button1
			// 
			button1.Location = new Point(49, 62);
			button1.Name = "button1";
			button1.Size = new Size(150, 46);
			button1.TabIndex = 0;
			button1.Text = "Teams";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Location = new Point(49, 141);
			button2.Name = "button2";
			button2.Size = new Size(150, 46);
			button2.TabIndex = 1;
			button2.Text = "Matches";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// richTextBox1
			// 
			richTextBox1.Location = new Point(249, 62);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.Size = new Size(1788, 902);
			richTextBox1.TabIndex = 2;
			richTextBox1.Text = "";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(13F, 32F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(2049, 976);
			Controls.Add(richTextBox1);
			Controls.Add(button2);
			Controls.Add(button1);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
		}

		#endregion

		private Button button1;
		private Button button2;
		private RichTextBox richTextBox1;
	}
}
