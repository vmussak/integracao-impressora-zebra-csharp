using GeradorEtiquetas.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeradorEtiquetas
{
    public partial class FormMaterial : Form
    {
        private readonly FormPrincipal _formPrincipal;
        private readonly CorMaterialRepository _corMaterialRepository;

        public FormMaterial(FormPrincipal formPrincipal)
        {
            _formPrincipal = formPrincipal;
            _corMaterialRepository = new CorMaterialRepository();
            InitializeComponent();
        }

        private void FormMaterial_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formPrincipal.Show();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("O campo Nome deve ser preenchido");
                return;
            }

            var material =_corMaterialRepository.Criar(txtNome.Text);
            _formPrincipal.AlterarItensComboMaterial();
            this.Close();
        }
    }
}
