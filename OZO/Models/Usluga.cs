using System;
using System.Collections.Generic;

namespace OZO.Models
{
    public partial class Usluga
    {
        public Usluga()
        {
            Posao = new HashSet<Posao>();
            UslugaReferentniTip = new HashSet<UslugaReferentniTip>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double Cijena { get; set; }
        public string Klijent { get; set; }
        public string Lokacija { get; set; }
        public string Kontakt { get; set; }

        public virtual ICollection<Posao> Posao { get; set; }
        public virtual ICollection<UslugaReferentniTip> UslugaReferentniTip { get; set; }
    }
}
