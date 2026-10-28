using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace travelgest
{
    public partial class Gestão_de_Clientes : Form
    {
        private Metodos metodos = new Metodos();
        private int clienteId = 0;

        public Gestão_de_Clientes()
        {
            InitializeComponent();

            this.Load += Gestão_de_Clientes_Load;
        }

       
        private void Gestão_de_Clientes_Load(object sender, EventArgs e)
        {
            ConfigurarTabela();
            CarregarClientes();
        }

       
        private void ConfigurarTabela()
        {
            dG_Clientes.AutoGenerateColumns = false;
            dG_Clientes.AllowUserToAddRows = false;
            dG_Clientes.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dG_Clientes.MultiSelect = false;
            dG_Clientes.ReadOnly = true;
        }

       
        private void CarregarClientes()
        {
            try
            {
                dG_Clientes.DataSource = null;
                dG_Clientes.DataSource = metodos.ListarClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao carregar clientes: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

      
        private void email_cadastar_TextChanged(
            object sender, EventArgs e)
        {
            try
            {
                DataTable tabela = metodos.ListarClientes();
                DataView vista = tabela.DefaultView;

                string pesquisa = pesquisar_cliente.Text.Trim()
                    .Replace("'", "''")
                    .Replace("[", "[[]")
                    .Replace("%", "[%]")
                    .Replace("*", "[*]");

                if (pesquisa.Length == 0)
                {
                    vista.RowFilter = "";
                }
                else
                {
                    vista.RowFilter =
                        "Convert(NomeCompleto, 'System.String') LIKE '%" +
                        pesquisa +
                        "%' OR Convert(Contacto, 'System.String') LIKE '%" +
                        pesquisa +
                        "%' OR Convert(Email, 'System.String') LIKE '%" +
                        pesquisa + "%'";
                }

                dG_Clientes.DataSource = vista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro na pesquisa: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

      
        private void btn_novoCliente_Click(object sender, EventArgs e)
        {
            clienteId = 0;
            LimparCampos();
            txt_nome.Focus();
        }

    
        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_nome.Text) ||
                string.IsNullOrWhiteSpace(txt_contacto.Text))
            {
                MessageBox.Show(
                    "Preenche o nome e o contacto do cliente.",
                    "Campos obrigatórios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                bool resultado = metodos.GuardarCliente(
                    clienteId,
                    txt_nome.Text.Trim(),
                    txt_contacto.Text.Trim(),
                    txt_email.Text.Trim(),
                    txt_morada.Text.Trim());

                if (resultado)
                {
                    if (clienteId == 0)
                    {
                        MessageBox.Show(
                            "Cliente adicionado com sucesso.");
                    }
                    else
                    {
                        MessageBox.Show(
                            "Cliente atualizado com sucesso.");
                    }

                    clienteId = 0;
                    LimparCampos();
                    CarregarClientes();
                }
                else
                {
                    MessageBox.Show(
                        "Não foi possível guardar o cliente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao guardar cliente: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        
        private object ObterValorCelula(
            DataGridViewRow linha, string nomeColuna)
        {
            foreach (DataGridViewColumn coluna in dG_Clientes.Columns)
            {
                if (string.Equals(
                        coluna.Name, nomeColuna,
                        StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(
                        coluna.DataPropertyName, nomeColuna,
                        StringComparison.OrdinalIgnoreCase))
                {
                    return linha.Cells[coluna.Index].Value;
                }
            }

            return null;
        }

        
        private void dG_Clientes_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow linha = dG_Clientes.Rows[e.RowIndex];

            object valorId = ObterValorCelula(linha, "Id");

            if (valorId == null || valorId == DBNull.Value)
                return;

            clienteId = Convert.ToInt32(valorId);

            object valorNome =
                ObterValorCelula(linha, "NomeCompleto");
            object valorContacto =
                ObterValorCelula(linha, "Contacto");
            object valorEmail =
                ObterValorCelula(linha, "Email");
            object valorMorada =
                ObterValorCelula(linha, "Morada");

            txt_nome.Text =
                valorNome == null || valorNome == DBNull.Value
                ? "" : Convert.ToString(valorNome);

            txt_contacto.Text =
                valorContacto == null || valorContacto == DBNull.Value
                ? "" : Convert.ToString(valorContacto);

            txt_email.Text =
                valorEmail == null || valorEmail == DBNull.Value
                ? "" : Convert.ToString(valorEmail);

            txt_morada.Text =
                valorMorada == null || valorMorada == DBNull.Value
                ? "" : Convert.ToString(valorMorada);
        }

       
        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (dG_Clientes.CurrentRow == null ||
                dG_Clientes.CurrentRow.Index < 0)
            {
                MessageBox.Show(
                    "Seleciona um cliente na tabela.");
                return;
            }

            object valorId = ObterValorCelula(
                dG_Clientes.CurrentRow, "Id");

            if (valorId == null || valorId == DBNull.Value)
            {
                MessageBox.Show(
                    "Não foi possível identificar o cliente selecionado.");
                return;
            }

            int id = Convert.ToInt32(valorId);

            DialogResult resposta = MessageBox.Show(
                "Tens a certeza de que pretendes remover este cliente?",
                "Confirmar remoção",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resposta != DialogResult.Yes)
                return;

            try
            {
                if (metodos.EliminarCliente(id))
                {
                    MessageBox.Show(
                        "Cliente removido com sucesso.");

                    clienteId = 0;
                    LimparCampos();
                    CarregarClientes();
                }
                else
                {
                    MessageBox.Show(
                        "Não foi possível remover o cliente.");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show(
                        "Não é possível remover este cliente porque " +
                        "existem reservas associadas a ele.");
                }
                else
                {
                    MessageBox.Show(
                        "Erro ao remover cliente: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao remover cliente: " + ex.Message);
            }
        }

        // Limpar os campos
        private void LimparCampos()
        {
            txt_nome.Clear();
            txt_contacto.Clear();
            txt_email.Clear();
            txt_morada.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_reservas_Click(object sender, EventArgs e)
        {
            Reservas_e_Vendas reserva = new Reservas_e_Vendas();
            reserva.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Relatório_e_Histórico relatorio = new Relatório_e_Histórico();
            relatorio.ShowDialog();
        }

        private void btn_destino_Click(object sender, EventArgs e)
        {
            Gestão_de_Viagens_e_Destinos viagem = new Gestão_de_Viagens_e_Destinos();
            viagem.ShowDialog();
        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
           Dasboard dasboad = new Dasboard();
            dasboad.ShowDialog();
        }

      
    }
}