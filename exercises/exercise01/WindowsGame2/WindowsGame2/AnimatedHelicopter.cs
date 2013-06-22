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
    class AnimatedHelicopter: Helicopter
    {
        protected int nFrames, frameWidth, currentFrame;

        public AnimatedHelicopter(int nFrames, int frameWidth, Vector2 position, Vector2 velocity,
                                    float rotation, float scale)
            :base(position, velocity, rotation, scale)
        {
            this.nFrames = nFrames;
            this.frameWidth = frameWidth;
            this.currentFrame = 0;
        }

       

        public int FrameWidth
        {
            get { return this.frameWidth; }
            set { this.frameWidth = value; }
        }

        public int CurrentFrame
        {
            get { return this.currentFrame; }
            set { this.currentFrame = value; }
        }


        public int NFrames
        {
            get { return this.nFrames; }
            set { this.nFrames = value; }
        }

        public void setBoudingBox()
        {

            Vector3 v = new Vector3(this.Position.X + this.texture.Width/this.nFrames, 
                this.Position.Y + this.texture.Height, 0);
            this.boundingBox = new BoundingBox(new Vector3(this.Position, 0), v);
        }

        public void NextFrame()
        {
            this.currentFrame++;
            this.currentFrame = this.currentFrame % this.nFrames;
        }

        // Bouncing changes coordinates of position when a collision with another helicopter
        // takes place in order to avoid it.
        public void Bouncing(AnimatedHelicopter anotherHelicopter)
        {
            // Find max X and max Y coordinates between both helicopters
            int max_x = (int)Math.Max(this.Position.X, anotherHelicopter.Position.X);
            int max_y = (int)Math.Max(this.Position.Y, anotherHelicopter.Position.Y);

            // Change anotherHelicopter X coordinates to the left of this helicopter with a margin of 2px
            if (this.Position.X == max_x)
            {
                anotherHelicopter.position.X = this.position.X - anotherHelicopter.FrameWidth - 2;
                // If new X coordinates of anotherHelicopter are negative, they are set to 0
                // and this X coordinates are set far enough  from anotherHelicopter's
                if (anotherHelicopter.position.X < 0)
                {
                    anotherHelicopter.position.X = 0;
                    this.position.X = anotherHelicopter.FrameWidth + 2;
                }
            }
            else
            {
                // Change this X coordinates to the left of anotherHelicopter with a margin of 2px
                this.position.X = anotherHelicopter.position.X - this.FrameWidth - 2;
                if (this.position.X < 0)
                {
                    this.position.X = 0;
                    anotherHelicopter.position.X = this.FrameWidth + 2;
                }
            }

            if (this.Position.Y == max_y)
            {
                // Change anotherHelicopter Y coordinates down of this helicopter with a margin of 2px
                anotherHelicopter.position.Y = this.position.Y - anotherHelicopter.FrameWidth - 2;
                if (anotherHelicopter.position.Y < 0)
                {
                    //if coordenates are negative, they are set to 0 and this is set far enough
                    // to avoid collision
                    anotherHelicopter.position.Y = 0;
                    this.position.Y = anotherHelicopter.getTexture().Height + 2;
                }
            }
            else
            {
                // Change this Y coordinates down of anotherHelicopter with a margin of 2px
                this.position.Y = anotherHelicopter.position.Y - this.FrameWidth - 2;
                if (this.position.Y < 0)
                {
                    //if coordenates are negative, they are set to 0 and anotherHelicopter set far enough
                    // to avoid collision
                    this.position.Y = 0;
                    anotherHelicopter.position.Y = this.getTexture().Height + 2;
                }
            }

            //Finally, once they are not overlapping, we change the velocity of anotherHelicopter
            anotherHelicopter.Velocity = anotherHelicopter.Velocity * -1;
                     
            }
    }
}
