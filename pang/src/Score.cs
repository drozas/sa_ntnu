using System;
using System.Collections.Generic;
using System.Text;

namespace pang_01
{

    class Score:IComparable 
    {
        int value;
        String name;

        public Score(String name, int score)
        {
            this.name = name;
            this.value = score;
        }
        public override String ToString()
        {
            return this.name + " - " + this.value.ToString();
        }
        public int getValue()
        {
            return value;
        }
        public String getName()
        {
            return name;
        }
        public int CompareTo(Object o)
        {
            if (!(o is Score))
                throw new InvalidCastException("this is not an score object");
            Score score2 = (Score)o;
            return this.value.CompareTo(score2.value) * -1;
        }

    }
}
