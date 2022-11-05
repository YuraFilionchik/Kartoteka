namespace DBFprog1
{
    partial class TrassaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	this.tbTrassa = new System.Windows.Forms.RichTextBox();
        	this.SuspendLayout();
        	// 
        	// tbTrassa
        	// 
        	this.tbTrassa.BackColor = System.Drawing.Color.Gainsboro;
        	this.tbTrassa.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.tbTrassa.Font = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        	this.tbTrassa.Location = new System.Drawing.Point(0, 0);
        	this.tbTrassa.Margin = new System.Windows.Forms.Padding(4);
        	this.tbTrassa.Name = "tbTrassa";
        	this.tbTrassa.ReadOnly = true;
        	this.tbTrassa.Size = new System.Drawing.Size(875, 649);
        	this.tbTrassa.TabIndex = 0;
        	this.tbTrassa.TabStop = false;
        	this.tbTrassa.Text = "";
        	// 
        	// TrassaForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.AutoScroll = true;
        	this.AutoSize = true;
        	this.BackColor = System.Drawing.Color.Black;
        	this.ClientSize = new System.Drawing.Size(875, 649);
        	this.Controls.Add(this.tbTrassa);
        	this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        	this.ForeColor = System.Drawing.Color.Yellow;
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        	this.KeyPreview = true;
        	this.Margin = new System.Windows.Forms.Padding(6);
        	this.MinimumSize = new System.Drawing.Size(600, 500);
        	this.Name = "TrassaForm";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        	this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox tbTrassa;

    }
}