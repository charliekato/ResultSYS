namespace ShowLaneOrder
{
    partial class frmsetup
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
            this.lblLapAliveTime = new System.Windows.Forms.Label();
            this.txtboxTimetoErase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblms1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtboxTimetoNext = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLapAliveTime
            // 
            this.lblLapAliveTime.AutoSize = true;
            this.lblLapAliveTime.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLapAliveTime.Location = new System.Drawing.Point(100, 109);
            this.lblLapAliveTime.Name = "lblLapAliveTime";
            this.lblLapAliveTime.Size = new System.Drawing.Size(401, 33);
            this.lblLapAliveTime.TabIndex = 0;
            this.lblLapAliveTime.Text = "ラップタイムを表示している時間";
            // 
            // txtboxTimetoErase
            // 
            this.txtboxTimetoErase.Location = new System.Drawing.Point(854, 109);
            this.txtboxTimetoErase.Name = "txtboxTimetoErase";
            this.txtboxTimetoErase.Size = new System.Drawing.Size(110, 31);
            this.txtboxTimetoErase.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(100, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(588, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "全員ゴールしてから次のレースを表示する時間";
            // 
            // lblms1
            // 
            this.lblms1.AutoSize = true;
            this.lblms1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblms1.Location = new System.Drawing.Point(978, 105);
            this.lblms1.Name = "lblms1";
            this.lblms1.Size = new System.Drawing.Size(47, 33);
            this.lblms1.TabIndex = 4;
            this.lblms1.Text = "秒";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(978, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 33);
            this.label1.TabIndex = 5;
            this.label1.Text = "秒";
            // 
            // txtboxTimetoNext
            // 
            this.txtboxTimetoNext.Location = new System.Drawing.Point(854, 192);
            this.txtboxTimetoNext.Name = "txtboxTimetoNext";
            this.txtboxTimetoNext.Size = new System.Drawing.Size(110, 31);
            this.txtboxTimetoNext.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(496, 274);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 60);
            this.button1.TabIndex = 7;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmsetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 416);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtboxTimetoNext);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblms1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtboxTimetoErase);
            this.Controls.Add(this.lblLapAliveTime);
            this.Name = "frmsetup";
            this.Text = "設定";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLapAliveTime;
        private System.Windows.Forms.TextBox txtboxTimetoErase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblms1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtboxTimetoNext;
        private System.Windows.Forms.Button button1;
    }
}