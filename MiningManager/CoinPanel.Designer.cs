namespace MiningManager
{
    partial class CoinPanel
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
            this.CPUbat = new System.Windows.Forms.Label();
            this.GPUbat = new System.Windows.Forms.Label();
            this.setCPU = new System.Windows.Forms.Button();
            this.setGPU = new System.Windows.Forms.Button();
            this.diffMnemonic = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.coinSymbol2 = new System.Windows.Forms.Label();
            this.coinSymbol1 = new System.Windows.Forms.Label();
            this.difficulty = new System.Windows.Forms.Label();
            this.rewInUsd = new System.Windows.Forms.Label();
            this.rewInCoins = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.wantReward = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.coinIcon = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.coinIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // CPUbat
            // 
            this.CPUbat.AutoEllipsis = true;
            this.CPUbat.AutoSize = true;
            this.CPUbat.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CPUbat.Location = new System.Drawing.Point(326, 60);
            this.CPUbat.Name = "CPUbat";
            this.CPUbat.Size = new System.Drawing.Size(69, 12);
            this.CPUbat.TabIndex = 23;
            this.CPUbat.Text = "CPU.bat empty";
            this.CPUbat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GPUbat
            // 
            this.GPUbat.AutoEllipsis = true;
            this.GPUbat.AutoSize = true;
            this.GPUbat.Cursor = System.Windows.Forms.Cursors.Default;
            this.GPUbat.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GPUbat.Location = new System.Drawing.Point(326, 8);
            this.GPUbat.MaximumSize = new System.Drawing.Size(265, 13);
            this.GPUbat.Name = "GPUbat";
            this.GPUbat.Size = new System.Drawing.Size(41, 12);
            this.GPUbat.TabIndex = 22;
            this.GPUbat.Text = "GPU.bat";
            this.GPUbat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // setCPU
            // 
            this.setCPU.Location = new System.Drawing.Point(328, 80);
            this.setCPU.Name = "setCPU";
            this.setCPU.Size = new System.Drawing.Size(100, 23);
            this.setCPU.TabIndex = 21;
            this.setCPU.Text = "Set CPU.bat";
            this.setCPU.UseVisualStyleBackColor = true;
            this.setCPU.Click += new System.EventHandler(this.setCPU_Click);
            // 
            // setGPU
            // 
            this.setGPU.Location = new System.Drawing.Point(328, 28);
            this.setGPU.Name = "setGPU";
            this.setGPU.Size = new System.Drawing.Size(100, 23);
            this.setGPU.TabIndex = 20;
            this.setGPU.Text = "Set GPU.bat";
            this.setGPU.UseVisualStyleBackColor = true;
            this.setGPU.Click += new System.EventHandler(this.setGPU_Click);
            // 
            // diffMnemonic
            // 
            this.diffMnemonic.Location = new System.Drawing.Point(272, 81);
            this.diffMnemonic.Name = "diffMnemonic";
            this.diffMnemonic.Size = new System.Drawing.Size(50, 20);
            this.diffMnemonic.TabIndex = 18;
            this.diffMnemonic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(272, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "USD";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // coinSymbol2
            // 
            this.coinSymbol2.Location = new System.Drawing.Point(272, 29);
            this.coinSymbol2.Name = "coinSymbol2";
            this.coinSymbol2.Size = new System.Drawing.Size(50, 20);
            this.coinSymbol2.TabIndex = 16;
            this.coinSymbol2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // coinSymbol1
            // 
            this.coinSymbol1.Location = new System.Drawing.Point(272, 3);
            this.coinSymbol1.Name = "coinSymbol1";
            this.coinSymbol1.Size = new System.Drawing.Size(50, 20);
            this.coinSymbol1.TabIndex = 19;
            this.coinSymbol1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // difficulty
            // 
            this.difficulty.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.difficulty.Location = new System.Drawing.Point(196, 81);
            this.difficulty.Name = "difficulty";
            this.difficulty.Size = new System.Drawing.Size(70, 20);
            this.difficulty.TabIndex = 15;
            this.difficulty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rewInUsd
            // 
            this.rewInUsd.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rewInUsd.Location = new System.Drawing.Point(196, 55);
            this.rewInUsd.Name = "rewInUsd";
            this.rewInUsd.Size = new System.Drawing.Size(70, 20);
            this.rewInUsd.TabIndex = 14;
            this.rewInUsd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rewInCoins
            // 
            this.rewInCoins.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rewInCoins.Location = new System.Drawing.Point(196, 29);
            this.rewInCoins.Name = "rewInCoins";
            this.rewInCoins.Size = new System.Drawing.Size(70, 20);
            this.rewInCoins.TabIndex = 13;
            this.rewInCoins.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(105, 81);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Difficulty";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // wantReward
            // 
            this.wantReward.Location = new System.Drawing.Point(196, 4);
            this.wantReward.Name = "wantReward";
            this.wantReward.Size = new System.Drawing.Size(70, 20);
            this.wantReward.TabIndex = 12;
            this.wantReward.TextChanged += new System.EventHandler(this.wantReward_TextChanged);
            this.wantReward.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.wantReward_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(105, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Current reward";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(105, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Current reward";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(105, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Want reward";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // coinIcon
            // 
            this.coinIcon.Location = new System.Drawing.Point(3, 5);
            this.coinIcon.Margin = new System.Windows.Forms.Padding(3, 35, 3, 25);
            this.coinIcon.Name = "coinIcon";
            this.coinIcon.Size = new System.Drawing.Size(96, 96);
            this.coinIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.coinIcon.TabIndex = 7;
            this.coinIcon.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // CoinPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CPUbat);
            this.Controls.Add(this.GPUbat);
            this.Controls.Add(this.setCPU);
            this.Controls.Add(this.setGPU);
            this.Controls.Add(this.diffMnemonic);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.coinSymbol2);
            this.Controls.Add(this.coinSymbol1);
            this.Controls.Add(this.difficulty);
            this.Controls.Add(this.rewInUsd);
            this.Controls.Add(this.rewInCoins);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.wantReward);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.coinIcon);
            this.MaximumSize = new System.Drawing.Size(600, 106);
            this.MinimumSize = new System.Drawing.Size(600, 106);
            this.Name = "CoinPanel";
            this.Size = new System.Drawing.Size(600, 106);
            ((System.ComponentModel.ISupportInitialize)(this.coinIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CPUbat;
        private System.Windows.Forms.Label GPUbat;
        private System.Windows.Forms.Button setCPU;
        private System.Windows.Forms.Button setGPU;
        private System.Windows.Forms.Label diffMnemonic;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label coinSymbol2;
        private System.Windows.Forms.Label coinSymbol1;
        private System.Windows.Forms.Label difficulty;
        private System.Windows.Forms.Label rewInUsd;
        private System.Windows.Forms.Label rewInCoins;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox wantReward;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox coinIcon;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
