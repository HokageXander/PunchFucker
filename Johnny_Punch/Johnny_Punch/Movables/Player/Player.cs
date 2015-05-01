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
            life = 10;
            this.speed = new Vector2(0, 0);
            offset = new Vector2(width / 2, height / 2);

        }

        public override void Update(GameTime gameTime)
        {
            oldKeyBoardState = keyBoardState;
            keyBoardState = Keyboard.GetState();

            pos += speed;
            posJump.X = pos.X;
            shadowScale = 1f - ((posJump.Y - pos.Y) * -0.01f);
            speed.X = 0;
            if (!onGround)
                speed.Y += 0.14f;
            else
                speed.Y = 0;

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
        }

        public void Moving(GameTime gameTime)
        {
            if (!fight)
            {
                #region Walk Right
                if (keyBoardState.IsKeyDown(Keys.D))
                {
                    speed.X = 3;
                    moving = true;
                    spriteEffect = SpriteEffects.None;
                    if (keyBoardState.IsKeyDown(Keys.W) || keyBoardState.IsKeyDown(Keys.S))
                        speed.X = 2f;
                }
                #endregion
                #region Walk Left
                if (keyBoardState.IsKeyDown(Keys.A) && pos.X >= Camera.prevCentre.X + 45)
                {
                    speed.X = -3;
                    moving = true;
                    spriteEffect = SpriteEffects.FlipHorizontally;
                    if (keyBoardState.IsKeyDown(Keys.W) || keyBoardState.IsKeyDown(Keys.S))
                        speed.X = -2f;
                }
                #endregion
                #region Walk Up
                if (keyBoardState.IsKeyDown(Keys.W) && feetBox.Y >= yLimitUp && onGround)
                {
                    speed.Y = -3;
                    moving = true;
                    if (keyBoardState.IsKeyDown(Keys.A) || keyBoardState.IsKeyDown(Keys.D))
                        speed.Y = -2f;
                }
                #endregion
                #region Walk Down
                if (keyBoardState.IsKeyDown(Keys.S) && feetBox.Y <= yLimitDown && onGround)
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
                //if (onGround)
                //    animationBox.Y = 0;
                if (keyBoardState.IsKeyDown(Keys.Space) && oldKeyBoardState.IsKeyDown(Keys.Space) && onGround)
                {
                    posJump.Y = pos.Y;
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
            if (fightingCooldown >= 300 && keyBoardState.IsKeyDown(Keys.K) && !fight && onGround)
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
                animationBox.Width = 77;
                animationBox.Y = 514;
                FightAnimation(90, 3, 77, gameTime);
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
                fightFrame = 0;
                fightFrameTime = 80;
                punchBox = new Rectangle((int)pos.X - 40, (int)pos.Y - 28, 0, 0);
            }

            #endregion

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (spriteEffect == SpriteEffects.None)
            {
                spriteBatch.Draw(TextureManager.playerShadow, new Vector2(posJump.X - 15, posJump.Y + (height / 2) - 9), null, new Color(0, 0, 0, 120), 0f, new Vector2(63 / 2, 21 / 2), shadowScale, SpriteEffects.None, 0);
            }
            else if (spriteEffect == SpriteEffects.FlipHorizontally)
                spriteBatch.Draw(TextureManager.playerShadow, new Vector2(posJump.X - 7, posJump.Y + (height / 2) - 9), null, new Color(0, 0, 0, 120), 0f, new Vector2(63 / 2, 21 / 2), shadowScale, SpriteEffects.None, 0);

            spriteBatch.Draw(tex, pos, animationBox, Color.White, 0f, new Vector2(49, 59.5f), 1f, spriteEffect, 0f);

            spriteBatch.Draw(tex, feetBox, Color.Red);
            //spriteBatch.Draw(tex, playerRightBox, Color.Blue);
            //spriteBatch.Draw(tex, playerLeftBox, Color.Red);
            //spriteBatch.Draw(tex, punchBox, Color.Blue);
            //spriteBatch.Draw(tex, boundingBox, Color.Red);
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
