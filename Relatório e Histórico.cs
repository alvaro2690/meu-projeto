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
    public partial class Relatório_e_Histórico : Form
    {
        public Relatório_e_Histórico()
        {
            InitializeComponent();
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

        private void btn_cliente_Click(object sender, EventArgs e)
        {
            Gestão_de_Clientes cliente = new Gestão_de_Clientes();
            cliente.ShowDialog();
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
