/*
 * Создано в SharpDevelop.
 * Пользователь: user
 * Дата: 11.05.2016
 * Время: 14:38
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
namespace DBFprog1
{
	partial class Result
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		public System.Windows.Forms.RichTextBox tbKart;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.RichTextBox tbTrass;
		
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
			this.tbKart = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbTrass = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// tbKart
			// 
			this.tbKart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tbKart.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbKart.Location = new System.Drawing.Point(13, 26);
			this.tbKart.Name = "tbKart";
			this.tbKart.ReadOnly = true;
			this.tbKart.Size = new System.Drawing.Size(824, 177);
			this.tbKart.TabIndex = 0;
			this.tbKart.TabStop = false;
			this.tbKart.Text = "";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(355, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Карточка";
			this.label1.Click += new System.EventHandler(this.Label1Click);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(372, 262);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Трасса";
			// 
			// tbTrass
			// 
			this.tbTrass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tbTrass.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold);
			this.tbTrass.Location = new System.Drawing.Point(12, 288);
			this.tbTrass.Name = "tbTrass";
			this.tbTrass.ReadOnly = true;
			this.tbTrass.Size = new System.Drawing.Size(824, 202);
			this.tbTrass.TabIndex = 2;
			this.tbTrass.TabStop = false;
			this.tbTrass.Text = "";
			// 
			// Result
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(849, 502);
			this.Controls.Add(this.tbTrass);
			this.Controls.Add(this.tbKart);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.KeyPreview = true;
			this.Name = "Result";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Результат поиска";
			this.ResumeLayout(false);

		}
	}
}
