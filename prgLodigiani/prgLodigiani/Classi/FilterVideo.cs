using prgLodigiani.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prgLodigiani.Classi
{
    public class FilterVideo : MVVMBase
    {
        DBldgEntities ldg = new DBldgEntities();

        public ObservableCollection<CATEGORIA> FiltroCategorie { get; set; }
        public ObservableCollection<ATLETA> FiltroGiocatori { get; set; }

        public ObservableCollection<VIDEO> ElencoVideo { get; set; }


        private CATEGORIA _CategoriaFiltrata;
        public CATEGORIA CategoriaFiltrata
        {
            get { return _CategoriaFiltrata; }
            set
            {
                _CategoriaFiltrata = value;
                FiltroGiocatori = new ObservableCollection<ATLETA>
                    (ldg.ATLETA.Where(x => x.CATEGORIA == _CategoriaFiltrata.CATEGORIA1).ToList());
                CambioProprieta("FiltroGiocatori");

                //string catSelect = value.Categoria;
                //ElencoGiocatori = new ObservableCollection<tbGiocatore>();
                //ElencoGiocatori = elencoGiocatori(catSelect);
            }
        }

        private ATLETA _GiocatoreFiltrato;
        public ATLETA GiocatoreFiltrato
        {
            get { return _GiocatoreFiltrato; }
            set
            {
                _GiocatoreFiltrato = value;

                //string catSelect = value.Categoria;
                //ElencoGiocatori = new ObservableCollection<tbGiocatore>();
                //ElencoGiocatori = elencoGiocatori(catSelect);
            }
        }

        private string _TipoVideoFiltrato;
        public string TipoVideoFiltrato
        {
            get { return _TipoVideoFiltrato; }
            set
            {
                _TipoVideoFiltrato = value;

                //string catSelect = value.Categoria;
                //ElencoGiocatori = new ObservableCollection<tbGiocatore>();
                //ElencoGiocatori = elencoGiocatori(catSelect);
            }
        }

        private int _TextVideo;
        public int TextVideo
        {
            get { return _TextVideo; }
            set
            {
                _TextVideo = value;
                CambioProprieta("TextVideo");
            }
        }

        private bool _IsComboBoxEnable;
        public bool IsComboBoxEnable
        {
            get { return _IsComboBoxEnable; }
            set
            {
                if (_IsComboBoxEnable != value)
                {
                    _IsComboBoxEnable = value;
                    CambioProprieta("IsComboBoxEnable");
                }

            }
        }

        private bool _IsToggleEnable;
        public bool IsToggleEnable
        {
            get { return _IsToggleEnable; }
            set
            {
                if (_IsToggleEnable != value)
                {
                    _IsToggleEnable = value;
                    CambioProprieta("IsToggleEnable");
                    IsComboBoxEnable = _IsToggleEnable;
                }

                if (_IsToggleEnable == false)
                {
                    TipoVideoFiltrato = null;

                }

            }
        }


        public FilterVideo()
        {
            IsToggleEnable = false;
            IsComboBoxEnable = false;

            ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO);

            FiltroCategorie = new ObservableCollection<CATEGORIA>(ldg.CATEGORIA);

            TextVideo = ElencoVideo.Count;

            CommandFiltro = new RelayCommand(Filtro, true);
            CommandPulisciFiltro = new RelayCommand(PulisciFiltro, true);
            CommandAggiorna = new RelayCommand(Aggiorna, true);

        }

        public RelayCommand CommandFiltro { get; set; }
        public void Filtro()
        {
            if (CategoriaFiltrata == null && TipoVideoFiltrato == null)
            {
                ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO);
                CambioProprieta("ElencoVideo");
            }
            else if (CategoriaFiltrata != null && GiocatoreFiltrato == null && TipoVideoFiltrato == null)
            {
                ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO
                    .Where(x => x.CATEGORIA == CategoriaFiltrata.CATEGORIA1)
                    .ToList());
                CambioProprieta("ElencoVideo");
            }
            else if (CategoriaFiltrata != null && GiocatoreFiltrato != null && TipoVideoFiltrato == null)
            {
                ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO
                    .Where(x => x.CATEGORIA == CategoriaFiltrata.CATEGORIA1 && x.ATLETA == GiocatoreFiltrato.ID)
                    .ToList());
                CambioProprieta("ElencoVideo");

            }
            else if (CategoriaFiltrata != null && GiocatoreFiltrato == null && TipoVideoFiltrato != null)
            {
                ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO
                    .Where(x => x.CATEGORIA == CategoriaFiltrata.CATEGORIA1 && x.TIPO == TipoVideoFiltrato)
                    .ToList());
                CambioProprieta("ElencoVideo");

            }
            else if (CategoriaFiltrata != null && GiocatoreFiltrato != null && TipoVideoFiltrato != null)
            {
                ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO
                    .Where(x => x.CATEGORIA == CategoriaFiltrata.CATEGORIA1 && x.ATLETA == GiocatoreFiltrato.ID && x.TIPO == TipoVideoFiltrato)
                    .ToList());
                CambioProprieta("ElencoVideo");

            }
        }

        public RelayCommand CommandPulisciFiltro { get; set; }
        public void PulisciFiltro()
        {
                ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO);
                CambioProprieta("ElencoVideo");
        }

        public RelayCommand CommandAggiorna { get; set; }
        public void Aggiorna()
        {
            if (CategoriaFiltrata == null && TipoVideoFiltrato == null)
            {
                ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO);
                CambioProprieta("ElencoVideo");
            }
            else if (CategoriaFiltrata != null && GiocatoreFiltrato == null && TipoVideoFiltrato == null)
            {
                ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO
                    .Where(x => x.CATEGORIA == CategoriaFiltrata.CATEGORIA1)
                    .ToList());
                CambioProprieta("ElencoVideo");
            }
            else if (CategoriaFiltrata != null && GiocatoreFiltrato != null && TipoVideoFiltrato == null)
            {
                ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO
                    .Where(x => x.CATEGORIA == CategoriaFiltrata.CATEGORIA1 && x.ATLETA == GiocatoreFiltrato.ID)
                    .ToList());
                CambioProprieta("ElencoVideo");

            }
            else if (CategoriaFiltrata != null && GiocatoreFiltrato == null && TipoVideoFiltrato != null)
            {
                ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO
                    .Where(x => x.CATEGORIA == CategoriaFiltrata.CATEGORIA1 && x.TIPO == TipoVideoFiltrato)
                    .ToList());
                CambioProprieta("ElencoVideo");

            }
            else if (CategoriaFiltrata != null && GiocatoreFiltrato != null && TipoVideoFiltrato != null)
            {
                ElencoVideo = new ObservableCollection<VIDEO>(ldg.VIDEO
                    .Where(x => x.CATEGORIA == CategoriaFiltrata.CATEGORIA1 && x.ATLETA == GiocatoreFiltrato.ID && x.TIPO == TipoVideoFiltrato)
                    .ToList());
                CambioProprieta("ElencoVideo");

            }
        }


    }
}
