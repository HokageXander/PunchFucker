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
        SoundEffectInstance LevelMusic, MenuMusic;
        public Menu menu;
        public PlayerManager playerManager;
        EnemyManager enemyManager;
        float musicVolume = 1f;
        LevelManager levelManager;
        KeyboardState keyBoardState, oldKeyBoardState;
        GamePadState gamePadState, oldGamePadState;
        public int firstDigitSeconds, secondDigitSeconds, firstDigitMinutes, secondDigitMinutes, firstDigitHours, secondDigitHours;
        public double time, digitSeconds;
        public int intro = 0; // vilket intro det är

        public TimeSpan introSwitch;

        public enum GameState
        {
            Intro, Menu, Play, Pause, End
        }
        public GameState gameState;

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch)
        {
            AudioManager.LoadContent(Content);

            LevelMusic = AudioManager.Level.CreateInstance();
            MenuMusic = AudioManager.MenuMusic.CreateInstance();

            TextureManager.LoadContent(Content);
            menu = new Menu(Content);
            enemyManager = new EnemyManager(GraphicsDevice);
            playerManager = new PlayerManager();
            levelManager = new LevelManager(Content);
            gameState = GameState.Intro;


        }

        public void Update(GameTime gameTime, GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            oldKeyBoardState = keyBoardState;
            keyBoardState = Keyboard.GetState();
            oldGamePadState = gamePadState;
            gamePadState = GamePad.GetState(PlayerIndex.One);
            Console.WriteLine(introSwitch);

            switch (gameState)
            {
                case GameState.Intro:
                    LevelMusic.IsLooped = true;

                    MenuMusic.IsLooped = true;
                    if (intro == 0)
                        MediaPlayer.Play(AudioManager.intro);

                    if (introSwitch.TotalSeconds > 0)
                        introSwitch = introSwitch.Subtract(gameTime.ElapsedGameTime);
                    else
                    {                                   // sköter om hur många sekunder varje intro ska vara
                        if (intro == 0)
                            introSwitch = TimeSpan.FromSeconds(20);
                        if (intro == 1)
                            introSwitch = TimeSpan.FromSeconds(13);
                        if (intro == 2)
                            introSwitch = TimeSpan.FromSeconds(5);
                        if (intro == 3)
                            introSwitch = TimeSpan.FromSeconds(14);
                        intro++;
                    }

                    if (intro == 5)
                    {
                        gameState = GameState.Menu;
                    }

                    if ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter) ||
                        gamePadState.Buttons.Start == ButtonState.Pressed && oldGamePadState.Buttons.Start == ButtonState.Released))
                    {
                        gameState = GameState.Menu;
                    }
                    break;

                case GameState.Menu:

                    MediaPlayer.Stop();

                    if (!menu.sound)
                    {
                        MenuMusic.Volume = musicVolume - 1;
                        LevelMusic.Volume = musicVolume - 1;
                    }
                    else
                        MenuMusic.Volume = musicVolume;
                    MenuMusic.Play();
                    LevelMusic.Stop();

                    menu.Update(gameTime);

                    if (menu.play == true)
                    {

                        LevelMusic.Play();

                        gameState = GameState.Play;
                        LevelManager.levelNr = 1;
                        enemyManager = new EnemyManager(GraphicsDevice); //allt under resettar och laddar in allt på nytt ifall man valt quit i pausmenyn
                        playerManager = new PlayerManager();
                        levelManager = new LevelManager(Content);
                        ResetTime();
                        Game1.ready = false;
                    }
                    break;

                case GameState.Play:
                    MenuMusic.Stop();
                    if (Game1.ready && (keyBoardState.IsKeyDown(Keys.P) && oldKeyBoardState.IsKeyUp(Keys.P) ||
                        gamePadState.Buttons.Start == ButtonState.Pressed && oldGamePadState.Buttons.Start == ButtonState.Released))
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

                    if (keyBoardState.IsKeyDown(Keys.NumPad8))
                    {
                        musicVolume += 0.02f;
                        if (musicVolume > 1)
                            musicVolume = 1;
                    }
                    else if (keyBoardState.IsKeyDown(Keys.NumPad2))
                    {
                        musicVolume -= 0.02f;
                        if (musicVolume < 0)
                            musicVolume = 0;
                    }

                    if (!menu.sound)
                    {
                        LevelMusic.Volume = musicVolume - 1;
                    }
                    else
                        LevelMusic.Volume = musicVolume;


                    time += gameTime.ElapsedGameTime.TotalSeconds;

                    if (LevelManager.end)
                        gameState = GameState.End;

                    break;

                case GameState.Pause:
                    menu.Update(gameTime);

                    if (!menu.sound)
                    {
                        LevelMusic.Volume = musicVolume - 1;
                    }
                    else
                        LevelMusic.Volume = musicVolume;

                    if (menu.play == true)
                        gameState = GameState.Play;

                    if (menu.menuState == Menu.MenuState.MainMenu)
                    {
                        gameState = GameState.Menu;
                        Camera.centre = new Vector2(34, 0);
                        Camera.prevCentre = Camera.centre;
                        Camera.transform = Matrix.CreateScale(new Vector3(1, 1, 0))
                * Matrix.CreateTranslation(new Vector3(-Camera.centre.X, -Camera.centre.Y, 0));
                    }
                    break;

                case GameState.End:

                    menu.play = false;
                    LevelMusic.Stop();
                    if ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter) ||
                        gamePadState.Buttons.Start == ButtonState.Pressed && oldGamePadState.Buttons.Start == ButtonState.Released))
                    {
                        LevelManager.end = false;
                        gameState = GameState.Menu;
                        menu.menuState = Menu.MenuState.MainMenu;
                        Camera.centre = new Vector2(34, 0);
                        Camera.prevCentre = Camera.centre;
                        Camera.transform = Matrix.CreateScale(new Vector3(1, 1, 0))
                * Matrix.CreateTranslation(new Vector3(-Camera.centre.X, -Camera.centre.Y, 0));
                    }
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
                    float healthPos = (float)(playerManager.playerList[1].maxLife - playerManager.playerList[1].life);
                    spriteBatch.Draw(TextureManager.lifeBarTex, new Rectangle((int)(254 + (15.5f * healthPos)), 620, (int)(155 * playerManager.playerList[1].percentLife), 35), Color.Green);
                    Console.WriteLine(healthPos);
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

                    if (intro == 1)                                                           // vilket intro som ska visas
                        spriteBatch.Draw(TextureManager.introScreen1, Vector2.Zero, Color.Red);
                    if (intro == 2)
                        spriteBatch.Draw(TextureManager.introScreen2, Vector2.Zero, Color.Blue);
                    if (intro == 3)
                        spriteBatch.Draw(TextureManager.introScreen3, Vector2.Zero, Color.Green);
                    if (intro == 4)
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
                    {

                        spriteBatch.Draw(TextureManager.endScreenTex, Vector2.Zero, Color.White);
                    }
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
