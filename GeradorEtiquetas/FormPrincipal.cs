using GeradorEtiquetas.Classes;
using GeradorEtiquetas.Repositories;
using GeradorEtiquetas.Services;
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
    public partial class FormPrincipal : Form
    {
        private readonly Helpers _helpers;
        private readonly CorMaterialRepository _corMaterialRepository;
        private readonly ReferenciaRepository _referenciaRepository;
        private readonly GeradorDeEtiqueta _geradorDeEtiqueta;

        public FormPrincipal()
        {
            _helpers = new Helpers();
            _corMaterialRepository = new CorMaterialRepository();
            _referenciaRepository = new ReferenciaRepository();
            _geradorDeEtiqueta = new GeradorDeEtiqueta();

            InitializeComponent();
            cmbMaterialCor.Items.AddRange(_corMaterialRepository.Get().ToArray());
            cmbMaterialCor.DisplayMember = "Nome";
            cmbMaterialCor.ValueMember = "Id";
        }

        public void AlterarItensComboMaterial()
        {
            cmbMaterialCor.Items.Clear();
            var materiais = _corMaterialRepository.Get().ToArray();
            cmbMaterialCor.Items.AddRange(materiais);
            cmbMaterialCor.SelectedIndex = materiais.Length - 1;
        }

        private void SomenteNumeros(object sender, KeyPressEventArgs e)
        {
            _helpers.SomenteNumeros(sender, e);
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            if (cmbMaterialCor.SelectedItem == null)
            {
                MessageBox.Show("O campo Material / Cor deve ser preenchido");
                return;
            }

            if (string.IsNullOrEmpty(txtRef.Text))
            {
                MessageBox.Show("O campo Referência deve ser preenchido");
                return;
            }

            Etiqueta etiqueta;
            var referencia = BuscarReferencia(txtRef.Text);
            var materialCor = (MaterialCor)cmbMaterialCor.SelectedItem;

            if (rbMasculino.Checked)
            {
                etiqueta = new Etiqueta("M", referencia, materialCor);

                for (int i = 36; i < 51; i++)
                {
                    string qtdNumero = ((TextBox)pnlMasc.Controls[$"txt{i}Masc"]).Text;

                    if (int.TryParse(qtdNumero, out int quantidade) && quantidade > 0)
                        etiqueta.AdicionarNumeracao(i, quantidade);
                }
            }
            else if (rbInfantil.Checked)
            {
                etiqueta = new Etiqueta("I", referencia, materialCor);

                for (int i = 25; i < 37; i++)
                {
                    string qtdNumero = ((TextBox)pnlInf.Controls[$"txt{i}Inf"]).Text;

                    if (int.TryParse(qtdNumero, out int quantidade) && quantidade > 0)
                        etiqueta.AdicionarNumeracao(i, quantidade);
                }
            }
            else
            {
                etiqueta = new Etiqueta("F", referencia, materialCor);

                for (int i = 33; i < 41; i++)
                {
                    string qtdNumero = ((TextBox)pnlFem.Controls[$"txt{i}Fem"]).Text;

                    if (int.TryParse(qtdNumero, out int quantidade) && quantidade > 0)
                        etiqueta.AdicionarNumeracao(i, quantidade);
                }
            }

            try
            {
                _geradorDeEtiqueta.Gerar(etiqueta);
                MessageBox.Show($"Etiquetas na fila de impressão.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao imprimir \n {ex}");
            }
        }

        private Referencia BuscarReferencia(string nome)
        {
            var referencia = _referenciaRepository.BuscarPorNome(nome);
            if (referencia != null)
                return referencia;

            return _referenciaRepository.CriarReferencia(nome);
        }

        private void btnAdicionarCorMaterial_Click(object sender, EventArgs e)
        {
            new FormMaterial(this).Show();
            this.Hide();
        }

        private void rbInfinino_CheckedChanged(object sender, EventArgs e)
        {
            pnlMasc.Hide();
            pnlInf.Hide();
            pnlFem.Show();
        }

        private void rbMasculino_CheckedChanged(object sender, EventArgs e)
        {
            pnlMasc.Show();
            pnlInf.Hide();
            pnlFem.Hide();
        }

        private void rbInfantil_CheckedChanged(object sender, EventArgs e)
        {
            pnlMasc.Hide();
            pnlInf.Show();
            pnlFem.Hide();
        }
    }
}
