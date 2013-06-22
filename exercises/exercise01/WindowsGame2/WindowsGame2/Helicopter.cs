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

namespace task03
{
    class Helicopter
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected float rotation;
        protected float scale;
        protected BoundingBox boundingBox;
        protected SpriteEffects effect;
        protected Texture2D texture;

        private static Random r = new Random();
        
        /*Constructor with default values*/
        public Helicopter(Vector2 position, Vector2 velocity, float rotation, float scale)
        {
            this.position = position;
            this.velocity = velocity;
            this.rotation = rotation;
            this.scale = scale;
            this.effect = SpriteEffects.None;
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


        public SpriteEffects Effect
        {
            get { return this.effect; }
            set { this.effect = value; }
        }
        public void setBoudingBox()
        {

            Vector3 v = new Vector3(this.Position.X + this.texture.Width, this.Position.Y + this.texture.Height, 0);
            this.boundingBox = new BoundingBox(new Vector3(this.Position, 0), v);
        }

        public BoundingBox getBoundingBox()
        {
            return this.boundingBox;
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
            this.scale = Helicopter.r.Next(1, 3);
                
        }

        public void Rotate()
        {
            this.rotation += (float)r.Next(0, 6);
        }

        public void RandomMovement()
        {
            this.position = this.position + this.velocity;//= this.velocity * -1;

        }
        public void setRandomPosition(int screenWidth, int screenHeight)
        {
            this.position.X = Vector2.One.X * r.Next(0, screenWidth - this.texture.Width);
            this.position.Y = Vector2.One.Y * r.Next(0, screenHeight  - this.texture.Height);
        }


        public void setTexture(Texture2D texture)
        {
            this.texture = texture;

        }

        public Texture2D getTexture()
        {
            return this.texture;
        }

    }
}
