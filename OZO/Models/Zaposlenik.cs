using System;
using System.Collections.Generic;

namespace OZO.Models
{
    public partial class Zaposlenik
    {
        public Zaposlenik()
        {
            CertifikatZaposlenik = new HashSet<CertifikatZaposlenik>();
        }

        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime Datum { get; set; }
        public int Plaća { get; set; }
        public int? IdPosao { get; set; }

        public string StručnaSprema { get; set; }
        public string NazivŠkole { get; set; }

        public virtual Posao IdPosaoNavigation { get; set; }
        public virtual ICollection<CertifikatZaposlenik> CertifikatZaposlenik { get; set; }
    }
}
