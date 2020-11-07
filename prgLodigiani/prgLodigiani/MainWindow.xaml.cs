using Microsoft.Win32;
using prgLodigiani.Classi;
using prgLodigiani.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prgLodigiani
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ControlloData();

            this.MaxHeight = SystemParameters.WorkArea.Height;

        }

        public string fileName { get; set; }
        public string fileEstensione { get; set; }

        public void ControlloData()
        {
            var today = DateTime.Today;
            var dataScadenza = new DateTime(2020, 12, 31);

            if (today>=dataScadenza)
            {
                ItemHome.IsEnabled = true;
                ItemCategorie.IsEnabled = false;
                ItemInserisciVideo.IsEnabled = false;
                ItemRicercaVideo.IsEnabled = false;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Campi relativi alla NavigationBar
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void GridNavBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                //this.Width = SystemParameters.WorkArea.Width;
                //this.Height = SystemParameters.WorkArea.Height;

            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Minimized;
                //this.Width = SystemParameters.WorkArea.Width;
                //this.Height = SystemParameters.WorkArea.Height;

            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }




        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Campi relativi alla SideBar
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void ButtonOpenSidebar_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenSidebar.Visibility = Visibility.Collapsed;
            ButtonCloseSidebar.Visibility = Visibility.Visible;

            //Panel.SetZIndex(GridInsertVideo, -2);
            //Panel.SetZIndex(GridHome, -2);
            //Panel.SetZIndex(GridTabRicerca, -2);
            //Panel.SetZIndex(GridElaborazione, -2);
            //Panel.SetZIndex(GridUtenti, -2);
            //Panel.SetZIndex(GridImpostazioni, -2);

        }

        private void ButtonCloseSidebar_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenSidebar.Visibility = Visibility.Visible;
            ButtonCloseSidebar.Visibility = Visibility.Collapsed;

        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {

                case "ItemHome":
                    GridHome.Visibility = Visibility.Visible;
                    GridInserisciVideo.Visibility = Visibility.Collapsed;
                    GridCategorie.Visibility = Visibility.Collapsed;
                    GridRicercaVideo.Visibility = Visibility.Collapsed;
                    //GridUtenti.Visibility = Visibility.Collapsed;
                    //GridImpostazioni.Visibility = Visibility.Collapsed;
                    break;
                case "ItemCategorie":
                    GridHome.Visibility = Visibility.Collapsed;
                    GridInserisciVideo.Visibility = Visibility.Collapsed;
                    GridCategorie.Visibility = Visibility.Visible;
                    GridRicercaVideo.Visibility = Visibility.Collapsed;
                    //GridUtenti.Visibility = Visibility.Collapsed;
                    //GridImpostazioni.Visibility = Visibility.Collapsed;
                    break;
                case "ItemInserisciVideo":
                    GridHome.Visibility = Visibility.Collapsed;
                    GridInserisciVideo.Visibility = Visibility.Visible;
                    GridCategorie.Visibility = Visibility.Collapsed;
                    GridRicercaVideo.Visibility = Visibility.Collapsed;
                    //GridUtenti.Visibility = Visibility.Collapsed;
                    //GridImpostazioni.Visibility = Visibility.Collapsed;
                    break;
                case "ItemRicercaVideo":
                    GridHome.Visibility = Visibility.Collapsed;
                    GridInserisciVideo.Visibility = Visibility.Collapsed;
                    GridCategorie.Visibility = Visibility.Collapsed;
                    GridRicercaVideo.Visibility = Visibility.Visible;
                    //GridUtenti.Visibility = Visibility.Collapsed;
                    //GridImpostazioni.Visibility = Visibility.Collapsed;
                    break;
                //case "ItemUtenti":
                //    GridHome.Visibility = Visibility.Collapsed;
                //    GridInsertVideo.Visibility = Visibility.Collapsed;
                //    GridTabRicerca.Visibility = Visibility.Collapsed;
                //    GridElaborazione.Visibility = Visibility.Collapsed;
                //    GridUtenti.Visibility = Visibility.Visible;
                //    GridImpostazioni.Visibility = Visibility.Collapsed;
                //    break;
                //case "ItemImpostazioni":
                //    GridHome.Visibility = Visibility.Collapsed;
                //    GridInsertVideo.Visibility = Visibility.Collapsed;
                //    GridTabRicerca.Visibility = Visibility.Collapsed;
                //    GridElaborazione.Visibility = Visibility.Collapsed;
                //    GridUtenti.Visibility = Visibility.Collapsed;
                //    GridImpostazioni.Visibility = Visibility.Visible;
                //    break;

                default:
                    break;
            }
        }






        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Campi relativi alla GridHome
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------







        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Campi relativi alla GridGestioneCategorie
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void lvCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvCategoria.IsEnabled = false;

            dgSquadra.IsEnabled = true;
            txtNome.IsEnabled = false;
            txtNome.Text = string.Empty;
            txtCognome.IsEnabled = false;
            txtCognome.Text = string.Empty;
            txtPeso.IsEnabled = false;
            txtPeso.Text = null;
            txtAltezza.IsEnabled = false;
            txtAltezza.Text = null;
            txtRuolo.IsEnabled = false;
            txtRuolo.Text = string.Empty;
            GridAtleti.Visibility = Visibility.Visible;
            bModifica.Visibility = Visibility.Collapsed;
            bSalva.Visibility = Visibility.Collapsed;
            bAggiungi.Visibility = Visibility.Collapsed;
            bAnnulla.Visibility = Visibility.Collapsed;
        }

        private void dgSquadra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtNome.IsEnabled = false;
            txtCognome.IsEnabled = false;
            txtPeso.IsEnabled = false;
            txtAltezza.IsEnabled = false;
            txtRuolo.IsEnabled = false;
            bModifica.Visibility = Visibility.Visible;
            bAggiungi.Visibility = Visibility.Collapsed;
            bSalva.Visibility = Visibility.Collapsed;
            bAnnulla.Visibility = Visibility.Collapsed;
        }

        private void bModifica_Click(object sender, RoutedEventArgs e)
        {
            txtNome.IsEnabled = true;
            txtCognome.IsEnabled = true;
            txtPeso.IsEnabled = true;
            txtAltezza.IsEnabled = true;
            txtRuolo.IsEnabled = true;
            bSalva.Visibility = Visibility.Visible;
            bAggiungi.Visibility = Visibility.Collapsed;
            bAnnulla.Visibility = Visibility.Visible;
        }

        private void bAnnulla_Click(object sender, RoutedEventArgs e)
        {
            dgSquadra.SelectedItem = null;
            txtNome.IsEnabled = false;
            txtNome.Text = string.Empty;
            txtCognome.IsEnabled = false;
            txtCognome.Text = string.Empty;
            txtPeso.IsEnabled = false;
            txtPeso.Text = null;
            txtAltezza.IsEnabled = false;
            txtAltezza.Text = null;
            txtRuolo.IsEnabled = false;
            txtRuolo.Text = string.Empty;
            bAggiungi.Visibility = Visibility.Collapsed;
            bSalva.Visibility = Visibility.Collapsed;
            bAnnulla.Visibility = Visibility.Collapsed;

        }

        private void bChangeCat_Click(object sender, RoutedEventArgs e)
        {
            lvCategoria.IsEnabled = true;
            lvCategoria.SelectedItem = null;

            dgSquadra.IsEnabled = false;
            txtNome.IsEnabled = false;
            txtNome.Text = string.Empty;
            txtCognome.IsEnabled = false;
            txtCognome.Text = string.Empty;
            txtPeso.IsEnabled = false;
            txtPeso.Text = null;
            txtAltezza.IsEnabled = false;
            txtAltezza.Text = null;
            txtRuolo.IsEnabled = false;
            txtRuolo.Text = string.Empty;
            GridAtleti.Visibility = Visibility.Collapsed;
            bSalva.Visibility = Visibility.Collapsed;
            bAggiungi.Visibility = Visibility.Collapsed;
            bAnnulla.Visibility = Visibility.Collapsed;
        }

        private void bSalva_Click(object sender, RoutedEventArgs e)
        {
            txtNome.IsEnabled = false;
            txtCognome.IsEnabled = false;
            txtPeso.IsEnabled = false;
            txtAltezza.IsEnabled = false;
            txtRuolo.IsEnabled = false;
            bModifica.Visibility = Visibility.Collapsed;
            bSalva.Visibility = Visibility.Collapsed;
            bAggiungi.Visibility = Visibility.Collapsed;
            bAnnulla.Visibility = Visibility.Collapsed;
        }

        private void bAggiungi_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Text = string.Empty;
            txtCognome.Text = string.Empty;
            txtPeso.Text = null;
            txtAltezza.Text = null;
            txtRuolo.Text = string.Empty;
            bSalva.Visibility = Visibility.Collapsed;
            bModifica.Visibility = Visibility.Collapsed;
            bAggiungi.Visibility = Visibility.Visible;
            bAnnulla.Visibility = Visibility.Visible;
        }

        private void bAddGiocatore_Click(object sender, RoutedEventArgs e)
        {
            dgSquadra.SelectedItem = null;
            txtNome.IsEnabled = true;
            txtNome.Text = string.Empty;
            txtCognome.IsEnabled = true;
            txtCognome.Text = string.Empty;
            txtPeso.IsEnabled = true;
            txtPeso.Text = null;
            txtAltezza.IsEnabled = true;
            txtAltezza.Text = null;
            txtRuolo.IsEnabled = true;
            txtRuolo.Text = string.Empty;
            bAggiungi.Visibility = Visibility.Visible;
            bSalva.Visibility = Visibility.Collapsed;
            bAnnulla.Visibility = Visibility.Visible;
        }



        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Campi relativi alla GridInserisciVideo
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void bSelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "File video (*.mp4, *.avi, *.mpg, *.mpeg, *.mov)|*.mp4, *.avi, *.mpg, *.mpeg, *.mov|All files (*.*)|*.*";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (ofd.ShowDialog() == true)
            {
                directoryFileTextBox.Text = System.IO.Path.GetFullPath(ofd.FileName);
                //lettura del nome del file per il metodo bUpdateFile_Click
                fileName = System.IO.Path.GetFileName(ofd.FileName);
                fileEstensione = System.IO.Path.GetExtension(ofd.FileName);
            }

            bSelectFile.Visibility = Visibility.Collapsed;
            bClearFile.Visibility = Visibility.Visible;
        }

        private void bClearFile_Click(object sender, RoutedEventArgs e)
        {
            bSelectFile.Visibility = Visibility.Visible;
            bClearFile.Visibility = Visibility.Collapsed;

            directoryFileTextBox.Text = string.Empty;
        }

        private void bSelectFileAll_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "File video (*.mp4)|*.mp4|All files (*.*)|*.*";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (ofd.ShowDialog() == true)
            {
                directoryFileAllenamentoTextBox.Text = System.IO.Path.GetFullPath(ofd.FileName);
                //lettura del nome del file per il metodo bUpdateFile_Click
                fileName = System.IO.Path.GetFileName(ofd.FileName);
                fileEstensione = System.IO.Path.GetExtension(ofd.FileName);

            }

            bSelectFileAll.Visibility = Visibility.Collapsed;
            bClearFileAll.Visibility = Visibility.Visible;
        }

        private void bClearFileAll_Click(object sender, RoutedEventArgs e)
        {
            bSelectFileAll.Visibility = Visibility.Visible;
            bClearFileAll.Visibility = Visibility.Collapsed;

            directoryFileAllenamentoTextBox.Text = string.Empty;
        }

        private void bSelectFileClip_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "File video (*.mp4)|*.mp4|All files (*.*)|*.*";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (ofd.ShowDialog() == true)
            {
                directoryFileClipTextBox.Text = System.IO.Path.GetFullPath(ofd.FileName);
                //lettura del nome del file per il metodo bUpdateFile_Click
                fileName = System.IO.Path.GetFileName(ofd.FileName);
                fileEstensione = System.IO.Path.GetExtension(ofd.FileName);

            }

            bSelectFileClip.Visibility = Visibility.Collapsed;
            bClearFileClip.Visibility = Visibility.Visible;
        }

        private void bClearFileClip_Click(object sender, RoutedEventArgs e)
        {
            bSelectFileClip.Visibility = Visibility.Visible;
            bClearFileClip.Visibility = Visibility.Collapsed;

            directoryFileClipTextBox.Text = string.Empty;
        }

        private void rbPartita_Click(object sender, RoutedEventArgs e)
        {
            gridPartita.Visibility = Visibility.Visible;
            gridAllenamento.Visibility = Visibility.Collapsed;
            gridClip.Visibility = Visibility.Collapsed;

        }

        private void rbAllenamento_Click(object sender, RoutedEventArgs e)
        {
            gridPartita.Visibility = Visibility.Collapsed;
            gridAllenamento.Visibility = Visibility.Visible;
            gridClip.Visibility = Visibility.Collapsed;
        }

        private void rbClip_Click(object sender, RoutedEventArgs e)
        {
            gridPartita.Visibility = Visibility.Collapsed;
            gridAllenamento.Visibility = Visibility.Collapsed;
            gridClip.Visibility = Visibility.Visible;
        }

        private void bUploadFile_Click(object sender, RoutedEventArgs e)
        {
            MoveFile mf = new MoveFile();
            var movefileP = mf.moveFile(directoryFileTextBox.Text, fileName, fileEstensione, catP.Text, (DateTime)dateP.SelectedDate, "Partita", avvP.Text, noteP.Text);
            
            if (movefileP)
            {
                txtUploadSuccessP.Visibility = Visibility.Visible;
            }
            else
            {
                txtUploadUnsuccessP.Visibility = Visibility.Visible;
            }

        }

        private void bUploadFileAll_Click(object sender, RoutedEventArgs e)
        {
            MoveFile mf = new MoveFile();
            var movefileAll = mf.moveFile(directoryFileAllenamentoTextBox.Text, fileName, fileEstensione, catAll.Text, (DateTime)dateAll.SelectedDate, "Allenamento", null , noteAll.Text);

            if (movefileAll)
            {
                txtUploadSuccessAll.Visibility = Visibility.Visible;
            }
            else
            {
                txtUploadUnsuccessAll.Visibility = Visibility.Visible;
            }

        }

        private void bUploadFileClip_Click(object sender, RoutedEventArgs e)
        {
            MoveFile mf = new MoveFile();
            var movefileClip = mf.moveFile(directoryFileClipTextBox.Text, fileName, fileEstensione, catClip.Text, (DateTime)dateClip.SelectedDate, "Clip", Convert.ToInt32(txtPlayerId.Text), noteClip.Text);

            if (movefileClip)
            {
                txtUploadSuccessClip.Visibility = Visibility.Visible;
            }
            else
            {
                txtUploadUnsuccessClip.Visibility = Visibility.Visible;
            }

        }

        private void bNewUploadFileP_Click(object sender, RoutedEventArgs e)
        {
            directoryFileTextBox.Text = string.Empty;
            dateP.SelectedDate = null;
            avvP.Text = string.Empty;
            noteP.Text = string.Empty;
            bSelectFile.Visibility = Visibility.Visible;
            bClearFile.Visibility = Visibility.Collapsed;

            txtUploadSuccessP.Visibility = Visibility.Collapsed;
            txtUploadUnsuccessP.Visibility = Visibility.Collapsed;

        }

        private void bNewUploadFileAll_Click(object sender, RoutedEventArgs e)
        {
            directoryFileAllenamentoTextBox.Text = string.Empty;
            dateAll.SelectedDate = null;
            noteAll.Text = string.Empty;
            bSelectFileAll.Visibility = Visibility.Visible;
            bClearFileAll.Visibility = Visibility.Collapsed;

            txtUploadSuccessAll.Visibility = Visibility.Collapsed;
            txtUploadUnsuccessAll.Visibility = Visibility.Collapsed;

        }

        private void bNewUploadFileClip_Click(object sender, RoutedEventArgs e)
        {
            directoryFileClipTextBox.Text = string.Empty;
            dateClip.SelectedDate = null;
            playerClip.SelectedItem = null;
            noteClip.Text = string.Empty;
            bSelectFileClip.Visibility = Visibility.Visible;
            bClearFileClip.Visibility = Visibility.Collapsed;

            txtUploadSuccessClip.Visibility = Visibility.Collapsed;
            txtUploadUnsuccessClip.Visibility = Visibility.Collapsed;

        }







        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Campi relativi alla GridRicercaVideo
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void bPulisciFiltro_Click(object sender, RoutedEventArgs e)
        {
            playerFiltri.SelectedItem = null;
            tipoVFiltri.SelectedItem = null;
        }










        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Campi relativi alla GridFooter
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




    }
}
