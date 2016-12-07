/// Travail pratique 3 «Titris Master»
/// 
/// Auteur: Charles-David Thibodeau
/// 
/// Co-auteur: Pierre Poulin (code d'initialisation de tableau)
/// 
/// Malheureusement, Diego n'a pas vraiment écrit de code, donc toutes les fonctions et valeurs partagées sont fait par l'auteur.
/// 

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using WMPLib;

namespace TP3
{
    public partial class TitrisForm : Form
    {
        public TitrisForm()
        {
            InitializeComponent();
            musiqueJeu.URL = "Resources/SonetMusique/TitrisMasterTheme.mp3";
            musiqueJeu.controls.play();
            soundEffectsGeler.URL = "Resources/SonetMusique/blocGele.mp3";
            soundEffectsGeler.controls.stop();
            soundEffectsLigneRetrait.URL = "Resources/SonetMusique/ligneComplete.mp3";
            soundEffectsLigneRetrait.controls.stop();
            soundEffectFinPartie.URL = "Resources/SonetMusique/GameOverMusic.mp3";
            soundEffectFinPartie.controls.stop();
        }

        #region Valeurs Partagées

        /// <summary>
        ///  Son qui joue lorsqu'une partie est terminée.
        /// </summary>
        WindowsMediaPlayer soundEffectFinPartie = new WindowsMediaPlayer();

        /// <summary>
        /// Variable jouant un son lorsqu'une ligne se fait retirer
        /// </summary>
        WindowsMediaPlayer soundEffectsLigneRetrait = new WindowsMediaPlayer();

        /// <summary>
        /// Variable jouant un son lorsque la pièce se gèle.
        /// </summary>
        WindowsMediaPlayer soundEffectsGeler = new WindowsMediaPlayer();

        /// <summary>
        /// Variable s'occupant de la musique
        /// </summary>
        WindowsMediaPlayer musiqueJeu = new WindowsMediaPlayer();

        /// <summary>
        /// Variable Définissant si la musique est arrêté ou non.
        /// </summary>
        int musiqueActifOuNon;

        /// <summary>
        /// Valeur définissant si le son est arrêté ou non.
        /// </summary>
        int sonActifOuNon;

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
        int ligneCourante;

        /// <summary>
        /// Colonne où se situe le coin en haut à gauche du tableau blocActif.
        /// </summary>
        int colonneCourante;

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

        /// <summary>
        /// Tableau qui va stocker le nombre de pièces de chaque type de pièces générées.
        /// </summary>
        int[] tableauStatistiquesPieces = new int[7];

        /// <summary>
        /// Nouveau nombre de colonnes sélectionné dans le formulaire d'option.
        /// </summary>
        int choixColonnes;

        /// <summary>
        /// Nouveau nombre de lignes sélectionné dans le formulaire d'option.
        /// </summary>
        int choixLignes;

        #endregion

        #region Code fourni (avec modifications)

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
            InitialiserSurfaceDeJeu(nbLignes, nbColonnes);
            colonneCourante = nbColonnes / 2 - 1;
            labelPointage.Text = "0";
            ExecuterTestsUnitaires();
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
                    newPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
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

        #region Tests unitaires
        /// <summary>
        /// Appel des tests unitaires.
        /// </summary>
        void ExecuterTestsUnitaires()
        {
            // CDThibodeau
            TestLigneSeule();
            TestLigneSeuleRetrait();
            TestLignesDoublesConsécutives();
            TestLignesDoublesNonConsécutives();
            TestTroisLignesConsécutives();
            TestQuatreLignesConsécutives();
            // CDThibodeau
        }

