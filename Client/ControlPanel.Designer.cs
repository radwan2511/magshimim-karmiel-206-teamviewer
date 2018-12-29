namespace TeamViewer___Client
{
    partial class ControlPanel
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
            this.Xlabel = new System.Windows.Forms.Label();
            this.move = new System.Windows.Forms.Button();
            this.XNumbers = new System.Windows.Forms.TextBox();
            this.YLabel = new System.Windows.Forms.Label();
            this.YNumbers = new System.Windows.Forms.TextBox();
            this.clickbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Xlabel
            // 
            this.Xlabel.AutoSize = true;
            this.Xlabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.Xlabel.Location = new System.Drawing.Point(164, 34);
            this.Xlabel.Name = "Xlabel";
            this.Xlabel.Size = new System.Drawing.Size(43, 24);
            this.Xlabel.TabIndex = 0;
            this.Xlabel.Text = "X : ";
            // 
            // move
            // 
            this.move.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.move.Location = new System.Drawing.Point(270, 97);
            this.move.Name = "move";
            this.move.Size = new System.Drawing.Size(121, 49);
            this.move.TabIndex = 1;
            this.move.Text = "Move";
            this.move.UseVisualStyleBackColor = true;
            this.move.Click += new System.EventHandler(this.move_Click);
            // 
            // XNumbers
            // 
            this.XNumbers.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.XNumbers.Location = new System.Drawing.Point(199, 31);
            this.XNumbers.Name = "XNumbers";
            this.XNumbers.Size = new System.Drawing.Size(133, 32);
            this.XNumbers.TabIndex = 2;
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.YLabel.Location = new System.Drawing.Point(427, 34);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(42, 24);
            this.YLabel.TabIndex = 3;
            this.YLabel.Text = "Y : ";
            // 
            // YNumbers
            // 
            this.YNumbers.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.YNumbers.Location = new System.Drawing.Point(476, 31);
            this.YNumbers.Name = "YNumbers";
            this.YNumbers.Size = new System.Drawing.Size(133, 32);
            this.YNumbers.TabIndex = 4;
            // 
            // clickbutton
            // 
            this.clickbutton.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.clickbutton.Location = new System.Drawing.Point(410, 97);
            this.clickbutton.Name = "clickbutton";
            this.clickbutton.Size = new System.Drawing.Size(121, 49);
            this.clickbutton.TabIndex = 5;
            this.clickbutton.Text = "Click";
            this.clickbutton.UseVisualStyleBackColor = true;
            this.clickbutton.Click += new System.EventHandler(this.clickbutton_Click);
            // 
            // DesktopRemoteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.clickbutton);
            this.Controls.Add(this.YNumbers);
            this.Controls.Add(this.YLabel);
            this.Controls.Add(this.XNumbers);
            this.Controls.Add(this.move);
            this.Controls.Add(this.Xlabel);
            this.Name = "DesktopRemoteControl";
            this.Text = "DesktopRemoteControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Xlabel;
        private System.Windows.Forms.Button move;
        private System.Windows.Forms.TextBox XNumbers;
        private System.Windows.Forms.Label YLabel;
        private System.Windows.Forms.TextBox YNumbers;
        private System.Windows.Forms.Button clickbutton;
    }
}