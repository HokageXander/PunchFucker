﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Johnny_Punch
{
    class EnemyManager
    {

        public List<Enemy> enemyList = new List<Enemy>();


        public EnemyManager(GraphicsDevice graphicsDevice)
        {
            EnemyType();

        }

        public void Update(GameTime gameTime)
        {
            foreach (Enemy enemy in enemyList)
            {
                enemy.Update(gameTime);
            }
            RemoveEnemy();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(spriteBatch);
            }
        }

        public void EnemyType()
        {
            //enemyList.Add(new Little_tim(TextureManager.Tiny_tim, new Vector2(500, 400)));
            enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(500, 400)));
            enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(450, 500)));
            enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(400, 300)));
            enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(1500, 400)));
        }

        public void SpawnEnemy(GameTime gameTime)
        {

        }

        public void RemoveEnemy()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].deathBlinkCount >= 7)
                {
                    enemyList.RemoveAt(i);
                }
            }
        }
        public void AggroPlayer(PlayerManager playerManager, GameTime gameTime)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                for (int j = 0; j < playerManager.playerList.Count; j++)
                {
                    if (!enemyList[i].dead && !enemyList[i].stunned)
                    {
                        enemyList[i].Aggro(playerManager.playerList[j]);
                        enemyList[i].Fight(gameTime, playerManager.playerList[j]);
                    }
                }
            }
        }

        public void FightPlayer(PlayerManager playerManager)
        {
            for (int i = 0; i < playerManager.playerList.Count; i++)
            {
                for (int j = 0; j < enemyList.Count; j++)
                {
                    float Ydistance = (enemyList[j].feetBox.Y - 4) - (playerManager.playerList[i].feetBox.Y - 4);
                    if (Ydistance < 0)
                    {
                        Ydistance *= -1;
                    }

                    if (enemyList[j].punchBox.Intersects(playerManager.playerList[i].boundingBox)
                       && Ydistance < 25 && !enemyList[j].hasHit && enemyList[j].fightFrame >= 2) // om vi intersectar och vi står i samma y-led och vi inte har träffat än och vi är vid slutet av animationen
                    {
                        enemyList[j].hasHit = true;
                        if (enemyList[j].spriteEffect == SpriteEffects.FlipHorizontally)
                        {
                            //playerManager.playerList[i].pos.X -= 2;
                            playerManager.playerList[i].life -= 1;
                            break;
                        }
                        else
                            //playerManager.playerList[i].pos.X += 2;
                        playerManager.playerList[i].life -= 1;
                        break;
                    }
                }
            }
        }
    }
}
