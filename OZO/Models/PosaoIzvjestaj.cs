using System;
using System.Collections.Generic;

namespace OZO.Models
{
    public partial class PosaoIzvjestaj
    {
        public int Id { get; set; }
        public int? IdPosao { get; set; }
        public string Sadrzaj { get; set; }

        public virtual Posao IdPosaoNavigation { get; set; }
    }
}
