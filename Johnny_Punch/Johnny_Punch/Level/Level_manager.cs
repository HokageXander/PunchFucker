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
using System.IO;

namespace Johnny_Punch
{
    class LevelManager
    {
        StreamReader streamReaderEnvironment, streamReaderItems;
        List<string> strings = new List<string>();
        List<Environment> enviromentList = new List<Environment>();
        public List<Item> itemList = new List<Item>();
        public static int levelNr = 1, levelEndPosX;
        public Rectangle nextLevelBox;
        public static bool end;

        public LevelManager(ContentManager Content)
        {
            streamReaderEnvironment = new StreamReader(@"lvl" + LevelManager.levelNr + "environment.txt");
            streamReaderItems = new StreamReader(@"lvl" + LevelManager.levelNr + "items.txt");
            MapReader(streamReaderEnvironment);
            ItemReader(streamReaderItems);
            
        }

        public void Update(GameTime gameTime)
        {
            if (levelNr == 1)
            {
                levelEndPosX = 5000; //man kan inte gå förbi detta i X-led (player.Movement)
                nextLevelBox = new Rectangle(levelEndPosX, (int)335, 40, 300); //tar man i denna går man över till level 2
            }
            if (levelNr == 2)
            {
                levelEndPosX = 2500;
                if (Boss.died)
                nextLevelBox = new Rectangle(levelEndPosX, (int)335, 40, 300);
                else
                    nextLevelBox = new Rectangle(levelEndPosX, (int)335, 0, 0);
                    
            }

            foreach (Item item in itemList)
            {
                item.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Environment enviroment in enviromentList)
            {
                enviroment.Draw(spriteBatch);
            }
            foreach (Item item in itemList)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.Draw(TextureManager.lifeBarTex, nextLevelBox, Color.Black);
        }

        public void MapReader(StreamReader streamReaderEnvironment)
        {
            streamReaderEnvironment = new StreamReader(@"lvl" + LevelManager.levelNr + "environment.txt");
            while (!streamReaderEnvironment.EndOfStream)
            {
                strings.Add(streamReaderEnvironment.ReadLine());
            }
            streamReaderEnvironment.Close();

            for (int i = 0; i < strings.Count; i++)
            {
                for (int j = 0; j < strings[i].Length; j++)
                {
                    if (strings[i][j] == 'c')
                    {
                        enviromentList.Add(new CityBackground(TextureManager.beachbackgroundTex, new Vector2(j * 91, i * 82)));
                    }
                    if (strings[i][j] == 'd')
                    {
                        enviromentList.Add(new CityBackground(TextureManager.beachback2groundTex, new Vector2(j * 91, i * 82)));
                    }
                    if (strings[i][j] == 'f')
                    {
                        enviromentList.Add(new CityBackground(TextureManager.beachback3groundTex, new Vector2(j * 91, i * 82)));
                    }
                    if (strings[i][j] == 'v')
                    {
                        enviromentList.Add(new CityBackground(TextureManager.jungleBackgroundTex, new Vector2(j * 91, i * 82)));
                    }
                    if (strings[i][j] == 'r')
                    {
                        enviromentList.Add(new Road(TextureManager.roadTex, new Vector2(j * 82, 10 + i * 82)));
                    }
                    if (strings[i][j] == 'j')
                    {
                        enviromentList.Add(new Road(TextureManager.jungleRoadTex, new Vector2(j * 82, 10 + i * 82)));
                    }
                    if (strings[i][j] == 't')
                    {
                        enviromentList.Add(new Road(TextureManager.templeRoadTex, new Vector2(j * 82, 10 + i * 82)));
                    }
                    if (strings[i][j] == 'm')
                    {
                        enviromentList.Add(new SmallPlant(TextureManager.smallPlantTex, new Vector2(j * 82, 10 + i * 82)));
                    }
                }
            }
            strings.Clear();
        }

        public void ItemReader(StreamReader streamReaderItems)
        {
            streamReaderItems = new StreamReader(@"lvl" + LevelManager.levelNr + "items.txt");
            while (!streamReaderItems.EndOfStream)
            {
                strings.Add(streamReaderItems.ReadLine());
            }
            streamReaderItems.Close();

            for (int i = 0; i < strings.Count; i++)
            {
                for (int j = 0; j < strings[i].Length; j++)
                {
                    if (strings[i][j] == 'w')
                    {
                        itemList.Add(new Watermelon(TextureManager.watermelon, new Vector2(j * 91, i * 82)));
                    }
                    if (strings[i][j] == 'p')
                    {
                        itemList.Add(new PinaColada(TextureManager.pinacolada, new Vector2(j * 91, i * 82)));
                    }
                    if (strings[i][j] == 'J') //konstigt att trädet är i items, men yolo
                    {
                        enviromentList.Add(new JungleTree(TextureManager.jungleEntranceTex, new Vector2(j * 82, 10 + i * 82)));
                    }
                }
            }
            strings.Clear();
        }

        public void NextLevel(PlayerManager playerManager, EnemyManager enemyManager)
        {
            for (int i = 0; i < playerManager.playerList.Count; i++)
            {
                if (playerManager.playerList[i].boundingBox.Intersects(nextLevelBox) && levelNr == 1)
                {
                    playerManager.playerList[i].pos = new Vector2(800, 400);
                    Camera.prevCentre.X = 0; //resettar så att kameran hamnar på spelaren igen
                    enemyManager.enemyList.Clear(); //raderar alla gamla fiender
                    enviromentList.Clear();
                    itemList.Clear();
                    LevelManager.levelNr++;
                    MapReader(streamReaderEnvironment); //läser om den nya textfilen i level 2
                    ItemReader(streamReaderItems);
                    Game1.ready = false; //fet loadingscreen
                }

                else if (playerManager.playerList[i].boundingBox.Intersects(nextLevelBox) && levelNr == 2)
                {
                    end = true;
                }
            }
        }
    }
}
