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
        public static bool bossEngaged, shootLeft, shootRight, dropBomb, died;
        public bool firstWalk = true;
        Rectangle topLeft, bottomLeft, topRight, bottomRight;

        public Boss(Texture2D tex, Vector2 pos)
            : base(tex, pos)
        {
            animationBox = new Rectangle(0, 0, 75, 116);
            width /= 9;
            height /= 9;
            aggroRadius = 350;
            damageToPlayer -= 1;
            life = 12;
            enemySpeed = 1.5f;
            scale = 1.2f;
            offset = new Vector2(width / 2, height / 2);

        }

        public override void Update(GameTime gameTime)
        {
            topLeft = new Rectangle(LevelManager.levelEndPosX - 1292, 280, 50, 50);
            bottomLeft = new Rectangle(LevelManager.levelEndPosX - 1247, 573, 50, 50);
            topRight = new Rectangle(LevelManager.levelEndPosX + 2, 280, 50, 50);
            bottomRight = new Rectangle(LevelManager.levelEndPosX - 10, 573, 50, 50);

            if (bossEngaged)
                BossMovement();
            if (dead)
            {
                velocity = new Vector2(0, 0);
                shootLeft = false;
                shootRight = false;
                dropBomb = false;
            }
            if (life <= 0)
            {
                died = true; //gör så att man kan gå vidare till level 3
            }

            pos += velocity;
            moving = true;
            animationBox.Y = 0;
            if (!firstWalk)
                boundingBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, width - 15, height - 10);
            else
                boundingBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, 0, 0);

            feetBox = new Rectangle((int)pos.X - (int)49, (int)pos.Y + (122 - 4) - (int)offset.Y, width - 10, height - (height - 4));
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(tex, boundingBox, null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 0.9f);

            //spriteBatch.Draw(tex, topLeft, null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 0.9f);
            //spriteBatch.Draw(tex, topRight, null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 0.9f);
            //spriteBatch.Draw(tex, bottomLeft, null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 0.9f);
            //spriteBatch.Draw(tex, bottomRight, null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 0.9f);

            base.Draw(spriteBatch);
        }

        public void BossMovement()
        {
            #region När han RP-går
            if (firstWalk)
            {
                direction = new Vector2(1900, 450) - pos;
                direction.Normalize();

                velocity.X = 1 * direction.X;
                velocity.Y = 1 * direction.Y;
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            #endregion
            #region När han spurtar iväg första gången
            if (pos.X <= 2200 && firstWalk)
            {
                firstWalk = false;
                direction = new Vector2(topRight.X, topRight.Y) - pos;
                direction.Normalize();

                velocity.X = 6 * direction.X;
                velocity.Y = 6 * direction.Y;
                spriteEffect = SpriteEffects.None;
            }
            #endregion
            #region När han når övre högra hörnet
            if (boundingBox.Intersects(topRight))
            {
                direction = new Vector2(bottomRight.X, bottomRight.Y) - pos;
                direction.Normalize();

                velocity.X = 1.2f * direction.X;
                velocity.Y = 1.2f * direction.Y;
                spriteEffect = SpriteEffects.FlipHorizontally;
                shootRight = true;
                dropBomb = false;
            }
            #endregion
            #region När han når nedre högra hörnet
            if (boundingBox.Intersects(bottomRight))
            {
                direction = new Vector2(topLeft.X + 50, topLeft.Y) - pos;
                direction.Normalize();

                velocity.X = 6 * direction.X;
                velocity.Y = 6 * direction.Y;
                spriteEffect = SpriteEffects.FlipHorizontally;
                dropBomb = true;
                shootRight = false;
            }
            #endregion
            #region När han når övre vänstra hörnet
            if (boundingBox.Intersects(topLeft))
            {
                direction = new Vector2(bottomLeft.X + 50, bottomLeft.Y) - pos;
                direction.Normalize();

                velocity.X = 1.2f * direction.X;
                velocity.Y = 1.2f * direction.Y;
                spriteEffect = SpriteEffects.None;
                shootLeft = true;
                dropBomb = false;
            }

            #endregion
            #region När han når nedre vänstra hörnet
            if (boundingBox.Intersects(bottomLeft))
            {
                direction = new Vector2(topRight.X, topRight.Y) - pos;
                direction.Normalize();

                velocity.X = 6 * direction.X;
                velocity.Y = 6 * direction.Y;
                spriteEffect = SpriteEffects.None;
                dropBomb = true;
                shootLeft = false;
            }
            #endregion
        }
    }
}
