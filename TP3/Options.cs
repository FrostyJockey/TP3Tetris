using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP3
{
    public partial class Options : Form
    {

        #region Variables Partagées

        /// <summary>
        /// Variable pour faire apparaître/appel au formulaire de jeu
        /// </summary>
        TitrisForm jeuTitris = new TitrisForm();

        /// <summary>
        /// Définit si le son doit être arrêté ou non.
        /// </summary>
        int sonCocheOuPas;

        #endregion

        public Options()
        {
            InitializeComponent();
        }

        private void buttonConfirmer_Click(object sender, EventArgs e)
        {
            ApplicationChoix();
        }

        public void ApplicationChoix()
        {
            int colonnesChoix = Decimal.ToInt32(trackBarColonnes.Value);
            int lignesChoix = Decimal.ToInt32(trackBarLignes.Value);
            jeuTitris.AppliquerOptions(colonnesChoix, lignesChoix, sonCocheOuPas);
            this.Hide();
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxSon_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSon.CheckState == CheckState.Checked)
            {
                sonCocheOuPas = 1;
            }
            else if (checkBoxSon.CheckState == CheckState.Unchecked)
            {
                sonCocheOuPas = 0;
            }
        }
    }
}
