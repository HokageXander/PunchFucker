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
        #region Environment graphics
        public static Texture2D backgroundTex { get; private set; }
        public static Texture2D statusBarTex { get; private set; }
        public static Texture2D statusBarAngryTex { get; private set; }
        public static Texture2D roadTex { get; private set; }
        public static Texture2D manHoleCoverTex { get; private set; }
        #endregion

        #region Player graphics
        public static Texture2D Player_tex { get; private set; }
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
            #region Environment graphics
            backgroundTex = Content.Load<Texture2D>(@"Images\background");
            statusBarTex = Content.Load<Texture2D>(@"Images\StatusBar");
            statusBarAngryTex = Content.Load<Texture2D>(@"Images\StatusBarAngry");
            roadTex = Content.Load<Texture2D>(@"Images\road");
            manHoleCoverTex = Content.Load<Texture2D>(@"Images\manholecover");
            #endregion

 
            #region Player graphics
            Player_tex = Content.Load<Texture2D>(@"Images\AlexSheet");
            playerShadow = Content.Load<Texture2D>(@"Images\shadow");
            #endregion

            #region Enemy graphics
            Tiny_tim = Content.Load<Texture2D>(@"Images\tinytim");
            standardEnemyTex = Content.Load<Texture2D>(@"images\Testenemy");
            #endregion

            #region Fonts
            timeFont = Content.Load<SpriteFont>(@"Fonts\timeFont");
            #endregion
        }
    }
}
