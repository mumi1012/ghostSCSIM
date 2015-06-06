using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.DAO
{
    /// <summary>
    /// Hilfsklasse für das Zusammensetzen der Stückliste
    /// </summary>
    public class TeileHelper
    {
        public int posId { get; set; }
        public int partId { get; set; }
        public int otPosId { get; set; }
        public int anzahl { get; set; }
        public string bezeichnung { get; set; }
        public string verwendung { get; set; }
        public double wert { get; set; }
        public string buchstabe { get; set; }
    }
}
