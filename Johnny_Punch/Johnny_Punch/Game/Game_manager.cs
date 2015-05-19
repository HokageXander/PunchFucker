using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Johnny_Punch
{
    class GameManager
    {

        public Menu menu;
        public PlayerManager playerManager;
        EnemyManager enemyManager;
        LevelManager levelManager;
        KeyboardState keyBoardState, oldKeyBoardState;
        public int firstDigitSeconds, secondDigitSeconds, firstDigitMinutes, secondDigitMinutes, firstDigitHours, secondDigitHours;
        public double time, digitSeconds;
        public int intro = 0;
        public double introTimer = 2f;
        public TimeSpan introSwitch;

        public enum GameState
        {
            Intro, Menu, Play, Pause, End
        }
        public GameState gameState;

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch)
        {
            TextureManager.LoadContent(Content);
            menu = new Menu(Content);
            enemyManager = new EnemyManager(GraphicsDevice);
            playerManager = new PlayerManager();
            levelManager = new LevelManager(Content);
            gameState = GameState.Intro;
 
            introSwitch = TimeSpan.FromSeconds(introTimer);
        }

        public void Update(GameTime gameTime, GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            oldKeyBoardState = keyBoardState;
            keyBoardState = Keyboard.GetState();
            Console.WriteLine(introSwitch);
            switch (gameState)
            {
                case GameState.Intro:


                    if (introSwitch.TotalSeconds > 0)
                        introSwitch = introSwitch.Subtract(gameTime.ElapsedGameTime);
                    else
                    {
                        introSwitch = TimeSpan.FromSeconds(introTimer);
                        intro++;
                    }

                    if (intro == 5)
                        gameState = GameState.Menu;
                    break;


                case GameState.Menu:
                    menu.Update(gameTime);
                    if (menu.play == true)
                    {
                        gameState = GameState.Play;
                        enemyManager = new EnemyManager(GraphicsDevice); //allt under resettar och laddar in allt på nytt ifall man valt quit i pausmenyn
                        playerManager = new PlayerManager();
                        levelManager = new LevelManager(Content);
                        ResetTime();
                        Game1.ready = false;
                    }
                    break;

                case GameState.Play:
                    if (keyBoardState.IsKeyDown(Keys.B) && oldKeyBoardState.IsKeyUp(Keys.B))
                    {
                        gameState = GameState.Pause;
                        menu.play = false;
                        menu.menuState = Menu.MenuState.Pause;
                    }


                    levelManager.Update(gameTime);
                    levelManager.NextLevel(playerManager, enemyManager);

                    playerManager.Update(gameTime);
                    playerManager.LandingPunches(enemyManager);
                    playerManager.CollectItems(levelManager);

                    enemyManager.Update(gameTime);
                    enemyManager.AggroPlayer(playerManager, gameTime);
                    enemyManager.FightPlayer(playerManager);
                    enemyManager.IsBlocked(playerManager, gameTime);
                    enemyManager.SpawnEnemy(playerManager);
                    enemyManager.BossFightStart(playerManager);
                    enemyManager.BossDamage(playerManager);
                    enemyManager.CameraStopWhenEnemySpawn(playerManager, gameTime);


                    TotalPlayTime(gameTime);


                    time += gameTime.ElapsedGameTime.TotalSeconds;

                    if (LevelManager.end)
                        gameState = GameState.End;
                    break;

                case GameState.Pause:
                    menu.Update(gameTime);

                    if (menu.play == true)
                        gameState = GameState.Play;

                    if (menu.menuState == Menu.MenuState.MainMenu)
                        gameState = GameState.Menu;
                    break;

                case GameState.End:
                    break;
            }
        }

        public void DrawStats(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < playerManager.playerList.Count; i++)// Lifebars till spelarna
            {
                if (playerManager.playerList[0].percentLife < 1.0f)
                {
                    spriteBatch.Draw(TextureManager.lifeBarTex, new Rectangle(120 /*- 50 * (PlayerManager.players - 1)*/, 608 - ((PlayerManager.players - 1) * 42), 155, 35), Color.Red);
                }
                spriteBatch.Draw(TextureManager.lifeBarTex, new Rectangle(120, 608 - ((PlayerManager.players - 1) * 42), (int)(155 * playerManager.playerList[0].percentLife), 35), Color.Green);

                if (PlayerManager.players == 2)
                {
                    if (playerManager.playerList[1].percentLife < 1.0f)
                    {
                        spriteBatch.Draw(TextureManager.lifeBarTex, new Rectangle(254, 620, 155, 35), Color.Red);
                    }
                    int healthPos = (int)(playerManager.playerList[1].maxLife - playerManager.playerList[1].life);
                    spriteBatch.Draw(TextureManager.lifeBarTex, new Rectangle(254 + (int)(15.5f * healthPos), 620, (int)(155 * playerManager.playerList[1].percentLife), 35), Color.Green);

                }
            }
            if (PlayerManager.players == 1)
            {
                spriteBatch.Draw(TextureManager.statusBarPlayerOneTex, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            }
            if (PlayerManager.players == 2)
            {
                spriteBatch.Draw(TextureManager.statusBarPlayerTwoTex, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            }

            switch (gameState)
            {
                case GameState.Pause:

                    menu.Draw(spriteBatch);

                    break;
            }

            spriteBatch.DrawString(TextureManager.timeFont, secondDigitHours.ToString() + firstDigitHours.ToString() +
            ":" + secondDigitMinutes.ToString() + firstDigitMinutes.ToString() +
            ":" + secondDigitSeconds.ToString() + firstDigitSeconds.ToString(), new Vector2(523, 630), Color.Green);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (gameState)
            {
                case GameState.Intro:


                    if (intro == 0)                  
                        spriteBatch.Draw(TextureManager.introScreen1, Vector2.Zero, Color.Red);
                    if (intro == 1)
                    {
                        spriteBatch.Draw(TextureManager.introScreen2, Vector2.Zero, Color.Blue);
                        
                    }
                    if(intro == 2)
                        spriteBatch.Draw(TextureManager.introScreen3, Vector2.Zero, Color.Green);
                    if(intro ==3)
                        spriteBatch.Draw(TextureManager.introScreen4, Vector2.Zero, Color.Pink);
           
                    break;


                case GameState.Menu:
                    menu.Draw(spriteBatch);
                    break;

                case GameState.Play:

                    levelManager.Draw(spriteBatch);
                    enemyManager.Draw(spriteBatch);
                    playerManager.Draw(spriteBatch);

                    break;

                case GameState.Pause:
                    levelManager.Draw(spriteBatch);
                    enemyManager.Draw(spriteBatch);
                    playerManager.Draw(spriteBatch);

                    break;

                case GameState.End:

                    if (Boss.died)
                        spriteBatch.Draw(TextureManager.endScreenTex, Vector2.Zero, Color.White);

                    else
                        spriteBatch.Draw(TextureManager.gameOverScreenTex, Vector2.Zero, Color.White);

                    break;
            }
        }



        public void TotalPlayTime(GameTime gameTime)
        {
            #region DigitTimer
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
            #endregion
        }

        public void ResetTime()
        {
            firstDigitSeconds = 0;
            secondDigitSeconds = 0;
            firstDigitMinutes = 0;
            secondDigitMinutes = 0;
            firstDigitHours = 0;
            secondDigitHours = 0;

            digitSeconds = 0;
        }
    }
}
