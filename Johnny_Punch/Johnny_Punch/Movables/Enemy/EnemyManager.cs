using System;
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
    }
}
