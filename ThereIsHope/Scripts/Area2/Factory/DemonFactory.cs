using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public abstract class DemonFactory
    {

        public abstract Demon buildDemon(string Type);
        public abstract Demon createDemon(string Type);

    }
