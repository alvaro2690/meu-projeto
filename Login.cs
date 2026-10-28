using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace travelgest
{
    public partial class Login : Form
    {
        private Metodos metodos = new Metodos();
        public Login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void entrar_login_Click(object sender, EventArgs e)
        {
            // Validar os campos
            if (string.IsNullOrWhiteSpace(email_login.Text) ||
                string.IsNullOrWhiteSpace(senha_login.Text))
            {
                MessageBox.Show(
                    "Preenche o email e a senha.",
                    "Campos obrigatórios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
               
                bool autenticado = metodos.Login(
                    email_login.Text.Trim(),
                    senha_login.Text
                );

                if (autenticado)
                {
                    MessageBox.Show(
                        "Login efetuado com sucesso!",
                        "Bem-vindo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Abrir o painel principal
                    Dasboard principal = new Dasboard();

                    principal.Show();

                    // Ocultar a tela de login
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        "Email ou senha incorretos.",
                        "Falha no login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    senha_login.Clear();
                    senha_login.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocorreu um erro ao efetuar o login:\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void conta_login_Click(object sender, EventArgs e)
        {
            Cadastar cadastrar = new Cadastar();
            cadastrar.ShowDialog();
        }

       
    }

        
    
}
