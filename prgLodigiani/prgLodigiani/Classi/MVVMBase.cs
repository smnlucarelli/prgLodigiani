using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prgLodigiani
{
    public class MVVMBase : INotifyPropertyChanged
    {
        //evento che permette la comunicazione nel MVVM
        public event PropertyChangedEventHandler PropertyChanged;
        public void CambioProprieta(string prop)
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
