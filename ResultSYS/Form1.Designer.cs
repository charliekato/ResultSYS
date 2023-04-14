namespace ResultSys
{
    partial class Form1
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
            this.btnShowLaneOrder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBxFolder = new System.Windows.Forms.TextBox();
            this.btnFolderSelect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lbxDbContents = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnQuit = new System.Windows.Forms.Button();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSetup = new System.Windows.Forms.Button();
            this.btnInitDB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnShowLaneOrder
            // 
            this.btnShowLaneOrder.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnShowLaneOrder.Location = new System.Drawing.Point(983, 191);
            this.btnShowLaneOrder.Name = "btnShowLaneOrder";
            this.btnShowLaneOrder.Size = new System.Drawing.Size(236, 76);
            this.btnShowLaneOrder.TabIndex = 0;
            this.btnShowLaneOrder.Text = "レーン順表示";
            this.btnShowLaneOrder.UseVisualStyleBackColor = true;
            this.btnShowLaneOrder.Click += new System.EventHandler(this.btnShowLaneOrder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(499, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 54);
            this.label1.TabIndex = 1;
            // 
            // txtBxFolder
            // 
            this.txtBxFolder.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBxFolder.Location = new System.Drawing.Point(341, 78);
            this.txtBxFolder.Name = "txtBxFolder";
            this.txtBxFolder.Size = new System.Drawing.Size(1082, 39);
            this.txtBxFolder.TabIndex = 2;
            this.txtBxFolder.TextChanged += new System.EventHandler(this.txtBxFolder_TextChanged);
            // 
            // btnFolderSelect
            // 
            this.btnFolderSelect.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnFolderSelect.Location = new System.Drawing.Point(410, 191);
            this.btnFolderSelect.Name = "btnFolderSelect";
            this.btnFolderSelect.Size = new System.Drawing.Size(263, 76);
            this.btnFolderSelect.TabIndex = 3;
            this.btnFolderSelect.Text = "パス設定";
            this.btnFolderSelect.UseVisualStyleBackColor = true;
            this.btnFolderSelect.Click += new System.EventHandler(this.btnFolderSelect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(27, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 38);
            this.label2.TabIndex = 4;
            this.label2.Text = "データベースパス";
            // 
            // lbxDbContents
            // 
            this.lbxDbContents.Font = new System.Drawing.Font("ＭＳ ゴシック", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbxDbContents.FormattingEnabled = true;
            this.lbxDbContents.ItemHeight = 27;
            this.lbxDbContents.Location = new System.Drawing.Point(11, 297);
            this.lbxDbContents.Name = "lbxDbContents";
            this.lbxDbContents.Size = new System.Drawing.Size(1950, 733);
            this.lbxDbContents.TabIndex = 6;
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(1797, 44);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(112, 54);
            this.btnQuit.TabIndex = 8;
            this.btnQuit.Text = "終了";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnSetup
            // 
            this.btnSetup.Location = new System.Drawing.Point(1797, 182);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(112, 46);
            this.btnSetup.TabIndex = 9;
            this.btnSetup.Text = "設定";
            this.btnSetup.UseVisualStyleBackColor = true;
            this.btnSetup.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnInitDB
            // 
            this.btnInitDB.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnInitDB.Location = new System.Drawing.Point(1515, 196);
            this.btnInitDB.Name = "btnInitDB";
            this.btnInitDB.Size = new System.Drawing.Size(156, 66);
            this.btnInitDB.TabIndex = 10;
            this.btnInitDB.Text = "InitDB";
            this.btnInitDB.UseVisualStyleBackColor = true;
            this.btnInitDB.Click += new System.EventHandler(this.btnInitDB_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1965, 1062);
            this.Controls.Add(this.btnInitDB);
            this.Controls.Add(this.btnSetup);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.lbxDbContents);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFolderSelect);
            this.Controls.Add(this.txtBxFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnShowLaneOrder);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnShowLaneOrder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBxFolder;
        private System.Windows.Forms.Button btnFolderSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxDbContents;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Button btnSetup;
        private System.Windows.Forms.Button btnInitDB;
    }
}

