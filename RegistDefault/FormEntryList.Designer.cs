namespace RegistDefault
{
    partial class FormEntryList
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
            this.lbxRaceList = new System.Windows.Forms.ListBox();
            this.btnSetDefault = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbxRaceList
            // 
            this.lbxRaceList.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbxRaceList.FormattingEnabled = true;
            this.lbxRaceList.ItemHeight = 16;
            this.lbxRaceList.Location = new System.Drawing.Point(22, 35);
            this.lbxRaceList.Name = "lbxRaceList";
            this.lbxRaceList.Size = new System.Drawing.Size(311, 148);
            this.lbxRaceList.TabIndex = 0;
            // 
            // btnSetDefault
            // 
            this.btnSetDefault.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSetDefault.Location = new System.Drawing.Point(260, 194);
            this.btnSetDefault.Name = "btnSetDefault";
            this.btnSetDefault.Size = new System.Drawing.Size(57, 32);
            this.btnSetDefault.TabIndex = 1;
            this.btnSetDefault.Text = "棄権";
            this.btnSetDefault.UseVisualStyleBackColor = true;
            // 
            // FormEntryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 248);
            this.Controls.Add(this.btnSetDefault);
            this.Controls.Add(this.lbxRaceList);
            this.Name = "FormEntryList";
            this.Text = "棄権するレースの選択";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxRaceList;
        private System.Windows.Forms.Button btnSetDefault;
    }
}