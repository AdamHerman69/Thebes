namespace ThebesUI
{
    partial class GameForm
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
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.bSaveGame = new System.Windows.Forms.Button();
            this.bExitGame = new System.Windows.Forms.Button();
            this.bUseZeppelin = new System.Windows.Forms.Button();
            this.pbBoard = new System.Windows.Forms.PictureBox();
            this.playerDisplay4 = new ThebesUI.PlayerDisplay();
            this.playerDisplay3 = new ThebesUI.PlayerDisplay();
            this.playerDisplay2 = new ThebesUI.PlayerDisplay();
            this.playerDisplay1 = new ThebesUI.PlayerDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.pbBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 907);
            this.splitter1.TabIndex = 15;
            this.splitter1.TabStop = false;
            // 
            // bSaveGame
            // 
            this.bSaveGame.Location = new System.Drawing.Point(551, 784);
            this.bSaveGame.Name = "bSaveGame";
            this.bSaveGame.Size = new System.Drawing.Size(131, 35);
            this.bSaveGame.TabIndex = 46;
            this.bSaveGame.Text = "Save game";
            this.bSaveGame.UseVisualStyleBackColor = true;
            // 
            // bExitGame
            // 
            this.bExitGame.Location = new System.Drawing.Point(688, 784);
            this.bExitGame.Name = "bExitGame";
            this.bExitGame.Size = new System.Drawing.Size(131, 35);
            this.bExitGame.TabIndex = 47;
            this.bExitGame.Text = "Exit game";
            this.bExitGame.UseVisualStyleBackColor = true;
            // 
            // bUseZeppelin
            // 
            this.bUseZeppelin.Location = new System.Drawing.Point(1661, 784);
            this.bUseZeppelin.Name = "bUseZeppelin";
            this.bUseZeppelin.Size = new System.Drawing.Size(131, 35);
            this.bUseZeppelin.TabIndex = 48;
            this.bUseZeppelin.Text = "Use Zeppelin";
            this.bUseZeppelin.UseVisualStyleBackColor = true;
            this.bUseZeppelin.Click += new System.EventHandler(this.bUseZeppelin_Click);
            // 
            // pbBoard
            // 
            this.pbBoard.Location = new System.Drawing.Point(551, 12);
            this.pbBoard.Name = "pbBoard";
            this.pbBoard.Size = new System.Drawing.Size(1241, 766);
            this.pbBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbBoard.TabIndex = 49;
            this.pbBoard.TabStop = false;
            // 
            // playerDisplay4
            // 
            this.playerDisplay4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.playerDisplay4.Location = new System.Drawing.Point(12, 632);
            this.playerDisplay4.Name = "playerDisplay4";
            this.playerDisplay4.Size = new System.Drawing.Size(491, 187);
            this.playerDisplay4.TabIndex = 3;
            this.playerDisplay4.Visible = false;
            // 
            // playerDisplay3
            // 
            this.playerDisplay3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.playerDisplay3.Location = new System.Drawing.Point(12, 418);
            this.playerDisplay3.Name = "playerDisplay3";
            this.playerDisplay3.Size = new System.Drawing.Size(491, 187);
            this.playerDisplay3.TabIndex = 2;
            this.playerDisplay3.Visible = false;
            // 
            // playerDisplay2
            // 
            this.playerDisplay2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.playerDisplay2.Location = new System.Drawing.Point(12, 214);
            this.playerDisplay2.Name = "playerDisplay2";
            this.playerDisplay2.Size = new System.Drawing.Size(491, 187);
            this.playerDisplay2.TabIndex = 1;
            this.playerDisplay2.Visible = false;
            // 
            // playerDisplay1
            // 
            this.playerDisplay1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.playerDisplay1.Location = new System.Drawing.Point(12, 12);
            this.playerDisplay1.Name = "playerDisplay1";
            this.playerDisplay1.Size = new System.Drawing.Size(491, 187);
            this.playerDisplay1.TabIndex = 0;
            this.playerDisplay1.Visible = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;   
            this.ClientSize = new System.Drawing.Size(1828, 907);
            this.Controls.Add(this.pbBoard);
            this.Controls.Add(this.bUseZeppelin);
            this.Controls.Add(this.bExitGame);
            this.Controls.Add(this.bSaveGame);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.playerDisplay4);
            this.Controls.Add(this.playerDisplay3);
            this.Controls.Add(this.playerDisplay2);
            this.Controls.Add(this.playerDisplay1);
            this.Name = "GameForm";
            this.Text = "GameForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbBoard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PlayerDisplay playerDisplay1;
        private PlayerDisplay playerDisplay2;
        private PlayerDisplay playerDisplay3;
        private PlayerDisplay playerDisplay4;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button bSaveGame;
        private System.Windows.Forms.Button bExitGame;
        private System.Windows.Forms.Button bUseZeppelin;
        private System.Windows.Forms.PictureBox pbBoard;
    }
}