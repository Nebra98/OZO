using System;
using System.Collections.Generic;

namespace OZO.Models
{
    public partial class NatjecajReferentniTip
    {
        public int Id { get; set; }
        public int? IdNatjecaj { get; set; }
        public int? IdReferentniTip { get; set; }

        public virtual Natječaj IdNatjecajNavigation { get; set; }
        public virtual ReferentniTip IdReferentniTipNavigation { get; set; }
    }
}
