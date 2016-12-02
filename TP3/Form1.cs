using System;
using System.Drawing;
using System.Windows.Forms;

namespace TP3
{
    public partial class TitrisForm : Form
    {
        public TitrisForm()
        {
            InitializeComponent();
        }

        // CDThibodeau
        #region Valeurs Partagées

        /// <summary>
        /// Nombre de lignes retirées durant la partie.
        /// </summary>
        int pointage;

        /// <summary>
        /// Numéro de la pièce (pour l'affichage)
        /// </summary>
        int typePiece;

        /// <summary>
        /// Images représentant un des blocs aléatoires, un bloc gelé ou un espace vide
        /// </summary>
        Bitmap[] imagesBlocs = new Bitmap[] { Properties.Resources.rien, Properties.Resources.freeze, Properties.Resources.carreBloc, Properties.Resources.barreBloc, Properties.Resources.TBloc, Properties.Resources.LBloc, Properties.Resources.JBloc, Properties.Resources.SBloc, Properties.Resources.ZBloc };

        /// <summary>
        /// Tableau représentant le bloc que le joueur peut bouger.
        /// </summary>
        int[,] blocActif;

        /// <summary>
        /// Ligne où se situe le coin en haut à gauche du tableau blocActif.
        /// </summary>
        int ligneCourante = 0;

        /// <summary>
        /// Colonne où se situe le coin en haut à gauche du tableau blocActif.
        /// </summary>
        int colonneCourante = 4;

        /// <summary>
        /// Nombre de colonnes dans le jeu.
        /// </summary>
        int nbColonnes = 10;

        /// <summary>
        /// Nombre de lignes dans le jeu.
        /// </summary>
        int nbLignes = 20;

        /// <summary>
        /// Valeur de la touche appuyé lors du déplacement
        /// (ne sert que de lien pour que la fonction "BlocPeutBouger" puisse être appelé dans l'évènement "timerDescente_Tick")
        /// </summary>
        int deplacement;

        /// <summary>
        /// Tableau de vérification de données (Exemple si le jeu peut retirer une ligne pleine (vérifie si la ligne contient des 1, ce qui équivaut aux blocs gelés))
        /// </summary>
        int[,] tableauJeuDonnees;

        #endregion
        // CDThibodeau

        #region Code fourni

        // Représentation visuelles du jeu en mémoire.
        PictureBox[,] toutesImagesVisuelles = null;

        /// <summary>
        /// Gestionnaire de l'événement se produisant lors du premier affichage 
        /// du formulaire principal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLoad(object sender, EventArgs e)
        {
            // Ne pas oublier de mettre en place les valeurs nécessaires à une partie.
            ExecuterTestsUnitaires();
            InitialiserSurfaceDeJeu(nbLignes, nbColonnes);
            GenerationPiece();
        }

        private void InitialiserSurfaceDeJeu(int nbLignes, int nbCols)
        {
            // Création d'une surface de jeu 10 colonnes x 20 lignes
            toutesImagesVisuelles = new PictureBox[nbLignes, nbCols];
            tableauJeuDonnees = new int[nbLignes, nbColonnes];
            tableauJeu.Controls.Clear();
            tableauJeu.ColumnCount = toutesImagesVisuelles.GetLength(1);
            tableauJeu.RowCount = toutesImagesVisuelles.GetLength(0);
            for (int i = 0; i < tableauJeu.RowCount; i++)
            {
                tableauJeu.RowStyles[i].Height = tableauJeu.Height / tableauJeu.RowCount;
                for (int j = 0; j < tableauJeu.ColumnCount; j++)
                {
                    tableauJeuDonnees[i, j] = 0;
                    tableauJeu.ColumnStyles[j].Width = tableauJeu.Width / tableauJeu.ColumnCount;
                    // Création dynamique des PictureBox qui contiendront les pièces de jeu
                    PictureBox newPictureBox = new PictureBox();
                    newPictureBox.Width = tableauJeu.Width / tableauJeu.ColumnCount;
                    newPictureBox.Height = tableauJeu.Height / tableauJeu.RowCount;
                    newPictureBox.BackColor = Color.Black;
                    newPictureBox.Margin = new Padding(0, 0, 0, 0);
                    newPictureBox.BorderStyle = BorderStyle.FixedSingle;
                    newPictureBox.Dock = DockStyle.Fill;

                    // Assignation de la représentation visuelle.
                    toutesImagesVisuelles[i, j] = newPictureBox;
                    // Ajout dynamique du PictureBox créé dans la grille de mise en forme.
                    // A noter que l' "origine" du repère dans le tableau est en haut à gauche.
                    tableauJeu.Controls.Add(newPictureBox, j, i);
                }
            }
        } // Ajout du tableau "tableauJeuDonnees" pour l'initialiser
        #endregion

