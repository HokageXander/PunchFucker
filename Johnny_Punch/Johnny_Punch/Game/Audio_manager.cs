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
    public static class AudioManager
    {
        public static Song Bla { get; private set; }
        public static SoundEffect Ble { get; private set; }

        public static void LoadContent(ContentManager Content)
        {
            Bla = Content.Load<Song>(@"Audio\Bla");
            Ble = Content.Load<SoundEffect>(@"Audio\Ble");    
        }
    }
}
