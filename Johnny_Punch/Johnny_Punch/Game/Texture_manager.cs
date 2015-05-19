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
        #region Intro
        public static Texture2D introScreen1 { get; private set; }
        public static Texture2D introScreen2 { get; private set; }
        public static Texture2D introScreen3 { get; private set; }
        public static Texture2D introScreen4 { get; private set; }
        #endregion

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

        public static Texture2D pauseNo { get; private set; }
        public static Texture2D pauseYes { get; private set; }
        public static Texture2D pauseResume { get; private set; }
        public static Texture2D pauseMenu { get; private set; }
        public static Texture2D pauseQuest { get; private set; }


        public static Texture2D endScreenTex { get; private set; }
        public static Texture2D gameOverScreenTex { get; private set; }

        #endregion

        #region StatusBar graphics
        public static Texture2D lifeBarPlayerOneTex { get; private set; }
        public static Texture2D lifeBarTex { get; private set; }

        #endregion

        #region Environment graphics
        public static Texture2D beachbackgroundTex { get; private set; }
        public static Texture2D statusBarPlayerOneTex { get; private set; }
        public static Texture2D statusBarPlayerTwoTex { get; private set; }
        public static Texture2D beachback2groundTex { get; private set; }        
        public static Texture2D beachback3groundTex { get; private set; }
        public static Texture2D roadTex { get; private set; }
        public static Texture2D startMenuTex { get; private set; }
        public static Texture2D smallPlantTex { get; private set; }
        public static Texture2D jungleEntranceTex { get; private set; }
        public static Texture2D jungleBackgroundTex { get; private set; }
        public static Texture2D jungleRoadTex { get; private set; }
        public static Texture2D jungleRoadThornTex { get; private set; }
        public static Texture2D templeRoadTex { get; private set; }

        #endregion

        #region Player graphics
        public static Texture2D Player1tex { get; private set; }
        public static Texture2D Player2tex { get; private set; }

        public static Texture2D playerShadow { get; private set; }

        #endregion

        #region Enemy graphics
        public static Texture2D standardEnemyTex { get; private set; }
        public static Texture2D bulletTex { get; private set; }
        public static Texture2D bombTex { get; private set; }
        public static Texture2D explosionTex { get; private set; }
        #endregion

        #region Items
        public static Texture2D watermelon { get; private set; }
        public static Texture2D pinacolada { get; private set; }
        public static Texture2D sabreTex { get; private set; }
        #endregion

        #region Fonts
        public static SpriteFont timeFont { get; private set; }
        #endregion

        #region ParticleEffects
        public static Texture2D bloodTex { get; private set; }
        #endregion

        public static void LoadContent(ContentManager Content)
        {
            #region Intro
            introScreen1 = Content.Load<Texture2D>(@"images\introScreen1");
            introScreen2 = Content.Load<Texture2D>(@"images\introScreen2");
            introScreen3 = Content.Load<Texture2D>(@"images\introScreen3");
            introScreen4 = Content.Load<Texture2D>(@"images\introScreen4");
            #endregion

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
            pauseMenu = Content.Load<Texture2D>(@"Images\MenuImages\pauseMenu");
            pauseNo = Content.Load<Texture2D>(@"Images\MenuImages\MenuNo");
            pauseYes = Content.Load<Texture2D>(@"Images\MenuImages\MenuYes");
            pauseResume = Content.Load<Texture2D>(@"Images\MenuImages\MenuResume");
            pauseQuest = Content.Load<Texture2D>(@"Images\MenuImages\PauseQuest");
            endScreenTex = Content.Load<Texture2D>(@"Images\endScreen");
            gameOverScreenTex = Content.Load<Texture2D>(@"Images\gameover");
            #endregion
             
            #region StatusBar graphics
            lifeBarTex = Content.Load<Texture2D>(@"Images\Lifebar");

            #endregion

            #region Environment graphics
            beachbackgroundTex = Content.Load<Texture2D>(@"Images\citybackground");
            beachback2groundTex = Content.Load<Texture2D>(@"Images\beachbackground2");
            beachback3groundTex = Content.Load<Texture2D>(@"Images\beachbackground3");
            statusBarPlayerOneTex = Content.Load<Texture2D>(@"Images\StatusBar1player");
            statusBarPlayerTwoTex = Content.Load<Texture2D>(@"Images\StatusBar");
            roadTex = Content.Load<Texture2D>(@"Images\road");
            startMenuTex = Content.Load<Texture2D>(@"Images\startmenu");
            smallPlantTex = Content.Load<Texture2D>(@"Images\plant");
            jungleEntranceTex = Content.Load<Texture2D>(@"Images\jungleEntrance");
            jungleBackgroundTex = Content.Load<Texture2D>(@"Images\junglebackground1");
            jungleRoadTex = Content.Load<Texture2D>(@"Images\jungleroad");
            jungleRoadThornTex = Content.Load<Texture2D>(@"Images\jungleroadThorn");
            templeRoadTex = Content.Load<Texture2D>(@"Images\templeroad");

            #endregion

            #region Player graphics
            Player1tex = Content.Load<Texture2D>(@"Images\AlexSheet");
            Player2tex = Content.Load<Texture2D>(@"Images\AlexSheetplayer2");
            playerShadow = Content.Load<Texture2D>(@"Images\shadow");
            #endregion

            #region Enemy graphics
            standardEnemyTex = Content.Load<Texture2D>(@"images\standardenemy");
            bulletTex = Content.Load<Texture2D>(@"images\bulletTex");
            bombTex = Content.Load<Texture2D>(@"images\bombTex");
            explosionTex = Content.Load<Texture2D>(@"images\explosion");
            #endregion

            #region Items
            watermelon = Content.Load<Texture2D>(@"images\melon");
            pinacolada = Content.Load<Texture2D>(@"images\drink");
            sabreTex = Content.Load<Texture2D>(@"images\sabre");
            #endregion

            #region Fonts
            timeFont = Content.Load<SpriteFont>(@"Fonts\timeFont");
            #endregion

            #region ParticleEffects
            bloodTex = Content.Load<Texture2D>(@"images\Blood");
            #endregion
        }
    }
}
