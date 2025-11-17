using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaschinenDataein.Models.Data
{
    public class AlarmDaten
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public long MaschinenId { get; set; }

        [ForeignKey(nameof(MaschinenId))]
        public virtual Maschine? Maschine { get; set; }

        // Alarmfelder AM1 bis AM94 als boolean
        public bool AM1 { get; set; }
        public bool AM2 { get; set; }
        public bool AM3 { get; set; }
        public bool AM4 { get; set; }
        public bool AM5 { get; set; }
        public bool AM6 { get; set; }
        public bool AM7 { get; set; }
        public bool AM8 { get; set; }
        public bool AM9 { get; set; }
        public bool AM10 { get; set; }
        public bool AM11 { get; set; }
        public bool AM12 { get; set; }
        public bool AM13 { get; set; }
        public bool AM14 { get; set; }
        public bool AM15 { get; set; }
        public bool AM16 { get; set; }
        public bool AM17 { get; set; }
        public bool AM18 { get; set; }
        public bool AM19 { get; set; }
        public bool AM20 { get; set; }
        public bool AM21 { get; set; }
        public bool AM22 { get; set; }
        public bool AM23 { get; set; }
        public bool AM24 { get; set; }
        public bool AM25 { get; set; }
        public bool AM26 { get; set; }
        public bool AM27 { get; set; }
        public bool AM28 { get; set; }
        public bool AM29 { get; set; }
        public bool AM30 { get; set; }
        public bool AM31 { get; set; }
        public bool AM32 { get; set; }
        public bool AM33 { get; set; }
        public bool AM34 { get; set; }
        public bool AM35 { get; set; }
        public bool AM36 { get; set; }
        public bool AM37 { get; set; }
        public bool AM38 { get; set; }
        public bool AM39 { get; set; }
        public bool AM40 { get; set; }
        public bool AM41 { get; set; }
        public bool AM42 { get; set; }
        public bool AM43 { get; set; }
        public bool AM44 { get; set; }
        public bool AM45 { get; set; }
        public bool AM46 { get; set; }
        public bool AM47 { get; set; }
        public bool AM48 { get; set; }
        public bool AM49 { get; set; }
        public bool AM50 { get; set; }
        public bool AM51 { get; set; }
        public bool AM52 { get; set; }
        public bool AM53 { get; set; }
        public bool AM54 { get; set; }
        public bool AM55 { get; set; }
        public bool AM56 { get; set; }
        public bool AM57 { get; set; }
        public bool AM58 { get; set; }
        public bool AM59 { get; set; }
        public bool AM60 { get; set; }
        public bool AM61 { get; set; }
        public bool AM62 { get; set; }
        public bool AM63 { get; set; }
        public bool AM64 { get; set; }
        public bool AM65 { get; set; }
        public bool AM66 { get; set; }
        public bool AM67 { get; set; }
        public bool AM68 { get; set; }
        public bool AM69 { get; set; }
        public bool AM70 { get; set; }
        public bool AM71 { get; set; }
        public bool AM72 { get; set; }
        public bool AM73 { get; set; }
        public bool AM74 { get; set; }
        public bool AM75 { get; set; }
        public bool AM76 { get; set; }
        public bool AM77 { get; set; }
        public bool AM78 { get; set; }
        public bool AM79 { get; set; }
        public bool AM80 { get; set; }
        public bool AM81 { get; set; }
        public bool AM82 { get; set; }
        public bool AM83 { get; set; }
        public bool AM84 { get; set; }
        public bool AM85 { get; set; }
        public bool AM86 { get; set; }
        public bool AM87 { get; set; }
        public bool AM88 { get; set; }
        public bool AM89 { get; set; }
        public bool AM90 { get; set; }
        public bool AM91 { get; set; }
        public bool AM92 { get; set; }
        public bool AM93 { get; set; }
        public bool AM94 { get; set; }

    }
}
