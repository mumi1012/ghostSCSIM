using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ghostSCSIM.Domain
{
    /// <summary>
    /// Vergleicht Teile anhand der Position in der Stückliste
    /// </summary>
    internal  class TeilComparer : IComparer<Teil>
    {
        public int Compare(Teil a, Teil b)
        {
            int result = a.getStuecklistenPos().CompareTo(b.getStuecklistenPos());
            return result;
        }
    }
}
