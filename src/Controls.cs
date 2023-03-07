using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeHappy
{
	/// <summary>
	/// GroupBox with LinkLabel on top
	/// </summary>
	public class GroupBoxLinkLabel : GroupBox
	{
		private LinkLabel linkLabel;
		public LinkBehavior LinkBehavior {
			get { return linkLabel.LinkBehavior;}
			set { linkLabel.LinkBehavior = value;}
		}
		private ContextMenuStrip contextMenu;
		
		public GroupBoxLinkLabel()
		{
			linkLabel = new LinkLabel();
			linkLabel.LinkClicked += LinkClicked;
			linkLabel.Left = 8;
			linkLabel.AutoSize = true;
			linkLabel.MouseEnter += LinkLabelMouseEnter;
			linkLabel.MouseLeave += LinkLabelMouseLeave;
			this.Controls.Add(linkLabel);
		}

		public override string Text {
			get { return linkLabel.Text; }
			set { linkLabel.Text = value; }
		}

		void LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
//			if (e.Button == MouseButtons.Right)
//				return;

			ShowContext();
		}
		
		private void ShowContext()
		{
			if (contextMenu != null)
				contextMenu.Show(linkLabel, 0, 16);
		}
		
		public void AddContextMenuStrip(ContextMenuStrip strip)
		{
			contextMenu = strip;
		}
		
		void LinkLabelMouseEnter(object sender, EventArgs e)
		{
			LinkLabel lb = (LinkLabel)sender;
			lb.LinkColor = lb.ActiveLinkColor;
		}
		
		void LinkLabelMouseLeave(object sender, EventArgs e)
		{
			LinkLabel lb = (LinkLabel)sender;
			lb.LinkColor = Color.Blue;
		}
	}
	
	
	/// <summary>
	/// Button with integrated CheckBox
	/// </summary>
	public class CheckBoxButton :Button
	{
		private CheckBox checkBox;
		
		public bool Checked {
			get {return checkBox.Checked;}
			set {checkBox.Checked = value;}
		}
		
		public  string CheckBoxText {
			get {return checkBox.Text;}
			set {checkBox.Text = value;}
		}
		
		public CheckBoxButton()
		{
			this.Width = 146;
			this.Height = 40;
			this.MinimumSize = new Size(80, 40);
			this.TextAlign = ContentAlignment.BottomCenter;
			this.Font = new Font(this.Font, FontStyle.Bold);
			checkBox = new CheckBox();
			checkBox.AutoSize = false;
			checkBox.Text = "checkBox";
			checkBox.Font = new Font(this.Font.Name, 8, FontStyle.Regular);
			checkBox.UseVisualStyleBackColor = true;
			checkBox.BackColor = Color.Transparent;
			checkBox.Location = new Point(3, 3);
			checkBox.Height = 17;
			checkBox.Width = this.Width - 6;
			checkBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			this.Controls.Add(checkBox);
		}
	}
}

