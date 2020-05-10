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
            this.lAssistantKnowledgeAmount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lAssistantsLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lDrawAmount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.bDigButton = new System.Windows.Forms.Button();
            this.nudWeeks = new System.Windows.Forms.NumericUpDown();
            this.lTotalKnowledge = new System.Windows.Forms.Label();
            this.flpTokens = new System.Windows.Forms.FlowLayoutPanel();
            this.clSingleUseCards = new ThebesUI.CardList();
            ((System.ComponentModel.ISupportInitialize)(this.nudWeeks)).BeginInit();
            this.SuspendLayout();
            // 
            // lDigSiteName
            // 
            this.lDigSiteName.AutoSize = true;
            this.lDigSiteName.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.lDigSiteName.Location = new System.Drawing.Point(12, 19);
            this.lDigSiteName.Name = "lDigSiteName";
            this.lDigSiteName.Size = new System.Drawing.Size(228, 39);
            this.lDigSiteName.TabIndex = 0;
            this.lDigSiteName.Text = "DigSite Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(15, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(237, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Your knowledge points:";
            // 
            // lSpecializedKnowledgeAmount
            // 
            this.lSpecializedKnowledgeAmount.AutoSize = true;
            this.lSpecializedKnowledgeAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lSpecializedKnowledgeAmount.Location = new System.Drawing.Point(15, 132);
            this.lSpecializedKnowledgeAmount.Name = "lSpecializedKnowledgeAmount";
            this.lSpecializedKnowledgeAmount.Size = new System.Drawing.Size(20, 22);
            this.lSpecializedKnowledgeAmount.TabIndex = 2;
            this.lSpecializedKnowledgeAmount.Text = "0";
            // 
            // lGeneralKnowledgeAmount
            // 
            this.lGeneralKnowledgeAmount.AutoSize = true;
            this.lGeneralKnowledgeAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lGeneralKnowledgeAmount.Location = new System.Drawing.Point(15, 178);
            this.lGeneralKnowledgeAmount.Name = "lGeneralKnowledgeAmount";
            this.lGeneralKnowledgeAmount.Size = new System.Drawing.Size(20, 22);
            this.lGeneralKnowledgeAmount.TabIndex = 3;
            this.lGeneralKnowledgeAmount.Text = "0";
            // 
            // lAssistantKnowledgeAmount
            // 
            this.lAssistantKnowledgeAmount.AutoSize = true;
            this.lAssistantKnowledgeAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lAssistantKnowledgeAmount.Location = new System.Drawing.Point(15, 221);
            this.lAssistantKnowledgeAmount.Name = "lAssistantKnowledgeAmount";
            this.lAssistantKnowledgeAmount.Size = new System.Drawing.Size(20, 22);
            this.lAssistantKnowledgeAmount.TabIndex = 4;
            this.lAssistantKnowledgeAmount.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label5.Location = new System.Drawing.Point(61, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(192, 22);
            this.label5.TabIndex = 5;
            this.label5.Text = "Specialized knowledge";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label6.Location = new System.Drawing.Point(61, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 22);
            this.label6.TabIndex = 6;
            this.label6.Text = "General knowledge";
            // 
            // lAssistantsLabel
            // 
            this.lAssistantsLabel.AutoSize = true;
            this.lAssistantsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lAssistantsLabel.Location = new System.Drawing.Point(61, 221);
            this.lAssistantsLabel.Name = "lAssistantsLabel";
            this.lAssistantsLabel.Size = new System.Drawing.Size(130, 22);
            this.lAssistantsLabel.TabIndex = 7;
            this.lAssistantsLabel.Text = "from assistants";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(15, 265);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(272, 25);
            this.label7.TabIndex = 8;
            this.label7.Text = "Single use cards available:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label8.Location = new System.Drawing.Point(12, 479);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 22);
            this.label8.TabIndex = 9;
            this.label8.Text = "Weeks to dig:";
            // 
            // lDrawAmount
            // 
            this.lDrawAmount.AutoSize = true;
            this.lDrawAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lDrawAmount.Location = new System.Drawing.Point(241, 479);
            this.lDrawAmount.Name = "lDrawAmount";
            this.lDrawAmount.Size = new System.Drawing.Size(173, 22);
            this.lDrawAmount.TabIndex = 11;
            this.lDrawAmount.Text = "You\'ll draw X tokens";
            this.lDrawAmount.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(678, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(140, 25);
            this.label10.TabIndex = 12;
            this.label10.Text = "Tokens here:";
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(783, 479);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(114, 33);
            this.bCancel.TabIndex = 15;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
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
            this.nudWeeks.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudWeeks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWeeks.Name = "nudWeeks";
            this.nudWeeks.Size = new System.Drawing.Size(77, 22);
            this.nudWeeks.TabIndex = 17;
            this.nudWeeks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWeeks.ValueChanged += new System.EventHandler(this.nudWeeks_ValueChanged);
            // 
            // lTotalKnowledge
            // 
            this.lTotalKnowledge.AutoSize = true;
            this.lTotalKnowledge.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.lTotalKnowledge.Location = new System.Drawing.Point(12, 444);
            this.lTotalKnowledge.Name = "lTotalKnowledge";
            this.lTotalKnowledge.Size = new System.Drawing.Size(162, 22);
            this.lTotalKnowledge.TabIndex = 18;
            this.lTotalKnowledge.Text = "Total knowledge: 0";
            // 
            // flpTokens
            // 
            this.flpTokens.Location = new System.Drawing.Point(683, 132);
            this.flpTokens.Name = "flpTokens";
            this.flpTokens.Size = new System.Drawing.Size(336, 334);
            this.flpTokens.TabIndex = 20;
            // 
            // clSingleUseCards
            // 
            this.clSingleUseCards.Location = new System.Drawing.Point(20, 307);
            this.clSingleUseCards.Name = "clSingleUseCards";
            this.clSingleUseCards.Size = new System.Drawing.Size(611, 127);
            this.clSingleUseCards.TabIndex = 19;
            // 
            // DigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 532);
            this.Controls.Add(this.flpTokens);
            this.Controls.Add(this.clSingleUseCards);
            this.Controls.Add(this.lTotalKnowledge);
            this.Controls.Add(this.nudWeeks);
            this.Controls.Add(this.bDigButton);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lDrawAmount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lAssistantsLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lAssistantKnowledgeAmount);
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
        private System.Windows.Forms.Label lAssistantKnowledgeAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lAssistantsLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lDrawAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bDigButton;
        private System.Windows.Forms.NumericUpDown nudWeeks;
        private System.Windows.Forms.Label lTotalKnowledge;
        private CardList clSingleUseCards;
        private System.Windows.Forms.FlowLayoutPanel flpTokens;
    }
}