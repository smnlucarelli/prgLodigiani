using prgLodigiani.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace prgLodigiani.Classi
{
    public class MoveFile : MVVMBase
    {
        //Apertura connessione EntityModel Database
        DBldgEntities ldg = new DBldgEntities();
        //Dichiarazione liste
        public ObservableCollection<VIDEO> ElencoVideo { get; set; }

        public ObservableCollection<CATEGORIA> ElencoCategorie { get; set; }

        public ObservableCollection<ATLETA> ElencoAtleti { get; set; }

        private CATEGORIA _CategoriaSelezionata;
        public CATEGORIA CategoriaSelezionata
        {
            get { return _CategoriaSelezionata; }
            set
            {
                    _CategoriaSelezionata = value;
                    ElencoAtleti = new ObservableCollection<ATLETA>
                        (ldg.ATLETA.Where(x => x.CATEGORIA == _CategoriaSelezionata.CATEGORIA1).ToList());
                    CambioProprieta("ElencoAtleti");

                //string catSelect = value.Categoria;
                //ElencoGiocatori = new ObservableCollection<tbGiocatore>();
                //ElencoGiocatori = elencoGiocatori(catSelect);
            }
        }

        private ATLETA _GiocatoreSelezionato;
        public ATLETA GiocatoreSelezionato
        {
            get { return _GiocatoreSelezionato; }
            set
            {
                _GiocatoreSelezionato = value;
                CambioProprieta("GiocatoreSelezionato");

                //string catSelect = value.Categoria;
                //ElencoGiocatori = new ObservableCollection<tbGiocatore>();
                //ElencoGiocatori = elencoGiocatori(catSelect);
            }
        }



        private Visibility _TextUploadSuccess;
        public Visibility TextUploadSuccess
        {
            get { return _TextUploadSuccess; }
            set
            {
                _TextUploadSuccess = value;
                CambioProprieta("TextUploadSuccess");
            }
        }

        private Visibility _TextUploadUnsuccess;
        public Visibility TextUploadUnsuccess
        {
            get { return _TextUploadUnsuccess; }
            set
            {
                _TextUploadUnsuccess = value;
                CambioProprieta("TextUploadUnsuccess");
            }
        }


        public MoveFile()
        {
            ElencoCategorie = new ObservableCollection<CATEGORIA>(ldg.CATEGORIA);

            TextUploadSuccess = Visibility.Collapsed;
            //CambioProprieta("TextUploadSuccess");
            TextUploadUnsuccess = Visibility.Collapsed;
            //CambioProprieta("TextUploadUnsuccess");
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Metodo che sposta il file video dalla cartella origine alla cartella destinazione
        /// </summary>
        /// <param name="dirFile"> Cartella iniziale del file </param>
        /// <param name="nomeFile"> Nome del file </param>
        /// <param name="cat"> Categoria </param>
        /// <param name="date"> Data del video </param>
        /// <param name="tipoV"> Tipologia del video (partita, allenamento, clip) </param>
        /// <param name="avv"> Avversario </param>
        /// <param name="note"> Note </param>
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public bool moveFile(string dirFile, string nomeFile, string estensioneFile, string cat, DateTime date, string tipoV, string avv = null, string note = null)
        {
            bool bEsito = false;
            //Inizializzazione variabili di anno e mese
            int year = date.Year;
            int month = date.Month;
            string yearRif = "";
            string monthRif = "";
            string shortYear = "";
            string shortMonth = "";


            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // Definizione mese e anno di riferimento in base alla data inserita
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            //In base al mese del parametro date prendiamo un case
            switch (month)
            {
                case 1:
                    monthRif = "06-gennaio";
                    shortMonth = "01";
                    break;
                case 2:
                    monthRif = "07-febbraio";
                    shortMonth = "02";
                    break;
                case 3:
                    monthRif = "08-marzo";
                    shortMonth = "03";
                    break;
                case 4:
                    monthRif = "09-aprile";
                    shortMonth = "04";
                    break;
                case 5:
                    monthRif = "10-maggio";
                    shortMonth = "05";
                    break;
                case 6:
                    monthRif = "11-giugno";
                    shortMonth = "06";
                    break;
                case 7:
                    monthRif = "12-luglio";
                    shortMonth = "07";
                    break;
                case 8:
                    monthRif = "01-agosto";
                    shortMonth = "08";
                    break;
                case 9:
                    monthRif = "02-settembre";
                    shortMonth = "09";
                    break;
                case 10:
                    monthRif = "03-ottobre";
                    shortMonth = "10";
                    break;
                case 11:
                    monthRif = "04-novembre";
                    shortMonth = "11";
                    break;
                case 12:
                    monthRif = "05-dicembre";
                    shortMonth = "12";
                    break;
                default:
                    monthRif = "01-agosto";
                    shortMonth = "08";
                    break;
            }

            //se il mese è maggiore o uguale ad agosto, l'anno sportivo di riferimento sarà a cavallo di due anni consecutivi
            //altrimenti sarà ancora l'anno sportivo precedente
            if (month >= 8)
            {
                yearRif = year + "-" + (year + 1);
                //tempYear e shortYear sono variabili temporanee per consentire di comporre la rinomina del file secondo regola stabilita
                //int tempYear = year + 1;
                shortYear = yearRif.ToString().Substring(2, 2) + yearRif.ToString().Substring(7, 2);
                //shortYear = shortYear + yearRif.ToString().Substring(7, 2);

            }
            else
            {
                yearRif = (year - 1) + "-" + year;
                //tempYear e shortYear sono variabili temporanee per consentire di comporre la rinomina del file secondo regola stabilita
                //int tempYear = year - 1;
                shortYear = yearRif.ToString().Substring(2, 2) + yearRif.ToString().Substring(7, 2);
                //shortYear = tempYear.ToString().Substring(2, 2) + shortYear;

            }

            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // Definizione directory finale e definizione metodo File.Move
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            nomeFile = shortYear + shortMonth + tipoV.Substring(0, 1).ToUpper() + "U" + cat.Trim().Substring(11, 2);

            var QueryVideo = ldg.VIDEO.Where(x => x.VIDEO1.Contains(nomeFile)).Count();

            nomeFile = nomeFile + (QueryVideo + 1).ToString() + estensioneFile;


            //Directory finale dal file
            string dirFinale = @"C:\Users\user06\Desktop\Lodigiani\" + cat.Trim() + @"\" + yearRif + @"\" + monthRif + @"\" + tipoV + @"\" + nomeFile;

            try
            {
                //Spostamento del file
                File.Move(dirFile, dirFinale);

                if (File.Exists(dirFinale))
                {
                    ElencoAtleti = new ObservableCollection<ATLETA>(ldg.ATLETA
                        .Where(x => x.CATEGORIA == cat));
                    foreach (var item in ElencoAtleti)
                    {
                        VIDEO nuovoVideo = new VIDEO()
                        {
                            VIDEO1 = nomeFile,
                            URL = dirFinale,
                            TIPO = tipoV,
                            DATA = date,
                            CATEGORIA = cat,
                            ANNO = yearRif,
                            MESE = monthRif,
                            ATLETA = item.ID,
                            AVVERSARIO = avv,
                            NOTE = note
                        };
                        ldg.VIDEO.Add(nuovoVideo);


                        ldg.SaveChanges();

                        bEsito = true;

                    }

                    //ElencoVideo = null;
                    //CambioProprieta("ElencoVideo");

                    //ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO);
                    //CambioProprieta("ElencoVideo");
                }
                else
                {
                    bEsito = false;
                }

                //Creazione record tabella VIDEO con i dettagli del video caricato

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return bEsito;

        }

        public bool moveFile(string dirFile, string nomeFile, string estensioneFile, string cat, DateTime date, string tipoV, int player, string note = null)
        {
            bool bEsito = false;
            //Inizializzazione variabili di anno e mese
            int year = date.Year;
            int month = date.Month;
            string yearRif = "";
            string monthRif = "";
            string shortYear = "";
            string shortMonth = "";


            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // Definizione mese e anno di riferimento in base alla data inserita
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            //In base al mese del parametro date prendiamo un case
            switch (month)
            {
                case 1:
                    monthRif = "06-gennaio";
                    shortMonth = "01";
                    break;
                case 2:
                    monthRif = "07-febbraio";
                    shortMonth = "02";
                    break;
                case 3:
                    monthRif = "08-marzo";
                    shortMonth = "03";
                    break;
                case 4:
                    monthRif = "09-aprile";
                    shortMonth = "04";
                    break;
                case 5:
                    monthRif = "10-maggio";
                    shortMonth = "05";
                    break;
                case 6:
                    monthRif = "11-giugno";
                    shortMonth = "06";
                    break;
                case 7:
                    monthRif = "12-luglio";
                    shortMonth = "07";
                    break;
                case 8:
                    monthRif = "01-agosto";
                    shortMonth = "08";
                    break;
                case 9:
                    monthRif = "02-settembre";
                    shortMonth = "09";
                    break;
                case 10:
                    monthRif = "03-ottobre";
                    shortMonth = "10";
                    break;
                case 11:
                    monthRif = "04-novembre";
                    shortMonth = "11";
                    break;
                case 12:
                    monthRif = "05-dicembre";
                    shortMonth = "12";
                    break;
                default:
                    monthRif = "01-agosto";
                    shortMonth = "08";
                    break;
            }

            //se il mese è maggiore o uguale ad agosto, l'anno sportivo di riferimento sarà a cavallo di due anni consecutivi
            //altrimenti sarà ancora l'anno sportivo precedente
            if (month >= 8)
            {
                yearRif = year + "-" + (year + 1);
                //tempYear e shortYear sono variabili temporanee per consentire di comporre la rinomina del file secondo regola stabilita
                //int tempYear = year + 1;
                shortYear = yearRif.ToString().Substring(2, 2) + yearRif.ToString().Substring(7, 2);
                //shortYear = shortYear + yearRif.ToString().Substring(7, 2);

            }
            else
            {
                yearRif = (year - 1) + "-" + year;
                //tempYear e shortYear sono variabili temporanee per consentire di comporre la rinomina del file secondo regola stabilita
                //int tempYear = year - 1;
                shortYear = yearRif.ToString().Substring(2, 2) + yearRif.ToString().Substring(7, 2);
                //shortYear = tempYear.ToString().Substring(2, 2) + shortYear;

            }

            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // Definizione directory finale e definizione metodo File.Move
            //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            nomeFile = shortYear + shortMonth + tipoV.Substring(0, 1).ToUpper() + "U" + cat.Trim().Substring(11, 2);

            var QueryVideo = ldg.VIDEO.Where(x => x.VIDEO1.Contains(nomeFile)).Count();

            nomeFile = nomeFile + (QueryVideo + 1).ToString() + estensioneFile;


            //Directory finale dal file
            string dirFinale = @"C:\Users\user06\Desktop\Lodigiani\" + cat.Trim() + @"\" + yearRif + @"\" + monthRif + @"\" + tipoV + @"\" + nomeFile;

            try
            {
                //Spostamento del file
                File.Move(dirFile, dirFinale);

                if (File.Exists(dirFinale))
                {
                    VIDEO nuovoVideo = new VIDEO()
                    {
                        VIDEO1 = nomeFile,
                        URL = dirFinale,
                        TIPO = tipoV,
                        DATA = date,
                        CATEGORIA = cat,
                        ANNO = yearRif,
                        MESE = monthRif,
                        ATLETA = player,
                        AVVERSARIO = null,
                        NOTE = note
                    };
                    ldg.VIDEO.Add(nuovoVideo);


                    ldg.SaveChanges();

                    bEsito = true;

                    //TextUploadSuccess = Visibility.Visible;
                    //CambioProprieta("TextUploadSuccess");

                    //ElencoVideo = null;
                    //CambioProprieta("ElencoVideo");

                    //ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO);
                    //CambioProprieta("ElencoVideo");
                }
                else
                {
                    bEsito = false;
                    //TextUploadUnsuccess = Visibility.Visible;
                    //CambioProprieta("TextUploadUnsuccess");
                }


                //Creazione record tabella VIDEO con i dettagli del video caricato

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return bEsito;
        }

    }
}
