using MaterialDesignThemes.Wpf;
using prgLodigiani.Classi;
using prgLodigiani.Configurazioni;
using prgLodigiani.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace prgLodigiani
{
    public class Squadre : MVVMBase
    {
        //Apertura connessione EntityModel Database
        DBldgEntities ldg = new DBldgEntities();

        public ObservableCollection<CATEGORIA> ElencoCategorie { get; set; }
        public ObservableCollection<ATLETA> ElencoGiocatori { get; set; }
        public ObservableCollection<ATLETA> NumeroAtleti { get; set; }
        public ObservableCollection<VIDEO> ElencoVideo { get; set; }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Campi necessari per la comunicazione con il MVVM MainWindow
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private string _TextNome;
        public string TextNome
        {
            get { return _TextNome; }
            set
            {
                _TextNome = value;
                CambioProprieta("TextNome");
            }
        }

        private string _TextCognome;
        public string TextCognome
        {
            get { return _TextCognome; }
            set
            {
                _TextCognome = value;
                CambioProprieta("TextCognome");
            }
        }

        private double _TextPeso;
        public double TextPeso
        {
            get { return _TextPeso; }
            set
            {
                _TextPeso = value;
                CambioProprieta("TextPeso");
            }
        }

        private int _TextAltezza;
        public int TextAltezza
        {
            get { return _TextAltezza; }
            set
            {
                _TextAltezza = value;
                CambioProprieta("TextAltezza");
            }
        }

        private string _TextRuolo;
        public string TextRuolo
        {
            get { return _TextRuolo; }
            set
            {
                _TextRuolo = value;
                CambioProprieta("TextRuolo");
            }
        }

        private string _MsgSalvataggio;
        public string MsgSalvataggio
        {
            get { return _MsgSalvataggio; }
            set
            {
                if (_MsgSalvataggio != value)
                {
                    _MsgSalvataggio = value;
                    CambioProprieta("MsgSalvataggio");
                }
            }
        }

        private int _TextCategorie;
        public int TextCategorie
        {
            get { return _TextCategorie; }
            set
            {
                _TextCategorie = value;
                CambioProprieta("TextCategorie");
            }
        }

        private int _TextAtleti;
        public int TextAtleti
        {
            get { return _TextAtleti; }
            set
            {
                _TextAtleti = value;
                CambioProprieta("TextAtleti");
            }
        }

        private CATEGORIA _CategoriaSelezionata;
        public CATEGORIA CategoriaSelezionata
        {
            get { return _CategoriaSelezionata; }
            set
            {
                _CategoriaSelezionata = value;

                if (_CategoriaSelezionata == null)
                {
                    _CategoriaSelezionata = null;
                    ElencoGiocatori = null;
                    CambioProprieta("ElencoGiocatori");

                }
                else
                {
                    ElencoGiocatori = new ObservableCollection<ATLETA>
                        (ldg.ATLETA.Where(x => x.CATEGORIA == _CategoriaSelezionata.CATEGORIA1).ToList());
                    CambioProprieta("ElencoGiocatori");

                }

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

                TextNome = value.NOME;
                TextCognome = value.COGNOME;
                TextPeso = (double)value.PESO;
                TextAltezza = (int)value.ALTEZZA;
                TextRuolo = value.RUOLO;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Inizializzazione liste, campi e command
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public Squadre()
        {
            //Inizializzazione liste per il MainWindow
            ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO);
            ElencoCategorie = new ObservableCollection<CATEGORIA>(ldg.CATEGORIA);
            NumeroAtleti = new ObservableCollection<ATLETA>(ldg.ATLETA);

            //Inizializzazione campi per la GridHome
            TextCategorie = ElencoCategorie.Count;
            TextAtleti = NumeroAtleti.Count;

            CommandAggiorna = new RelayCommand(Aggiorna, true);

            CommandAggiungi = new RelayCommand(Aggiungi, true);

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //Dichiarazione e definizione comandi
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public RelayCommand CommandAggiorna { get; set; }
        public void Aggiorna()
        {
            GiocatoreSelezionato.NOME = TextNome.Trim();
            GiocatoreSelezionato.COGNOME = TextCognome.Trim();
            GiocatoreSelezionato.NOMECOMPLETO = TextNome.Trim() + " " + TextCognome.Trim();
            GiocatoreSelezionato.PESO = TextPeso;
            GiocatoreSelezionato.ALTEZZA = TextAltezza;
            if (TextRuolo == null)
            {
                GiocatoreSelezionato.RUOLO = string.Empty;
            }
            else
            {
                GiocatoreSelezionato.RUOLO = TextRuolo.Trim();
            }


            try
            {
                ldg.SaveChanges();

                ElencoGiocatori = null;

                ElencoGiocatori = new ObservableCollection<ATLETA>
                    (ldg.ATLETA.Where(x => x.CATEGORIA == _CategoriaSelezionata.CATEGORIA1).ToList());
                CambioProprieta("ElencoGiocatori");
            }
            catch (Exception ex)
            {
                MsgSalvataggio = "Errore: " + ex.Message;
            }
           
        }

        public RelayCommand CommandAggiungi { get; set; }
        public void Aggiungi()
        {

            ATLETA nuovoGiocatore = new ATLETA()
            {
                NOME = TextNome,
                COGNOME = TextCognome,
                NOMECOMPLETO = TextNome + " " + TextCognome, 
                PESO = TextPeso,
                ALTEZZA = TextAltezza,
                RUOLO = TextRuolo,
                CATEGORIA = CategoriaSelezionata.CATEGORIA1
            };
            ldg.ATLETA.Add(nuovoGiocatore);

            try
            {
                ldg.SaveChanges();

                ElencoGiocatori = null;
                CambioProprieta("ElencoGiocatori");

                ElencoGiocatori = new ObservableCollection<ATLETA>
                    (ldg.ATLETA.Where(x => x.CATEGORIA == _CategoriaSelezionata.CATEGORIA1).ToList());
                CambioProprieta("ElencoGiocatori");
            }
            catch (Exception ex)
            {
                MsgSalvataggio = "Errore: " + ex.Message;
            }

        }

    }



}
