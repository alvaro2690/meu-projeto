
using System;
using System.Data;
using System.Windows.Forms;

namespace travelgest
{
    public partial class Reservas_e_Vendas : Form
    {
        private Metodos metodos = new Metodos();
        private DataTable tabelaReservas;

        public Reservas_e_Vendas()
        {
            InitializeComponent();
        }

        
        private void Reservas_e_Vendas_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            ConfigurarFiltros();
            CarregarReservas();
        }

       
        private void ConfigurarDataGridView()
        {
            dG_Reservas.AutoGenerateColumns = false;
            dG_Reservas.AllowUserToAddRows = false;
            dG_Reservas.ReadOnly = true;
            dG_Reservas.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dG_Reservas.MultiSelect = false;
            dG_Reservas.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            AssociarColuna("Id", "Id");
            AssociarColuna("Cliente", "Cliente");
            AssociarColuna("Viagem", "Viagem");
            AssociarColuna("DataReserva", "DataReserva");
            AssociarColuna("Valor", "Valor");
            AssociarColuna("Status", "Status");

            if (dG_Reservas.Columns.Contains("DataReserva"))
            {
                dG_Reservas.Columns["DataReserva"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dG_Reservas.Columns.Contains("Valor"))
            {
                dG_Reservas.Columns["Valor"]
                    .DefaultCellStyle.Format = "N2";
            }
        }

        private void AssociarColuna(string nome, string campo)
        {
            if (dG_Reservas.Columns.Contains(nome))
            {
                dG_Reservas.Columns[nome].DataPropertyName = campo;
            }
        }

       
        private void CarregarReservas()
        {
            try
            {
                tabelaReservas = metodos.ListarReservas();

                dG_Reservas.DataSource = null;
                dG_Reservas.DataSource = tabelaReservas;

                AplicarFiltros();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao carregar as reservas: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

       
        private void ConfigurarFiltros()
        {
            cb_viagem.Items.Clear();
            cb_viagem.Items.Add("Todas as Viagens");

            try
            {
                DataTable viagens = metodos.ListarViagens();

                foreach (DataRow linha in viagens.Rows)
                {
                    cb_viagem.Items.Add(
                        linha["Origem"].ToString() + " - " +
                        linha["Destino"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao carregar as viagens: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            cb_viagem.SelectedIndex = 0;

            cb_status.Items.Clear();
            cb_status.Items.Add("Todos os status");
            cb_status.Items.Add("Pendente");
            cb_status.Items.Add("Confirmada");
            cb_status.SelectedIndex = 0;
        }

       
        private void pesquisar_cliente_TextChanged(
            object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        
        private void cmbViagens_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void cmbStatus_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            AplicarFiltros();
        }

       
        private void AplicarFiltros()
        {
            if (tabelaReservas == null)
                return;

            string filtro = "";

            string cliente = pesquisar_cliente.Text.Trim()
                .Replace("'", "''")
                .Replace("[", "[[]")
                .Replace("%", "[%]")
                .Replace("*", "[*]");

            if (cliente != "")
            {
                filtro =
                    "Convert(Cliente, 'System.String') LIKE '%" +
                    cliente + "%'";
            }

            if (cb_viagem.SelectedIndex > 0 &&
                cb_viagem.SelectedItem != null)
            {
                string viagem = cb_viagem.SelectedItem.ToString()
                    .Replace("'", "''");

                if (filtro != "")
                    filtro += " AND ";

                filtro += "Viagem = '" + viagem + "'";
            }

            if (cb_status.SelectedIndex > 0 &&
                cb_status.SelectedItem != null)
            {
                string estado = cb_status.SelectedItem.ToString()
                    .Replace("'", "''");

                if (filtro != "")
                    filtro += " AND ";

                filtro += "Status = '" + estado + "'";
            }

            try
            {
                tabelaReservas.DefaultView.RowFilter = filtro;
                dG_Reservas.DataSource = tabelaReservas.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao filtrar reservas: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        
        private void btn_novaReserva_Click(
            object sender, EventArgs e)
        {
            PrepararNovaReserva();
        }

        private void btn_novareserva_Click_1(
            object sender, EventArgs e)
        {
            PrepararNovaReserva();
        }

        private void PrepararNovaReserva()
        {
            txt_cliente.Clear();
            txt_viagem.Clear();
            txt_valor.Clear();

            txt_data.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txt_cliente.Focus();
        }

       
        private void btn_guardar_Click(object sender, EventArgs e)
        {
            int clienteId;
            int viagemId;

            if (!int.TryParse(txt_cliente.Text.Trim(), out clienteId) ||
                !int.TryParse(txt_viagem.Text.Trim(), out viagemId))
            {
                MessageBox.Show(
                    "Introduz um ID de cliente e um ID de viagem válidos.",
                    "Dados inválidos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string estado = cmb_status.Text.Trim();

            if (estado != "Pendente" && estado != "Confirmada")
            {
                MessageBox.Show(
                    "Seleciona Pendente ou Confirmada.",
                    "Estado inválido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool adicionado =
                    metodos.AdicionarReserva(clienteId, viagemId, estado);

                if (adicionado)
                {
                    MessageBox.Show(
                        "Reserva adicionada com sucesso.",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CarregarReservas();
                    PrepararNovaReserva();
                }
                else
                {
                    MessageBox.Show(
                        "A reserva não foi adicionada.",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao adicionar a reserva: " + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

       
        private void dG_Reservas_CellContentClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string nomeBotao =
                dG_Reservas.Columns[e.ColumnIndex].Name;

            if (nomeBotao != "btnEditar" &&
                nomeBotao != "btnRemover")
                return;

            DataGridViewRow linha = dG_Reservas.Rows[e.RowIndex];

            if (!dG_Reservas.Columns.Contains("Id"))
            {
                MessageBox.Show("A coluna Id não está configurada.");
                return;
            }

            object valorId = linha.Cells["Id"].Value;

            if (valorId == null || valorId == DBNull.Value)
                return;

            int id = Convert.ToInt32(valorId);

            if (nomeBotao == "btnEditar")
            {
                MessageBox.Show(
                    "Reserva selecionada para edição: " + id +
                    ". A função de edição ainda precisa de ser implementada.",
                    "Editar Reserva",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else if (nomeBotao == "btnRemover")
            {
                DialogResult resposta = MessageBox.Show(
                    "Tens a certeza de que pretendes remover esta reserva?",
                    "Confirmar remoção",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resposta != DialogResult.Yes)
                    return;

                try
                {
                    if (metodos.EliminarReserva(id))
                    {
                        MessageBox.Show(
                            "Reserva removida com sucesso.");

                        CarregarReservas();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Não foi possível remover a reserva.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Erro ao remover a reserva: " + ex.Message,
                        "Erro",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

 
        private void btn_reservas_Click(object sender, EventArgs e)
        {
            Reservas_e_Vendas reserva = new Reservas_e_Vendas();
            reserva.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Relatório_e_Histórico relatorio =
                new Relatório_e_Histórico();

            relatorio.ShowDialog();
        }

        private void btn_cliente_Click(object sender, EventArgs e)
        {
            Gestão_de_Clientes cliente = new Gestão_de_Clientes();
            cliente.ShowDialog();
        }

        private void btn_destino_Click(object sender, EventArgs e)
        {
            Gestão_de_Viagens_e_Destinos viagem =
                new Gestão_de_Viagens_e_Destinos();

            viagem.ShowDialog();
        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            Dasboard dashboard = new Dasboard();
            dashboard.ShowDialog();
        }

        private void txt_statu_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
