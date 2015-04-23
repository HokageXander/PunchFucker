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

        public void LandingPunches(EnemyManager enemyManager)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                for (int j = 0; j < enemyManager.enemyList.Count; j++)
                {
                    float Ydistance = (playerList[i].feetBox.Y - 4) - (enemyManager.enemyList[j].feetBox.Y - 4);
                    if (Ydistance < 0)
                    {
                        Ydistance *= -1;
                    }
                    Console.WriteLine(Ydistance);

                    if (playerList[i].punchBox.Intersects(enemyManager.enemyList[j].boundingBox) /* && !(enemyManager.enemyList[j].blocking)*/
                       && Ydistance < 25)
                    {
                        enemyManager.enemyList[j].life -= 1;
                        if (playerList[i].spriteEffect == SpriteEffects.FlipHorizontally)
                        {
                            enemyManager.enemyList[j].pos.X -= 2;
                        }
                        else
                        enemyManager.enemyList[j].pos.X += 2;
                    }
                }
            }
        }

    }

}
