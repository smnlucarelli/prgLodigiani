//------------------------------------------------------------------------------
// <auto-generated>
//     Codice generato da un modello.
//
//     Le modifiche manuali a questo file potrebbero causare un comportamento imprevisto dell'applicazione.
//     Se il codice viene rigenerato, le modifiche manuali al file verranno sovrascritte.
// </auto-generated>
//------------------------------------------------------------------------------

namespace prgLodigiani.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class ATLETA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ATLETA()
        {
            this.VIDEO = new HashSet<VIDEO>();
        }
    
        public int ID { get; set; }
        public string NOME { get; set; }
        public string COGNOME { get; set; }
        public string NOMECOMPLETO { get; set; }
        public Nullable<int> ALTEZZA { get; set; }
        public Nullable<double> PESO { get; set; }
        public string RUOLO { get; set; }
        public string CATEGORIA { get; set; }
    
        public virtual CATEGORIA CATEGORIA1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIDEO> VIDEO { get; set; }
    }
}
