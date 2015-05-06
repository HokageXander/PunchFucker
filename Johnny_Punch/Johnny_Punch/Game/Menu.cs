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
    class Menu
    {
        int menuNumber = 1;
        KeyboardState keyBoardState, oldKeyBoardState;
        public bool play, quit, sound = true;

        public enum MenuState
        {
            MainMenu, NewGame, Options
        }
        MenuState menuState;
        public Menu(ContentManager Content)
        {
            TextureManager.LoadContent(Content);

            menuState = MenuState.MainMenu;
        }

        public void Update(GameTime gameTime)
        {
            oldKeyBoardState = keyBoardState;
            keyBoardState = Keyboard.GetState();

            #region Key Up
            if (keyBoardState.IsKeyDown(Keys.Up) && oldKeyBoardState.IsKeyUp(Keys.Up)) // väljer vilken "knapp" man vill till i menyn
            {
                menuNumber--;
                if (menuNumber == 0 && menuState == MenuState.MainMenu) // om man trycker upp vid toppen går man till botten
                {
                    menuNumber = 3;
                }
                if (menuNumber == 0 && menuState == MenuState.NewGame)
                {
                    menuNumber = 2;
                }
                if (menuNumber == 0 && menuState == MenuState.Options)
                {
                    menuNumber = 1;
                }
            }
            #endregion

            #region Key Down
            if (keyBoardState.IsKeyDown(Keys.Down) && oldKeyBoardState.IsKeyUp(Keys.Down))
            {
                menuNumber++;
                if (menuNumber == 4 && menuState == MenuState.MainMenu)
                {
                    menuNumber = 1;
                }
                if (menuNumber == 3 && menuState == MenuState.NewGame)
                {
                    menuNumber = 1;
                }
                if (menuNumber == 2 && menuState == MenuState.Options)
                {
                    menuNumber = 1;
                }
            }
            #endregion

            #region menuStates
            switch (menuState)
            {
                case MenuState.MainMenu:
                    if (menuNumber == 1 && keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) // om man är på 1 som är markerad röd går man dit
                    {
                        menuState = MenuState.NewGame;
                        menuNumber = 1;
                    }
                    if (menuNumber == 2 && keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter))
                    {
                        menuState = MenuState.Options;
                        menuNumber = 1;
                    }
                    if (menuNumber == 3 && keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter))
                    {
                        quit = true;
                    }
                    break;

                case MenuState.NewGame:
                    if (menuNumber == 1 && keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter))
                    {
                        PlayerManager.players = 1;
                        play = true;
                    }
                    if (menuNumber == 2 && keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter))
                    {
                        PlayerManager.players = 2;
                        play = true;
                    }
                    if (keyBoardState.IsKeyDown(Keys.Back) && oldKeyBoardState.IsKeyUp(Keys.Back))
                    {
                        menuState = MenuState.MainMenu;
                        menuNumber = 1;
                    }
                    break;

                case MenuState.Options:
                    if (menuNumber == 1 && keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter) && sound)
                    {
                        sound = false;
                    }
                    else if (menuNumber == 1 && keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter) && !sound)
                    {
                        sound = true;
                    }
                    if (keyBoardState.IsKeyDown(Keys.Back) && oldKeyBoardState.IsKeyUp(Keys.Back))
                    {
                        menuState = MenuState.MainMenu;
                        menuNumber = 1;
                    }
                    break;
            #endregion

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.menuBackground, Vector2.Zero, Color.White);

            #region menuStates
            switch (menuState)
            {
                case MenuState.MainMenu:
                    if (menuNumber == 1)
                    {
                        spriteBatch.Draw(TextureManager.menuNewGame, new Vector2(300, 200), Color.Red);
                    }
                    else
                        spriteBatch.Draw(TextureManager.menuNewGame, new Vector2(300, 200), Color.Yellow);

                    if (menuNumber == 2)
                    {
                        spriteBatch.Draw(TextureManager.menuOptions, new Vector2(300, 325), Color.Red);
                    }
                    else
                        spriteBatch.Draw(TextureManager.menuOptions, new Vector2(300, 325), Color.Yellow);

                    if (menuNumber == 3)
                    {
                        spriteBatch.Draw(TextureManager.menuQuit, new Vector2(300, 450), Color.Red);
                    }
                    else
                        spriteBatch.Draw(TextureManager.menuQuit, new Vector2(300, 450), Color.Yellow);
                    break;

                case MenuState.NewGame:
                    if (menuNumber == 1)
                    {
                        spriteBatch.Draw(TextureManager.menuOnePlayer, new Vector2(300, 200), Color.Red);
                    }
                    else
                        spriteBatch.Draw(TextureManager.menuOnePlayer, new Vector2(300, 200), Color.Yellow);
                    if (menuNumber == 2)
                    {
                        spriteBatch.Draw(TextureManager.menuTwoPlayer, new Vector2(300, 325), Color.Red);
                    }
                    else
                        spriteBatch.Draw(TextureManager.menuTwoPlayer, new Vector2(300, 325), Color.Yellow);
                    break;

                case MenuState.Options:
                    if (menuNumber == 1)
                    {
                        spriteBatch.Draw(TextureManager.menuSound, new Vector2(0, 200), Color.Red);
                    }
                    else
                        spriteBatch.Draw(TextureManager.menuSound, new Vector2(0, 200), Color.Yellow);

                    if (sound)
                    {
                        spriteBatch.Draw(TextureManager.menuSoundOn, new Vector2(900, 230), Color.Yellow);
                    }
                    else
                        spriteBatch.Draw(TextureManager.menuSoundOff, new Vector2(903, 227), Color.Yellow);
                    break;
            #endregion
            }
        }
    }
}
