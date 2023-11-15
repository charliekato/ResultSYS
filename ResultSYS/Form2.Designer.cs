namespace ResultSys
{
    partial class Form2
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

        //#region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnQuit = new System.Windows.Forms.Button();
            this.lblRaceName0 = new System.Windows.Forms.Label();
            this.tbxPrgNo = new System.Windows.Forms.TextBox();
            this.tbxKumi = new System.Windows.Forms.TextBox();
            this.lblPrgNo = new System.Windows.Forms.Label();
            this.lblKumi = new System.Windows.Forms.Label();
            this.lblHyphen = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnShowPrev = new System.Windows.Forms.Button();
            this.btnShowNext = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblLapInterval = new System.Windows.Forms.Label();
            this.lbl2xpoolLength = new System.Windows.Forms.Label();
            this.cbxMonitorEnable = new System.Windows.Forms.CheckBox();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.lblPending = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(2348, -2);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(100, 75);
            this.btnQuit.TabIndex = 1;
            this.btnQuit.Text = "終了";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // lblRaceName0
            // 
            this.lblRaceName0.AutoSize = true;
            this.lblRaceName0.BackColor = System.Drawing.SystemColors.Control;
            this.lblRaceName0.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblRaceName0.Location = new System.Drawing.Point(669, 33);
            this.lblRaceName0.Name = "lblRaceName0";
            this.lblRaceName0.Size = new System.Drawing.Size(134, 27);
            this.lblRaceName0.TabIndex = 15;
            this.lblRaceName0.Text = "RaceName";
            // 
            // tbxPrgNo
            // 
            this.tbxPrgNo.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbxPrgNo.Location = new System.Drawing.Point(27, 51);
            this.tbxPrgNo.Name = "tbxPrgNo";
            this.tbxPrgNo.Size = new System.Drawing.Size(85, 23);
            this.tbxPrgNo.TabIndex = 16;
            // 
            // tbxKumi
            // 
            this.tbxKumi.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbxKumi.Location = new System.Drawing.Point(150, 51);
            this.tbxKumi.Name = "tbxKumi";
            this.tbxKumi.Size = new System.Drawing.Size(51, 23);
            this.tbxKumi.TabIndex = 17;
            this.tbxKumi.TextChanged += new System.EventHandler(this.tbxKumi_TextChanged);
            // 
            // lblPrgNo
            // 
            this.lblPrgNo.AutoSize = true;
            this.lblPrgNo.Location = new System.Drawing.Point(47, 21);
            this.lblPrgNo.Name = "lblPrgNo";
            this.lblPrgNo.Size = new System.Drawing.Size(19, 12);
            this.lblPrgNo.TabIndex = 18;
            this.lblPrgNo.Text = "No";
            // 
            // lblKumi
            // 
            this.lblKumi.AutoSize = true;
            this.lblKumi.Location = new System.Drawing.Point(152, 21);
            this.lblKumi.Name = "lblKumi";
            this.lblKumi.Size = new System.Drawing.Size(17, 12);
            this.lblKumi.TabIndex = 19;
            this.lblKumi.Text = "組";
            // 
            // lblHyphen
            // 
            this.lblHyphen.AutoSize = true;
            this.lblHyphen.Location = new System.Drawing.Point(118, 62);
            this.lblHyphen.Name = "lblHyphen";
            this.lblHyphen.Size = new System.Drawing.Size(11, 12);
            this.lblHyphen.TabIndex = 20;
            this.lblHyphen.Text = "-";
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(225, 38);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(86, 59);
            this.btnShow.TabIndex = 21;
            this.btnShow.Text = "表示";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_click);
            // 
            // btnShowPrev
            // 
            this.btnShowPrev.Location = new System.Drawing.Point(338, 41);
            this.btnShowPrev.Name = "btnShowPrev";
            this.btnShowPrev.Size = new System.Drawing.Size(66, 53);
            this.btnShowPrev.TabIndex = 22;
            this.btnShowPrev.Text = "<";
            this.btnShowPrev.UseVisualStyleBackColor = true;
            this.btnShowPrev.Click += new System.EventHandler(this.btnShowPrev_Click);
            // 
            // btnShowNext
            // 
            this.btnShowNext.Location = new System.Drawing.Point(430, 42);
            this.btnShowNext.Name = "btnShowNext";
            this.btnShowNext.Size = new System.Drawing.Size(61, 52);
            this.btnShowNext.TabIndex = 23;
            this.btnShowNext.Text = ">";
            this.btnShowNext.UseVisualStyleBackColor = true;
            this.btnShowNext.Click += new System.EventHandler(this.btnShowNext_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(0, 0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 31;
            // 
            // lblLapInterval
            // 
            this.lblLapInterval.AutoSize = true;
            this.lblLapInterval.Location = new System.Drawing.Point(1894, 25);
            this.lblLapInterval.Name = "lblLapInterval";
            this.lblLapInterval.Size = new System.Drawing.Size(62, 12);
            this.lblLapInterval.TabIndex = 25;
            this.lblLapInterval.Text = "lap interval";
            // 
            // lbl2xpoolLength
            // 
            this.lbl2xpoolLength.AutoSize = true;
            this.lbl2xpoolLength.Location = new System.Drawing.Point(2069, 25);
            this.lbl2xpoolLength.Name = "lbl2xpoolLength";
            this.lbl2xpoolLength.Size = new System.Drawing.Size(26, 12);
            this.lbl2xpoolLength.TabIndex = 26;
            this.lbl2xpoolLength.Text = "50m";
            // 
            // cbxMonitorEnable
            // 
            this.cbxMonitorEnable.AutoSize = true;
            this.cbxMonitorEnable.Location = new System.Drawing.Point(1661, 22);
            this.cbxMonitorEnable.Name = "cbxMonitorEnable";
            this.cbxMonitorEnable.Size = new System.Drawing.Size(91, 16);
            this.cbxMonitorEnable.TabIndex = 27;
            this.cbxMonitorEnable.Text = "結果取り込み";
            this.cbxMonitorEnable.UseVisualStyleBackColor = true;
            // 
            // lblPending
            // 
            this.lblPending.AutoSize = true;
            this.lblPending.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPending.ForeColor = System.Drawing.Color.Red;
            this.lblPending.Location = new System.Drawing.Point(1494, 11);
            this.lblPending.Name = "lblPending";
            this.lblPending.Size = new System.Drawing.Size(66, 19);
            this.lblPending.TabIndex = 30;
            this.lblPending.Text = "中断中";
            // 
            // Form2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1372, 749);
            this.Controls.Add(this.lblPending);
            this.Controls.Add(this.cbxMonitorEnable);
            this.Controls.Add(this.lbl2xpoolLength);
            this.Controls.Add(this.lblLapInterval);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnShowNext);
            this.Controls.Add(this.btnShowPrev);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.lblHyphen);
            this.Controls.Add(this.lblKumi);
            this.Controls.Add(this.lblPrgNo);
            this.Controls.Add(this.tbxKumi);
            this.Controls.Add(this.tbxPrgNo);
            this.Controls.Add(this.lblRaceName0);
            this.Controls.Add(this.btnQuit);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //#endregion
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.TextBox tbxPrgNo;
        private System.Windows.Forms.TextBox tbxKumi;
        private System.Windows.Forms.Label lblPrgNo;
        private System.Windows.Forms.Label lblKumi;
        private System.Windows.Forms.Label lblHyphen;
        private System.Windows.Forms.Label lblRaceName0;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Button btnShowPrev;
        private System.Windows.Forms.Button btnShowNext;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblLapInterval;
        private System.Windows.Forms.Label lbl2xpoolLength;
        private System.Windows.Forms.CheckBox cbxMonitorEnable;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Label lblPending;
    }
    //////////        
}