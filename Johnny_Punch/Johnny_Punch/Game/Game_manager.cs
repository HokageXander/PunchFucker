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






        public enum GameState
        {
            Start, Play, Pause, End
        }
        public GameState gameState;

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch)
        {
            TextureManager.LoadContent(Content);
            enemyManager = new EnemyManager(GraphicsDevice);
            playerManager = new PlayerManager();
            levelManager = new LevelManager(Content);
            gameState = GameState.Play;



        }

        public void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                case GameState.Start:
                    break;

                case GameState.Play:
                    enemyManager.Update(gameTime);
                    enemyManager.AggroPlayer(playerManager);
                    playerManager.Update(gameTime);
                    playerManager.LandingPunches(enemyManager);
                    TotalPlayTime(gameTime);


                    time += gameTime.ElapsedGameTime.TotalSeconds;
                    break;

                case GameState.Pause:
                    break;

                case GameState.End:
                    break;
            }




        }
        public void DrawStats(SpriteBatch spriteBatch)
        {

            if (!Enemy.angryFace)
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
            switch (gameState)
            {
                case GameState.Start:

                    break;

                case GameState.Play:

                    levelManager.Draw(spriteBatch);
                    enemyManager.Draw(spriteBatch);
                    playerManager.Draw(spriteBatch);

                    break;

                case GameState.Pause:
                    break;

                case GameState.End:

                    break;
            }

            //spriteBatch.Draw(TextureManager.backgroundTex, Vector2.Zero, Color.White);


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
