using System;
using System.Drawing;
using System.Windows.Forms;

namespace Feeleen
{
	public class ExceptionForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblMain;
		private System.Windows.Forms.Label lblCaption;
		private System.Windows.Forms.TextBox tbStack;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnContinue;
		private System.Windows.Forms.Button btnDetails;
		private System.Windows.Forms.Button btnExit;
		private System.ComponentModel.Container components = null;

		private int minHeight = 184;
		private int maxHeight = 368;

		private string doExpand = "Подробности >>";
		private string doMinimize = "Подробности <<";

		private bool expanded = false;

		public ExceptionForm()
		{
			InitializeComponent();
		}

		public static bool ShowExceptionForm(Exception ex)
		{
			using (ExceptionForm form = new ExceptionForm())
			{
				form.Height = form.minHeight;
				form.tbStack.Text = form.GetFullException(ex);
				form.SetFormSizeConstraints();
				form.lblCaption.Text = "Ошибка " + ex.Source;
				form.lblMain.Text = ex.Message;
				switch (form.ShowDialog())
				{
					case DialogResult.OK:
						return true;
					case DialogResult.Abort:
						Application.Exit();
						break;
				}
			}
			return false;
		}

		private string GetFullException(Exception ex)
		{
			string res = ex.Message + "\r\n" + ex.StackTrace;
			if (ex.InnerException!= null)
			{
				res += "\r\n" + GetFullException(ex.InnerException);
			}
			return res;
		}
		
		private void SetFormSizeConstraints()
		{
			this.Height = (expanded) ? minHeight : maxHeight;
			this.MaximumSize = (expanded) ?  new Size() : new Size(this.Width, minHeight);
			this.MinimumSize = (expanded) ? new Size(this.Width, minHeight + 200) : new Size(this.Width, minHeight);
		}

		#region Standart WinForms code

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExceptionForm));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblMain = new System.Windows.Forms.Label();
			this.lblCaption = new System.Windows.Forms.Label();
			this.tbStack = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.btnContinue = new System.Windows.Forms.Button();
			this.btnDetails = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 24);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lblMain
			// 
			this.lblMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblMain.Location = new System.Drawing.Point(48, 24);
			this.lblMain.Name = "lblMain";
			this.lblMain.Size = new System.Drawing.Size(360, 100);
			this.lblMain.TabIndex = 1;
			// 
			// lblCaption
			// 
			this.lblCaption.AutoSize = true;
			this.lblCaption.Location = new System.Drawing.Point(48, 5);
			this.lblCaption.Name = "lblCaption";
			this.lblCaption.Size = new System.Drawing.Size(0, 16);
			this.lblCaption.TabIndex = 2;
			// 
			// tbStack
			// 
			this.tbStack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbStack.Location = new System.Drawing.Point(48, 160);
			this.tbStack.Multiline = true;
			this.tbStack.Name = "tbStack";
			this.tbStack.ReadOnly = true;
			this.tbStack.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbStack.Size = new System.Drawing.Size(360, 171);
			this.tbStack.TabIndex = 3;
			this.tbStack.Text = "";
			// 
			// button1
			// 
			this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
			this.button1.Location = new System.Drawing.Point(8, 160);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(32, 32);
			this.button1.TabIndex = 4;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnContinue
			// 
			this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnContinue.Location = new System.Drawing.Point(288, 128);
			this.btnContinue.Name = "btnContinue";
			this.btnContinue.Size = new System.Drawing.Size(120, 28);
			this.btnContinue.TabIndex = 5;
			this.btnContinue.Text = "Продолжить";
			// 
			// btnDetails
			// 
			this.btnDetails.Location = new System.Drawing.Point(48, 128);
			this.btnDetails.Name = "btnDetails";
			this.btnDetails.Size = new System.Drawing.Size(112, 28);
			this.btnDetails.TabIndex = 6;
			this.btnDetails.Text = "Подробности >>";
			this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
			// 
			// btnExit
			// 
			this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Abort;
			this.btnExit.Location = new System.Drawing.Point(288, 336);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(120, 28);
			this.btnExit.TabIndex = 7;
			this.btnExit.Text = "Завершить работу";
			this.btnExit.Visible = false;
			// 
			// ExceptionForm
			// 
			this.AcceptButton = this.btnContinue;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(416, 367);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnDetails);
			this.Controls.Add(this.btnContinue);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tbStack);
			this.Controls.Add(this.lblCaption);
			this.Controls.Add(this.lblMain);
			this.Controls.Add(this.pictureBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ExceptionForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Ошибка в приложении";
			this.ResumeLayout(false);

		}
		#endregion
		#endregion

		private void btnDetails_Click(object sender, System.EventArgs e)
		{
			this.btnDetails.Text = (this.btnDetails.Text == doMinimize) ? doExpand : doMinimize;
			expanded = !expanded;
			SetFormSizeConstraints();
			this.btnExit.Visible = expanded;
			
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject(tbStack.Text);
		}
	}
}
