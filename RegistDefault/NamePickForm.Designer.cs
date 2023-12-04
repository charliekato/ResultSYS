namespace RegistDefault
{
    partial class NamePickForm
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
            this.txtBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtBoxTeam = new System.Windows.Forms.TextBox();
            this.btnTeamSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbxName = new System.Windows.Forms.ListBox();
            this.btnNameSelect = new System.Windows.Forms.Button();
            this.lblIndivisual = new System.Windows.Forms.Label();
            this.lblTeam = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtBoxName
            // 
            this.txtBoxName.Location = new System.Drawing.Point(87, 26);
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.Size = new System.Drawing.Size(215, 19);
            this.txtBoxName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(22, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(353, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "棄権する人の氏名の一部を入力し「検索」ボタンを押す";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(327, 18);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(48, 32);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtBoxTeam
            // 
            this.txtBoxTeam.Location = new System.Drawing.Point(87, 128);
            this.txtBoxTeam.Name = "txtBoxTeam";
            this.txtBoxTeam.Size = new System.Drawing.Size(215, 19);
            this.txtBoxTeam.TabIndex = 3;
            // 
            // btnTeamSearch
            // 
            this.btnTeamSearch.Location = new System.Drawing.Point(327, 121);
            this.btnTeamSearch.Name = "btnTeamSearch";
            this.btnTeamSearch.Size = new System.Drawing.Size(48, 32);
            this.btnTeamSearch.TabIndex = 4;
            this.btnTeamSearch.Text = "検索";
            this.btnTeamSearch.UseVisualStyleBackColor = true;
            this.btnTeamSearch.Click += new System.EventHandler(this.btnTeamSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(22, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(347, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "棄権するチーム名の一部を入力し「検索」ボタンを押す";
            // 
            // lbxName
            // 
            this.lbxName.Font = new System.Drawing.Font("ＭＳ ゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbxName.FormattingEnabled = true;
            this.lbxName.HorizontalScrollbar = true;
            this.lbxName.Location = new System.Drawing.Point(41, 197);
            this.lbxName.MultiColumn = true;
            this.lbxName.Name = "lbxName";
            this.lbxName.Size = new System.Drawing.Size(261, 329);
            this.lbxName.TabIndex = 6;
            // 
            // btnNameSelect
            // 
            this.btnNameSelect.Location = new System.Drawing.Point(327, 486);
            this.btnNameSelect.Name = "btnNameSelect";
            this.btnNameSelect.Size = new System.Drawing.Size(48, 40);
            this.btnNameSelect.TabIndex = 7;
            this.btnNameSelect.Text = "選択";
            this.btnNameSelect.UseVisualStyleBackColor = true;
            this.btnNameSelect.Click += new System.EventHandler(this.btnNameSelect_Click);
            // 
            // lblIndivisual
            // 
            this.lblIndivisual.AutoSize = true;
            this.lblIndivisual.Location = new System.Drawing.Point(26, 6);
            this.lblIndivisual.Name = "lblIndivisual";
            this.lblIndivisual.Size = new System.Drawing.Size(64, 12);
            this.lblIndivisual.TabIndex = 8;
            this.lblIndivisual.Text = "個人はこちら";
            // 
            // lblTeam
            // 
            this.lblTeam.AutoSize = true;
            this.lblTeam.Location = new System.Drawing.Point(26, 113);
            this.lblTeam.Name = "lblTeam";
            this.lblTeam.Size = new System.Drawing.Size(95, 12);
            this.lblTeam.TabIndex = 9;
            this.lblTeam.Text = "リレーチームはこちら";
            // 
            // NamePickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 563);
            this.Controls.Add(this.lblTeam);
            this.Controls.Add(this.lblIndivisual);
            this.Controls.Add(this.btnNameSelect);
            this.Controls.Add(this.lbxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnTeamSearch);
            this.Controls.Add(this.txtBoxTeam);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxName);
            this.Name = "NamePickForm";
            this.Text = "名前の検索";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtBoxTeam;
        private System.Windows.Forms.Button btnTeamSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxName;
        private System.Windows.Forms.Button btnNameSelect;
        private System.Windows.Forms.Label lblIndivisual;
        private System.Windows.Forms.Label lblTeam;
    }
}