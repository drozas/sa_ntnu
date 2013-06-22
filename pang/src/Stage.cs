using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using XQUEST.GameObjectManagement;
using XQUEST.Helpers;
using XQUEST.SpriteAnimationFramework;


namespace pang_01
{
    class Stage
    {
        private int ballsNoSize1;
        private int ballsNoSize2;
        private int ballsNoSize3;

        public Stage(int level)
        {
         ballsNoSize1 = 0;
         ballsNoSize2 = 0;
         ballsNoSize3 = 1;
      
        }
        public int BallsNoSize1
        {
            get { return ballsNoSize1;}
            set { ballsNoSize1 = value; }
            
        }
        public int BallsNoSize2
        {
            get { return ballsNoSize2; }
            set { ballsNoSize2 = value; }
        }
        public int BallsNoSize3
        {
            get { return ballsNoSize3; }
            set { ballsNoSize3 = value; }
        }
    }
}
