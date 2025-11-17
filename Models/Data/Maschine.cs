using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MaschinenDataein.Models.Data
{
    public class Maschine
    {
        public Maschine () { }

        public Maschine (long id, string bezeichnung, string ipAdresse)
        {
            Id = id;
            Bezeichnung = bezeichnung;
            IpAdresse = ipAdresse;
        }

        public long Id { get; set; }

        [Required]
        public string? Bezeichnung { get; set; }

        public string? IpAdresse { get; set; }
    }
}
