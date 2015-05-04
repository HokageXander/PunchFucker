using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Johnny_Punch
{
    abstract class Enemy : Movables
    {
        public static bool angryFace;
        protected int aggroRadius;
        protected Vector2 velocity, direction;
        public int damageToPlayer;
        protected float enemySpeed;
        public Enemy(Texture2D tex, Vector2 pos)
            : base(tex, pos)
        {


        }
        public override void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, width, height);
            if (!dead)
                AnimationTypes(gameTime);
            Death(gameTime);
            if ((fightFrame == 0 && !moving) || walkFrame == 0)
            {
                animationBox.X = 0;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.playerShadow, new Vector2(pos.X, pos.Y + (height / 2)), null, new Color(0, 0, 0, 120), 0f, new Vector2(width / 2, height - height / 1.3f), 1, SpriteEffects.None, 0);
            if (whiteNdead) // om han är död blir han vit
                spriteBatch.Draw(tex, pos, animationBox, new Color(255, 255, 255, 0), 0f, offset, 1f, spriteEffect, 0f);
            else // om han inte är död är han färggrann
                spriteBatch.Draw(tex, pos, animationBox, Color.White, 0f, offset, 1f, spriteEffect, 0f);
            spriteBatch.Draw(tex, feetBox, Color.Red);
            //spriteBatch.Draw(tex, boundingBox, Color.Red);
        }

        public void AnimationTypes(GameTime gameTime)
        {

            if (moving)
            {
                Animation(150, 3, 75, gameTime);
            }
            else
                Animation(150, 1, 75, gameTime);

            if (stunned)
            {
                animationBox.Y = 247;
                stunnedTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (stunnedTimer >= 250)
                {
                    stunned = false;
                    stunnedTimer = 0;
                }
            }
        }
        public void Aggro(Player player)
        {
            angryFace = false;
            animationBox.Y = 0;
            Vector2 feetPos = new Vector2(feetBox.X, feetBox.Y);
            Vector2 playerfeetPos = new Vector2(player.feetBox.X, player.feetBox.Y);
            if (Vector2.Distance(feetPos, playerfeetPos) < aggroRadius && !(feetBox.Intersects(player.playerRightBox) || feetBox.Intersects(player.playerLeftBox)))
            {
                moving = true;
                if (feetPos.X < playerfeetPos.X)
                {
                    direction = player.playerLeftPos - feetPos;
                }
                else
                    direction = player.playerRightPos - feetPos;

                direction.Normalize();
                velocity.X = enemySpeed * direction.X;
                velocity.Y = enemySpeed * direction.Y;
                pos += velocity;
                angryFace = true;
                if (direction.X < 0)
                    spriteEffect = SpriteEffects.FlipHorizontally;
                else
                    spriteEffect = SpriteEffects.None;
            }
            else
                moving = false;

            if (feetBox.Intersects(player.playerRightBox))
                spriteEffect = SpriteEffects.FlipHorizontally;
            else if (feetBox.Intersects(player.playerLeftBox))
                spriteEffect = SpriteEffects.None;
        }

        public void Fight(GameTime gameTime, Player player)
        {
            if (feetBox.Intersects(player.playerLeftBox) || feetBox.Intersects(player.playerRightBox))
            {
                fightTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                punch = true;
                if (fightTime > 500)
                {
                    animationBox.Y = 380;
                    FightAnimation(150, 3, 92, gameTime);
                }
            }

            if (punch && fightFrame >= 3)
            {
                punch = false;
                fightFrame = 0;
                fightTime = -200;
            }
        }

        public void Death(GameTime gameTime)
        {
            Console.WriteLine(life);
            if (life <= 0)
            {
                dead = true;
                animationBox.Y = 638;
                animationBox.Width = 125;
                deathTimer1 += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (deathTimer1 >= 1500)
                {
                    deathTimer2 += gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (deathTimer2 >= 150 && !whiteNdead)
                    {
                        whiteNdead = true; //gör gubben vit
                        deathBlinkCount++; //räknar, efter 7 blinkningar deletas gubben från banan
                        deathTimer2 = 0;
                    }
                    if (deathTimer2 >= 150 && whiteNdead)
                    {
                        whiteNdead = false; //gör gubben färggrann
                        deathBlinkCount++;
                        deathTimer2 = 0;
                    }
                }
            }
        }
    }
}
