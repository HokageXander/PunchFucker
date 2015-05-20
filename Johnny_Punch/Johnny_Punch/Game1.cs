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

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameManager gameManager;
        Camera camera;
        public static bool ready;
        float loadingRotation = 1;
        double loadingTime;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameManager = new GameManager();

            gameManager.LoadContent(Content, GraphicsDevice, spriteBatch);
            camera = new Camera(GraphicsDevice.Viewport);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || gameManager.menu.quit == true)
                this.Exit();
            Window.Title = "Johnny Punch-Fucker";
            if (!ready && gameManager.gameState == GameManager.GameState.Play)
            {
                loadingRotation *= 1.008f; //gör att cirkeln roterar vid loadingScreen
                loadingTime += gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (loadingTime >= 2f && !ready)
            {
                ready = true; // för att spelet ska hinna ladda in före kameran går igång. Kameran annars ledsen :(
                loadingTime = 0;
                gameManager.digitSeconds = 0;
            }

            //if (gameManager.gameState == GameManager.GameState.Play && ready == false && loadingTime >= 0.8f)
            //{
            //    Camera.prevCentre = Camera.centre;
            //    //gameManager.menu.play = false;
            //}
            gameManager.Update(gameTime, GraphicsDevice, Content);

            base.Update(gameTime);
            #region Camera Update
            if (gameManager.gameState == GameManager.GameState.Play && ready || gameManager.gameState == GameManager.GameState.Pause)
            {
                if (PlayerManager.players == 1)
                {
                    camera.Update(gameManager.playerManager.playerList[0].GetPos, gameManager.playerManager.playerList[0].GetRec, Window);
                }
                else if (PlayerManager.players == 2)
                {
                    if (!gameManager.playerManager.playerList[0].dead)
                    {
                        camera.Update(gameManager.playerManager.playerList[0].GetPos, gameManager.playerManager.playerList[0].GetRec, Window);
                    }
                    else if(gameManager.playerManager.playerList[0].dead)
                        camera.Update(gameManager.playerManager.playerList[1].GetPos, gameManager.playerManager.playerList[1].GetRec, Window);
                }
            }
            #endregion
        }

        protected override void Draw(GameTime gameTime)
        {
            if (gameManager.gameState == GameManager.GameState.Play || gameManager.gameState == GameManager.GameState.Pause)
            {
                GraphicsDevice.Clear(Color.LightPink);
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, camera.GetTransform);
                gameManager.Draw(spriteBatch);
                spriteBatch.End();

                spriteBatch.Begin();
                gameManager.DrawStats(spriteBatch);
                if (!ready)
                {
                    spriteBatch.Draw(TextureManager.loadingScreen, Vector2.Zero, Color.White);
                    spriteBatch.Draw(TextureManager.loadingCircle, new Vector2(640, 450), null, Color.White, loadingRotation, new Vector2(50, 50), 1, SpriteEffects.None, 1);
                }
                spriteBatch.End();
            }
            else // för att få kameran ur funktion när man är i menyn
            {
                GraphicsDevice.Clear(Color.LightPink);
                spriteBatch.Begin();
                gameManager.Draw(spriteBatch);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
