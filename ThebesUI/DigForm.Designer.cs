namespace ThebesUI
{
    partial class DigForm
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
            this.lDigSiteName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lSpecializedKnowledgeAmount = new System.Windows.Forms.Label();
            this.lGeneralKnowledgeAmount = new System.Windows.Forms.Label();
            this.lAssistentKnowledgeAmount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lAssistentsLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lDrawAmount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lvTokens = new System.Windows.Forms.ListView();
            this.lvSingleUseCards = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.bDigButton = new System.Windows.Forms.Button();
            this.nudWeeks = new System.Windows.Forms.NumericUpDown();
            this.lTotalKnowledge = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeeks)).BeginInit();
            this.SuspendLayout();
            // 
            // lDigSiteName
            // 
            this.lDigSiteName.AutoSize = true;
            this.lDigSiteName.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.lDigSiteName.Location = new System.Drawing.Point(12, 19);
            this.lDigSiteName.Name = "lDigSiteName";
            this.lDigSiteName.Size = new System.Drawing.Size(278, 48);
            this.lDigSiteName.TabIndex = 0;
            this.lDigSiteName.Text = "DigSite Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(15, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(297, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Your knowledge points:";
            // 
            // lSpecializedKnowledgeAmount
            // 
            this.lSpecializedKnowledgeAmount.AutoSize = true;
            this.lSpecializedKnowledgeAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lSpecializedKnowledgeAmount.Location = new System.Drawing.Point(15, 132);
            this.lSpecializedKnowledgeAmount.Name = "lSpecializedKnowledgeAmount";
            this.lSpecializedKnowledgeAmount.Size = new System.Drawing.Size(24, 26);
            this.lSpecializedKnowledgeAmount.TabIndex = 2;
            this.lSpecializedKnowledgeAmount.Text = "0";
            // 
            // lGeneralKnowledgeAmount
            // 
            this.lGeneralKnowledgeAmount.AutoSize = true;
            this.lGeneralKnowledgeAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lGeneralKnowledgeAmount.Location = new System.Drawing.Point(15, 178);
            this.lGeneralKnowledgeAmount.Name = "lGeneralKnowledgeAmount";
            this.lGeneralKnowledgeAmount.Size = new System.Drawing.Size(24, 26);
            this.lGeneralKnowledgeAmount.TabIndex = 3;
            this.lGeneralKnowledgeAmount.Text = "0";
            // 
            // lAssistentKnowledgeAmount
            // 
            this.lAssistentKnowledgeAmount.AutoSize = true;
            this.lAssistentKnowledgeAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lAssistentKnowledgeAmount.Location = new System.Drawing.Point(15, 221);
            this.lAssistentKnowledgeAmount.Name = "lAssistentKnowledgeAmount";
            this.lAssistentKnowledgeAmount.Size = new System.Drawing.Size(24, 26);
            this.lAssistentKnowledgeAmount.TabIndex = 4;
            this.lAssistentKnowledgeAmount.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label5.Location = new System.Drawing.Point(61, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(234, 26);
            this.label5.TabIndex = 5;
            this.label5.Text = "Specialized knowledge";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label6.Location = new System.Drawing.Point(61, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(199, 26);
            this.label6.TabIndex = 6;
            this.label6.Text = "General knowledge";
            // 
            // lAssistentsLabel
            // 
            this.lAssistentsLabel.AutoSize = true;
            this.lAssistentsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lAssistentsLabel.Location = new System.Drawing.Point(61, 221);
            this.lAssistentsLabel.Name = "lAssistentsLabel";
            this.lAssistentsLabel.Size = new System.Drawing.Size(159, 26);
            this.lAssistentsLabel.TabIndex = 7;
            this.lAssistentsLabel.Text = "from assistents";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(15, 265);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(343, 29);
            this.label7.TabIndex = 8;
            this.label7.Text = "Single use cards available:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label8.Location = new System.Drawing.Point(12, 479);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 26);
            this.label8.TabIndex = 9;
            this.label8.Text = "Weeks to dig:";
            // 
            // lDrawAmount
            // 
            this.lDrawAmount.AutoSize = true;
            this.lDrawAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lDrawAmount.Location = new System.Drawing.Point(241, 479);
            this.lDrawAmount.Name = "lDrawAmount";
            this.lDrawAmount.Size = new System.Drawing.Size(210, 26);
            this.lDrawAmount.TabIndex = 11;
            this.lDrawAmount.Text = "You\'ll draw X tokens";
            this.lDrawAmount.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(823, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(172, 29);
            this.label10.TabIndex = 12;
            this.label10.Text = "Tokens here:";
            // 
            // lvTokens
            // 
            this.lvTokens.HideSelection = false;
            this.lvTokens.Location = new System.Drawing.Point(828, 132);
            this.lvTokens.Name = "lvTokens";
            this.lvTokens.Size = new System.Drawing.Size(201, 302);
            this.lvTokens.TabIndex = 13;
            this.lvTokens.UseCompatibleStateImageBehavior = false;
            // 
            // lvSingleUseCards
            // 
            this.lvSingleUseCards.CheckBoxes = true;
            this.lvSingleUseCards.HideSelection = false;
            this.lvSingleUseCards.Location = new System.Drawing.Point(20, 309);
            this.lvSingleUseCards.Name = "lvSingleUseCards";
            this.lvSingleUseCards.Size = new System.Drawing.Size(338, 125);
            this.lvSingleUseCards.TabIndex = 14;
            this.lvSingleUseCards.UseCompatibleStateImageBehavior = false;
            this.lvSingleUseCards.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvSingleUseCards_ItemCheck);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(783, 479);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 33);
            this.button1.TabIndex = 15;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // bDigButton
            // 
            this.bDigButton.Location = new System.Drawing.Point(913, 479);
            this.bDigButton.Name = "bDigButton";
            this.bDigButton.Size = new System.Drawing.Size(116, 33);
            this.bDigButton.TabIndex = 16;
            this.bDigButton.Text = "Dig";
            this.bDigButton.UseVisualStyleBackColor = true;
            this.bDigButton.Click += new System.EventHandler(this.bDigButton_Click);
            // 
            // nudWeeks
            // 
            this.nudWeeks.Location = new System.Drawing.Point(158, 485);
            this.nudWeeks.Name = "nudWeeks";
            this.nudWeeks.Size = new System.Drawing.Size(77, 22);
            this.nudWeeks.TabIndex = 17;
            this.nudWeeks.ValueChanged += new System.EventHandler(this.nudWeeks_ValueChanged);
            // 
            // lTotalKnowledge
            // 
            this.lTotalKnowledge.AutoSize = true;
            this.lTotalKnowledge.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lTotalKnowledge.Location = new System.Drawing.Point(12, 444);
            this.lTotalKnowledge.Name = "lTotalKnowledge";
            this.lTotalKnowledge.Size = new System.Drawing.Size(193, 26);
            this.lTotalKnowledge.TabIndex = 18;
            this.lTotalKnowledge.Text = "Total knowledge: 0";
            // 
            // DigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 532);
            this.Controls.Add(this.lTotalKnowledge);
            this.Controls.Add(this.nudWeeks);
            this.Controls.Add(this.bDigButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lvSingleUseCards);
            this.Controls.Add(this.lvTokens);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lDrawAmount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lAssistentsLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lAssistentKnowledgeAmount);
            this.Controls.Add(this.lGeneralKnowledgeAmount);
            this.Controls.Add(this.lSpecializedKnowledgeAmount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lDigSiteName);
            this.Name = "DigForm";
            this.Text = "DigForm";
            ((System.ComponentModel.ISupportInitialize)(this.nudWeeks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lDigSiteName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lSpecializedKnowledgeAmount;
        private System.Windows.Forms.Label lGeneralKnowledgeAmount;
        private System.Windows.Forms.Label lAssistentKnowledgeAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lAssistentsLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lDrawAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListView lvTokens;
        private System.Windows.Forms.ListView lvSingleUseCards;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bDigButton;
        private System.Windows.Forms.NumericUpDown nudWeeks;
        private System.Windows.Forms.Label lTotalKnowledge;
    }
}