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
    class Bomb : BossAttacks
    {
        public double bombTimer;
        public Bomb(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            bombTimer = 1;
        }
    }
}
