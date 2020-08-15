using System;
using System.Collections.Generic;

namespace OZO.Models
{
    public partial class UslugaReferentniTip
    {
        public int Id { get; set; }
        public int? IdUsluga { get; set; }
        public int? IdReferentniTip { get; set; }

        public virtual ReferentniTip IdReferentniTipNavigation { get; set; }
        public virtual Usluga IdUslugaNavigation { get; set; }
    }
}
