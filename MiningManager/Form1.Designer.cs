namespace MiningManager
{
    partial class MiningManager
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.mainPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.apply = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.coinStatusPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // mainPanel
            // 
            this.mainPanel.AutoScroll = true;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(623, 420);
            this.mainPanel.TabIndex = 0;
            // 
            // apply
            // 
            this.apply.Enabled = false;
            this.apply.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.apply.ForeColor = System.Drawing.Color.Green;
            this.apply.Location = new System.Drawing.Point(523, 424);
            this.apply.Margin = new System.Windows.Forms.Padding(0);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(100, 21);
            this.apply.TabIndex = 0;
            this.apply.Text = "Apply Config";
            this.apply.UseVisualStyleBackColor = true;
            this.apply.Click += new System.EventHandler(this.apply_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // coinStatusPanel
            // 
            this.coinStatusPanel.Location = new System.Drawing.Point(0, 421);
            this.coinStatusPanel.Name = "coinStatusPanel";
            this.coinStatusPanel.Size = new System.Drawing.Size(240, 24);
            this.coinStatusPanel.TabIndex = 1;
            // 
            // MiningManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 446);
            this.Controls.Add(this.coinStatusPanel);
            this.Controls.Add(this.apply);
            this.Controls.Add(this.mainPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximumSize = new System.Drawing.Size(640, 960);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MiningManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mining Manager";
            this.Load += new System.EventHandler(this.MiningManager_Load);
            this.Shown += new System.EventHandler(this.MiningManager_Shown);
            this.Resize += new System.EventHandler(this.MiningManager_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.FlowLayoutPanel mainPanel;
        private System.Windows.Forms.Button apply;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.FlowLayoutPanel coinStatusPanel;
    }
}

