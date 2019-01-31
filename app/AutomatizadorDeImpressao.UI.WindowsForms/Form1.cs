using System;
using System.Windows.Forms;

namespace AutomatizadorDeImpressao.UI.WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            using (var orquestrador = new Domain.Orquestrador())
            {
                orquestrador.Iniciar();
            }
        }
    }
}
