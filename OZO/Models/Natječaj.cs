using System;
using System.Collections.Generic;

namespace OZO.Models
{
    public partial class Natječaj
    {
        public Natječaj()
        {
            NatjecajReferentniTip = new HashSet<NatjecajReferentniTip>();
            Posao = new HashSet<Posao>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double Cijena { get; set; }
        public string Poslodavac { get; set; }
        public bool Status { get; set; }
        public string Lokacija { get; set; }
        public string Kontakt { get; set; }
        public virtual ICollection<NatjecajReferentniTip> NatjecajReferentniTip { get; set; }
        public virtual ICollection<Posao> Posao { get; set; }
    }
}
