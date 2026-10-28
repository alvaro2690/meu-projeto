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
    public partial class Cadastar: Form
    {
        private Metodos metodos = new Metodos();

        public Cadastar()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void esquecer_login_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.ShowDialog();
        }

        private void cadastrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nome_cadastar.Text) ||
        string.IsNullOrWhiteSpace(email_cadastar.Text) ||
        string.IsNullOrWhiteSpace(contacto_cadastar.Text) ||
        string.IsNullOrWhiteSpace(senha_cadastar.Text) ||
        string.IsNullOrWhiteSpace(confirmar_cadastrar.Text))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            if (senha_cadastar.Text != confirmar_cadastrar.Text)
            {
                MessageBox.Show("As senhas não coincidem.");
                return;
            }

            try
            {
                if (metodos.CadastrarUtilizador(
                    nome_cadastar.Text, email_cadastar.Text,
                    contacto_cadastar.Text, senha_cadastar.Text))
                {
                    MessageBox.Show("Utilizador cadastrado com sucesso.");
                    this.Close();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                    MessageBox.Show("Este email já está registado.");
                else
                    MessageBox.Show("Erro no cadastro: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }
    }
}
