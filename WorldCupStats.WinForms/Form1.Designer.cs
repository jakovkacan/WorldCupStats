namespace WorldCupStats.WinForms
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
			button3 = new Button();
			SuspendLayout();
			// 
			// button1
			// 
			button1.Location = new Point(26, 29);
			button1.Margin = new Padding(2, 1, 2, 1);
			button1.Name = "button1";
			button1.Size = new Size(81, 22);
			button1.TabIndex = 0;
			button1.Text = "Teams";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Location = new Point(26, 66);
			button2.Margin = new Padding(2, 1, 2, 1);
			button2.Name = "button2";
			button2.Size = new Size(81, 22);
			button2.TabIndex = 1;
			button2.Text = "Matches";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// richTextBox1
			// 
			richTextBox1.Location = new Point(134, 29);
			richTextBox1.Margin = new Padding(2, 1, 2, 1);
			richTextBox1.Name = "richTextBox1";
			richTextBox1.Size = new Size(965, 425);
			richTextBox1.TabIndex = 2;
			richTextBox1.Text = "";
			// 
			// button3
			// 
			button3.Location = new Point(26, 104);
			button3.Margin = new Padding(2, 1, 2, 1);
			button3.Name = "button3";
			button3.Size = new Size(81, 22);
			button3.TabIndex = 3;
			button3.Text = "Settings";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(778, 371);
			Controls.Add(button3);
			Controls.Add(richTextBox1);
			Controls.Add(button2);
			Controls.Add(button1);
			Margin = new Padding(2, 1, 2, 1);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
		}

		#endregion

		private Button button1;
		private Button button2;
		private RichTextBox richTextBox1;
		private Button button3;
	}
}
