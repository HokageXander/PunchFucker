using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Johnny_Punch
{
    class PlayerManager
    {
        
        public List<Player> playerList = new List<Player>();
        public PlayerManager()
        {

        }

        public void Update(GameTime gameTime)
        {
            AddPlayer();
            

            foreach (Player player in playerList)
            {
                player.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Player player in playerList)
            {
                player.Draw(spriteBatch);
            }
        }
        public void AddPlayer()
        {
            if (playerList.Count <= 0)
            {
                playerList.Add(new Player(TextureManager.Player_tex, new Vector2(800, 400))); 
            }
        }
        

    }

}
