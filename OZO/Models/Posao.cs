using System;
using System.Collections.Generic;

namespace OZO.Models
{
    public partial class Posao
    {
        public Posao()
        {
            PosaoIzvjestaj = new HashSet<PosaoIzvjestaj>();
            PosaoOprema = new HashSet<PosaoOprema>();
            Zaposlenik = new HashSet<Zaposlenik>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double Cijena { get; set; }
        public string Klijent { get; set; }
        public string Lokacija { get; set; }
        public string Kontakt { get; set; }
        public int? IdUsluga { get; set; }
        public int? IdNatjecaj { get; set; }

        public virtual Natječaj IdNatjecajNavigation { get; set; }
        public virtual Usluga IdUslugaNavigation { get; set; }
        public virtual ICollection<PosaoIzvjestaj> PosaoIzvjestaj { get; set; }
        public virtual ICollection<PosaoOprema> PosaoOprema { get; set; }
        public virtual ICollection<Zaposlenik> Zaposlenik { get; set; }
    }
}
