using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Johnny_Punch
{
    class Boss : Enemy
    {
        public Boss(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            animationBox = new Rectangle(0, 0, 75, 116);
            width /= 9;
            height /= 9;
            aggroRadius = 350;
            damageToPlayer -= 1;
            life = 20;
            enemySpeed = 1.5f;
            scale = 1.2f;
            offset = new Vector2(width / 2, height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            if (!onGround || dead)
                speed.Y += 0.14f;
            else
                speed.Y = 0;

            Moves();

            boundingBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, width - 15, height - 10);
            feetBox = new Rectangle((int)pos.X - (int)49, (int)pos.Y + (122 - 4) - (int)offset.Y, width - 10, height - (height - 4));
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(tex, boundingBox, null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 0.9f);
            base.Draw(spriteBatch);
        }

        public void Moves()
        {
            if (!onGround)
            {
                animationBox.Width = 75;
                animationBox.Y = 116;
                animationBox.X = 0;

                if (pos.Y >= posJump.Y)
                {
                    pos.Y = posJump.Y;
                    onGround = true;
                    speed.Y = 0;
                }
            }
        }
    }
}
