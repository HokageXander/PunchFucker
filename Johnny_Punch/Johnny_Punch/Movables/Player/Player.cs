using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Johnny_Punch
{
    class Player : Movables
    {
        //public int lifePoints;
        public Vector2 speed, playerLeftPos, playerRightPos;
        public Rectangle playerLeftBox, playerRightBox;
        public KeyboardState keyBoardState, oldKeyBoardState;
        float shadowScale;
        int yLimitUp = 335, yLimitDown = 583;


        public Player(Texture2D tex, Vector2 pos)
            : base(tex, pos)
        {

            posJump = pos;
            animationBox = new Rectangle(0, 0, 75, 116);
            width /= 9;
            height /= 10;
            shadowScale = 1;
            this.life = 10;
            maxLife = life;
            this.speed = new Vector2(0, 0);
            offset = new Vector2(width / 2, height / 2);

        }

        public override void Update(GameTime gameTime)
        {
            oldKeyBoardState = keyBoardState;
            keyBoardState = Keyboard.GetState();

            Death(gameTime);
            FloatLayerCalculator();
            //Console.Write(life);

            percentLife = life / maxLife;

            pos += speed;
            posJump.X = pos.X;
            shadowScale = 1f - ((posJump.Y - pos.Y) * -0.01f);
            speed.X = 0;
            if (!onGround || dead)
                speed.Y += 0.14f;
            else
                speed.Y = 0;

            if (!dead)
            {
                boundingBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, width - 20, height);
                playerLeftPos = new Vector2(feetBox.X - 55, feetBox.Y); //positionen som fienden ska gå till vänster om spelaren
                playerRightPos = new Vector2(feetBox.X + width - 30, feetBox.Y);

                if (onGround) //Om vi är på marken så är Y = pos.Y
                {
                    feetBox = new Rectangle((int)pos.X - (int)49, (int)pos.Y + (113 - 4) - (int)offset.Y, width, height - (height - 4));
                    if (spriteEffect == SpriteEffects.FlipHorizontally)
                    {
                        playerRightBox = new Rectangle((int)pos.X + 5, (int)pos.Y + 35, 25, 25); //rektangeln till höger om spelaren. Om fienden krockar i börjar han slåss
                        playerLeftBox = new Rectangle((int)pos.X - 30, (int)pos.Y + 35, 25, 25);
                    }
                    else
                    {
                        playerRightBox = new Rectangle((int)pos.X - 15, (int)pos.Y + 35, 25, 25); //rektangeln till höger om spelaren. Om fienden krockar i börjar han slåss
                        playerLeftBox = new Rectangle((int)pos.X - 44, (int)pos.Y + 35, 25, 25);
                    }
                }
                else // Om vi är i luften är Y = jumpPos.Y
                {
                    feetBox = new Rectangle((int)pos.X - (int)49, (int)posJump.Y + (113 - 4) - (int)offset.Y, width, height - (height - 4));
                    if (spriteEffect == SpriteEffects.FlipHorizontally)
                    {
                        playerRightBox = new Rectangle((int)pos.X + 5, (int)posJump.Y + 35, 25, 25); //rektangeln till höger om spelaren. Om fienden krockar i börjar han slåss
                        playerLeftBox = new Rectangle((int)pos.X - 30, (int)posJump.Y + 35, 25, 25);
                    }
                    else
                    {
                        playerRightBox = new Rectangle((int)pos.X - 15, (int)posJump.Y + 35, 25, 25); //rektangeln till höger om spelaren. Om fienden krockar i börjar han slåss
                        playerLeftBox = new Rectangle((int)pos.X - 44, (int)posJump.Y + 35, 25, 25);
                    }
                }

                Moving(gameTime);
                Fight(gameTime);
                Block(gameTime);


                if ((fightFrame == 0 && !moving) || walkFrame == 0 && !fight) //Tar bort den gamla animationen som höll på när man byter till en annan animation
                {
                    animationBox.X = 0;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            #region Shadows
            if (spriteEffect == SpriteEffects.None && !dead)
            {
                spriteBatch.Draw(TextureManager.playerShadow, new Vector2(posJump.X - 15, posJump.Y + (height / 2) - 9), null, new Color(0, 0, 0, 120), 0f, new Vector2(63 / 2, 21 / 2), shadowScale, SpriteEffects.None, 0.1f);
            }
            else if (spriteEffect == SpriteEffects.FlipHorizontally && !dead)
                spriteBatch.Draw(TextureManager.playerShadow, new Vector2(posJump.X - 7, posJump.Y + (height / 2) - 9), null, new Color(0, 0, 0, 120), 0f, new Vector2(63 / 2, 21 / 2), shadowScale, SpriteEffects.None, 0.1f);
            else if (dead)
            {
                posJump.Y = pos.Y;
                spriteBatch.Draw(TextureManager.playerShadow, new Vector2(posJump.X - 7, posJump.Y + (height / 2) - 9), null, new Color(0, 0, 0, 120), 0f, new Vector2(63 / 2, 21 / 2), shadowScale, SpriteEffects.None, 0.1f);
            }
            #endregion

            spriteBatch.Draw(tex, pos, animationBox, Color.White, 0f, new Vector2(49, 59.5f), 1f, spriteEffect, floatLayerNr);

            //spriteBatch.Draw(tex, feetBox, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            //spriteBatch.Draw(tex, playerRightBox, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            //spriteBatch.Draw(tex, playerLeftBox, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(tex, punchBox, null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 1);
            //spriteBatch.Draw(tex, boundingBox, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(tex, blockBox, null, Color.Aquamarine, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public void Death(GameTime gameTime)
        {
            if (life <= 0)
            {
                dead = true;
                animationBox.Y = 1020;
                animationBox.X = 0;
                animationBox.Width = 125;
                deathTimer1 += gameTime.ElapsedGameTime.TotalMilliseconds;
                boundingBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, 0, 0);
            }
        }

        public void Moving(GameTime gameTime)
        {
            if (!fight)
            {
                #region Walk Right
                if (keyBoardState.IsKeyDown(Keys.D) && !block)
                {
                    speed.X = 3;
                    moving = true;
                    spriteEffect = SpriteEffects.None;
                    if (keyBoardState.IsKeyDown(Keys.W) || keyBoardState.IsKeyDown(Keys.S))
                        speed.X = 2f;
                }
                #endregion
                #region Walk Left
                if (keyBoardState.IsKeyDown(Keys.A) && pos.X >= Camera.prevCentre.X + 45 && !block)
                {
                    speed.X = -3;
                    moving = true;
                    spriteEffect = SpriteEffects.FlipHorizontally;
                    if (keyBoardState.IsKeyDown(Keys.W) || keyBoardState.IsKeyDown(Keys.S))
                        speed.X = -2f;
                }
                #endregion
                #region Walk Up
                if (keyBoardState.IsKeyDown(Keys.W) && feetBox.Y >= yLimitUp && onGround && !block)
                {
                    speed.Y = -3;
                    moving = true;
                    if (keyBoardState.IsKeyDown(Keys.A) || keyBoardState.IsKeyDown(Keys.D))
                        speed.Y = -2f;
                }
                #endregion
                #region Walk Down
                if (keyBoardState.IsKeyDown(Keys.S) && feetBox.Y <= yLimitDown && onGround && !block)
                {
                    speed.Y = 3;
                    moving = true;
                    if (keyBoardState.IsKeyDown(Keys.A) || keyBoardState.IsKeyDown(Keys.D))
                        speed.Y = 2f;
                }
                #endregion
                #region Moving Bool
                if (!(keyBoardState.IsKeyDown(Keys.A)) && !(keyBoardState.IsKeyDown(Keys.D)) &&
                    !(keyBoardState.IsKeyDown(Keys.W)) && !(keyBoardState.IsKeyDown(Keys.S)))
                {
                    moving = false;
                }
                if (moving && onGround)
                {
                    posJump.Y = pos.Y;
                    animationBox.Width = 75;
                    animationBox.Y = 0;
                    Animation(120, 4, 75, gameTime);
                }
                else if (!moving && onGround && !fight)
                {
                    animationBox.Width = 75;
                    animationBox.X = 0;
                    animationBox.Y = 0;
                    //Animation(120, 1, 75, gameTime);
                }

                #endregion
                #region Onground Bool and Jump
                if (!onGround)
                {
                    animationBox.Width = 75;
                    animationBox.Y = 116;
                    animationBox.X = 0;
                    //Animation(120, 1, 75, gameTime);
                    if (keyBoardState.IsKeyDown(Keys.W) && feetBox.Y >= yLimitUp)
                    {
                        pos.Y += -1.5f;
                        posJump.Y += -1.5f;
                    }
                    if (keyBoardState.IsKeyDown(Keys.S) && feetBox.Y <= yLimitDown && posJump.Y <= yLimitDown - 50)
                    {
                        pos.Y += 1.5f;
                        posJump.Y += 1.5f;
                    }
                    if (pos.Y >= posJump.Y)
                    {
                        pos.Y = posJump.Y;
                        onGround = true;
                        speed.Y = 0;
                    }
                }
                if (keyBoardState.IsKeyDown(Keys.Space) && oldKeyBoardState.IsKeyDown(Keys.Space) && onGround && !block) // här hoppar man
                {
                    posJump.Y = pos.Y; //när man hoppar svaras punkten man hoppade från i y-led. Man landar på den punkten i y-led sen
                    speed.Y = -3.2f;
                    onGround = false;
                }
                #endregion
            }

        }

        public void Fight(GameTime gameTime)
        {
            if (fight)
            {
                fightTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                fightTime = 0;
                fightingCooldown += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            #region StandardHit
            if (fightingCooldown >= 300 && keyBoardState.IsKeyDown(Keys.T) && !fight && onGround && !block)
            {
                frameTime = 120;
                walkFrame = 0;
                moving = false;
                fight = true;
                punch = true;
                fightingCooldown = 0;

            }
            if (punch)
            {
                animationBox.Width = 81;
                animationBox.Y = 514;
                FightAnimation(90, 3, 81, gameTime);
                if (spriteEffect == SpriteEffects.FlipHorizontally)
                {
                    punchBox = new Rectangle((int)pos.X - 10 - (fightFrame * 15), (int)pos.Y - 25, 50, 20);
                }
                else
                    punchBox = new Rectangle((int)pos.X - 40 + (fightFrame * 8), (int)pos.Y - 25, 50, 20);

            }
            if (punch && fightFrame >= 3)
            {
                fight = false;
                punch = false;
                hasHit = false;
                fightFrame = 0;
                fightFrameTime = 80;
                punchBox = new Rectangle((int)pos.X - 40, (int)pos.Y - 28, 0, 0);
            }

            #endregion

        }

        public void Block(GameTime gameTime)
        {
            if (fightingCooldown >= 300 && keyBoardState.IsKeyDown(Keys.G) && !fight && !block && onGround)
            {
                block = true;
                animationBox.Width = 75;
                animationBox.X = 0;
                animationBox.Y = 0;
                fightingCooldown = 0;
            }

            if (block)
            {
                blockTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (spriteEffect == SpriteEffects.FlipHorizontally)
                {
                    blockBox = new Rectangle((int)pos.X - 50, (int)pos.Y - 50, 35, height);
                }
                else
                    blockBox = new Rectangle((int)pos.X, (int)pos.Y - 50, 35, height);
            }
            if (block && blockTimer >= 700)
            {
                block = false;
                blockTimer = 0;
                blockBox = new Rectangle((int)pos.X, (int)pos.Y - 40, 0, 0);
            }
        }

        public void FloatLayerCalculator()
        {

            if (spriteEffect == SpriteEffects.None)

            {
                floatLayerNr = 0 + posJump.Y * 0.0010f; //numret blir mellan 0.335 och 0.583, vilket placerar en i rätt ordning(ritas först 0, ritas sist 1(?))
            }
            else
                floatLayerNr = 0 + pos.Y * 0.0010f;
        }

        public Vector2 GetPos
        {
            get
            {
                return pos;
            }
        }

        public Rectangle GetRec
        {
            get
            {
                return boundingBox;
            }
        }

    }
}