        #region Code à développer
        /// <summary>
        /// Faites ici les appels requis pour vos tests unitaires.
        /// </summary>
        void ExecuterTestsUnitaires()
        {
            ExecuterTestABC();
            // A compléter...
        }

        // A renommer et commenter!
        void ExecuterTestABC()
        {
            // Mise en place des données du test

            // Exécuter de la méthode à tester

            // Validation des résultats

            // Clean-up
        }

        #endregion

        // CDThibodeau
        /// <summary>
        /// Fonction générant un bloc aléatoire.
        /// </summary>
        public void GenerationPiece()
        {
            Random rnd = new Random();
            typePiece = rnd.Next(2, 9);
            if (typePiece == (int)TypeBloc.Carre) // Carré
            {
                blocActif = new int[2, 2] { { 2, 2 }, { 2, 2 } };
            }
            else if (typePiece == (int)TypeBloc.Barre) // Barre
            {
                Random rndSens = new Random();
                int sens = rndSens.Next(0, 2);
                if (sens == 0)
                {
                    blocActif = new int[4, 1] { { 3 }, { 3 }, { 3 }, { 3 } };
                }
                else
                {
                    blocActif = new int[1, 4] { { 3, 3, 3, 3 } };
                }
            }
            else if (typePiece == (int)TypeBloc.T) // Bloc T
            {
                blocActif = new int[2, 3] { { 0, 4, 0 }, { 4, 4, 4 } };
            }
            else if (typePiece == (int)TypeBloc.L) // Bloc L
            {
                blocActif = new int[3, 2] { { 5, 0 }, { 5, 0 }, { 5, 5 } };
            }
            else if (typePiece == (int)TypeBloc.J) // Bloc J
            {
                blocActif = new int[3, 2] { { 0, 6 }, { 0, 6 }, { 6, 6 } };
            }
            else if (typePiece == (int)TypeBloc.S) // Bloc S
            {
                blocActif = new int[2, 3] { { 0, 7, 7 }, { 7, 7, 0 } };
            }
            else if (typePiece == (int)TypeBloc.Z) // Bloc Z
            {
                blocActif = new int[2, 3] { { 8, 8, 0 }, { 0, 8, 8 } };
            }
            AfficherJeu();
        }
        // CDThibodeau

        // CDThibodeau
        /// <summary>
        /// Fonction vérifiant si le bloc peut bouger ou non en vérfiant si le bloc a atteint les contours ou un autre bloc gelé.
        /// Gèle le bloc si le bloc ne peut plus descendre.
        /// </summary>
        /// <param name="deplacement"> entier, valeur de la touche appuyé pour se déplacer</param>
        /// <returns></returns>
        bool BlocPeutBouger(int deplacement)
        {
            int offsetX = 0;
            int offsetY = 0;
            if(deplacement == 'a')
            {
                offsetX = -1;
            }
            else if (deplacement == 'd')
            {
                offsetX = 1;
            }
            else if (deplacement == 's')
            {
                offsetY=1;
            }
            bool peutBouger = true;
            for (int i = 0; i < blocActif.GetLength(0); i++)
            {
                for (int j = 0; j < blocActif.GetLength(1); j++)
                {
                    // Si reel bloc
                  if(blocActif[i,j] != 0)
                    {
                        // Si dans les limites du tableau
                        peutBouger = peutBouger && (colonneCourante + j + offsetX) < nbColonnes;
                        peutBouger = peutBouger && (colonneCourante + j + offsetX) >= 0;
                        peutBouger = peutBouger && (ligneCourante + i + offsetY) < nbLignes;
                        // Si pas le prochain bloc gelé
                        peutBouger = peutBouger && tableauJeuDonnees[ligneCourante + i + offsetY, colonneCourante + j + offsetX] != (int) TypeBloc.Gele;
                    }
                }
            }
            return peutBouger;
        }
        // CDThibodeau

        // CDThibodeau
        /// <summary>
        /// Quitte le jeu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonQuitterPartie_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // CDThibodeau
        // CDThibodeau

        // CDThibodeau
        /// <summary>
        /// Évènement descendant le bloc à chaque 0.5 seconde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDescente_Tick(object sender, EventArgs e)
        {
            BougerPiece('s');
        }
        // CDThibodeau

