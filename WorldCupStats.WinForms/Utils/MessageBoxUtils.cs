namespace WorldCupStats.WinForms.Utils;

public static class MessageBoxUtils
{
	public static void ShowInfo(string message, string title = "Information")
	{
		MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
	}
	public static void ShowError(string message, string title = "Error")
	{
		MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
	}
	public static DialogResult ShowConfirmation(string message, string title = "Confirmation")
	{
		return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
	}
}