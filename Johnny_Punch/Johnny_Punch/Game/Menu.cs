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
        GamePadState gamePadState, oldGamePadState;
        public bool play, quit, howTo, sound = true;



        public enum MenuState
        {
            MainMenu, NewGame, Options, HowToPlay, Pause, PauseOptions, PauseQuit
        }
        public MenuState menuState;
        public Menu(ContentManager Content)
        {
            TextureManager.LoadContent(Content);

            menuState = MenuState.MainMenu;
        }

        public void Update(GameTime gameTime)
        {
            oldKeyBoardState = keyBoardState;
            keyBoardState = Keyboard.GetState();

            oldGamePadState = gamePadState;
            gamePadState = GamePad.GetState(PlayerIndex.One);
            #region Key Up
            if ((keyBoardState.IsKeyDown(Keys.Up) && oldKeyBoardState.IsKeyUp(Keys.Up)) ||
                (gamePadState.DPad.Up == ButtonState.Pressed && oldGamePadState.DPad.Up == ButtonState.Released)) // väljer vilken "knapp" man vill till i menyn
            {
                AudioManager.MenuMove.Play();
                menuNumber--;
                if (menuNumber == 0 && menuState == MenuState.MainMenu || menuNumber == 0 && menuState == MenuState.Pause) // om man trycker upp vid toppen går man till botten
                {
                    menuNumber = 3;
                }
                if (menuNumber == 0 && (menuState == MenuState.NewGame || menuState == MenuState.PauseQuit))
                {
                    menuNumber = 2;
                }
                if (menuNumber == 0 && menuState == MenuState.Options || menuNumber == 0 && menuState == MenuState.PauseOptions)
                {
                    menuNumber = 2;
                }
            }
            #endregion
            #region Key Down
            if ((keyBoardState.IsKeyDown(Keys.Down) && oldKeyBoardState.IsKeyUp(Keys.Down)) ||
                (gamePadState.DPad.Down == ButtonState.Pressed && oldGamePadState.DPad.Down == ButtonState.Released))
            {
                AudioManager.MenuMove.Play();

                menuNumber++;
                if (menuNumber == 4 && menuState == MenuState.MainMenu || menuNumber == 4 && menuState == MenuState.Pause)
                {
                    menuNumber = 1;
                }
                if (menuNumber == 3 && (menuState == MenuState.NewGame || menuState == MenuState.PauseQuit))
                {
                    menuNumber = 1;
                }
                if (menuNumber == 3 && menuState == MenuState.Options || menuNumber == 3 && menuState == MenuState.PauseOptions)
                {
                    menuNumber = 1;
                }
           
            }
            #endregion

            #region menuStates
            switch (menuState)
            {
                #region Main Menu
                case MenuState.MainMenu:
                    if (menuNumber == 1 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released))) // om man är på 1 som är markerad röd går man dit
                    {
                        menuState = MenuState.NewGame;
                        menuNumber = 1;
                    }
                    if (menuNumber == 2 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)))
                    {
                        menuState = MenuState.Options;
                        menuNumber = 1;
                    }
                    if (menuNumber == 3 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)))
                    {
                        quit = true;
                    }
                    break;
                #endregion
                #region NewGame
                case MenuState.NewGame:
                    if (menuNumber == 1 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)))
                    {
                        PlayerManager.players = 1;
                        play = true;
                        menuNumber = 1;
                    }
                    if (menuNumber == 2 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)))
                    {
                        PlayerManager.players = 2;
                        play = true;
                        menuNumber = 1;

                    }
                    if ((keyBoardState.IsKeyDown(Keys.Back) && oldKeyBoardState.IsKeyUp(Keys.Back)) ||
                        (gamePadState.Buttons.B == ButtonState.Pressed && oldGamePadState.Buttons.B == ButtonState.Released))
                    {
                        AudioManager.MenuBack.Play();

                        menuState = MenuState.MainMenu;
                        menuNumber = 1;
                    }
                    break;
                #endregion
                #region Options
                case MenuState.Options:
                    if (menuNumber == 1 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)) && sound)
                    {
                        sound = false;
                    }
                    else if (menuNumber == 1 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter))
                        || (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)) && !sound)
                    {
                        sound = true;
                    }
                    if (menuNumber == 2 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter))
                        || (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)))
                    {
                        menuState = MenuState.HowToPlay;
                    }
                    if ((keyBoardState.IsKeyDown(Keys.Back) && oldKeyBoardState.IsKeyUp(Keys.Back)) ||
                        (gamePadState.Buttons.B == ButtonState.Pressed && oldGamePadState.Buttons.B == ButtonState.Released))
                    {
                        AudioManager.MenuBack.Play();

                        menuState = MenuState.MainMenu;
                        menuNumber = 1;
                    }
                    
                    break;
                    #endregion
                #region How To Play
                case MenuState.HowToPlay:
                    if ((keyBoardState.IsKeyDown(Keys.Back) && oldKeyBoardState.IsKeyUp(Keys.Back)) ||
                       (gamePadState.Buttons.B == ButtonState.Pressed && oldGamePadState.Buttons.B == ButtonState.Released))
                    {
                        AudioManager.MenuBack.Play();

                        menuState = MenuState.Options;
                        menuNumber = 2;
                    }

                    break;
                #endregion
                #region Pause
                case MenuState.Pause:
                    if (menuNumber == 1 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)))
                    {
                        play = true;
                    }
                    if (menuNumber == 2 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)))
                    {
                        menuState = MenuState.PauseOptions;
                        menuNumber = 1;
                    }
                    if (menuNumber == 3 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)))
                    {
                        menuNumber = 1;
                        menuState = MenuState.PauseQuit;
                    }                    
                    break;
                #endregion
                #region Pause Options
                case MenuState.PauseOptions:
                    if (menuNumber == 1 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)) && sound)
                    {
                        sound = false;
                    }
                       else if (menuNumber == 1 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)) && !sound)
                    {
                        sound = true;
                        menuNumber = 1;
                    }
                    if (((keyBoardState.IsKeyDown(Keys.Back) && oldKeyBoardState.IsKeyUp(Keys.Back)) ||
                        (gamePadState.Buttons.B == ButtonState.Pressed && oldGamePadState.Buttons.B == ButtonState.Released)) && !howTo)
                    {
                        AudioManager.MenuBack.Play();
                        menuState = MenuState.Pause;
                        menuNumber = 2;
                    }

                    if (menuNumber == 2 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                       (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)) && !howTo)
                    {
                        howTo = true;
                    }
                    if (((keyBoardState.IsKeyDown(Keys.Back) && oldKeyBoardState.IsKeyUp(Keys.Back)) ||
                        (gamePadState.Buttons.B == ButtonState.Pressed && oldGamePadState.Buttons.B == ButtonState.Released)) && howTo)
                    {
                        AudioManager.MenuBack.Play();

                        howTo = false;
                        menuState = MenuState.PauseOptions;
                        menuNumber = 2;
                    }

                    break;
                #endregion
                #region Pause Quit
                case MenuState.PauseQuit:
                    if (menuNumber == 2 && (keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)))
                    {
                        menuState = MenuState.MainMenu;
                        
                    }
                    else if (menuNumber == 1 && ((keyBoardState.IsKeyDown(Keys.Enter) && oldKeyBoardState.IsKeyUp(Keys.Enter)) ||
                        (gamePadState.Buttons.A == ButtonState.Pressed && oldGamePadState.Buttons.A == ButtonState.Released)))
                    {
                        menuState = MenuState.Pause;
                        menuNumber = 3;
                    }
                    if ((keyBoardState.IsKeyDown(Keys.Back) && oldKeyBoardState.IsKeyUp(Keys.Back)) ||
                        (gamePadState.Buttons.B == ButtonState.Pressed && oldGamePadState.Buttons.B == ButtonState.Released))
                    {
                        menuState = MenuState.Pause;
                        menuNumber = 3;
                    }
                    break;
                #endregion
            #endregion

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!(menuState == MenuState.Pause || menuState == MenuState.PauseOptions || menuState == MenuState.PauseQuit ))
                spriteBatch.Draw(TextureManager.menuBackground, Vector2.Zero, Color.White);
            else
            spriteBatch.Draw(TextureManager.pauseMenu, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);


            #region menuStates
            switch (menuState)
            {
                #region Main Menu
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
                #endregion
                #region New Game
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
                #endregion
                #region Options
                case MenuState.Options:
                    if (menuNumber == 1)
                    {
                        spriteBatch.Draw(TextureManager.menuSound, new Vector2(252, 200), Color.Red);
                    }
                    else
                        spriteBatch.Draw(TextureManager.menuSound, new Vector2(252, 200), Color.Yellow);

                    if (sound)
                    {
                        spriteBatch.Draw(TextureManager.menuSoundOn, new Vector2(1020, 230), Color.Yellow);
                    }
                    else
                        spriteBatch.Draw(TextureManager.menuSoundOff, new Vector2(1023, 227), Color.Yellow);

                    if (menuNumber == 2)
                    {
                        spriteBatch.Draw(TextureManager.menuHow, new Vector2(310, 350), Color.Red);
                    }
                    else
                        spriteBatch.Draw(TextureManager.menuHow, new Vector2(310, 350), Color.Yellow);
                    break;
                #endregion
                #region How To Play
                case MenuState.HowToPlay:
                    spriteBatch.Draw(TextureManager.menuMoves, new Vector2(290, 260), Color.White);
                    break;
                #endregion
                #region Pause
                case MenuState.Pause:
               
                        if (menuNumber == 1)
                        {

                            spriteBatch.Draw(TextureManager.pauseResume, new Vector2(300, 175), Color.Red);
                        }
                        else
                            spriteBatch.Draw(TextureManager.pauseResume, new Vector2(300, 175), Color.Yellow);

                        if (menuNumber == 2)
                        {
                            spriteBatch.Draw(TextureManager.menuOptions, new Vector2(300, 300), Color.Red);
                        }
                        else
                            spriteBatch.Draw(TextureManager.menuOptions, new Vector2(300, 300), Color.Yellow);

                        if (menuNumber == 3)
                        {
                            spriteBatch.Draw(TextureManager.menuQuit, new Vector2(300, 425), Color.Red);
                        }
                        else
                            spriteBatch.Draw(TextureManager.menuQuit, new Vector2(300, 425), Color.Yellow);                                            
                    break;
                #endregion
                #region Pause Options
                case MenuState.PauseOptions:

                     if (menuNumber == 1)
                    {
                        spriteBatch.Draw(TextureManager.menuSound, new Vector2(290, 200), Color.Red);
                    }
                    else
                        spriteBatch.Draw(TextureManager.menuSound, new Vector2(290, 200), Color.Yellow);
                    
                    if (sound)
                    {
                        spriteBatch.Draw(TextureManager.menuSoundOn, new Vector2(800, 230), Color.Yellow);
                    }
                    else
                        spriteBatch.Draw(TextureManager.menuSoundOff, new Vector2(800, 227), Color.Yellow);                        
                    
                     if (menuNumber == 2)
                     {
                         spriteBatch.Draw(TextureManager.menuHow, new Vector2(335, 350), Color.Red);
                     }
                     else
                         spriteBatch.Draw(TextureManager.menuHow, new Vector2(335, 350), Color.Yellow);
                     if (howTo)
                     {
                         spriteBatch.Draw(TextureManager.pauseMenuHow, new Vector2(20, 25), Color.White);
                     }
                     
                         break;
                #endregion
                #region Pause Quit
                case MenuState.PauseQuit:
                    spriteBatch.Draw(TextureManager.pauseQuest, new Vector2(345, 250), Color.Yellow);
                        if (menuNumber == 2)
                        {
                            spriteBatch.Draw(TextureManager.pauseYes, new Vector2(425, 405), Color.Red);
                        }
                        else
                            spriteBatch.Draw(TextureManager.pauseYes, new Vector2(425, 405), Color.Yellow);
                        if (menuNumber == 1)
                        {
                            spriteBatch.Draw(TextureManager.pauseNo, new Vector2(700, 403), Color.Red);
                        }
                        else
                            spriteBatch.Draw(TextureManager.pauseNo, new Vector2(700, 403), Color.Yellow);                    
                    break;
                #endregion
            #endregion
            }
        }
    }
}
