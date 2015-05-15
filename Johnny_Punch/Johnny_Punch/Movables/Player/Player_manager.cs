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
        public ParticleExplosion particleExplosion;

        public PlayerManager()
        {

        }

        public void Update(GameTime gameTime)
        {
            AddPlayer();

            foreach (ParticleExplosion e in ParticleExplosion.explosionList.ToList())//updaterar alla partikeleffekter 
            {
                e.Update(gameTime);
            }
            foreach (ParticleExplosion e in ParticleExplosion.explosionList)
            {
                if (e.IsDead)
                    ParticleExplosion.explosionList.Remove(e);
                break;
            }

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
            foreach (ParticleExplosion e in ParticleExplosion.explosionList)
            {
                e.Draw(spriteBatch);
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
                       && Ydistance < 25 && !playerList[i].hasHit && playerList[i].fightFrame >= 1) // om vi intersectar och vi står i samma y-led och vi inte har träffat än
                    {
                        playerList[i].hasHit = true;
                        enemyManager.enemyList[j].stunned = true;

                        if (playerList[i].spriteEffect == SpriteEffects.FlipHorizontally)
                        {
                            //enemyManager.enemyList[j].pos.X -= 2;
                            enemyManager.enemyList[j].life -= 1;
                        particleExplosion = new ParticleExplosion(TextureManager.bloodTex, new Vector2(playerList[0].punchBox.X, playerList[0].punchBox.Y), Color.Red);
                            break;

                        }
                        else
                        {
                            //enemyManager.enemyList[j].pos.X += 2;
                            particleExplosion = new ParticleExplosion(TextureManager.bloodTex, new Vector2(playerList[0].punchBox.X + playerList[0].punchBox.Width, playerList[0].punchBox.Y), Color.Red);

                            enemyManager.enemyList[j].life -= 1;
                        }
                        break;
                    }
                }
            }
        }

        public void CollectItems(LevelManager item)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                for (int j = 0; j < item.itemList.Count; j++)
                {
<<<<<<< HEAD
                    if (playerList[i].feetBox.Intersects(item.itemList[j].boundingBox) /*&& !(item.itemList[j] is Sabre)*/)
=======
                    if (playerList[i].feetBox.Intersects(item.itemList[j].boundingBox))
>>>>>>> origin/master
                    {
                        item.itemList.RemoveAt(j);
                        if (playerList[i].life <= 9)
                            playerList[i].life++;

                        //particleExplosion = new ParticleExplosion(TextureManager.bloodTex, new Vector2(playerList[0].feetBox.X, playerList[0].feetBox.Y), Color.Yellow);
                    }
<<<<<<< HEAD
                    //if (playerList[i].feetBox.Intersects(item.itemList[j].boundingBox) && item.itemList[j] is Sabre)
                    //{
                    //    item.itemList.RemoveAt(j);
                    //    //item.itemList.Add(new Sabre(TextureManager.sabreTex, new Vector2(playerList[i].pos.X + 10, playerList[i].pos.Y)));
                    //}
=======
>>>>>>> origin/master
                }
            }
        }
    }
}
