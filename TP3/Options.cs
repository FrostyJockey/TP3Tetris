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

        /// <summary>
        /// Initiallise les barres pour qu'ils soient sur le nombre de colonnes et de lignes.
        /// </summary>
        /// <param name="nbColonnes">Nombre de colonnes courant</param>
        /// <param name="nbLignes">Nombre de lignes courant</param>
        public void InitialiserOptions(int nbColonnes, int nbLignes)
        {
            barreColonnes = nbColonnes;
            barreLignes = nbLignes;
            trackBarColonnes.Value = barreColonnes;
            trackBarLignes.Value = barreLignes;
        }


        /// <summary>
        /// Au clic, appel la fonction appliquant les options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConfirmer_Click(object sender, EventArgs e)
        {
            ApplicationChoix();
        }


        /// <summary>
        /// Applique le choix des options.
        /// </summary>
        public void ApplicationChoix()
        {
            int colonnesChoix = Decimal.ToInt32(trackBarColonnes.Value);
            int lignesChoix = Decimal.ToInt32(trackBarLignes.Value);
            if (colonnesChoix != barreColonnes || lignesChoix != barreLignes)
            {
                DialogResult reponse = MessageBox.Show("Pour appliquer la partie, le jeu devera être redémarré. Souhaitez-vous continuer?", "Attention!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (reponse == DialogResult.Yes)
                {
                    titrisJeuActif.RedemarrageJeuOptions(colonnesChoix, lignesChoix);
                    this.Close();
                }
            }
            titrisJeuActif.AppliquerOptions(sonCocheOuPas, musiqueCocheOuPas);
            this.Close();
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Fonction indiquant si le son devrait arrêter ou non.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Fonction indiquant si la musique devrait arrêter ou non.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Fonction modifiant le text affichant le nombre de colonnes que l'utilisateur s'apprête à choisir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarColonnes_Scroll(object sender, EventArgs e)
        {
            textBoxColonnes.Text = (trackBarColonnes.Value).ToString();
        }

        /// <summary>
        /// Fonction modifiant le text affichant le nombre de lignes que l'utilisateur s'apprête à choisir.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarLignes_Scroll(object sender, EventArgs e)
        {
            textBoxLignes.Text = (trackBarLignes.Value).ToString();
        }
    }
}
