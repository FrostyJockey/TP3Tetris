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
        // CE FICHIER .cs A ÉTÉ CRÉÉ ET CODÉ PAR CDThibodeau
        // Merci Stack Overflow pour le code pour faire une référence de formulaire dans un autre (voir la déclaration de titrisJeuActif)
        #region Variables Partagées

        /// <summary>
        /// Définit si le son doit être arrêté ou non.
        /// </summary>
        int sonCocheOuPas;

        /// <summary>
        /// 
        /// </summary>
        int musiqueCocheOuPas;

        /// <summary>
        ///  Référence du premier formulaire. (merci Stack Overflow)
        /// </summary>
        private TitrisForm titrisJeuActif;

        /// <summary>
        /// Valeur qui va déterminer la position du trackBar de colonnes.
        /// </summary>
        int barreColonnes;

        /// <summary>
        /// Valeur qui va déterminer la position du trackBar de lignes.
        /// </summary>
        int barreLignes;

        #endregion

        public Options(TitrisForm formulaireActif)
        {
            InitializeComponent();
            titrisJeuActif = formulaireActif;
        }


        public void InitialiserOptions(int nbColonnes, int nbLignes)
        {
            barreColonnes = nbColonnes;
            barreLignes = nbLignes;
            trackBarColonnes.Value = barreColonnes;
            trackBarLignes.Value = barreLignes;
        }

        private void buttonConfirmer_Click(object sender, EventArgs e)
        {
            ApplicationChoix();
        }

        public void ApplicationChoix()
        {
            int colonnesChoix = Decimal.ToInt32(trackBarColonnes.Value);
            int lignesChoix = Decimal.ToInt32(trackBarLignes.Value);
            titrisJeuActif.AppliquerOptions(colonnesChoix, lignesChoix, sonCocheOuPas, musiqueCocheOuPas);
            this.Close();
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

        private void checkBoxMusique_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMusique.CheckState == CheckState.Checked)
            {
                musiqueCocheOuPas = 1;
            }
            else if (checkBoxMusique.CheckState == CheckState.Unchecked)
            {
                musiqueCocheOuPas = 0;
            }
        }

        private void trackBarColonnes_Scroll(object sender, EventArgs e)
        {
            barreColonnes = trackBarColonnes.Value;
            textBoxColonnes.Text = (trackBarColonnes.Value).ToString();
        }

        private void trackBarLignes_Scroll(object sender, EventArgs e)
        {
            barreLignes = trackBarLignes.Value;
            textBoxLignes.Text = (trackBarLignes.Value).ToString();
        }
    }
}
