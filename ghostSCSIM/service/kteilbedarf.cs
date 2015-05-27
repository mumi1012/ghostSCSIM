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

        //TODO Sonderfälle bearbeiten

        public int getk21bedarf(int p1bedarf)
        {

            int k21bedarf = p1bedarf;

            return k21bedarf;
        }

        public int getk22bedarf(int p2bedarf)
        {

            int k22bedarf = p2bedarf;

            return k22bedarf;
        }

        public int getk22bedarf(int p3bedarf)
        {

            int k23bedarf = p3bedarf;

            return k23bedarf;
        }

        //TODO anstatt leeren Konstruktor befüllten Konstruktor für Zugriff auf ebedarfe verwenden
        public Ebedarf ebedarf = new Ebedarf();

        public int getk24bedarf(Ebedarf ebedarf)
        {

            int k24bedarf = ebedarf.e4bedarf + 2 * ebedarf.e29bedarf + 2 * ebedarf.e30bedarf + ebedarf.e31bedarf
                + 2 * ebedarf.e49bedarf + 2 * ebedarf.e50bedarf + ebedarf.e51bedarf + 2 * ebedarf.e54bedarf
                + 2 * ebedarf.e55bedarf + ebedarf.e56bedarf;
                //  + p1bedarf + p2bedarf + p3bedarf

            return k24bedarf;
        }

        public int getk25bedarf(Ebedarf ebedarf)
        {

            int k25bedarf = 2 * ebedarf.e29bedarf + 2 * ebedarf.e30bedarf + 2 * ebedarf.e49bedarf + 2 * ebedarf.e50bedarf
                + 2 * ebedarf.e54bedarf + 2 * ebedarf.e55bedarf;

            return k25bedarf;
        }

        public int getk27bedarf(Ebedarf ebedarf)
        {

            int k27bedarf = ebedarf.e31bedarf + ebedarf.e51bedarf + ebedarf.e56bedarf;
            //  + p1bedarf + p2bedarf + p3bedarf

            return k27bedarf;
        }

        public int getk28bedarf(Ebedarf ebedarf)
        {

            int k28bedarf = ebedarf.e16bedarf + 3 * ebedarf.e18bedarf + 4 * ebedarf.e19bedarf + 5 * ebedarf.e20bedarf;

            return k28bedarf;
        }

        public int getk32bedarf(Ebedarf ebedarf)
        {

            int k32bedarf = ebedarf.e10bedarf + ebedarf.e11bedarf + ebedarf.e12bedarf + ebedarf.e13bedarf + ebedarf.e14bedarf +
                ebedarf.e15bedarf + ebedarf.e18bedarf + ebedarf.e19bedarf + ebedarf.e20bedarf;

            return k32bedarf;
        }

        public int getk33bedarf(Ebedarf ebedarf)
        {

            int k33bedarf = ebedarf.e6bedarf + ebedarf.e9bedarf;

            return k33bedarf;
        }

        public int getk34bedarf(Ebedarf ebedarf)
        {

            int k34bedarf = 36 * ebedarf.e6bedarf + 36 * ebedarf.e9bedarf;

            return k34bedarf;
        }

        public int getk35bedarf(Ebedarf ebedarf)
        {

            int k35bedarf = 2 * ebedarf.e4bedarf + 2 * ebedarf.e5bedarf + 2 * ebedarf.e6bedarf + 2 * ebedarf.e7bedarf
                + 2 * ebedarf.e8bedarf + 2 * ebedarf.e9bedarf;

            return k35bedarf;
        }

        public int getk36bedarf(Ebedarf ebedarf)
        {

            int k36bedarf = ebedarf.e4bedarf + ebedarf.e8bedarf + ebedarf.e9bedarf;

            return k36bedarf;
        }

        public int getk37bedarf(Ebedarf ebedarf)
        {

            int k37bedarf = ebedarf.e7bedarf + ebedarf.e8bedarf + ebedarf.e9bedarf;

            return k37bedarf;
        }

        public int getk38bedarf(Ebedarf ebedarf)
        {

            int k38bedarf = ebedarf.e7bedarf + ebedarf.e8bedarf + ebedarf.e9bedarf;

            return k38bedarf;
        }

        public int getk39bedarf(Ebedarf ebedarf)
        {

            int k39bedarf = ebedarf.e10bedarf + ebedarf.e11bedarf + ebedarf.e12bedarf + ebedarf.e13bedarf +
                ebedarf.e14bedarf + ebedarf.e15bedarf;

            return k39bedarf;
        }

        public int getk40bedarf(Ebedarf ebedarf)
        {

            int k40bedarf = ebedarf.e16bedarf;

            return k40bedarf;

        }

        public int getk41bedarf(Ebedarf ebedarf)
        {

            int k41bedarf = ebedarf.e16bedarf;

            return k41bedarf;
        }

        public int getk42bedarf(Ebedarf ebedarf)
        {

            int k42bedarf = 2 * ebedarf.e16bedarf;

            return k42bedarf;
        }

        public int getk43bedarf(Ebedarf ebedarf)
        {

            int k43bedarf = ebedarf.e17bedarf;

            return k43bedarf;
        }

        public int getk44bedarf(Ebedarf ebedarf)
        {

            int k44bedarf = ebedarf.e17bedarf + 2 * ebedarf.e26bedarf;

            return k44bedarf;
        }

        public int getk45bedarf(Ebedarf ebedarf)
        {

            int k45bedarf = ebedarf.e16bedarf;

            return k45bedarf;
        }

        public int getk46bedarf(Ebedarf ebedarf)
        {

            int k46bedarf = ebedarf.e16bedarf;

            return k46bedarf;
        }

        public int getk47bedarf(Ebedarf ebedarf)
        {

            int k47bedarf = ebedarf.e26bedarf;

            return k47bedarf;
        }

        public int getk48bedarf(Ebedarf ebedarf)
        {

            int k48bedarf = 2 * ebedarf.e26bedarf;

            return k48bedarf;
        }

        public int getk52bedarf(Ebedarf ebedarf)
        {

            int k52bedarf = ebedarf.e4bedarf + ebedarf.e7bedarf;

            return k52bedarf;
        }

        public int getk53bedarf(Ebedarf ebedarf)
        {

            int k53bedarf = 36 * ebedarf.e4bedarf + 36 * ebedarf.e7bedarf;

            return k53bedarf;
        }

        public int getk57bedarf(Ebedarf ebedarf)
        {

            int k57bedarf = ebedarf.e5bedarf + ebedarf.e8bedarf;

            return k57bedarf;
        }

        public int getk58bedarf(Ebedarf ebedarf)
        {

            int k58bedarf = 36 * ebedarf.e5bedarf + 36 * ebedarf.e7bedarf;

            return k58bedarf;
        }

        public int getk59bedarf(Ebedarf ebedarf)
        {

            int k59bedarf = 2 * ebedarf.e17bedarf + 2 * ebedarf.e18bedarf + 2 * ebedarf.e19bedarf;

            return k59bedarf;
        }

    }
}
