namespace ThebesUI
{
    partial class DigResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DigResult));
            this.label1 = new System.Windows.Forms.Label();
            this.flpTokens = new System.Windows.Forms.FlowLayoutPanel();
            this.bOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(64, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(477, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "You found these:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flpTokens
            // 
            this.flpTokens.Location = new System.Drawing.Point(64, 59);
            this.flpTokens.Margin = new System.Windows.Forms.Padding(2);
            this.flpTokens.Name = "flpTokens";
            this.flpTokens.Size = new System.Drawing.Size(477, 262);
            this.flpTokens.TabIndex = 1;
            // 
            // bOK
            // 
            this.bOK.Location = new System.Drawing.Point(249, 326);
            this.bOK.Margin = new System.Windows.Forms.Padding(2);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(86, 30);
            this.bOK.TabIndex = 2;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // DigResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.flpTokens);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DigResult";
            this.Text = "DigResult";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flpTokens;
        private System.Windows.Forms.Button bOK;
    }
}