namespace ThebesUI
{
    partial class WelcomeForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.newGameBox = new System.Windows.Forms.GroupBox();
            this.playerInput4 = new ThebesUI.PlayerInput();
            this.playerInput3 = new ThebesUI.PlayerInput();
            this.playerInput2 = new ThebesUI.PlayerInput();
            this.playerInput1 = new ThebesUI.PlayerInput();
            this.newGameBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Color";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player name";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(-67, 57);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(18, 17);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(272, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // newGameBox
            // 
            this.newGameBox.Controls.Add(this.playerInput4);
            this.newGameBox.Controls.Add(this.playerInput3);
            this.newGameBox.Controls.Add(this.playerInput2);
            this.newGameBox.Controls.Add(this.playerInput1);
            this.newGameBox.Controls.Add(this.button1);
            this.newGameBox.Controls.Add(this.checkBox1);
            this.newGameBox.Controls.Add(this.label1);
            this.newGameBox.Controls.Add(this.label2);
            this.newGameBox.Location = new System.Drawing.Point(37, 29);
            this.newGameBox.Name = "newGameBox";
            this.newGameBox.Size = new System.Drawing.Size(379, 252);
            this.newGameBox.TabIndex = 4;
            this.newGameBox.TabStop = false;
            this.newGameBox.Text = "New Game";
            // 
            // playerInput4
            // 
            this.playerInput4.Location = new System.Drawing.Point(6, 159);
            this.playerInput4.Name = "playerInput4";
            this.playerInput4.Size = new System.Drawing.Size(356, 31);
            this.playerInput4.TabIndex = 21;
            // 
            // playerInput3
            // 
            this.playerInput3.Location = new System.Drawing.Point(6, 122);
            this.playerInput3.Name = "playerInput3";
            this.playerInput3.Size = new System.Drawing.Size(356, 31);
            this.playerInput3.TabIndex = 20;
            // 
            // playerInput2
            // 
            this.playerInput2.Location = new System.Drawing.Point(6, 85);
            this.playerInput2.Name = "playerInput2";
            this.playerInput2.Size = new System.Drawing.Size(356, 31);
            this.playerInput2.TabIndex = 19;
            // 
            // playerInput1
            // 
            this.playerInput1.Location = new System.Drawing.Point(6, 48);
            this.playerInput1.Name = "playerInput1";
            this.playerInput1.Size = new System.Drawing.Size(356, 31);
            this.playerInput1.TabIndex = 18;
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 454);
            this.Controls.Add(this.newGameBox);
            this.Name = "WelcomeForm";
            this.Text = "WelcomeForm";
            this.newGameBox.ResumeLayout(false);
            this.newGameBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox newGameBox;
        private PlayerInput playerInput4;
        private PlayerInput playerInput3;
        private PlayerInput playerInput2;
        private PlayerInput playerInput1;
    }
}