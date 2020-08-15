using System;
using System.Collections.Generic;

namespace OZO.Models
{
    public partial class Oprema
    {
        public Oprema()
        {
            PosaoOprema = new HashSet<PosaoOprema>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Status { get; set; }
        public bool Dostupnost { get; set; }
        public int? IdReferentniTip { get; set; }

        public virtual ReferentniTip IdReferentniTipNavigation { get; set; }
        public virtual ICollection<PosaoOprema> PosaoOprema { get; set; }
    }
}
