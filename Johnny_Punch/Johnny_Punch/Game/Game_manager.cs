using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Johnny_Punch
{
    class GameManager
    {
        EnemyManager enemyManager;
        public PlayerManager playerManager;
        LevelManager levelManager;


        int firstDigitSeconds, secondDigitSeconds, firstDigitMinutes, secondDigitMinutes, firstDigitHours, secondDigitHours;
        double time, digitSeconds;






        enum GameState
        {
            Start, Play, Pause, End
        }

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch)
        {
            TextureManager.LoadContent(Content);
            enemyManager = new EnemyManager(GraphicsDevice);
            playerManager = new PlayerManager();
            levelManager = new LevelManager(Content);
        }

        public void Update(GameTime gameTime)
        {
            enemyManager.Update(gameTime);
            playerManager.Update(gameTime);
            TotalPlayTime(gameTime);
            

            for (int i = 0; i < enemyManager.enemyList.Count; i++)
            {
                for (int j = 0; j < playerManager.playerList.Count; j++)
                {
                    enemyManager.enemyList[i].Aggro(playerManager.playerList[j]);
                }
            }
            //totalPlayTime = (int)time;

            time += gameTime.ElapsedGameTime.TotalSeconds;

        }
        public void DrawStats(SpriteBatch spriteBatch)
        {
            
            if(!Enemy.angryFace)
            {
            spriteBatch.Draw(TextureManager.statusBarTex, Vector2.Zero, Color.White);
            }
            else
            {
                spriteBatch.Draw(TextureManager.statusBarAngryTex, Vector2.Zero, Color.White);
            }
            spriteBatch.DrawString(TextureManager.timeFont, secondDigitHours.ToString() + firstDigitHours.ToString() +
                ":" + secondDigitMinutes.ToString() + firstDigitMinutes.ToString() +
                ":" + secondDigitSeconds.ToString() + firstDigitSeconds.ToString(), new Vector2(523, 630), Color.Green);
        }
   



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.backgroundTex, Vector2.Zero, Color.White);
            levelManager.Draw(spriteBatch);
            enemyManager.Draw(spriteBatch);
            playerManager.Draw(spriteBatch);

            //spriteBatch.DrawString(TextureManager.timeFont, totalPlayTime.ToString(), new Vector2(590, 630), Color.Green);




        }


        public static void CheckIsDead()
        {

        }

        public void TotalPlayTime(GameTime gameTime)
        {
            digitSeconds += gameTime.ElapsedGameTime.TotalSeconds;

            firstDigitSeconds = (int)digitSeconds;
            if (firstDigitSeconds >= 10)
            {
                secondDigitSeconds++;
                firstDigitSeconds = 0;
                digitSeconds = 0;
            }
            if (secondDigitSeconds >= 6)
            {
                firstDigitMinutes++;
                secondDigitSeconds = 0;
            }

            if (firstDigitMinutes >= 10)
            {
                secondDigitMinutes++;
                firstDigitMinutes = 0;
            }
            if (secondDigitMinutes >= 6)
            {
                firstDigitHours++;
                secondDigitMinutes = 0;
            }
            if (firstDigitHours >= 10)
            {
                secondDigitHours++;
                firstDigitHours = 0;
            }
        }
    }
}
