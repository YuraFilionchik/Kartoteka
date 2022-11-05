/*
 * Created by SharpDevelop.
 * User: user
 * Date: 18.04.2016
 * Time: 9:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace DBFprog1.Authorize
{
	partial class Login
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.MaskedTextBox tbPass;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.LinkLabel lbError;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.tbPass = new System.Windows.Forms.MaskedTextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.lbError = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Имя:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(13, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Пароль:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbName
			// 
			this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbName.Location = new System.Drawing.Point(86, 12);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(180, 31);
			this.tbName.TabIndex = 1;
			// 
			// tbPass
			// 
			this.tbPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbPass.Location = new System.Drawing.Point(86, 56);
			this.tbPass.Name = "tbPass";
			this.tbPass.PasswordChar = '#';
			this.tbPass.Size = new System.Drawing.Size(180, 31);
			this.tbPass.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1.Location = new System.Drawing.Point(12, 108);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(154, 52);
			this.button1.TabIndex = 3;
			this.button1.Text = "Вход";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button2.Location = new System.Drawing.Point(172, 108);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(154, 52);
			this.button2.TabIndex = 3;
			this.button2.Text = "Отмена";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// lbError
			// 
			this.lbError.LinkColor = System.Drawing.Color.Red;
			this.lbError.Location = new System.Drawing.Point(13, 90);
			this.lbError.Name = "lbError";
			this.lbError.Size = new System.Drawing.Size(313, 24);
			this.lbError.TabIndex = 0;
			// 
			// Login
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(336, 172);
			this.ControlBox = false;
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tbPass);
			this.Controls.Add(this.tbName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lbError);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Login";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Кто вы?";
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