        /// <summary>
        /// Test unitaire sur le retrait d'une ligne pleine
        /// </summary>
        void TestLigneSeule()
        {
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Initiallisation de la ligne pleine.
            {
                tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1, j] = 1;
            }
            DecalerLignes();
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Vérification qu'il n'y a rien.
            {
                Debug.Assert(tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1, j] == (int)TypeBloc.Aucun, "Erreur dans le retrait de ligne");
            }
            for (int i = 0; i < tableauJeuDonnees.GetLength(0); i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Nettoyage même si non nécessaire.
                {
                    tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1 - i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Test unitaire sur le retrait et le décalage avec une ligne pleine
        /// </summary>
        void TestLigneSeuleRetrait()
        {
            Random rndBlocs = new Random();
            int[] ligneNonPleine = new int[tableauJeuDonnees.GetLength(1)];
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Initiallisation de la ligne pleine.
            {
                tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1, j] = 1;
            }
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Initiallisation de la ligne non pleine.
            {
                ligneNonPleine[j] = rndBlocs.Next(0, 2);
                tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 2, j] = ligneNonPleine[j];
            }
            DecalerLignes();
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Vérification que la ligne du dessus a bien décalée.
            {
                Debug.Assert(tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1, j] == ligneNonPleine[j],  "Erreur dans le décalage de la ligne seule [ligne seule retrait]");
            }
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Vérification qu'il n'y a rien au dessus.
            {
                Debug.Assert(tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 2, j] == (int)TypeBloc.Aucun, "Erreur dans le retrait de la ligne pleine [ligne seule retrait]");
            }
            for (int i = 0; i < tableauJeuDonnees.GetLength(0); i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Nettoyage même si non nécessaire.
                {
                    tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1 - i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Test unitaire sur le retrait de deux lignes consécutives.
        /// </summary>
        void TestLignesDoublesConsécutives()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Initiallisation des lignes pleines.
                {
                    tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1 - i, j] = 1;
                }
            }
            DecalerLignes();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // S'assurer qu'il n'y a bien rien.
                {
                    Debug.Assert(tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1 - i, j] == (int)TypeBloc.Aucun, "Erreur dans le retrait des deux lignes");
                }
            }
            for (int i = 0; i < tableauJeuDonnees.GetLength(0); i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Nettoyage même si non nécessaire.
                {
                    tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1 - i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Test unitaire sur l'effacement de deux lignes non consécutives.
        /// </summary>
        void TestLignesDoublesNonConsécutives()
        {
            Random rndBlocs = new Random();
            int[] ligneNonPleine = new int[tableauJeuDonnees.GetLength(1)];
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Initiallisation de la première ligne pleine.
            {
                tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1, j] = 1;
            }
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Initiallisation de la ligne non pleine.
            {
                ligneNonPleine[j] = rndBlocs.Next(0, 2);
                tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 2, j] = ligneNonPleine[j];
            }
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Initiallisation de la deuxième ligne pleine.
            {
                tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 3, j] = 1;
            }
            DecalerLignes();
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // S'assurer que la ligne décalé est bien au fond.
            {
                Debug.Assert(tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1, j] == ligneNonPleine[j], "Erreur dans le décalage");
            }
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // S'assurer qu'il n'y a bien rien au dessus de la ligne avec des blocs.
            {
                Debug.Assert(tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 2, j] == (int)TypeBloc.Aucun, "Erreur dans le décalage");
            }
            for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // S'assurer qu'il n'y a bien rien au dessus de la ligne avec des blocs.
            {
                Debug.Assert(tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 3, j] == (int)TypeBloc.Aucun, "Erreur dans le décalage");
            }
            for (int i = 0; i < tableauJeuDonnees.GetLength(0); i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Nettoyage même si non nécessaire.
                {
                    tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1 - i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Test unitaire sur le retrait de trois lignes consécutives.
        /// </summary>
        void TestTroisLignesConsécutives()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Initiallisation des lignes pleines.
                {
                    tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1 - i, j] = 1;
                }
            }
            DecalerLignes();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // S'assurer qu'il n'y a bien rien.
                {
                    Debug.Assert(tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1 - i, j] == (int)TypeBloc.Aucun, "Erreur dans le décalage");
                }
            }
            for (int i = 0; i < tableauJeuDonnees.GetLength(0); i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Nettoyage même si non nécessaire.
                {
                    tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1 - i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Test unitaire sur le retrait de quatre lignes consécutives.
        /// </summary>
        void TestQuatreLignesConsécutives()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Initiallisation des lignes pleines.
                {
                    tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1 - i, j] = 1;
                }
            }
            DecalerLignes();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // S'assurer qu'il n'y a bien rien.
                {
                    Debug.Assert(tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1 - i, j] == (int)TypeBloc.Aucun, "Erreur dans le décalage");
                }
            }
            for (int i = 0; i < tableauJeuDonnees.GetLength(0); i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++) // Nettoyage même si non nécessaire.
                {
                    tableauJeuDonnees[tableauJeuDonnees.GetLength(0) - 1 - i, j] = 0;
                }
            }
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
                    {
                        blocActif = new int[4, 1] { { 3 }, { 3 }, { 3 }, { 3 } };
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
                tableauStatistiquesPieces[typePiece - 2] += 1;
            if (BlocPeutBouger('s') == true)
            {
                AfficherJeu();
            }
            else
            {
                AfficherJeu();
                FinirPartie();
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
            int offsetX = 0;
            int offsetY = 0;
            if(deplacement == 'a') // Si déplacement à gauche
            {
                offsetX = -1;
            }
            else if (deplacement == 'd') // Si déplacement à droite
            {
                offsetX = 1;
            }
            else if (deplacement == 's') // Si déplacement vers le bas
            {
                offsetY=1;
            }
            bool peutBouger = true;
            for (int i = 0; i < blocActif.GetLength(0); i++)
            {
                for (int j = 0; j < blocActif.GetLength(1); j++)
                {
                  if(blocActif[i,j] != 0) // Si bloc est réel
                    {
                        // Si dans les limites du tableau
                        peutBouger = peutBouger && (colonneCourante + j + offsetX) < nbColonnes;
                        peutBouger = peutBouger && (colonneCourante + j + offsetX) >= 0;
                        peutBouger = peutBouger && (ligneCourante + i + offsetY) < nbLignes;
                        // Si le prochain bloc n'est pas gelé
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
        /// <summary>
        /// Évènement descendant le bloc à chaque 0.5 seconde.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerDescente_Tick(object sender, EventArgs e)
        {
            if (musiqueJeu.playState == WMPPlayState.wmppsStopped && musiqueActifOuNon != 1) // Si la musique n'est pas désactivée
            {
                musiqueJeu.controls.play();
            }
            BougerPiece('s');
        }
        // CDThibodeau

        // CDThibodeau
        /// <summary>
        /// Fonction affichant le bloc sur le tableau de jeu en fonction de la position des blocs.
        /// </summary>
        void AfficherJeu()
        {
            for (int i = 0; i < toutesImagesVisuelles.GetLength(0); i++)
            {
                for (int j = 0; j < toutesImagesVisuelles.GetLength(1); j++)
                {
                    if (tableauJeuDonnees[i, j] == (int)TypeBloc.Aucun) // Si la donnée à la position [i, j] est 0...
                    {
                        toutesImagesVisuelles[i, j].BackgroundImage = imagesBlocs[(int)TypeBloc.Aucun]; // ...image de fond revient rien.
                    }
                    else
                    {
                        toutesImagesVisuelles[i, j].BackgroundImage = imagesBlocs[(int)TypeBloc.Gele]; // image de fond devient gelée.
                    }
                }
            }

            for (int i = 0; i < blocActif.GetLength(0); i++)
            {
                for (int j = 0; j < blocActif.GetLength(1); j++)
                {
                    if (ligneCourante + blocActif.GetLength(0) - 1 != nbLignes) // Si le bloc n'atteint pas le fond
                    {
                        if (blocActif[i, j] != (int)TypeBloc.Aucun) // Si la partie du bloc n'est pas rien
                        {
                            toutesImagesVisuelles[ligneCourante + i, colonneCourante + j].BackgroundImage = imagesBlocs[typePiece];
                        }
                        
                            
                    }
                }
            }   
        }
        //CDThibodeau

        // CDThibodeau
        /// <summary>
        /// Fonction gelant le bloc actif lorsqu'il ne peut plus descendre.
        /// </summary>
        void GelerPiece()
        {
            if (BlocPeutBouger('s') == false) // Si le bloc ne peut plus bouger
            {
                for (int i = 0; i < blocActif.GetLength(0); i++)
                {
                    for (int j = 0; j < blocActif.GetLength(1); j++)
                    {
                        if (blocActif[i,j] != (int)TypeBloc.Aucun) // Si la partie du bloc n'est pas rien...
                        {
                            tableauJeuDonnees[ligneCourante + i, colonneCourante + j] = (int)TypeBloc.Gele; // ... partie devient gelée.
                        }

                    }
                }
                if (sonActifOuNon == 0) // Si le son est actif
                {
                    soundEffectsGeler.controls.play();
                }
                pointage += DecalerLignes();
                labelPointage.Text = pointage.ToString();
                ligneCourante = 0;
                colonneCourante = (nbColonnes/2) - 1;
                GenerationPiece();
            }
        }
        // CDThibodeau

        //CDThibodeau
        /// <summary>
        /// Fonction bougeant la pièce en fonction de la touche appuyée.
        /// </summary>
        /// <param name="deplacement"></param>
        void BougerPiece(int deplacement)
        {
            int offsetX = 0;
            int offsetY = 0;
            if (deplacement == 'a') // Si déplacement gauche
            {
                offsetX = -1;
            }
            else if (deplacement == 'd') // Si déplacement droite
            {
                offsetX = 1;
            }
            else if (deplacement == 's') // Si déplacement bas
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
            {
                GelerPiece();
            }
            
        }
        // CDThibodeau

        // CDThibodeau
        /// <summary>
        /// Fonction appelant une autre fonction lorsqu'une touche est appuyée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TitrisForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            BougerPiece(e.KeyChar);
        }
        // CDThibodeau

        // CDThibodeau
        /// <summary>
        /// Fonction qui décale les lignes si le jeu contient une ligne pleine.
        /// </summary>
        /// <returns>Retourne le nombre de lignes pleines retirée si le jeu doit en retirer.</returns>
        int DecalerLignes()
        {
            int[] ligneAVerifier = new int[tableauJeuDonnees.GetLength(1)];
            int nbLignesRetires = 0;
            bool decalageEstPossible = false;
            int ligneADecaler;
            for (int i = 0; i < tableauJeuDonnees.GetLength(0); i++)
            {
                for (int j = 0; j < tableauJeuDonnees.GetLength(1); j++)
                {
                    ligneAVerifier[j] = tableauJeuDonnees[(tableauJeuDonnees.GetLength(0)-1) - i, j]; // Initiallisation de la ligne à analyser
                }
                decalageEstPossible = VerifierLignesPleines(ligneAVerifier);
                if (decalageEstPossible == true)
                {
                    if (sonActifOuNon == 0) // Si le son est actif
                    {
                        soundEffectsLigneRetrait.controls.play();
                    }
                    ligneADecaler = i;
                    RetirerLigne(ligneADecaler);
                    for (int iDecalage = 0; iDecalage < (tableauJeuDonnees.GetLength(0) - 1 - ligneADecaler); iDecalage++)
                    {
                        for (int jDecalage = 0; jDecalage < tableauJeuDonnees.GetLength(1); jDecalage++)
                        {
                            toutesImagesVisuelles[(toutesImagesVisuelles.GetLength(0) - 1 - ligneADecaler) - iDecalage, jDecalage].BackgroundImage = toutesImagesVisuelles[(toutesImagesVisuelles.GetLength(0) - 2 - ligneADecaler) - iDecalage, jDecalage].BackgroundImage; // Décalage des images
                            tableauJeuDonnees[(tableauJeuDonnees.GetLength(0) - 1 - ligneADecaler) - iDecalage, jDecalage] = tableauJeuDonnees[(tableauJeuDonnees.GetLength(0) - 2 - ligneADecaler) - iDecalage, jDecalage]; // Décalage des données
                        }
                    }
                    nbLignesRetires += 10; // 10 points~!
                    i--; // Revérification
                }
            }
            return nbLignesRetires;
        }
        // CDThibodeau

        // CDThibodeau
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
                if (ligneAVerifier[j] != (int)TypeBloc.Gele) // Si un bloc n'est pas gelé
                {
                    decalageEstPossible = false;
                }
            }
            return decalageEstPossible;
        }
        // CDThibodeau

        // CDThibodeau
        /// <summary>
        /// Finction retirant une ligne pleine retrouvé dans le jeu.
        /// </summary>
        /// <param name="ligneADecaler">Ligne au-dessus de celle qui se fait effacer. Celle qui va commencer le décallement, en gros.</param>
        void RetirerLigne(int ligneADecaler)
        {
            for (int jDecalage = 0; jDecalage < tableauJeuDonnees.GetLength(1); jDecalage++)
            {
                toutesImagesVisuelles[(toutesImagesVisuelles.GetLength(0) - 1 - ligneADecaler), jDecalage].BackgroundImage = imagesBlocs[(int)TypeBloc.Aucun]; // Effacement des images formant la ligne
                tableauJeuDonnees[(tableauJeuDonnees.GetLength(0) - 1 - ligneADecaler), jDecalage] = (int)TypeBloc.Aucun; // Effacement des données formant la ligne
            }
        }


        // CDThibodeau
        /// <summary>
        /// Fonction faisant apparaître le formulaire des options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options optionMenu = new Options(this, nbColonnes, nbLignes);
            musiqueJeu.controls.stop();
            timerDescente.Stop();
            DialogResult optionClicBouton = optionMenu.ShowDialog();
            timerDescente.Start();
            if (musiqueActifOuNon == 0) // Si la music est active
            {
                musiqueJeu.controls.play();
            }
        }
        // CDThibodeau

        /// <summary>
        /// Fonction appliquant les options de l'autre formulaire à celui-ci.
        /// </summary>
        /// <param name="colonnesChoix"> entier, ouveau nombre de colonnes du niveau. </param>
        /// <param name="lignesChoix"> entier, nouveau nombre de lignes. </param>
        /// <param name="sonCocheOuPas"> entier, valeur représentant si le son doit être arrêté ou non.</param>
        /// <param name="musiqueCocheOuPas"> entier, valeur représentant si la musique doit être arrêté ou non.</param>
        public void AppliquerOptions(int sonCocheOuPas, int musiqueCocheOuPas)
        {
            timerDescente.Start();
            sonActifOuNon = sonCocheOuPas;
            musiqueActifOuNon = musiqueCocheOuPas;
            if (musiqueActifOuNon == 0) // Si musique est active
            {
                musiqueJeu.controls.play();
            }
        }

        /// <summary>
        /// Fonction annonçant la fin d'une partie.
        /// </summary>
        void FinirPartie()
        {
            timerDescente.Enabled = false;
            musiqueJeu.controls.stop();
            for (int i = 0; i < toutesImagesVisuelles.GetLength(0); i++)
            {
                for (int j = 0; j < toutesImagesVisuelles.GetLength(1); j++)
                {
                    toutesImagesVisuelles[i, j].BackgroundImage = imagesBlocs[(int)TypeBloc.Gele]; // Effet où le tableau est recouvert
                    toutesImagesVisuelles[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            labelFinPartie.Visible = true; // Le supposé formulaire de fin de partie (voir document word pour explication)
            soundEffectFinPartie.controls.play();
            buttonQuitterPartie.Visible = false;
            labelRejouer.Visible = true;
            buttonOui.Visible = true;
            buttonNon.Visible = true;
        }

        /// <summary>
        /// Au clic, la partie redémarre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOui_Click(object sender, EventArgs e)
        {
            labelFinPartie.Visible = false;
            musiqueJeu.controls.play();
            buttonQuitterPartie.Visible = true;
            labelRejouer.Visible = false;
            buttonOui.Visible = false;
            buttonNon.Visible = false;
            InitialiserSurfaceDeJeu(nbLignes, nbColonnes);
            colonneCourante = nbColonnes / 2 - 1;
            labelPointage.Text = "0";
            ExecuterTestsUnitaires();
            timerDescente.Enabled = true;
            GenerationPiece(); // Redémarrage normal
        }

        /// <summary>
        /// Au clic, l'application se ferme.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Redémarrage du jeu en quittant les options.
        /// </summary>
        /// <param name="colonnesChoix">Nouveau choix de colonnes.</param>
        /// <param name="lignesChoix">Nouveau choix de lignes.</param>
        public void RedemarrageJeuOptions(int colonnesChoix, int lignesChoix)
        {
            choixColonnes = colonnesChoix;
            choixLignes = lignesChoix;
            nbLignes = choixLignes;
            nbColonnes = choixColonnes;
            labelFinPartie.Visible = false;
            musiqueJeu.controls.play();
            soundEffectFinPartie.controls.stop();
            buttonQuitterPartie.Visible = true;
            labelRejouer.Visible = false;
            buttonOui.Visible = false;
            buttonNon.Visible = false;
            InitialiserSurfaceDeJeu(nbLignes, nbColonnes);
            colonneCourante = nbColonnes / 2 - 1;
            labelPointage.Text = "0";
            ExecuterTestsUnitaires();
            timerDescente.Enabled = true;
            GenerationPiece(); // Redémarrage avec le nouveau tableau
        }

        /// <summary>
        /// Au cic, la partie redémarre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void redémarrerJeuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelFinPartie.Visible = false;
            musiqueJeu.controls.play();
            buttonQuitterPartie.Visible = true;
            labelRejouer.Visible = false;
            buttonOui.Visible = false;
            buttonNon.Visible = false;
            InitialiserSurfaceDeJeu(nbLignes, nbColonnes);
            colonneCourante = nbColonnes / 2 - 1;
            labelPointage.Text = "0";
            ExecuterTestsUnitaires();
            timerDescente.Enabled = true;
            GenerationPiece(); // Redémarrage normal
        }
    }
}
