using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace pang_01
{
    
    class HighScores
    {
        private ArrayList list;
        
        public HighScores(FileManager fm)
        {
            list = fm.loadScore();
        }

        public String showScores()
        {
            this.SortScores();
            String display = "HIGH SCORES \n";
            foreach (Score s in list)
                display += s.ToString() + "\n";
            display += "\n To return to menu press Back";
            return display;
            

        }
        public void newHighscore(Score s)
        {
            list.Add(s);
            SortScores();

     
        }
     
        public String ToString()
        {
            String str = "HIGH SCORE: \n \n";
            foreach (Score s in list)
                str += s.ToString() + "\n";
            str += "Press Back to return.";
            return str;
        }
        public void SortScores()
        {
            this.list.Sort();
            this.list = this.list.GetRange(0, Pang.MAXINHIGHSCORELIST);
        }
        public Score get(int i)
        {
            return (Score)list[i];
        }
        public int getLowest() {
            Score s = (Score)list[Pang.MAXINHIGHSCORELIST-1];
            return s.getValue();
        }

    }
}
