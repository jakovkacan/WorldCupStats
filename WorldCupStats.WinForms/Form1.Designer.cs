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
			textBox1 = new TextBox();
			button4 = new Button();
			progressBar1 = new ProgressBar();
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
			// textBox1
			// 
			textBox1.Location = new Point(26, 224);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(100, 23);
			textBox1.TabIndex = 4;
			textBox1.Text = "Player Name";
			// 
			// button4
			// 
			button4.Location = new Point(26, 253);
			button4.Name = "button4";
			button4.Size = new Size(75, 23);
			button4.TabIndex = 5;
			button4.Text = "Browse";
			button4.UseVisualStyleBackColor = true;
			button4.Click += button4_Click;
			// 
			// progressBar1
			// 
			progressBar1.Location = new Point(12, 155);
			progressBar1.Name = "progressBar1";
			progressBar1.Size = new Size(114, 23);
			progressBar1.Style = ProgressBarStyle.Marquee;
			progressBar1.TabIndex = 6;
			progressBar1.Visible = false;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(778, 371);
			Controls.Add(progressBar1);
			Controls.Add(button4);
			Controls.Add(textBox1);
			Controls.Add(button3);
			Controls.Add(richTextBox1);
			Controls.Add(button2);
			Controls.Add(button1);
			Margin = new Padding(2, 1, 2, 1);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button button1;
		private Button button2;
		private RichTextBox richTextBox1;
		private Button button3;
		private TextBox textBox1;
		private Button button4;
		private ProgressBar progressBar1;
	}
}
