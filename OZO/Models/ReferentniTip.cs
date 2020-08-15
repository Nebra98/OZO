using System;
using System.Collections.Generic;

namespace OZO.Models
{
    public partial class ReferentniTip
    {
        public ReferentniTip()
        {
            NatjecajReferentniTip = new HashSet<NatjecajReferentniTip>();
            Oprema = new HashSet<Oprema>();
            UslugaReferentniTip = new HashSet<UslugaReferentniTip>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<NatjecajReferentniTip> NatjecajReferentniTip { get; set; }
        public virtual ICollection<Oprema> Oprema { get; set; }
        public virtual ICollection<UslugaReferentniTip> UslugaReferentniTip { get; set; }
    }
}
