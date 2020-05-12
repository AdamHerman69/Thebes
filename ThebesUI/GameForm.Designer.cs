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
            this.pBoard = new System.Windows.Forms.Panel();
            this.playerDisplay4 = new ThebesUI.PlayerDisplay();
            this.playerDisplay3 = new ThebesUI.PlayerDisplay();
            this.playerDisplay2 = new ThebesUI.PlayerDisplay();
            this.playerDisplay1 = new ThebesUI.PlayerDisplay();
            this.bEndYear = new System.Windows.Forms.Button();
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
            this.bSaveGame.Click += new System.EventHandler(this.bSaveGame_Click);
            // 
            // bExitGame
            // 
            this.bExitGame.Location = new System.Drawing.Point(688, 784);
            this.bExitGame.Name = "bExitGame";
            this.bExitGame.Size = new System.Drawing.Size(131, 35);
            this.bExitGame.TabIndex = 47;
            this.bExitGame.Text = "Exit game";
            this.bExitGame.UseVisualStyleBackColor = true;
            this.bExitGame.Click += new System.EventHandler(this.bExitGame_Click);
            // 
            // bUseZeppelin
            // 
            this.bUseZeppelin.Location = new System.Drawing.Point(1484, 784);
            this.bUseZeppelin.Name = "bUseZeppelin";
            this.bUseZeppelin.Size = new System.Drawing.Size(131, 35);
            this.bUseZeppelin.TabIndex = 48;
            this.bUseZeppelin.Text = "Use Zeppelin";
            this.bUseZeppelin.UseVisualStyleBackColor = true;
            this.bUseZeppelin.Click += new System.EventHandler(this.bUseZeppelin_Click);
            // 
            // pBoard
            // 
            this.pBoard.Location = new System.Drawing.Point(551, 12);
            this.pBoard.Name = "pBoard";
            this.pBoard.Size = new System.Drawing.Size(1064, 766);
            this.pBoard.TabIndex = 49;
            this.pBoard.Click += new System.EventHandler(this.pBoard_Click);
            // 
            // playerDisplay4
            // 
            this.playerDisplay4.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.playerDisplay4.Location = new System.Drawing.Point(12, 632);
            this.playerDisplay4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.playerDisplay4.Name = "playerDisplay4";
            this.playerDisplay4.Size = new System.Drawing.Size(491, 187);
            this.playerDisplay4.TabIndex = 3;
            this.playerDisplay4.Visible = false;
            // 
            // playerDisplay3
            // 
            this.playerDisplay3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.playerDisplay3.Location = new System.Drawing.Point(12, 418);
            this.playerDisplay3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.playerDisplay3.Name = "playerDisplay3";
            this.playerDisplay3.Size = new System.Drawing.Size(491, 187);
            this.playerDisplay3.TabIndex = 2;
            this.playerDisplay3.Visible = false;
            // 
            // playerDisplay2
            // 
            this.playerDisplay2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.playerDisplay2.Location = new System.Drawing.Point(12, 214);
            this.playerDisplay2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.playerDisplay2.Name = "playerDisplay2";
            this.playerDisplay2.Size = new System.Drawing.Size(491, 187);
            this.playerDisplay2.TabIndex = 1;
            this.playerDisplay2.Visible = false;
            // 
            // playerDisplay1
            // 
            this.playerDisplay1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.playerDisplay1.Location = new System.Drawing.Point(12, 12);
            this.playerDisplay1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.playerDisplay1.Name = "playerDisplay1";
            this.playerDisplay1.Size = new System.Drawing.Size(491, 187);
            this.playerDisplay1.TabIndex = 0;
            this.playerDisplay1.Visible = false;
            // 
            // bEndYear
            // 
            this.bEndYear.Location = new System.Drawing.Point(1347, 784);
            this.bEndYear.Name = "bEndYear";
            this.bEndYear.Size = new System.Drawing.Size(131, 35);
            this.bEndYear.TabIndex = 50;
            this.bEndYear.Text = "End Year";
            this.bEndYear.UseVisualStyleBackColor = true;
            this.bEndYear.Click += new System.EventHandler(this.bEndYear_Click);
            // 
            // GameForm
            // 
            this.ClientSize = new System.Drawing.Size(1828, 907);
            this.Controls.Add(this.bEndYear);
            this.Controls.Add(this.pBoard);
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
        private System.Windows.Forms.Panel pBoard;
        private System.Windows.Forms.Button bEndYear;
    }
}