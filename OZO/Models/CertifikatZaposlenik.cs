using System;
using System.Collections.Generic;

namespace OZO.Models
{
    public partial class CertifikatZaposlenik
    {
        public int Id { get; set; }
        public int? IdCertifikat { get; set; }
        public int? IdZaposlenik { get; set; }

        public virtual Certifikat IdCertifikatNavigation { get; set; }
        public virtual Zaposlenik IdZaposlenikNavigation { get; set; }
    }
}
