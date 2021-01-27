using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class StromgDemon : DemonFactory
    {
    public override Demon buildDemon(string Type)
    {
        Demon tempDemon = createDemon(Type);
        return tempDemon;
    }

    public override Demon createDemon(string Type)
    {
        if (Type == "Base")
        {
            return new BaseDemon(3, 0f, 100);
        }
        else
        {
            return null;
        }
    }
}

