namespace ThebesUI
{
    partial class DigSiteKnowledge
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lDigSiteName = new System.Windows.Forms.Label();
            this.lSpecializedKnowledge = new System.Windows.Forms.Label();
            this.lRumors = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lDigSiteName
            // 
            this.lDigSiteName.AutoSize = true;
            this.lDigSiteName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lDigSiteName.Location = new System.Drawing.Point(3, 0);
            this.lDigSiteName.Name = "lDigSiteName";
            this.lDigSiteName.Size = new System.Drawing.Size(67, 26);
            this.lDigSiteName.TabIndex = 0;
            this.lDigSiteName.Text = "name";
            // 
            // lSpecializedKnowledge
            // 
            this.lSpecializedKnowledge.AutoSize = true;
            this.lSpecializedKnowledge.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lSpecializedKnowledge.Location = new System.Drawing.Point(89, 0);
            this.lSpecializedKnowledge.Name = "lSpecializedKnowledge";
            this.lSpecializedKnowledge.Size = new System.Drawing.Size(24, 26);
            this.lSpecializedKnowledge.TabIndex = 1;
            this.lSpecializedKnowledge.Text = "0";
            // 
            // lRumors
            // 
            this.lRumors.AutoSize = true;
            this.lRumors.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lRumors.Location = new System.Drawing.Point(143, 0);
            this.lRumors.Name = "lRumors";
            this.lRumors.Size = new System.Drawing.Size(24, 26);
            this.lRumors.TabIndex = 2;
            this.lRumors.Text = "0";
            // 
            // DigSiteKnowledge2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lRumors);
            this.Controls.Add(this.lSpecializedKnowledge);
            this.Controls.Add(this.lDigSiteName);
            this.Name = "DigSiteKnowledge2";
            this.Size = new System.Drawing.Size(174, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lDigSiteName;
        private System.Windows.Forms.Label lSpecializedKnowledge;
        private System.Windows.Forms.Label lRumors;
    }
}
