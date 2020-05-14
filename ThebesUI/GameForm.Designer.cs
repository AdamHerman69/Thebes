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
            this.bSaveGame = new System.Windows.Forms.Button();
            this.bExitGame = new System.Windows.Forms.Button();
            this.bUseZeppelin = new System.Windows.Forms.Button();
            this.pBoard = new System.Windows.Forms.Panel();
            this.bEndYear = new System.Windows.Forms.Button();
            this.flpPlayerDisplay = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
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
            this.bUseZeppelin.Location = new System.Drawing.Point(1347, 784);
            this.bUseZeppelin.Name = "bUseZeppelin";
            this.bUseZeppelin.Size = new System.Drawing.Size(131, 35);
            this.bUseZeppelin.TabIndex = 48;
            this.bUseZeppelin.Text = "Use Zeppelin";
            this.bUseZeppelin.UseVisualStyleBackColor = true;
            this.bUseZeppelin.Visible = false;
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
            // bEndYear
            // 
            this.bEndYear.Location = new System.Drawing.Point(1484, 784);
            this.bEndYear.Name = "bEndYear";
            this.bEndYear.Size = new System.Drawing.Size(131, 35);
            this.bEndYear.TabIndex = 50;
            this.bEndYear.Text = "End Year";
            this.bEndYear.UseVisualStyleBackColor = true;
            this.bEndYear.Click += new System.EventHandler(this.bEndYear_Click);
            // 
            // flpPlayerDisplay
            // 
            this.flpPlayerDisplay.Location = new System.Drawing.Point(12, 12);
            this.flpPlayerDisplay.Name = "flpPlayerDisplay";
            this.flpPlayerDisplay.Size = new System.Drawing.Size(533, 940);
            this.flpPlayerDisplay.TabIndex = 51;
            // 
            // GameForm
            // 
            this.ClientSize = new System.Drawing.Size(1828, 964);
            this.Controls.Add(this.flpPlayerDisplay);
            this.Controls.Add(this.bEndYear);
            this.Controls.Add(this.pBoard);
            this.Controls.Add(this.bUseZeppelin);
            this.Controls.Add(this.bExitGame);
            this.Controls.Add(this.bSaveGame);
            this.DoubleBuffered = true;
            this.Name = "GameForm";
            this.Text = "Thebes";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bSaveGame;
        private System.Windows.Forms.Button bExitGame;
        private System.Windows.Forms.Button bUseZeppelin;
        private System.Windows.Forms.Panel pBoard;
        private System.Windows.Forms.Button bEndYear;
        private System.Windows.Forms.FlowLayoutPanel flpPlayerDisplay;
    }
}