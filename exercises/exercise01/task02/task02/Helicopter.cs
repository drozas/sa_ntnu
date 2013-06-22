using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace task02
{
    class Helicopter
    {
        private Vector2 position;
        private Vector2 velocity;
        private float rotation;
        private float scale;


        private Random r = new Random();
        
        /*Constructor with default values*/
        public Helicopter(Vector2 position, Vector2 velocity, float rotation, float scale)
        {
            this.position = position;
            this.velocity = velocity;
            this.rotation = rotation;
            this.scale = scale;
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }


        public Vector2 Velocity
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }


        public float Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }


        public float Scale
        {
            get { return this.scale; }
            set { this.scale = value; }
        }



        public void MoveUp()
        {
            this.position.Y = this.position.Y - this.velocity.Y;
        }

        public void MoveDown()
        {
            this.position.Y = this.position.Y + this.velocity.Y;
        }

        public void MoveRight()
        {
            this.position.X = this.position.X + this.velocity.X;
        }

        public void MoveLeft()
        {
            this.position.X = this.position.X - this.velocity.X;
        }

        public void Resize()
        {
            this.scale = this.r.Next(1, 3);
                
        }

        public void Rotate()
        {
            this.rotation += (float)r.Next(0, 6);
        }
               

    }
}
