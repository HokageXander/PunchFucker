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
        bool ready;
        float loadingRotation = 1;
        //suck myu tits

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
            if (!ready)
                loadingRotation *= 1.008f; //gör att cirkeln roterar vid loadingScreen

            if (gameManager.firstDigitSeconds >= 2f && !ready)
            {
                ready = true; // för att spelet ska hinna ladda in före kameran går igång. Kameran annars ledsen :(
                gameManager.digitSeconds = 0;
            }
            gameManager.Update(gameTime);

            base.Update(gameTime);
            if (gameManager.gameState == GameManager.GameState.Play && ready)
            {
                camera.Update(gameManager.playerManager.playerList[0].GetPos, gameManager.playerManager.playerList[0].GetRec, Window);
                Window.Title = camera.GetCameraPos.ToString() + gameManager.playerManager.playerList[0].pos.X.ToString();
            }

        }


        protected override void Draw(GameTime gameTime)
        {
            if (gameManager.gameState == GameManager.GameState.Play)
            {
                GraphicsDevice.Clear(Color.LightPink);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.GetTransform);
                gameManager.Draw(spriteBatch);
                spriteBatch.End();

                spriteBatch.Begin();
                gameManager.DrawStats(spriteBatch);
                if (!ready)
                {
                    spriteBatch.Draw(TextureManager.loadingScreen, Vector2.Zero, Color.White);
                    spriteBatch.Draw(TextureManager.loadingCircle, new Vector2(640, 450), null, Color.White, loadingRotation, new Vector2(36, 38), 1,SpriteEffects.None, 1);
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
