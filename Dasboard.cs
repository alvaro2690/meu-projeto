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
    public partial class Dasboard : Form
    {
        public Dasboard()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Dasboard_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

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

        private void btn_reservas_Click(object sender, EventArgs e)
        {
            Reservas_e_Vendas reserva = new Reservas_e_Vendas();
            reserva.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Relatório_e_Histórico relatorio = new Relatório_e_Histórico();
            relatorio.ShowDialog();
        }
    }
}
