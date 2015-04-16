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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            gameManager.Update(gameTime);
            
            base.Update(gameTime);
            camera.Update(gameManager.playerManager.playerList[0].GetPos, gameManager.playerManager.playerList[0].GetRec, Window);
            Window.Title = camera.GetCameraPos.ToString() + gameManager.playerManager.playerList[0].pos.X.ToString();
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightPink);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.GetTransform);
            
            gameManager.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            gameManager.DrawStats(spriteBatch);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
