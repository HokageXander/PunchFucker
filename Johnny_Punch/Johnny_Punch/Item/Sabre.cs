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
    class Sabre : Item
    {
        public Sabre(Texture2D tex, Vector2 pos)
            : base(tex, pos)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        //public void SabreEquipped(PlayerManager playerManager)
        //{
        //    for (int i = 0; i < playerManager.playerList.Count; i++)
        //    {
        //        pos = new Vector2(playerManager.playerList[i].pos.X + 10, playerManager.playerList[i].pos.Y);
        //    }
        //}
    }
}
