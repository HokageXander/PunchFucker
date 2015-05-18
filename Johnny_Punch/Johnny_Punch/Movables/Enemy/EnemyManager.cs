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
        public ParticleExplosion particleExplosion;
        bool spawn1, spawn2;

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
            if (LevelManager.levelNr == 1)
            {
                enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(450, 500)));
                enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(400, 300)));
                enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(500, 400)));
                enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(2000, 450)));
            }
        }

        public void SpawnEnemy(PlayerManager playerManager)
        {
            for (int j = 0; j < playerManager.playerList.Count; j++)
            {
                if (playerManager.playerList[j].pos.X > 2000 && !spawn1 && LevelManager.levelNr == 1) // när spelaren har nått en punkt så spawnas det fiender, så många som man lägger i if-satsen
                {
                    enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(2000, 800)));
                    enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(2650, 450)));
                    spawn1 = true; // måste göra boolen true, så går fienderna till en, och så spawnas det inte mer än vad som står åvan
                }
                if (playerManager.playerList[j].pos.X > 3000 && !spawn2 && LevelManager.levelNr == 1)
                {
                    enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(3300, 800)));
                    enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(2400, 450)));
                    enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(3550, 300)));
                    enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(3550, 550)));
                    enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(2800, 800)));
                    enemyList.Add(new StandardEnemy(TextureManager.standardEnemyTex, new Vector2(3000, 800)));
                    spawn2 = true;
                }
                if (LevelManager.levelNr == 2 && spawn1 && spawn2)
                {
                    spawn1 = false;
                    spawn2 = false;
                    //enemyList.Add(new Octopimp(TextureManager.OctopimpTex, new Vector2(2000, 340)));
                    enemyList.Add(new Boss(TextureManager.standardEnemyTex, new Vector2(2000, 450)));

                }
            }
        }

        public void RemoveEnemy()
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].deathBlinkCount >= 7) // när han blinkat vitt ett par gånger så dör han
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
                    if (!enemyList[i].dead && !enemyList[i].stunned && !(enemyList[i] is Boss))
                    {
                        enemyList[i].Aggro(playerManager.playerList[j]);
                        enemyList[i].Fight(gameTime, playerManager.playerList[j]);
                    }
                    if (spawn1 || spawn2&& !(enemyList[i] is Boss)) // när fiender spawnas så aggrar dom på spelaren direkt
                        enemyList[i].SpawnAggro(playerManager.playerList[j]);
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
                       && Ydistance < 25 && !enemyList[j].hasHit && enemyList[j].fightFrame >= 2
                        && !(enemyList[j].punchBox.Intersects(playerManager.playerList[i].blockBox))) // om vi intersectar och vi står i samma y-led och vi inte har träffat än och vi är vid slutet av animationen
                    {
                        enemyList[j].hasHit = true;
                        if (enemyList[j].spriteEffect == SpriteEffects.FlipHorizontally)
                        {
                            //playerManager.playerList[i].pos.X -= 2;
                            playerManager.playerList[i].life -= 1;
                            particleExplosion = new ParticleExplosion(TextureManager.bloodTex, new Vector2(enemyList[j].punchBox.X, enemyList[j].punchBox.Y), Color.DarkRed);

                            break;

                        }
                        else
                            //playerManager.playerList[i].pos.X += 2;
                            playerManager.playerList[i].life -= 1;
                        particleExplosion = new ParticleExplosion(TextureManager.bloodTex, new Vector2(enemyList[j].punchBox.X + enemyList[j].punchBox.Width, enemyList[j].punchBox.Y), Color.DarkRed);

                        break;
                    }
                }
            }
        }

        public void BossAggro(PlayerManager playerManager)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                for (int j = 0; j < playerManager.playerList.Count; j++)
                {
                    if (enemyList[i] is Boss)
                    {
                        enemyList[i].BossFight();
                    }
                }
            }
        }

        public void IsBlocked(PlayerManager playerManager, GameTime gameTime) //stoppar gubbens slag om han slår på blocken
        {
            for (int i = 0; i < playerManager.playerList.Count; i++)
            {
                for (int j = 0; j < enemyList.Count; j++)
                {
                    if (enemyList[j].punchBox.Intersects(playerManager.playerList[i].blockBox))
                    {
                        enemyList[j].Animation(150, 1, 75, gameTime);
                        enemyList[j].punch = false;
                        enemyList[j].hasHit = false;
                        enemyList[j].fightFrame = 0;
                        enemyList[j].fightingCooldown = -200;
                        enemyList[j].punchBox = new Rectangle((int)enemyList[j].pos.X - 44, (int)enemyList[j].pos.Y - 65, 0, 0); //resettar slaget hitbox ovanför gubben igen

                        particleExplosion = new ParticleExplosion(TextureManager.bloodTex, new Vector2(playerManager.playerList[0].blockBox.X, playerManager.playerList[0].blockBox.Y + 50), Color.Blue);

                    }
                }
            }
        }

    }
}
