using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ghostSCSIM.XML;

namespace ghostSCSIM.Domain
{
    class KTeilBedarfsplanung
    {

        //Produktionsplanung E-Teile für aktuelle Periode (Programm: Produktionsplan -> Produktion)
        //TODO: wo genau liegen die?

        static int e4bedarf;
        static int e5bedarf;
        static int e6bedarf;
        static int e7bedarf;
        static int e8bedarf;
        static int e9bedarf;
        static int e10bedarf;
        static int e11bedarf;
        static int e12bedarf;
        static int e13bedarf;
        static int e14bedarf;
        static int e15bedarf;
        static int e16bedarf;
        static int e17bedarf;
        static int e18bedarf;
        static int e19bedarf;
        static int e20bedarf;
        static int e26bedarf;
        static int e29bedarf;
        static int e30bedarf;
        static int e31bedarf;
        static int e49bedarf;
        static int e50bedarf;
        static int e51bedarf;
        static int e54bedarf;
        static int e55bedarf;
        static int e56bedarf;

        // Bruttobedarf KTeile für ETeile aktuelle Periode

        //K24

        int k24bedarf = e16bedarf + 2*e29bedarf + 2*e30bedarf + e31bedarf + 2*e49bedarf + 2*e50bedarf + e51bedarf + 2*e54bedarf + 2*e55bedarf + e56bedarf;

        //K25

        int k25bedarf = 2 * e49bedarf + 2 * e30bedarf + 2 * e49bedarf + 2 * e50bedarf + 2 * e54bedarf + 2 * e55bedarf;

        //K27

        int k27bedarf = e31bedarf + e51bedarf + e56bedarf;

        //K28

        int k28bedarf = e16bedarf + 3 * e18bedarf + 4 * e19bedarf + 5 * e20bedarf;

        //K32

        int k32bedarf = e10bedarf + e11bedarf + e12bedarf + e13bedarf + e14bedarf + e15bedarf + e18bedarf + e19bedarf + e20bedarf;

        //K33

        int k33bedarf = e6bedarf + e9bedarf;

        //K34

        int k34bedarf = 36 * e6bedarf + 36 * e9bedarf;

        //K35

        int k35bedarf = 2 * e4bedarf + 2 * e5bedarf + 2 * e6bedarf + 2 * e7bedarf + 2 * e8bedarf + 2 * e9bedarf;

        //K36

        int k36bedarf = e4bedarf + e8bedarf + e9bedarf;

        //K37

        int k37bedarf = e7bedarf + e8bedarf + e9bedarf;

        //K38

        int k38bedarf = e7bedarf + e8bedarf + e9bedarf;

        //K39

        int k39bedarf = e11bedarf + e12bedarf + e13bedarf + e14bedarf + e15bedarf;

        //K40

        int k40bedarf = e16bedarf;

        //K41

        int k41bedarf = e16bedarf;

        //K42

        int k42bedarf = 2 * e16bedarf;

        //K43

        int k43bedarf = e17bedarf;

        //K44

        int k44bedarf = e17bedarf + 2 * e26bedarf;

        //K45

        int k45bedarf = e16bedarf;

        //K46

        int k46bedarf = e16bedarf;

        //K47

        int k47bedarf = e26bedarf;

        //K48

        int k48bedarf = 2 * e26bedarf;

        //K52

        int k52bedarf = e4bedarf + e7bedarf;

        //K53

        int k53bedarf = 36 * e4bedarf + 36 * e7bedarf;

        //K57

        int k57bedarf = e5bedarf + e8bedarf;

        //K58

        int k58bedarf = 36 * e5bedarf + 36 * e7bedarf;

        //K59

        int k59bedarf = 2 * e17bedarf + 2 * e18bedarf + 2 * e19bedarf;

    }
}
