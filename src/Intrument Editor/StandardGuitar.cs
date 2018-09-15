using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intrument_Editor
{

    
    class StandardGuitar : editor
    {
        private static String[] stardardTune = { "e", "B", "G", "D", "A", "E" };

        /**
         * Constructor with a tune given by the user
         * precondition: precondition: String[] with length 6
         */
        public StandardGuitar(String[] tune) : base(6, tune) { }

        /**
         * Sets default constructor
         */
        public StandardGuitar() : base(6, stardardTune) { }
        
    }
}
