using System;
using System.Collections.Generic;

namespace OZO.Models
{
    public partial class Certifikat
    {
        public Certifikat()
        {
            CertifikatZaposlenik = new HashSet<CertifikatZaposlenik>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<CertifikatZaposlenik> CertifikatZaposlenik { get; set; }
    }
}
