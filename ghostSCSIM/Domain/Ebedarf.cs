using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ghostSCSIM.XML;
using ghostSCSIM.service;

namespace ghostSCSIM.Domain
{
    public class Ebedarf
    {

         public int test = 3;
         public int e4bedarf;
         public int e5bedarf;
         public int e6bedarf;
         public int e7bedarf;
         public int e8bedarf;
         public int e9bedarf;
         public int e10bedarf;
         public int e11bedarf;
         public int e12bedarf;
         public int e13bedarf;
         public int e14bedarf;
         public int e15bedarf;
         public int e16bedarf;
         public int e17bedarf;
         public int e18bedarf;
         public int e19bedarf;
         public int e20bedarf;
         public int e26bedarf;
         public int e29bedarf;
         public int e30bedarf;
         public int e31bedarf;
         public int e49bedarf;
         public int e50bedarf;
         public int e51bedarf;
         public int e54bedarf;
         public int e55bedarf;
         public int e56bedarf;

        public Ebedarf() {

        }

        public Ebedarf(int test)
        {
            this.test = test;
        }

        //Konstruktor mit Daten aus Programm: Produktionsplan -> Produktion befüllen
        public Ebedarf(int e4bedarf, int e5bedarf, int e6bedarf, int e7bedarf, int e8bedarf, int e9bedarf, int e10bedarf, int e11bedarf, int e12bedarf, int e13bedarf, 
            int e14bedarf, int e15bedarf, int e16bedarf, int e17bedarf, int e18bedarf, int e19bedarf, int e20bedarf, int e26bedarf, int e29bedarf, int e30bedarf, int e31bedarf,
                int e49bedarf, int e50bedarf, int e51bedarf, int e54bedarf, int e55bedarf, int e56bedarf)
        {
            this.e4bedarf = e4bedarf;
            this.e5bedarf = e5bedarf;
            this.e6bedarf = e6bedarf;
            this.e7bedarf = e7bedarf;
            this.e8bedarf = e8bedarf;
            this.e9bedarf = e9bedarf;
            this.e10bedarf = e10bedarf;
            this.e11bedarf = e11bedarf;
            this.e12bedarf = e12bedarf;
            this.e13bedarf = e13bedarf;
            this.e14bedarf = e14bedarf;
            this.e15bedarf = e15bedarf;
            this.e16bedarf = e16bedarf;
            this.e17bedarf = e17bedarf;
            this.e18bedarf = e18bedarf;
            this.e19bedarf = e19bedarf;
            this.e20bedarf = e20bedarf;
            this.e26bedarf = e26bedarf;
            this.e29bedarf = e29bedarf;
            this.e30bedarf = e30bedarf;
            this.e31bedarf = e31bedarf;
            this.e49bedarf = e49bedarf;
            this.e50bedarf = e50bedarf;
            this.e51bedarf = e51bedarf;
            this.e54bedarf = e54bedarf;
            this.e55bedarf = e55bedarf;
            this.e56bedarf = e56bedarf;

        }


        // Bruttobedarf KTeile für ETeile aktuelle Periode

        //K24

        /*
         * Formelsicherung
         * 
        //K25

        int k25bedarf = 2 * e29bedarf + 2 * e30bedarf + 2 * e49bedarf + 2 * e50bedarf + 2 * e54bedarf + 2 * e55bedarf;

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

        int k39bedarf = e10bedarf + e11bedarf + e12bedarf + e13bedarf + e14bedarf + e15bedarf;

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
         * */

    }
}
