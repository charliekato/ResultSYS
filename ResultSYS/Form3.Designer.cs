﻿namespace ResultSys
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
            this.btnOK = new System.Windows.Forms.Button();
            this.cmbBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblLapAliveTime
            // 
            this.lblLapAliveTime.AutoSize = true;
            this.lblLapAliveTime.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLapAliveTime.Location = new System.Drawing.Point(46, 54);
            this.lblLapAliveTime.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblLapAliveTime.Name = "lblLapAliveTime";
            this.lblLapAliveTime.Size = new System.Drawing.Size(201, 16);
            this.lblLapAliveTime.TabIndex = 0;
            this.lblLapAliveTime.Text = "ラップタイムを表示している時間";
            // 
            // txtboxTimetoErase
            // 
            this.txtboxTimetoErase.Location = new System.Drawing.Point(394, 54);
            this.txtboxTimetoErase.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtboxTimetoErase.Name = "txtboxTimetoErase";
            this.txtboxTimetoErase.Size = new System.Drawing.Size(53, 19);
            this.txtboxTimetoErase.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(46, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(294, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "全員ゴールしてから次のレースを表示する時間";
            // 
            // lblms1
            // 
            this.lblms1.AutoSize = true;
            this.lblms1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblms1.Location = new System.Drawing.Point(451, 52);
            this.lblms1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.lblms1.Name = "lblms1";
            this.lblms1.Size = new System.Drawing.Size(23, 16);
            this.lblms1.TabIndex = 4;
            this.lblms1.Text = "秒";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(451, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "秒";
            // 
            // txtboxTimetoNext
            // 
            this.txtboxTimetoNext.Location = new System.Drawing.Point(394, 96);
            this.txtboxTimetoNext.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtboxTimetoNext.Name = "txtboxTimetoNext";
            this.txtboxTimetoNext.Size = new System.Drawing.Size(53, 19);
            this.txtboxTimetoNext.TabIndex = 6;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(224, 192);
            this.btnOK.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64, 30);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbBox
            // 
            this.cmbBox.FormattingEnabled = true;
            this.cmbBox.Location = new System.Drawing.Point(224, 129);
            this.cmbBox.Name = "cmbBox";
            this.cmbBox.Size = new System.Drawing.Size(80, 20);
            this.cmbBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(46, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "シリアルポート　番号";
            // 
            // frmsetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 259);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbBox);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtboxTimetoNext);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblms1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtboxTimetoErase);
            this.Controls.Add(this.lblLapAliveTime);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
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
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cmbBox;
        private System.Windows.Forms.Label label3;
    }
}