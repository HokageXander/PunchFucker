using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Johnny_Punch
{
    abstract class Movables : GameObject
    {
        public Rectangle animationBox, feetBox;
        protected SpriteEffects spriteEffect;
        protected int frame;
        public double frameTime, frameInterval;
        protected bool moving, onGround = true;
        public Vector2 posJump;

        public Movables(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            feetBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y + (height - 4) - height / 2, width, height - (height - 4));
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public void Animation(int animationSpeed, int animationLength, int animationWidth, GameTime gameTime)
        {
            //frameTime = animationSpeed;
            frameInterval = animationSpeed;

            frameTime -= gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTime <= 0)
            {
                frameTime = frameInterval;
                frame++;
                animationBox.X = (frame % animationLength) * animationWidth;
            }
        }
    }
}
