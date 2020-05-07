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
            this.bStartNew = new System.Windows.Forms.Button();
            this.newGameBox = new System.Windows.Forms.GroupBox();
            this.playerInput4 = new ThebesUI.PlayerInput();
            this.playerInput3 = new ThebesUI.PlayerInput();
            this.playerInput2 = new ThebesUI.PlayerInput();
            this.playerInput1 = new ThebesUI.PlayerInput();
            this.gameFromFileBox = new System.Windows.Forms.GroupBox();
            this.bStartLoaded = new System.Windows.Forms.Button();
            this.tbFilePath = new System.Windows.Forms.MaskedTextBox();
            this.bBrowse = new System.Windows.Forms.Button();
            this.tbFileName = new System.Windows.Forms.MaskedTextBox();
            this.newGameBox.SuspendLayout();
            this.gameFromFileBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Color";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Player name";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(-67, 57);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // bStartNew
            // 
            this.bStartNew.Location = new System.Drawing.Point(272, 207);
            this.bStartNew.Name = "bStartNew";
            this.bStartNew.Size = new System.Drawing.Size(75, 23);
            this.bStartNew.TabIndex = 5;
            this.bStartNew.Text = "Start";
            this.bStartNew.UseVisualStyleBackColor = true;
            this.bStartNew.Click += new System.EventHandler(this.bStartNew_Click);
            // 
            // newGameBox
            // 
            this.newGameBox.Controls.Add(this.playerInput4);
            this.newGameBox.Controls.Add(this.playerInput3);
            this.newGameBox.Controls.Add(this.playerInput2);
            this.newGameBox.Controls.Add(this.playerInput1);
            this.newGameBox.Controls.Add(this.bStartNew);
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
            // gameFromFileBox
            // 
            this.gameFromFileBox.Controls.Add(this.bStartLoaded);
            this.gameFromFileBox.Controls.Add(this.tbFilePath);
            this.gameFromFileBox.Controls.Add(this.bBrowse);
            this.gameFromFileBox.Controls.Add(this.tbFileName);
            this.gameFromFileBox.Location = new System.Drawing.Point(520, 29);
            this.gameFromFileBox.Name = "gameFromFileBox";
            this.gameFromFileBox.Size = new System.Drawing.Size(355, 252);
            this.gameFromFileBox.TabIndex = 5;
            this.gameFromFileBox.TabStop = false;
            this.gameFromFileBox.Text = "Load game";
            // 
            // bStartLoaded
            // 
            this.bStartLoaded.Location = new System.Drawing.Point(254, 207);
            this.bStartLoaded.Name = "bStartLoaded";
            this.bStartLoaded.Size = new System.Drawing.Size(75, 23);
            this.bStartLoaded.TabIndex = 25;
            this.bStartLoaded.Text = "Start";
            this.bStartLoaded.UseVisualStyleBackColor = true;
            this.bStartLoaded.Click += new System.EventHandler(this.bStartLoaded_Click);
            // 
            // tbFilePath
            // 
            this.tbFilePath.Location = new System.Drawing.Point(22, 57);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(307, 22);
            this.tbFilePath.TabIndex = 24;
            // 
            // bBrowse
            // 
            this.bBrowse.Location = new System.Drawing.Point(254, 28);
            this.bBrowse.Name = "bBrowse";
            this.bBrowse.Size = new System.Drawing.Size(75, 23);
            this.bBrowse.TabIndex = 23;
            this.bBrowse.Text = "Browse";
            this.bBrowse.UseVisualStyleBackColor = true;
            this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(22, 28);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(226, 22);
            this.tbFileName.TabIndex = 22;
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 454);
            this.Controls.Add(this.gameFromFileBox);
            this.Controls.Add(this.newGameBox);
            this.Name = "WelcomeForm";
            this.Text = "WelcomeForm";
            this.newGameBox.ResumeLayout(false);
            this.newGameBox.PerformLayout();
            this.gameFromFileBox.ResumeLayout(false);
            this.gameFromFileBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button bStartNew;
        private System.Windows.Forms.GroupBox newGameBox;
        private PlayerInput playerInput4;
        private PlayerInput playerInput3;
        private PlayerInput playerInput2;
        private PlayerInput playerInput1;
        private System.Windows.Forms.GroupBox gameFromFileBox;
        private System.Windows.Forms.MaskedTextBox tbFilePath;
        private System.Windows.Forms.Button bBrowse;
        private System.Windows.Forms.MaskedTextBox tbFileName;
        private System.Windows.Forms.Button bStartLoaded;
    }
}