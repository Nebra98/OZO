using System;
using System.Collections.Generic;

namespace OZO.Models
{
    public partial class PosaoOprema
    {
        public int Id { get; set; }
        public int? IdPosao { get; set; }
        public int? IdOprema { get; set; }

        public virtual Oprema IdOpremaNavigation { get; set; }
        public virtual Posao IdPosaoNavigation { get; set; }
    }
}
