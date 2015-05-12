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


        public enum GameState
        {
            Menu, Play, Pause, End
        }
        public GameState gameState;

        public void LoadContent(ContentManager Content, GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch)
        {
            TextureManager.LoadContent(Content);
            menu = new Menu(Content);
            enemyManager = new EnemyManager(GraphicsDevice);
            playerManager = new PlayerManager();
            levelManager = new LevelManager(Content);
            gameState = GameState.Menu;
        }

        public void Update(GameTime gameTime)
        {
            oldKeyBoardState = keyBoardState;
            keyBoardState = Keyboard.GetState();

            switch (gameState)
            {
                case GameState.Menu:
                    menu.Update(gameTime);
                    if (menu.play == true)
                    {
                        gameState = GameState.Play;
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

                    playerManager.Update(gameTime);
                    playerManager.LandingPunches(enemyManager);
                    playerManager.CollectItems(levelManager);

                    enemyManager.Update(gameTime);
                    enemyManager.AggroPlayer(playerManager, gameTime);
                    enemyManager.FightPlayer(playerManager);
                    enemyManager.IsBlocked(playerManager, gameTime);
                    CheckIsDead();
                    TotalPlayTime(gameTime);
                    

                    time += gameTime.ElapsedGameTime.TotalSeconds;
                    break;

                case GameState.Pause:
                    menu.Update(gameTime);

                    if (menu.play == true)
                    {
                        gameState = GameState.Play;
                    }
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
                    spriteBatch.Draw(TextureManager.lifeBarTex, new Rectangle(120 /*- 50 * (PlayerManager.players - 1)*/, 608 - ((PlayerManager.players - 1)* 42), 155, 35), Color.Red);
                }
                spriteBatch.Draw(TextureManager.lifeBarTex, new Rectangle(120, 608 - ((PlayerManager.players - 1) * 42), (int)(155 * playerManager.playerList[0].percentLife), 35), Color.Green);

                if (PlayerManager.players == 2)
                {
                    if (playerManager.playerList2[0].percentLife < 1.0f)
                    {
                        spriteBatch.Draw(TextureManager.lifeBarTex, new Rectangle(254, 620, 155, 35), Color.Red);
                    }
                    int healthPos = (int)(playerManager.playerList2[0].maxLife - playerManager.playerList2[0].life);
                    spriteBatch.Draw(TextureManager.lifeBarTex, new Rectangle(254 + (int)(15.5f * healthPos), 620, (int)(155 * playerManager.playerList2[0].percentLife), 35), Color.Green);

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

            switch(gameState)
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
                case GameState.Menu:
                    menu.Draw(spriteBatch);
                    break;

                case GameState.Play:

                    levelManager.Draw(spriteBatch);
                    enemyManager.Draw(spriteBatch);
                    playerManager.Draw(spriteBatch);

                    foreach (ParticleExplosion e in ParticleExplosion.explosionList)
                    {
                        e.Draw(spriteBatch);
                    }

                    break;

                case GameState.Pause:
                    levelManager.Draw(spriteBatch);
                    enemyManager.Draw(spriteBatch);
                    playerManager.Draw(spriteBatch);

                    foreach (ParticleExplosion e in ParticleExplosion.explosionList)
                    {
                        e.Draw(spriteBatch);
                    }
                    break;

                case GameState.End:

                    break;
            }
        }


        public static void CheckIsDead( )
        {
            foreach (ParticleExplosion e in ParticleExplosion.explosionList)
            {
                if(e.IsDead )
                ParticleExplosion.explosionList.Remove(e);
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
    }
}
