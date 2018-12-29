namespace TeamViewer___Client
{
    partial class MainScreen
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
            this.SignOutButton = new System.Windows.Forms.Button();
            this.ControlScreenButton = new System.Windows.Forms.Button();
            this.GroupFileShareButton = new System.Windows.Forms.Button();
            this.FileShareButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SignOutButton
            // 
            this.SignOutButton.BackColor = System.Drawing.Color.White;
            this.SignOutButton.Font = new System.Drawing.Font("Niagara Engraved", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignOutButton.ForeColor = System.Drawing.Color.Black;
            this.SignOutButton.Location = new System.Drawing.Point(416, 313);
            this.SignOutButton.Name = "SignOutButton";
            this.SignOutButton.Size = new System.Drawing.Size(204, 58);
            this.SignOutButton.TabIndex = 9;
            this.SignOutButton.Text = "Sign Out";
            this.SignOutButton.UseVisualStyleBackColor = false;
            // 
            // ControlScreenButton
            // 
            this.ControlScreenButton.BackColor = System.Drawing.Color.White;
            this.ControlScreenButton.Font = new System.Drawing.Font("Niagara Engraved", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ControlScreenButton.ForeColor = System.Drawing.Color.Black;
            this.ControlScreenButton.Location = new System.Drawing.Point(416, 228);
            this.ControlScreenButton.Name = "ControlScreenButton";
            this.ControlScreenButton.Size = new System.Drawing.Size(204, 58);
            this.ControlScreenButton.TabIndex = 10;
            this.ControlScreenButton.Text = "Control Screen";
            this.ControlScreenButton.UseVisualStyleBackColor = false;
            this.ControlScreenButton.Click += new System.EventHandler(this.ControlScreenButton_Click);
            // 
            // GroupFileShareButton
            // 
            this.GroupFileShareButton.BackColor = System.Drawing.Color.White;
            this.GroupFileShareButton.Font = new System.Drawing.Font("Niagara Engraved", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupFileShareButton.ForeColor = System.Drawing.Color.Black;
            this.GroupFileShareButton.Location = new System.Drawing.Point(173, 313);
            this.GroupFileShareButton.Name = "GroupFileShareButton";
            this.GroupFileShareButton.Size = new System.Drawing.Size(204, 58);
            this.GroupFileShareButton.TabIndex = 11;
            this.GroupFileShareButton.Text = "Group File Share";
            this.GroupFileShareButton.UseVisualStyleBackColor = false;
            // 
            // FileShareButton
            // 
            this.FileShareButton.BackColor = System.Drawing.Color.White;
            this.FileShareButton.Font = new System.Drawing.Font("Niagara Engraved", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileShareButton.ForeColor = System.Drawing.Color.Black;
            this.FileShareButton.Location = new System.Drawing.Point(173, 228);
            this.FileShareButton.Name = "FileShareButton";
            this.FileShareButton.Size = new System.Drawing.Size(204, 58);
            this.FileShareButton.TabIndex = 12;
            this.FileShareButton.Text = "File Share";
            this.FileShareButton.UseVisualStyleBackColor = false;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FileShareButton);
            this.Controls.Add(this.GroupFileShareButton);
            this.Controls.Add(this.ControlScreenButton);
            this.Controls.Add(this.SignOutButton);
            this.Name = "MainScreen";
            this.Text = "MainScreen";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SignOutButton;
        private System.Windows.Forms.Button ControlScreenButton;
        private System.Windows.Forms.Button GroupFileShareButton;
        private System.Windows.Forms.Button FileShareButton;
    }
}

