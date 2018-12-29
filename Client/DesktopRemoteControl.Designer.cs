namespace TeamViewer___Client
{
    partial class DesktopRemoteControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesktopRemoteControl));
            this.rdp = new AxMSTSCLib.AxMsTscAxNotSafeForScripting();
            this.connectButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.XLabel = new System.Windows.Forms.Label();
            this.YLabel = new System.Windows.Forms.Label();
            this.YNumbers = new System.Windows.Forms.TextBox();
            this.XNumbers = new System.Windows.Forms.TextBox();
            this.clickbutton = new System.Windows.Forms.Button();
            this.move = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rdp)).BeginInit();
            this.SuspendLayout();
            // 
            // rdp
            // 
            this.rdp.Enabled = true;
            this.rdp.Location = new System.Drawing.Point(18, 77);
            this.rdp.Name = "rdp";
            this.rdp.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rdp.OcxState")));
            this.rdp.Size = new System.Drawing.Size(764, 335);
            this.rdp.TabIndex = 1;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(190, 428);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(174, 42);
            this.connectButton.TabIndex = 2;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(391, 428);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(174, 42);
            this.disconnectButton.TabIndex = 3;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // XLabel
            // 
            this.XLabel.AutoSize = true;
            this.XLabel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XLabel.Location = new System.Drawing.Point(186, 29);
            this.XLabel.Name = "XLabel";
            this.XLabel.Size = new System.Drawing.Size(36, 23);
            this.XLabel.TabIndex = 4;
            this.XLabel.Text = "X :";
            // 
            // YLabel
            // 
            this.YLabel.AutoSize = true;
            this.YLabel.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YLabel.Location = new System.Drawing.Point(387, 29);
            this.YLabel.Name = "YLabel";
            this.YLabel.Size = new System.Drawing.Size(34, 23);
            this.YLabel.TabIndex = 6;
            this.YLabel.Text = "Y :";
            // 
            // YNumbers
            // 
            this.YNumbers.Location = new System.Drawing.Point(428, 31);
            this.YNumbers.Name = "YNumbers";
            this.YNumbers.Size = new System.Drawing.Size(100, 20);
            this.YNumbers.TabIndex = 7;
            // 
            // XNumbers
            // 
            this.XNumbers.Location = new System.Drawing.Point(228, 33);
            this.XNumbers.Name = "XNumbers";
            this.XNumbers.Size = new System.Drawing.Size(100, 20);
            this.XNumbers.TabIndex = 8;
            // 
            // clickbutton
            // 
            this.clickbutton.Location = new System.Drawing.Point(698, 31);
            this.clickbutton.Name = "clickbutton";
            this.clickbutton.Size = new System.Drawing.Size(75, 23);
            this.clickbutton.TabIndex = 10;
            this.clickbutton.Text = "Click";
            this.clickbutton.UseVisualStyleBackColor = true;
            this.clickbutton.Click += new System.EventHandler(this.clickbutton_Click);
            // 
            // move
            // 
            this.move.Location = new System.Drawing.Point(594, 31);
            this.move.Name = "move";
            this.move.Size = new System.Drawing.Size(75, 23);
            this.move.TabIndex = 11;
            this.move.Text = "Move";
            this.move.UseVisualStyleBackColor = true;
            this.move.Click += new System.EventHandler(this.move_Click);
            // 
            // DesktopRemoteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 482);
            this.Controls.Add(this.move);
            this.Controls.Add(this.clickbutton);
            this.Controls.Add(this.XNumbers);
            this.Controls.Add(this.YNumbers);
            this.Controls.Add(this.YLabel);
            this.Controls.Add(this.XLabel);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.rdp);
            this.Name = "DesktopRemoteControl";
            this.Text = "DesktopRemoteControl";
            ((System.ComponentModel.ISupportInitialize)(this.rdp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxMSTSCLib.AxMsTscAxNotSafeForScripting rdp;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Label XLabel;
        private System.Windows.Forms.Label YLabel;
        private System.Windows.Forms.TextBox YNumbers;
        private System.Windows.Forms.TextBox XNumbers;
        private System.Windows.Forms.Button clickbutton;
        private System.Windows.Forms.Button move;
    }
}