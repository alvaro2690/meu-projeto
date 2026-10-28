namespace travelgest
{
    partial class Login
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.email_login = new System.Windows.Forms.TextBox();
            this.senha_login = new System.Windows.Forms.TextBox();
            this.conta_login = new System.Windows.Forms.Label();
            this.entrar_login = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(414, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 51);
            this.label1.TabIndex = 2;
            this.label1.Text = "Travel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(548, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 51);
            this.label2.TabIndex = 3;
            this.label2.Text = "Gest";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(375, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(340, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sistema de Gestão de viagens";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkBlue;
            this.label4.Location = new System.Drawing.Point(436, 313);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Acesso ao sistema\r\n";
            // 
            // email_login
            // 
            this.email_login.Location = new System.Drawing.Point(380, 378);
            this.email_login.Multiline = true;
            this.email_login.Name = "email_login";
            this.email_login.Size = new System.Drawing.Size(335, 34);
            this.email_login.TabIndex = 6;
            this.email_login.Text = "Email";
            this.email_login.UseWaitCursor = true;
            // 
            // senha_login
            // 
            this.senha_login.Location = new System.Drawing.Point(380, 454);
            this.senha_login.Multiline = true;
            this.senha_login.Name = "senha_login";
            this.senha_login.Size = new System.Drawing.Size(335, 34);
            this.senha_login.TabIndex = 7;
            this.senha_login.Text = "Senha";
            this.senha_login.UseWaitCursor = true;
            // 
            // conta_login
            // 
            this.conta_login.AutoSize = true;
            this.conta_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conta_login.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.conta_login.Location = new System.Drawing.Point(461, 579);
            this.conta_login.Name = "conta_login";
            this.conta_login.Size = new System.Drawing.Size(201, 17);
            this.conta_login.TabIndex = 20;
            this.conta_login.Text = "Ainda não tem uma conta?";
            this.conta_login.Click += new System.EventHandler(this.conta_login_Click);
            // 
            // entrar_login
            // 
            this.entrar_login.BackColor = System.Drawing.SystemColors.HotTrack;
            this.entrar_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entrar_login.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.entrar_login.Location = new System.Drawing.Point(380, 524);
            this.entrar_login.Name = "entrar_login";
            this.entrar_login.Size = new System.Drawing.Size(335, 38);
            this.entrar_login.TabIndex = 19;
            this.entrar_login.Text = "Entrar";
            this.entrar_login.UseVisualStyleBackColor = false;
            this.entrar_login.Click += new System.EventHandler(this.entrar_login_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::travelgest.Properties.Resources.logo_removebg_preview;
            this.pictureBox2.Location = new System.Drawing.Point(464, 74);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(150, 112);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::travelgest.Properties.Resources.Avião_sobre_o_paraíso_tropical;
            this.pictureBox1.Location = new System.Drawing.Point(-1, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(357, 757);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 705);
            this.Controls.Add(this.conta_login);
            this.Controls.Add(this.entrar_login);
            this.Controls.Add(this.senha_login);
            this.Controls.Add(this.email_login);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox email_login;
        private System.Windows.Forms.TextBox senha_login;
        private System.Windows.Forms.Button entrar_login;
        private System.Windows.Forms.Label conta_login;
    }
}

