namespace RegistDefault
{
    partial class GameSelecter
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxFolder = new System.Windows.Forms.TextBox();
            this.btnFolderSelect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbxDbContents = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnQuit = new System.Windows.Forms.Button();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSelect.Location = new System.Drawing.Point(454, 96);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(109, 38);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "大会選択";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(230, 118);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 27);
            this.label1.TabIndex = 1;
            // 
            // txtBxFolder
            // 
            this.txtBxFolder.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBxFolder.Location = new System.Drawing.Point(157, 39);
            this.txtBxFolder.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.txtBxFolder.Name = "txtBxFolder";
            this.txtBxFolder.Size = new System.Drawing.Size(616, 23);
            this.txtBxFolder.TabIndex = 2;
            this.txtBxFolder.TextChanged += new System.EventHandler(this.txtBxFolder_TextChanged);
            // 
            // btnFolderSelect
            // 
            this.btnFolderSelect.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFolderSelect.Location = new System.Drawing.Point(189, 96);
            this.btnFolderSelect.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnFolderSelect.Name = "btnFolderSelect";
            this.btnFolderSelect.Size = new System.Drawing.Size(121, 38);
            this.btnFolderSelect.TabIndex = 3;
            this.btnFolderSelect.Text = "パス設定";
            this.btnFolderSelect.UseVisualStyleBackColor = true;
            this.btnFolderSelect.Click += new System.EventHandler(this.btnFolderSelect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "データベースパス";
            // 
            // lbxDbContents
            // 
            this.lbxDbContents.Font = new System.Drawing.Font("ＭＳ ゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbxDbContents.FormattingEnabled = true;
            this.lbxDbContents.Location = new System.Drawing.Point(5, 148);
            this.lbxDbContents.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.lbxDbContents.Name = "lbxDbContents";
            this.lbxDbContents.Size = new System.Drawing.Size(902, 368);
            this.lbxDbContents.TabIndex = 6;
            // 
            // btnQuit
            // 
            this.btnQuit.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnQuit.Location = new System.Drawing.Point(705, 96);
            this.btnQuit.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(68, 38);
            this.btnQuit.TabIndex = 8;
            this.btnQuit.Text = "終了";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // EventSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 374);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.lbxDbContents);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFolderSelect);
            this.Controls.Add(this.txtBxFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelect);
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "EventSelectForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxFolder;
        private System.Windows.Forms.Button btnFolderSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxDbContents;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.ToolTip toolTip2;
    }
}