        // CDThibodeau
        ///
        void AfficherJeu()
        {
            for (int i = 0; i < toutesImagesVisuelles.GetLength(0); i++)
            {
                for (int j = 0; j < toutesImagesVisuelles.GetLength(1); j++)
                {
                    if(tableauJeuDonnees[i,j]==(int)TypeBloc.Aucun)
                        toutesImagesVisuelles[ i,  j].Image = imagesBlocs[(int)TypeBloc.Aucun];
                    else
                        toutesImagesVisuelles[i, j].Image = imagesBlocs[(int)TypeBloc.Gele];
                }
            }

            for (int i = 0; i < blocActif.GetLength(0); i++)
            {
                for (int j = 0; j < blocActif.GetLength(1); j++)
                {
                    if (ligneCourante + blocActif.GetLength(0) - 1 != nbLignes)
                    {
                        if (blocActif[i, j] != 0)
                        {
                            toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = imagesBlocs[typePiece];
                        }
                        
                            
                    }
                }
            }
                
        }
        //CDThibodeau

        // CDThibodeau
        /// <summary>
        /// 
        /// </summary>
        void GelerPiece()
        {
            if (BlocPeutBouger('s') == false)
            {
                for (int i = 0; i < blocActif.GetLength(0); i++)
                {
                    for (int j = 0; j < blocActif.GetLength(1); j++)
                    {
                        if (blocActif[i,j] !=0)
                        {
                            tableauJeuDonnees[ligneCourante + i, colonneCourante + j] = (int)TypeBloc.Gele;
                        }

                    }
                }

                pointage = DecalerLignes();
                labelPointage.Text = pointage.ToString();
                ligneCourante = 0;
                colonneCourante = 4;
                GenerationPiece();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deplacement"></param>
        void BougerPiece(int deplacement)
        {
            int offsetX = 0;
            int offsetY = 0;
            if (deplacement == 'a')
            {
                offsetX = -1;
            }
            else if (deplacement == 'd')
            {
                offsetX = 1;
            }
            else if (deplacement == 's')
            {
                offsetY = 1;
            }

            if (BlocPeutBouger(deplacement) == true)
            {
                ligneCourante += offsetY;
                colonneCourante += offsetX;
                AfficherJeu();
            }
            else
                GelerPiece();
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitrisForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            BougerPiece(e.KeyChar);
        }

        /// <summary>
        /// Fonction qui décale les lignes si le jeu contient une ligne pleine.
        /// </summary>
        /// <returns>Retourne le nombre de lignes pleines retirée si le jeu doit en retirer.</returns>
        int DecalerLignes()
        {
            int[] ligneAVerifier = new int[tableauJeuDonnees.GetLength(1)];
            int nbLignesRetires = 0;
            bool decalageEstPossible;
            for (int i = 0; i < tableauJeuDonnees.GetLength(0); i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++)
                {
                    ligneAVerifier[j] = tableauJeuDonnees[(tableauJeuDonnees.GetLength(0)-1) - i, j];
                }
                decalageEstPossible = VerifierLignesPleines(ligneAVerifier);
                if (decalageEstPossible == true)
                {
                    for (int iDecalage = 0; iDecalage < (tableauJeuDonnees.GetLength(0)-1); iDecalage++)
                    {
                        for (int jDecalage = 0; jDecalage < tableauJeuDonnees.GetLength(1); jDecalage++)
                        {
                            toutesImagesVisuelles[(toutesImagesVisuelles.GetLength(0) - 1) - iDecalage, jDecalage].Image = imagesBlocs[(int)TypeBloc.Aucun];
                            toutesImagesVisuelles[(toutesImagesVisuelles.GetLength(0) - 1) - iDecalage, jDecalage].Image = toutesImagesVisuelles[(toutesImagesVisuelles.GetLength(0) - 2) - iDecalage, jDecalage].Image;
                            tableauJeuDonnees[(tableauJeuDonnees.GetLength(0) - 1) - iDecalage, jDecalage] = tableauJeuDonnees[(tableauJeuDonnees.GetLength(0) - 2) - iDecalage, jDecalage];
                            nbLignesRetires++;
                        }
                    }
                }
            }
            return nbLignesRetires;
        }

        /// <summary>
        /// Fonction vérifiant s'il y a une ligne pleine à la ligne "ligneAVerifier" dans le tableau "tableauJeuDonnees".
        /// </summary>
        /// <param name="ligneAVerifier"> tableau d'entiers, Numéro de la ligne à vérifier.</param>
        /// <returns>Retourne un booléen: true = La ligne est pleine | false = La ligne n'est pas pleine.</returns>
        bool VerifierLignesPleines(int[] ligneAVerifier)
        {
            bool decalageEstPossible = true;
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++)
            {
                if (ligneAVerifier[j] != (int)TypeBloc.Gele)
                {
                    decalageEstPossible = false;
                }
            }
            return decalageEstPossible;
        }
        // CDThibodeau
    }
}
