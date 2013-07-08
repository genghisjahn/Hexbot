namespace HexBot
{
    partial class frmMain
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
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnPaint = new System.Windows.Forms.Button();
            this.pboxMain = new System.Windows.Forms.PictureBox();
            this.btnGoBot = new System.Windows.Forms.Button();
            this.timerBot = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pboxMain)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(698, 12);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(255, 300);
            this.txtLog.TabIndex = 0;
            // 
            // btnPaint
            // 
            this.btnPaint.Location = new System.Drawing.Point(698, 318);
            this.btnPaint.Name = "btnPaint";
            this.btnPaint.Size = new System.Drawing.Size(75, 23);
            this.btnPaint.TabIndex = 1;
            this.btnPaint.Text = "Paint";
            this.btnPaint.UseVisualStyleBackColor = true;
            // 
            // pboxMain
            // 
            this.pboxMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pboxMain.Location = new System.Drawing.Point(21, 0);
            this.pboxMain.Name = "pboxMain";
            this.pboxMain.Size = new System.Drawing.Size(671, 730);
            this.pboxMain.TabIndex = 2;
            this.pboxMain.TabStop = false;
            this.pboxMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pboxMain_Paint);
            
            // 
            // btnGoBot
            // 
            this.btnGoBot.Location = new System.Drawing.Point(698, 347);
            this.btnGoBot.Name = "btnGoBot";
            this.btnGoBot.Size = new System.Drawing.Size(75, 23);
            this.btnGoBot.TabIndex = 3;
            this.btnGoBot.Text = "Go Robot!";
            this.btnGoBot.UseVisualStyleBackColor = true;
            this.btnGoBot.Click += new System.EventHandler(this.btnGoBot_Click);
            // 
            // timerBot
            // 
            this.timerBot.Interval = 1000;
            this.timerBot.Tick += new System.EventHandler(this.timerBot_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 737);
            this.Controls.Add(this.btnGoBot);
            this.Controls.Add(this.pboxMain);
            this.Controls.Add(this.btnPaint);
            this.Controls.Add(this.txtLog);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HexBot";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pboxMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnPaint;
        private System.Windows.Forms.PictureBox pboxMain;
        private System.Windows.Forms.Button btnGoBot;
        private System.Windows.Forms.Timer timerBot;
    }
}

