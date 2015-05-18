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

        protected int aggroRadius;
        protected Vector2 velocity, direction;
        public int damageToPlayer;
        protected float enemySpeed, scale;
        public double bossShootTimer;

        public Enemy(Texture2D tex, Vector2 pos)
            : base(tex, pos)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (!dead)
            {
                //boundingBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, width, height);
                AnimationTypes(gameTime);
            }
            else
                punchBox = new Rectangle((int)pos.X - 44, (int)pos.Y - 65, 0, 0); //tar bort punchboxen om han dör

            Death(gameTime);
            FloatLayerCalculator();

            if ((fightFrame == 0 && !moving) || walkFrame == 0)
            {
                animationBox.X = 0;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.playerShadow, new Vector2(pos.X, pos.Y + (height / 2)), null, new Color(0, 0, 0, 120), 0f, new Vector2(width / 2, height - height / 1.3f), 1, SpriteEffects.None, 0.1f);
            if (whiteNdead) // om han är död blir han vit
                spriteBatch.Draw(tex, pos, animationBox, new Color(255, 255, 255, 0), 0f, offset, scale, spriteEffect, floatLayerNr);
            else // om han inte är död är han färggrann
                spriteBatch.Draw(tex, pos, animationBox, Color.White, 0f, offset, scale, spriteEffect, floatLayerNr);
            spriteBatch.Draw(tex, feetBox, null, Color.Red, 0f, offset, spriteEffect, 0.8f);
            spriteBatch.Draw(tex, punchBox, Color.PaleGoldenrod);
            //spriteBatch.Draw(tex, boundingBox, Color.Red);

        }

        public void AnimationTypes(GameTime gameTime)
        {

            if (moving)
            {
                Animation(150, 3, 75, gameTime);
            }
            else if (!moving && !punch)
                Animation(150, 1, 75, gameTime);

            if (stunned)
            {
                animationBox.Y = 247;
                animationBox.X = 0;
                fightingCooldown = 0; //3 rader ner = resettar fiendens slag
                fightFrame = 0;
                punchBox = new Rectangle((int)pos.X - 44, (int)pos.Y - 65, 0, 0);
                stunnedTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (stunnedTimer >= 250)
                {
                    stunned = false;
                    stunnedTimer = 0;
                }
            }
        }

        public void Aggro(Player player) // aggrofunktionen när dom är nära spelaren
        {

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

                if (direction.X < 0)
                    spriteEffect = SpriteEffects.FlipHorizontally;
                else
                    spriteEffect = SpriteEffects.None;
            }
            else
                moving = false;

            if (Vector2.Distance(feetPos, playerfeetPos) < aggroRadius && !(feetBox.Intersects(player.playerRightBox) || feetBox.Intersects(player.playerLeftBox))
                && fightFrame >= 1) //om fienden är mitt i ett slag och hamnar ur range från spelaren så resettas animationen till animation.X = 0;
                animationBox.X = 0;

            if (feetBox.Intersects(player.playerRightBox))
                spriteEffect = SpriteEffects.FlipHorizontally;
            else if (feetBox.Intersects(player.playerLeftBox))
                spriteEffect = SpriteEffects.None;
        }

        public void SpawnAggro(Player player) // aggrofunktionen när dom spawnar och inte är i aggrozonen
        {
            Vector2 feetPos = new Vector2(feetBox.X, feetBox.Y);
            Vector2 playerfeetPos = new Vector2(player.feetBox.X, player.feetBox.Y);
            if (Vector2.Distance(feetPos, playerfeetPos) > aggroRadius)
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

                if (direction.X < 0)
                    spriteEffect = SpriteEffects.FlipHorizontally;
                else
                    spriteEffect = SpriteEffects.None;
            }
        }

        public void Fight(GameTime gameTime, Player player)
        {
            if (feetBox.Intersects(player.playerLeftBox) || feetBox.Intersects(player.playerRightBox) && !stunned)
            {
                fightingCooldown += gameTime.ElapsedGameTime.TotalMilliseconds;
                punch = true;
                if (fightingCooldown > 500)
                {
                    animationBox.Y = 380;
                    FightAnimation(150, 3, 92, gameTime);
                    #region slagets Hitbox
                    if (spriteEffect == SpriteEffects.FlipHorizontally) // om han är vänd åt ena hållet så rör sig slagets hitbox beroende på vilken frame den är på
                    {
                        if (fightFrame >= 1)
                        {
                            punchBox = new Rectangle((int)pos.X + 15, (int)pos.Y - 60, 30, 20);
                        }
                        if (fightFrame >= 2)
                        {
                            punchBox = new Rectangle((int)pos.X - 44, (int)pos.Y - 18, 27, 20);
                        }
                    }
                    else
                    {
                        if (fightFrame >= 1)
                        {
                            punchBox = new Rectangle((int)pos.X - 44, (int)pos.Y - 65, 30, 20);
                        }
                        if (fightFrame >= 2)
                        {
                            punchBox = new Rectangle((int)pos.X + 5, (int)pos.Y - 15, 27, 20);
                        }
                    }
                    #endregion
                }
            }
            else if (!(feetBox.Intersects(player.playerLeftBox) || feetBox.Intersects(player.playerRightBox)))
            {
                fightingCooldown = 0; //3 rader ner = resettar fiendens slag
                fightFrame = 0;
                punchBox = new Rectangle((int)pos.X - 44, (int)pos.Y - 65, 0, 0);
            }

            if (punch && fightFrame >= 3 && fightFrameTime <= 70)
            {
                punch = false;
                hasHit = false;
                fightFrame = 0;
                fightingCooldown = -200;
                punchBox = new Rectangle((int)pos.X - 44, (int)pos.Y - 65, 0, 0); //resettar slaget hitbox ovanför gubben igen
            }
        }

        public void Death(GameTime gameTime)
        {
            if (life <= 0)
            {
                dead = true;
                animationBox.Y = 638;
                animationBox.X = 0;
                animationBox.Width = 125;
                deathTimer1 += gameTime.ElapsedGameTime.TotalMilliseconds;
                boundingBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, 0, 0);

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

        public void FloatLayerCalculator()
        {
            floatLayerNr = 0 + pos.Y * 0.0010f; //numret blir mellan 0.335 och 0.583, vilket placerar en i rätt ordning(ritas först 0, ritas sist 1(?))
        }

        public void BossShoot(List<BossAttacks> bossAttacksList, GameTime gameTime, int dirNr)
        {
            bossShootTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (bossShootTimer >= 400)
            {
                bossShootTimer = 0;
                bossAttacksList.Add(new Bullet(TextureManager.bulletTex, new Vector2(pos.X, pos.Y), dirNr));
            }
        }

        public void BossDropBomb()
        {

        }
    }
}
