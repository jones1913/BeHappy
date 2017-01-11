
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeHappy
{
	/// <summary>
	/// Description of MessageWindow.
	/// </summary>
	public partial class MessageWindow : Form
	{
		public MessageWindow()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			contextMenuStrip1.Items.Add("Copy", null, (sender, e) => richTextBox1.Copy());
			richTextBox1.ContextMenuStrip = contextMenuStrip1;
		}
		
		public void AddText(string text)
		{
			richTextBox1.Text += text;
			richTextBox1.SelectionStart = richTextBox1.Text.Length;
			richTextBox1.ScrollToCaret();
		}
		
		public void ClearText()
		{
			richTextBox1.Clear();
		}
		
		void BtnCloseClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
