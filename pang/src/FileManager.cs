using System;
using System.Collections.Generic;
//using System.Collections.ArrayList;
using System.Text;
using AMS.Profile;
using System.Collections;

namespace pang_01
{
    class FileManager
    {
        

        public FileManager()
        {

        }
        public Stage loadStage(int stageNumber)
        {
            String fileName = "stage";
            if (stageNumber < 10)
                fileName +="0";

            fileName += stageNumber.ToString();

            Xml file = new Xml(fileName);
            Stage stage = new Stage(stageNumber);


            //file.SetValue("stage", "balls01", 1);
            //file.SetValue("stage", "balls02", 0);
            //file.SetValue("stage", "balls03", 0);
            

            stage.BallsNoSize1= Convert.ToInt32(file.GetValue("stage", "balls01"));
            stage.BallsNoSize2 = Convert.ToInt32(file.GetValue("stage", "balls02"));
            stage.BallsNoSize3 = Convert.ToInt32(file.GetValue("stage", "balls03"));

            return stage;
                
        }
        public void writeScore(HighScores highScores)
        {
            String fileName = "score";
            int numberScores;

            Xml file = new Xml(fileName);

            int totalNumber = 5;
            file.SetValue("score", "totalNumber", totalNumber );


            for (int i = 0; i < totalNumber; i++)
            {
                Score s = highScores.get(i);
                file.SetValue("score", "score" + i.ToString(), s.getValue());
                file.SetValue("score", "name" + i.ToString(), s.getName());
            }
        }
        public ArrayList loadScore()
        {
            String fileName = "score";
            int numberScores;
            
            Xml file = new Xml(fileName);
            ArrayList scoreList = new ArrayList();

           /* 
            }*/
            numberScores = Convert.ToInt32(file.GetValue("score", "totalNumber"));
            for (int i = 0; i < numberScores; i++)          
            {
                
                int score= Convert.ToInt32(file.GetValue("score","score" + i.ToString()));
                String name  = Convert.ToString(file.GetValue("score","name" + i.ToString()));
                scoreList.Add(new Score(name, score));
            }

            return scoreList;

        }
    }
}
