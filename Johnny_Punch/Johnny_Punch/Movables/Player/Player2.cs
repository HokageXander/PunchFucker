using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Johnny_Punch
{
    class Player2 : Movables
    {
        //public int lifePoints;
        public Vector2 playerLeftPos, playerRightPos;
        public Rectangle playerLeftBox, playerRightBox;
        public KeyboardState keyBoardState, oldKeyBoardState;
        float shadowScale;
        int yLimitUp = 335, yLimitDown = 583;


        public Player2(Texture2D tex, Vector2 pos)
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
            percentLife = life / maxLife;
            pos += speed;
            posJump.X = pos.X;
            shadowScale = 1f - ((posJump.Y - pos.Y) * -0.01f);
            speed.X = 0;
            if (!onGround)
                speed.Y += 0.14f;
            else
                speed.Y = 0;

            FloatLayerCalculator();

            boundingBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, width, height);
            if (onGround) //Om vi är på marken så är Y = pos.Y
            {
                feetBox = new Rectangle((int)pos.X - (int)49, (int)pos.Y + (113 - 4) - (int)offset.Y, width, height - (height - 4));
                playerLeftPos = new Vector2(feetBox.X - width, feetBox.Y);
                playerRightPos = new Vector2(feetBox.X + width, feetBox.Y);
                playerRightBox = new Rectangle((int)pos.X + 15, (int)pos.Y + 35, 30, 25);
                playerLeftBox = new Rectangle((int)pos.X - 52, (int)pos.Y + 35, 30, 25);
            }
            else // Om vi är i luften är Y = jumpPos.Y
            {
                feetBox = new Rectangle((int)pos.X - (int)49, (int)posJump.Y + (113 - 4) - (int)offset.Y, width, height - (height - 4));
                playerRightBox = new Rectangle((int)pos.X + 15, (int)posJump.Y + 35, 30, 25);
                playerLeftBox = new Rectangle((int)pos.X - 52, (int)posJump.Y + 35, 30, 25);
            }
            playerLeftPos = new Vector2(feetBox.X - width, feetBox.Y);
            playerRightPos = new Vector2(feetBox.X + width, feetBox.Y);

            Moving(gameTime);
            Fight(gameTime);

            if ((fightFrame == 0 && !moving) || walkFrame == 0) //Tar bort den gamla animationen som höll på när man byter till en annan animation
            {
                animationBox.X = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (spriteEffect == SpriteEffects.None)
            {
                spriteBatch.Draw(TextureManager.playerShadow, new Vector2(posJump.X - 15, posJump.Y + (height / 2) - 9), null, new Color(0, 0, 0, 120), 0f, new Vector2(63 / 2, 21 / 2), shadowScale, SpriteEffects.None, 0.1f);
            }
            else if (spriteEffect == SpriteEffects.FlipHorizontally)
                spriteBatch.Draw(TextureManager.playerShadow, new Vector2(posJump.X - 7, posJump.Y + (height / 2) - 9), null, new Color(0, 0, 0, 120), 0f, new Vector2(63 / 2, 21 / 2), shadowScale, SpriteEffects.None, 0.1f);

            spriteBatch.Draw(tex, pos, animationBox, Color.White, 0f, new Vector2(49, 59.5f), 1f, spriteEffect, floatLayerNr);

            spriteBatch.Draw(tex, feetBox, Color.Red);
            //spriteBatch.Draw(tex, playerRightBox, Color.Blue);
            //spriteBatch.Draw(tex, playerLeftBox, Color.Red);
            //spriteBatch.Draw(tex, punchBox, Color.Blue);
            //spriteBatch.Draw(tex, boundingBox, Color.Red);
        }

        public void Moving(GameTime gameTime)
        {
            if (!fight)
            {
                #region Walk Right
                if (keyBoardState.IsKeyDown(Keys.L))
                {
                    speed.X = 3;
                    moving = true;
                    spriteEffect = SpriteEffects.None;
                    if (keyBoardState.IsKeyDown(Keys.I) || keyBoardState.IsKeyDown(Keys.K))
                        speed.X = 2f;
                }
                #endregion
                #region Walk Left
                if (keyBoardState.IsKeyDown(Keys.J) && pos.X >= Camera.prevCentre.X + 45)
                {
                    speed.X = -3;
                    moving = true;
                    spriteEffect = SpriteEffects.FlipHorizontally;
                    if (keyBoardState.IsKeyDown(Keys.I) || keyBoardState.IsKeyDown(Keys.K))
                        speed.X = -2f;
                }
                #endregion
                #region Walk Up
                if (keyBoardState.IsKeyDown(Keys.I) && feetBox.Y >= yLimitUp && onGround)
                {
                    speed.Y = -3;
                    moving = true;
                    if (keyBoardState.IsKeyDown(Keys.J) || keyBoardState.IsKeyDown(Keys.L))
                        speed.Y = -2f;
                }
                #endregion
                #region Walk Down
                if (keyBoardState.IsKeyDown(Keys.K) && feetBox.Y <= yLimitDown && onGround)
                {
                    speed.Y = 3;
                    moving = true;
                    if (keyBoardState.IsKeyDown(Keys.J) || keyBoardState.IsKeyDown(Keys.L))
                        speed.Y = 2f;
                }
                #endregion
                #region Moving Bool
                if (!(keyBoardState.IsKeyDown(Keys.J)) && !(keyBoardState.IsKeyDown(Keys.L)) &&
                    !(keyBoardState.IsKeyDown(Keys.I)) && !(keyBoardState.IsKeyDown(Keys.K)))
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
                    if (keyBoardState.IsKeyDown(Keys.I) && feetBox.Y >= yLimitUp)
                    {
                        pos.Y += -1.5f;
                        posJump.Y += -1.5f;
                    }
                    if (keyBoardState.IsKeyDown(Keys.K) && feetBox.Y <= yLimitDown && posJump.Y <= yLimitDown - 50)
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
                if (keyBoardState.IsKeyDown(Keys.O) && oldKeyBoardState.IsKeyDown(Keys.O) && onGround)
                {
                    posJump.Y = pos.Y;
                    speed.Y = -3.2f;
                    onGround = false;
                    life -= 1;
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
            if (fightingCooldown >= 300 && keyBoardState.IsKeyDown(Keys.P) && !fight && onGround)
            {
                frameTime = 120;
                walkFrame = -1;
                moving = false;
                fight = true;
                punch = true;
                fightingCooldown = 0;
            }
            if (punch)
            {

                animationBox.Width = 83;
                animationBox.Y = 514;
                FightAnimation(90, 3, 83, gameTime);
                if (spriteEffect == SpriteEffects.FlipHorizontally)
                {
                    punchBox = new Rectangle((int)pos.X - 40 - (fightFrame * 10), (int)pos.Y - 28, 50, 20);
                }
                else
                    punchBox = new Rectangle((int)pos.X - 40 + (fightFrame * 15), (int)pos.Y - 28, 50, 20);
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

        public void FloatLayerCalculator()
        {
            if (!onGround)
            {
                floatLayerNr = 0 + posJump.Y * 0.0010f; //numret blir mellan 0.335 och 0.583, vilket placerar en i rätt ordning
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
