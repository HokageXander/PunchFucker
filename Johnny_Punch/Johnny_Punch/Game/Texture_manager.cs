using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Johnny_Punch
{
    public static class TextureManager
    {

        #region Menu
        public static Texture2D loadingScreen { get; private set; }
        public static Texture2D loadingCircle { get; private set; }
        public static Texture2D menuBackground { get; private set; }
        public static Texture2D menuNewGame { get; private set; }
        public static Texture2D menuOptions { get; private set; }
        public static Texture2D menuQuit { get; private set; }
        public static Texture2D menuOnePlayer { get; private set; }
        public static Texture2D menuTwoPlayer { get; private set; }
        public static Texture2D menuSound { get; private set; }
        public static Texture2D menuSoundOn { get; private set; }
        public static Texture2D menuSoundOff { get; private set; }

        #endregion

        #region StatusBar graphics
        public static Texture2D lifeBarTex { get; private set; }
        #endregion

        #region Environment graphics
        public static Texture2D citybackgroundTex { get; private set; }
        public static Texture2D statusBarTex { get; private set; }
        public static Texture2D roadTex { get; private set; }
        public static Texture2D startMenuTex { get; private set; }
        public static Texture2D manHoleCoverTex { get; private set; }
        #endregion

        #region Player graphics
        public static Texture2D Player1tex { get; private set; }
        public static Texture2D Player2tex { get; private set; }

        public static Texture2D playerShadow { get; private set; }

        #endregion

        #region Enemy graphics
        public static Texture2D Tiny_tim { get; private set; }
        public static Texture2D standardEnemyTex { get; private set; }
        #endregion

        #region Fonts
        public static SpriteFont timeFont { get; private set; }
        #endregion


        public static void LoadContent(ContentManager Content)
        {
            #region Menu
            loadingScreen = Content.Load<Texture2D>(@"images\loadingScreen");
            loadingCircle = Content.Load<Texture2D>(@"images\loadingCircle");
            menuBackground = Content.Load<Texture2D>(@"Images\MenuImages\menuBackground");
            menuNewGame = Content.Load<Texture2D>(@"Images\MenuImages\MenuNewGame");
            menuOptions = Content.Load<Texture2D>(@"Images\MenuImages\MenuOptions");
            menuQuit = Content.Load<Texture2D>(@"Images\MenuImages\MenuQuit");
            menuOnePlayer = Content.Load<Texture2D>(@"Images\MenuImages\MenuOnePlayer");
            menuTwoPlayer = Content.Load<Texture2D>(@"Images\MenuImages\MenuTwoPlayer");
            menuSound = Content.Load<Texture2D>(@"Images\MenuImages\MenuSound");
            menuSoundOn = Content.Load<Texture2D>(@"Images\MenuImages\MenuSoundOn");
            menuSoundOff = Content.Load<Texture2D>(@"Images\MenuImages\MenuSoundOff");
            #endregion

            #region StatusBar graphics
            lifeBarTex = Content.Load<Texture2D>(@"Images\Lifebar");
            #endregion

            #region Environment graphics
            citybackgroundTex = Content.Load<Texture2D>(@"Images\citybackground");
            statusBarTex = Content.Load<Texture2D>(@"Images\StatusBar");
            roadTex = Content.Load<Texture2D>(@"Images\road");
            startMenuTex = Content.Load<Texture2D>(@"Images\startmenu");
            manHoleCoverTex = Content.Load<Texture2D>(@"Images\manholecover");
            #endregion

            #region Player graphics
            Player1tex = Content.Load<Texture2D>(@"Images\AlexSheet");
            Player2tex = Content.Load<Texture2D>(@"Images\AlexSheetplayer2");
            playerShadow = Content.Load<Texture2D>(@"Images\shadow");
            #endregion

            #region Enemy graphics
            Tiny_tim = Content.Load<Texture2D>(@"Images\tinytim");
            standardEnemyTex = Content.Load<Texture2D>(@"images\standardenemy");
            #endregion

            #region Fonts
            timeFont = Content.Load<SpriteFont>(@"Fonts\timeFont");
            #endregion
        }
    }
}
