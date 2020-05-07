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
            //this.playerDisplay1 = new ThebesUI.PlayerDisplay();
            //this.playerDisplay2 = new ThebesUI.PlayerDisplay();
            //this.playerDisplay3 = new ThebesUI.PlayerDisplay();
            //this.playerDisplay4 = new ThebesUI.PlayerDisplay();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.bSaveGame = new System.Windows.Forms.Button();
            this.bExitGame = new System.Windows.Forms.Button();
            this.bUseZeppelin = new System.Windows.Forms.Button();
            this.pbCard1 = new System.Windows.Forms.PictureBox();
            this.pbCard2 = new System.Windows.Forms.PictureBox();
            this.pbCard3 = new System.Windows.Forms.PictureBox();
            this.pbCard4 = new System.Windows.Forms.PictureBox();
            this.pbExhibition1 = new System.Windows.Forms.PictureBox();
            this.pbExhibition2 = new System.Windows.Forms.PictureBox();
            this.pbExhibition3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExhibition1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExhibition2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExhibition3)).BeginInit();
            this.SuspendLayout();
            // 
            // playerDisplay1
            // 
            this.playerDisplay1.Location = new System.Drawing.Point(12, 12);
            this.playerDisplay1.Name = "playerDisplay1";
            this.playerDisplay1.Size = new System.Drawing.Size(491, 187);
            this.playerDisplay1.TabIndex = 0;
            this.playerDisplay1.Visible = false;
            // 
            // playerDisplay2
            // 
            this.playerDisplay2.Location = new System.Drawing.Point(12, 214);
            this.playerDisplay2.Name = "playerDisplay2";
            this.playerDisplay2.Size = new System.Drawing.Size(491, 187);
            this.playerDisplay2.TabIndex = 1;
            this.playerDisplay2.Visible = false;
            // 
            // playerDisplay3
            // 
            this.playerDisplay3.Location = new System.Drawing.Point(12, 418);
            this.playerDisplay3.Name = "playerDisplay3";
            this.playerDisplay3.Size = new System.Drawing.Size(491, 187);
            this.playerDisplay3.TabIndex = 2;
            this.playerDisplay3.Visible = false;
            // 
            // playerDisplay4
            // 
            this.playerDisplay4.Location = new System.Drawing.Point(12, 632);
            this.playerDisplay4.Name = "playerDisplay4";
            this.playerDisplay4.Size = new System.Drawing.Size(491, 187);
            this.playerDisplay4.TabIndex = 3;
            this.playerDisplay4.Visible = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 843);
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
            // pbCard1
            // 
            this.pbCard1.AccessibleDescription = "0";
            this.pbCard1.Location = new System.Drawing.Point(1447, 60);
            this.pbCard1.Name = "pbCard1";
            this.pbCard1.Size = new System.Drawing.Size(135, 187);
            this.pbCard1.TabIndex = 49;
            this.pbCard1.TabStop = false;
            // 
            // pbCard2
            // 
            this.pbCard2.AccessibleDescription = "1";
            this.pbCard2.Location = new System.Drawing.Point(1620, 60);
            this.pbCard2.Name = "pbCard2";
            this.pbCard2.Size = new System.Drawing.Size(135, 187);
            this.pbCard2.TabIndex = 50;
            this.pbCard2.TabStop = false;
            // 
            // pbCard3
            // 
            this.pbCard3.AccessibleDescription = "2";
            this.pbCard3.Location = new System.Drawing.Point(1447, 281);
            this.pbCard3.Name = "pbCard3";
            this.pbCard3.Size = new System.Drawing.Size(135, 187);
            this.pbCard3.TabIndex = 51;
            this.pbCard3.TabStop = false;
            // 
            // pbCard4
            // 
            this.pbCard4.AccessibleDescription = "3";
            this.pbCard4.Location = new System.Drawing.Point(1620, 281);
            this.pbCard4.Name = "pbCard4";
            this.pbCard4.Size = new System.Drawing.Size(135, 187);
            this.pbCard4.TabIndex = 52;
            this.pbCard4.TabStop = false;
            // 
            // pbExhibition1
            // 
            this.pbExhibition1.AccessibleDescription = "0";
            this.pbExhibition1.Location = new System.Drawing.Point(567, 482);
            this.pbExhibition1.Name = "pbExhibition1";
            this.pbExhibition1.Size = new System.Drawing.Size(209, 135);
            this.pbExhibition1.TabIndex = 53;
            this.pbExhibition1.TabStop = false;
            // 
            // pbExhibition2
            // 
            this.pbExhibition2.AccessibleDescription = "1";
            this.pbExhibition2.Location = new System.Drawing.Point(567, 632);
            this.pbExhibition2.Name = "pbExhibition2";
            this.pbExhibition2.Size = new System.Drawing.Size(209, 135);
            this.pbExhibition2.TabIndex = 54;
            this.pbExhibition2.TabStop = false;
            // 
            // pbExhibition3
            // 
            this.pbExhibition3.AccessibleDescription = "2";
            this.pbExhibition3.Location = new System.Drawing.Point(798, 632);
            this.pbExhibition3.Name = "pbExhibition3";
            this.pbExhibition3.Size = new System.Drawing.Size(209, 135);
            this.pbExhibition3.TabIndex = 55;
            this.pbExhibition3.TabStop = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1828, 843);
            this.Controls.Add(this.pbExhibition3);
            this.Controls.Add(this.pbExhibition2);
            this.Controls.Add(this.pbExhibition1);
            this.Controls.Add(this.pbCard4);
            this.Controls.Add(this.pbCard3);
            this.Controls.Add(this.pbCard2);
            this.Controls.Add(this.pbCard1);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCard4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExhibition1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExhibition2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExhibition3)).EndInit();
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
        private System.Windows.Forms.PictureBox pbCard1;
        private System.Windows.Forms.PictureBox pbCard2;
        private System.Windows.Forms.PictureBox pbCard3;
        private System.Windows.Forms.PictureBox pbCard4;
        private System.Windows.Forms.PictureBox pbExhibition1;
        private System.Windows.Forms.PictureBox pbExhibition2;
        private System.Windows.Forms.PictureBox pbExhibition3;
    }
}