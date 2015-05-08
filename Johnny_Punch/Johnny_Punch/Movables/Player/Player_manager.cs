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
        public List<Player2> playerList2 = new List<Player2>();
        public static int players = 1;
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
            foreach (Player2 player in playerList2)
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
            foreach (Player2 player in playerList2)
            {
                player.Draw(spriteBatch);
            }
        }

        public void AddPlayer()
        {
            if (playerList.Count <= 0 && players == 1)
            {
                playerList.Add(new Player(TextureManager.Player1tex, new Vector2(800, 400)));
            }
            if (playerList.Count <= 0 && players == 2)
            {
                playerList.Add(new Player(TextureManager.Player1tex, new Vector2(800, 400)));
                playerList2.Add(new Player2(TextureManager.Player2tex, new Vector2(900, 400)));
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

                    if (playerList[i].punchBox.Intersects(enemyManager.enemyList[j].boundingBox)
                       && Ydistance < 25 && !playerList[i].hasHit) // om vi intersectar och vi står i samma y-led och vi inte har träffat än
                    {
                        playerList[i].hasHit = true;
                        enemyManager.enemyList[j].stunned = true;
                        if (playerList[i].spriteEffect == SpriteEffects.FlipHorizontally)
                        {
                            enemyManager.enemyList[j].pos.X -= 2;
                            enemyManager.enemyList[j].life -= 1;
                            break;
                        }
                        else
                        {
                            enemyManager.enemyList[j].pos.X += 2;
                            enemyManager.enemyList[j].life -= 1;
                        }
                        break;
                    }
                }
            }
        }
    }
}
