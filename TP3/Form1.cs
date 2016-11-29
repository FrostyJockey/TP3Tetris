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
        /// Images représentant un des blocs aléatoires, un bloc gelé ou un espace vide
        /// </summary>
        Bitmap[] imagesBlocs = new Bitmap[] { Properties.Resources.rien, Properties.Resources.freeze, Properties.Resources.carreBloc, Properties.Resources.barreBloc, Properties.Resources.TBloc, Properties.Resources.LBloc, Properties.Resources.JBloc, Properties.Resources.SBloc, Properties.Resources.ZBloc };

        /// <summary>
        /// Tableau représentant le bloc que le joueur peut bouger.
        /// </summary>
        Bitmap[,] blocActif;

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
        /// (ne sert que de lien pour que la fonction "BloPeutBouger" puisse être appelé dans l'évènement "timerDescente_Tick")
        /// </summary>
        int deplacement;
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
            tableauJeu.Controls.Clear();
            tableauJeu.ColumnCount = toutesImagesVisuelles.GetLength(1);
            tableauJeu.RowCount = toutesImagesVisuelles.GetLength(0);
            for (int i = 0; i < tableauJeu.RowCount; i++)
            {
                tableauJeu.RowStyles[i].Height = tableauJeu.Height / tableauJeu.RowCount;
                for (int j = 0; j < tableauJeu.ColumnCount; j++)
                {
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
        }
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

        // CDThibodeau
        /// <summary>
        /// Fonction générant un bloc aléatoire.
        /// </summary>
        public void GenerationPiece()
        {
            Random rnd = new Random();
            int typePiece = rnd.Next(2, 9);
            if (typePiece == (int)TypeBloc.Carre) // Carré
            {
                blocActif = new Bitmap[2, 2] { { imagesBlocs[typePiece], imagesBlocs[typePiece] }, { imagesBlocs[typePiece], imagesBlocs[typePiece] } };
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = blocActif[i, j];
                    }
                }
            }
            else if (typePiece == (int)TypeBloc.Barre) // Barre
            {
                Random rndSens = new Random();
                int sens = rndSens.Next(0, 2);
                if (sens == 0)
                {
                    blocActif = new Bitmap[4, 1] { { imagesBlocs[typePiece] }, { imagesBlocs[typePiece] }, { imagesBlocs[typePiece] }, { imagesBlocs[typePiece] } };
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 1; j++)
                        {
                            toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = blocActif[i, j];
                        }
                    }
                }
                else
                {
                    blocActif = new Bitmap[1, 4] { { imagesBlocs[typePiece], imagesBlocs[typePiece], imagesBlocs[typePiece], imagesBlocs[typePiece] } };
                    for (int i = 0; i < 1; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = blocActif[i, j];
                        }
                    }
                }
            }
            else if (typePiece == (int)TypeBloc.T) // Bloc T
            {
                blocActif = new Bitmap[2, 3] { { imagesBlocs[(int)TypeBloc.Aucun], imagesBlocs[typePiece], imagesBlocs[(int)TypeBloc.Aucun] }, { imagesBlocs[typePiece], imagesBlocs[typePiece], imagesBlocs[typePiece] } };
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = blocActif[i, j];
                    }
                }
            }
            else if (typePiece == (int)TypeBloc.L) // Bloc L
            {
                blocActif = new Bitmap[3, 2] { { imagesBlocs[typePiece], imagesBlocs[(int)TypeBloc.Aucun] }, { imagesBlocs[typePiece], imagesBlocs[(int)TypeBloc.Aucun] }, { imagesBlocs[typePiece], imagesBlocs[typePiece] } };
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = blocActif[i, j];
                    }
                }
            }
            else if (typePiece == (int)TypeBloc.J) // Bloc J
            {
                blocActif = new Bitmap[3, 2] { { imagesBlocs[(int)TypeBloc.Aucun], imagesBlocs[typePiece] }, { imagesBlocs[(int)TypeBloc.Aucun], imagesBlocs[typePiece] }, { imagesBlocs[typePiece], imagesBlocs[typePiece] } };
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = blocActif[i, j];
                    }
                }
            }
            else if (typePiece == (int)TypeBloc.S) // Bloc S
            {
                blocActif = new Bitmap[2, 3] { { imagesBlocs[(int)TypeBloc.Aucun], imagesBlocs[typePiece], imagesBlocs[typePiece] }, { imagesBlocs[typePiece], imagesBlocs[typePiece], imagesBlocs[(int)TypeBloc.Aucun] } };
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = blocActif[i, j];
                    }
                }
            }
            else if (typePiece == (int)TypeBloc.Z) // Bloc Z
            {
                blocActif = new Bitmap[2, 3] { { imagesBlocs[typePiece], imagesBlocs[typePiece], imagesBlocs[(int)TypeBloc.Aucun] }, { imagesBlocs[(int)TypeBloc.Aucun], imagesBlocs[typePiece], imagesBlocs[typePiece] } };
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = blocActif[i, j];
                    }
                }
            }

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
            bool peutBouger = true;
            if (deplacement == 65) // Si déplacement à gauche
            {
                for (int i = 0; i < blocActif.GetLength(0); i++)
                {
                    if (colonneCourante == 0)
                    {
                        peutBouger = false;
                    }
                    else
                    if (toutesImagesVisuelles[ligneCourante + i, colonneCourante - 1].Image == imagesBlocs[(int)TypeBloc.Gele])
                    {
                        peutBouger = false;
                    }
                }
            }
            else if (deplacement == 68) // Si déplacement à droite
            {
                for (int i = 0; i < blocActif.GetLength(0); i++)
                {
                    if (colonneCourante + blocActif.GetLength(1) == nbColonnes)
                    {
                        peutBouger = false;
                    }
                    else
                    if (toutesImagesVisuelles[ligneCourante + i, colonneCourante + blocActif.GetLength(1)].Image == imagesBlocs[(int)TypeBloc.Gele])
                    {
                        peutBouger = false;
                    }
                }
            }
            else // Si déplacement vers le bas
            {
                for (int j = 0; j < blocActif.GetLength(1); j++)
                {
                    if (ligneCourante + blocActif.GetLength(0) == nbLignes) // Si le bloc atteint le fond
                    {
                        peutBouger = false;
                        for (int iGeler = 0; iGeler < blocActif.GetLength(0); iGeler++)
                        {
                            for (int jGeler = 0; jGeler < blocActif.GetLength(1); jGeler++)
                            {
                                if (blocActif[iGeler, jGeler] != imagesBlocs[(int)TypeBloc.Aucun])
                                {
                                    blocActif[iGeler, jGeler] = imagesBlocs[(int)TypeBloc.Gele];
                                    toutesImagesVisuelles[ligneCourante + iGeler, colonneCourante + jGeler].Image = blocActif[iGeler, jGeler];
                                }
                            }
                        }
                        ligneCourante = 0;
                        colonneCourante = 4;
                        GenerationPiece();
                    }
                    else if (toutesImagesVisuelles[ligneCourante + blocActif.GetLength(0), colonneCourante + j].Image == imagesBlocs[(int)TypeBloc.Gele])
                    {
                        if (toutesImagesVisuelles[(ligneCourante + blocActif.GetLength(0)) - 1, colonneCourante + j].Image != imagesBlocs[(int)TypeBloc.Aucun])
                        {
                            peutBouger = false;
                            for (int iGeler = 0; iGeler < blocActif.GetLength(0); iGeler++)
                            {
                                for (int jGeler = 0; jGeler < blocActif.GetLength(1); jGeler++)
                                {
                                    if (blocActif[iGeler, jGeler] != imagesBlocs[(int)TypeBloc.Aucun])
                                    {
                                        blocActif[iGeler, jGeler] = imagesBlocs[(int)TypeBloc.Gele];
                                        toutesImagesVisuelles[ligneCourante + iGeler, colonneCourante + jGeler].Image = blocActif[iGeler, jGeler];
                                    }
                                }
                            }
                            ligneCourante = 0;
                            colonneCourante = 4;
                            GenerationPiece();
                        }
                        else
                        {
                            blocActif[blocActif.GetLength(0) - 1, j] = imagesBlocs[(int)TypeBloc.Gele];
                        }
                    }
                }
            }
            return peutBouger;
        }
        // CDThibodeau


        #endregion

        // CDThibodeau
        private void buttonQuitterPartie_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // CDThibodeau

        // CDThibodeau
        /// <summary>
        /// Évènement bougeant le bloc en fonction de la touche appuyé.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitrisForm_KeyDown(object sender, KeyEventArgs e)
        {
            int deplacement = e.KeyValue;
            if (deplacement == 65)
            {
                if (BlocPeutBouger(deplacement) == true)
                {
                    for (int i = 0; i < blocActif.GetLength(0); i++)
                    {
                        for (int j = 0; j < blocActif.GetLength(1); j++)
                        {
                            toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = imagesBlocs[(int)TypeBloc.Aucun];
                        }
                    }
                    colonneCourante -= 1;
                    for (int i = 0; i < blocActif.GetLength(0); i++)
                    {
                        for (int j = 0; j < blocActif.GetLength(1); j++)
                        {
                            toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = blocActif[i, j];
                        }
                    }
                }
            }
            else if (deplacement == 68)
            {
                if ((BlocPeutBouger(deplacement) == true))
                {
                    if ((colonneCourante + blocActif.GetLength(1) < nbColonnes) && (toutesImagesVisuelles[ligneCourante, (colonneCourante + blocActif.GetLength(1))].Image != imagesBlocs[(int)TypeBloc.Gele]))
                    {
                        for (int i = 0; i < blocActif.GetLength(0); i++)
                        {
                            for (int j = 0; j < blocActif.GetLength(1); j++)
                            {
                                toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = imagesBlocs[(int)TypeBloc.Aucun];
                            }
                        }
                        colonneCourante += 1;
                        for (int i = 0; i < blocActif.GetLength(0); i++)
                        {
                            for (int j = 0; j < blocActif.GetLength(1); j++)
                            {
                                toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = blocActif[i, j];
                            }
                        }
                    }
                }
            }
            else if (deplacement == 83)
            {
                if (BlocPeutBouger(deplacement) == true)
                {
                    for (int i = 0; i < blocActif.GetLength(0); i++)
                    {
                        for (int j = 0; j < blocActif.GetLength(1); j++)
                        {
                            toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = imagesBlocs[(int)TypeBloc.Aucun];
                        }
                    }
                    ligneCourante += 1;
                    for (int i = 0; i < blocActif.GetLength(0); i++)
                    {
                        for (int j = 0; j < blocActif.GetLength(1); j++)
                        {
                            toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = blocActif[i, j];
                        }
                    }
                }
            }
        }
        // CDThibodeau

        // CDThibodeau
        /// <summary>
        /// Évènement descendant le bloc à chaque 0.5 seconde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDescente_Tick(object sender, EventArgs e)
        {
            if (BlocPeutBouger(deplacement) == true)
            {
                for (int i = 0; i < blocActif.GetLength(0); i++)
                {
                    for (int j = 0; j < blocActif.GetLength(1); j++)
                    {
                        toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = imagesBlocs[(int)TypeBloc.Aucun];
                    }
                }
                ligneCourante += 1;
                for (int i = 0; i < blocActif.GetLength(0); i++)
                {
                    for (int j = 0; j < blocActif.GetLength(1); j++)
                    {
                        toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].Image = blocActif[i, j];
                    }
                }
            }
        }
        // CDThibodeau
    }
}
