namespace ThebesUI
{
    partial class PlayerInput
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
            this.cbSelected = new System.Windows.Forms.CheckBox();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.rbAI = new System.Windows.Forms.RadioButton();
            this.rbHuman = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // cbSelected
            // 
            this.cbSelected.AutoSize = true;
            this.cbSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbSelected.Location = new System.Drawing.Point(10, 7);
            this.cbSelected.Name = "cbSelected";
            this.cbSelected.Size = new System.Drawing.Size(15, 14);
            this.cbSelected.TabIndex = 9;
            this.cbSelected.UseVisualStyleBackColor = true;
            // 
            // cbColor
            // 
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Items.AddRange(new object[] {
            "Red",
            "Green",
            "Blue",
            "Yellow"});
            this.cbColor.Location = new System.Drawing.Point(151, 3);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(71, 24);
            this.cbColor.TabIndex = 10;
            // 
            // tbName
            // 
            this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.tbName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tbName.Location = new System.Drawing.Point(34, 3);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(111, 20);
            this.tbName.TabIndex = 8;
            this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
            // 
            // rbAI
            // 
            this.rbAI.AutoSize = true;
            this.rbAI.Location = new System.Drawing.Point(306, 5);
            this.rbAI.Name = "rbAI";
            this.rbAI.Size = new System.Drawing.Size(38, 20);
            this.rbAI.TabIndex = 7;
            this.rbAI.TabStop = true;
            this.rbAI.Text = "AI";
            this.rbAI.UseVisualStyleBackColor = true;
            // 
            // rbHuman
            // 
            this.rbHuman.AutoSize = true;
            this.rbHuman.Location = new System.Drawing.Point(228, 5);
            this.rbHuman.Name = "rbHuman";
            this.rbHuman.Size = new System.Drawing.Size(66, 20);
            this.rbHuman.TabIndex = 6;
            this.rbHuman.TabStop = true;
            this.rbHuman.Text = "human";
            this.rbHuman.UseVisualStyleBackColor = true;
            // 
            // PlayerInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbSelected);
            this.Controls.Add(this.cbColor);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.rbAI);
            this.Controls.Add(this.rbHuman);
            this.Name = "PlayerInput";
            this.Size = new System.Drawing.Size(373, 31);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbSelected;
        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.RadioButton rbAI;
        private System.Windows.Forms.RadioButton rbHuman;
    }
}
