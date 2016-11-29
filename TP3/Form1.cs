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
        /// 
        /// </summary>
        Bitmap[] imagesBlocs = new Bitmap[] { Properties.Resources.rien, Properties.Resources.freeze, Properties.Resources.carreBloc, Properties.Resources.barreBloc, Properties.Resources.TBloc, Properties.Resources.LBloc, Properties.Resources.JBloc, Properties.Resources.SBloc, Properties.Resources.ZBloc };

        /// <summary>
        /// 
        /// </summary>
        Bitmap[,] blocActif;

        /// <summary>
        /// 
        /// </summary>
        int ligneCourante = 0;

        /// <summary>
        /// 
        /// </summary>
        int colonneCourante = 4;

        /// <summary>
        /// 
        /// </summary>
        int nbColonnes = 10;

        /// <summary>
        /// 
        /// </summary>
        int nbLignes = 20;
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
            Random rnd = new Random();
            int typePiece = rnd.Next(2, 9);
            GenerationPiece(typePiece);
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
        void GenerationPiece(int typePiece)
        {
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


        #endregion

        // CDThibodeau
        private void buttonQuitterPartie_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // CDThibodeau
    }
}
