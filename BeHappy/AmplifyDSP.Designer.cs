
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeHappy.Amplify
{
	partial class ConfigurationDialog
	{
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.cbxDB = new System.Windows.Forms.CheckBox();
			this.numValue = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(49, 46);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(125, 46);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Amplify by";
			// 
			// cbxDB
			// 
			this.cbxDB.AutoSize = true;
			this.cbxDB.Location = new System.Drawing.Point(158, 13);
			this.cbxDB.Name = "cbxDB";
			this.cbxDB.Size = new System.Drawing.Size(41, 17);
			this.cbxDB.TabIndex = 3;
			this.cbxDB.Text = "DB";
			this.cbxDB.UseVisualStyleBackColor = true;
			// 
			// numValue
			// 
			this.numValue.DecimalPlaces = 4;
			this.numValue.Increment = new decimal(new int[] {
			                                      	1,
			                                      	0,
			                                      	0,
			                                      	65536});
			this.numValue.Location = new System.Drawing.Point(72, 12);
			this.numValue.Minimum = new decimal(new int[] {
			                                    	100,
			                                    	0,
			                                    	0,
			                                    	-2147483648});
			this.numValue.Name = "numValue";
			this.numValue.Size = new System.Drawing.Size(80, 20);
			this.numValue.TabIndex = 7;
			// 
			// ConfigurationDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(201, 71);
			this.Controls.Add(this.numValue);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbxDB);
			this.Name = "ConfigurationDialog";
			this.Text = "Amplify Configuration Dialog";
			this.Controls.SetChildIndex(this.cbxDB, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.button1, 0);
			this.Controls.SetChildIndex(this.button2, 0);
			this.Controls.SetChildIndex(this.numValue, 0);
			((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		
		private Label label1;
		internal CheckBox cbxDB;
		internal NumericUpDown numValue;
		private System.ComponentModel.IContainer components = null;
	}
}
