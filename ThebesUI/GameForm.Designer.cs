﻿namespace ThebesUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.bSaveGame = new System.Windows.Forms.Button();
            this.bExitGame = new System.Windows.Forms.Button();
            this.pBoard = new System.Windows.Forms.Panel();
            this.bEndYear = new System.Windows.Forms.Button();
            this.flpPlayerDisplay = new System.Windows.Forms.FlowLayoutPanel();
            this.cbUseZeppelin = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // bSaveGame
            // 
            this.bSaveGame.Location = new System.Drawing.Point(414, 784);
            this.bSaveGame.Name = "bSaveGame";
            this.bSaveGame.Size = new System.Drawing.Size(131, 35);
            this.bSaveGame.TabIndex = 46;
            this.bSaveGame.Text = "Save game";
            this.bSaveGame.UseVisualStyleBackColor = true;
            this.bSaveGame.Click += new System.EventHandler(this.bSaveGame_Click);
            // 
            // bExitGame
            // 
            this.bExitGame.Location = new System.Drawing.Point(551, 784);
            this.bExitGame.Name = "bExitGame";
            this.bExitGame.Size = new System.Drawing.Size(131, 35);
            this.bExitGame.TabIndex = 47;
            this.bExitGame.Text = "Exit game";
            this.bExitGame.UseVisualStyleBackColor = true;
            this.bExitGame.Click += new System.EventHandler(this.bExitGame_Click);
            // 
            // pBoard
            // 
            this.pBoard.Location = new System.Drawing.Point(417, 12);
            this.pBoard.Name = "pBoard";
            this.pBoard.Size = new System.Drawing.Size(1064, 766);
            this.pBoard.TabIndex = 49;
            this.pBoard.Click += new System.EventHandler(this.pBoard_Click);
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
            // flpPlayerDisplay
            // 
            this.flpPlayerDisplay.AutoScroll = true;
            this.flpPlayerDisplay.Location = new System.Drawing.Point(12, 12);
            this.flpPlayerDisplay.Name = "flpPlayerDisplay";
            this.flpPlayerDisplay.Size = new System.Drawing.Size(396, 809);
            this.flpPlayerDisplay.TabIndex = 51;
            // 
            // cbUseZeppelin
            // 
            this.cbUseZeppelin.AutoSize = true;
            this.cbUseZeppelin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cbUseZeppelin.Location = new System.Drawing.Point(1219, 789);
            this.cbUseZeppelin.Name = "cbUseZeppelin";
            this.cbUseZeppelin.Size = new System.Drawing.Size(122, 24);
            this.cbUseZeppelin.TabIndex = 52;
            this.cbUseZeppelin.Text = "Use Zeppelin";
            this.cbUseZeppelin.UseVisualStyleBackColor = true;
            this.cbUseZeppelin.CheckedChanged += new System.EventHandler(this.cbUseZeppelin_CheckedChanged);
            // 
            // GameForm
            // 
            this.ClientSize = new System.Drawing.Size(1494, 833);
            this.Controls.Add(this.cbUseZeppelin);
            this.Controls.Add(this.flpPlayerDisplay);
            this.Controls.Add(this.bEndYear);
            this.Controls.Add(this.pBoard);
            this.Controls.Add(this.bExitGame);
            this.Controls.Add(this.bSaveGame);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1510, 872);
            this.Name = "GameForm";
            this.Text = "Thebes";
            this.Shown += new System.EventHandler(this.GameForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bSaveGame;
        private System.Windows.Forms.Button bExitGame;
        private System.Windows.Forms.Panel pBoard;
        private System.Windows.Forms.Button bEndYear;
        private System.Windows.Forms.FlowLayoutPanel flpPlayerDisplay;
        private System.Windows.Forms.CheckBox cbUseZeppelin;
    }
}