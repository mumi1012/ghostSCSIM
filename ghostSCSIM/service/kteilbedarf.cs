using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ghostSCSIM.Domain;

namespace ghostSCSIM.service
{
    public class kteilbedarf
    {
        public kteilbedarf()
        {

        }

        //Testkonstruktor zur Übergabe eines Feldes aus einer Domainklasse
        public Ebedarf ebedarftest = new Ebedarf(3);

        //Testmethode zur Übergabe eines Feldes aus einer Domainklasse
        public int testmethode(Ebedarf ebedarftest)
        {
            int ergebnis = ebedarftest.test;

            return ergebnis;
        }

        //TODO anstatt leeren Konstruktor befüllten Konstruktor für Zugriff auf ebedarfe verwenden
        public Ebedarf ebedarf = new Ebedarf();

        public int getk24bedarf(Ebedarf ebedarf)
        {

            int k24bedarf = ebedarf.e4bedarf + 2 * ebedarf.e29bedarf + 2 * ebedarf.e30bedarf + ebedarf.e31bedarf + 2 * ebedarf.e49bedarf + 2 * ebedarf.e50bedarf + ebedarf.e51bedarf + 2 * ebedarf.e54bedarf + 2 * ebedarf.e55bedarf + ebedarf.e56bedarf;

            return k24bedarf;
        }

    }
}
