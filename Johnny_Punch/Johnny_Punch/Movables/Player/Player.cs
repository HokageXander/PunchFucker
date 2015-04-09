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
        public Vector2 speed;
        public KeyboardState keyBoardState, oldKeyBoardState;
        public bool fight, punch;
        public double fightTime, fightingCooldown = 500;
        int yLimitUp = 335, yLimitDown = 583;


        public Player(Texture2D tex, Vector2 pos)
            : base(tex, pos)
        {
            //this.tex = tex;
            this.pos = pos;
            this.boundingBox = boundingBox;
            animationBox = new Rectangle(0, 0, 75, 116);
            width /= 9;
            height /= 10;

            this.speed = new Vector2(0, 0);
            offset = new Vector2(width / 2, height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            oldKeyBoardState = keyBoardState;
            keyBoardState = Keyboard.GetState();

            pos += speed;
            speed.X = 0;
            if (!onGround)
                speed.Y += 0.14f;
            else
                speed.Y = 0;

            boundingBox = new Rectangle((int)pos.X - width / 2, (int)pos.Y - height / 2, width, height);
            feetBox = new Rectangle((int)pos.X - (int)49, (int)pos.Y + (117 - 4) - (int)offset.Y, width, height - (height - 4));
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
                if (keyBoardState.IsKeyDown(Keys.A))
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
                    animationBox.Width = 75;
                    animationBox.Y = 0;
                    Animation(120, 3, 75, gameTime);
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
            if (fightingCooldown >= 500 && keyBoardState.IsKeyDown(Keys.K) && !fight && onGround)
            {
                moving = false;
                fight = true;
                punch = true;
                fightingCooldown = 0;
            }
            if (punch)
            {
                animationBox.Width = 90;
                animationBox.Y = 514;
                Animation(120, 3, 87, gameTime);
            }
            if (punch && fightTime >= 380)
            {
                fight = false;
                punch = false;
            }

            #endregion
            Console.WriteLine(frameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, animationBox, Color.White, 0f, new Vector2(49, 59.5f), 1f, spriteEffect, 0f);

            spriteBatch.Draw(tex, feetBox, Color.Red);
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
