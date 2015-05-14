﻿using System;
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
        List<string> strings = new List<string>();
        List<Environment> enviromentList = new List<Environment>();
        public List<Item> itemList = new List<Item>();
        StreamReader streamReaderEnvironment, streamReaderItems;
        public static int levelNr = 2;

        public LevelManager(ContentManager Content)
        {
            streamReaderEnvironment = new StreamReader(@"lvl1environment.txt");
            streamReaderItems = new StreamReader(@"lvl1items.txt");
            MapReader(streamReaderEnvironment);
            ItemReader(streamReaderItems);
        }

        public void Update(GameTime gameTime)
        {
            if (levelNr == 1) //vet ej om detta kommer att fungera, men kan få vara grunden tills vidare
            {
                streamReaderEnvironment = new StreamReader(@"lvl1environment.txt");
                streamReaderItems = new StreamReader(@"lvl1items.txt");
            }
            if (levelNr == 2)
            {
                streamReaderEnvironment = new StreamReader(@"lvl2environment.txt");
                streamReaderItems = new StreamReader(@"lvl2items.txt");
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
        }

        public void MapReader(StreamReader streamReaderEnvironment)
        {
            while (!streamReaderEnvironment.EndOfStream)
            {
                strings.Add(streamReaderEnvironment.ReadLine());
            }
            streamReaderEnvironment.Close();

            for (int i = 0; i < strings.Count; i++)
            {
                for (int j = 0; j < strings[i].Length; j++)
                {
                    if (strings[i][j] == 'r')
                    {
                        enviromentList.Add(new Road(TextureManager.roadTex, new Vector2(j * 82, 10 + i * 82)));
                    }
                    if (strings[i][j] == 'm')
                    {
                        enviromentList.Add(new SmallPlant(TextureManager.smallPlantTex, new Vector2(j * 82, 10 + i * 82)));
                    }
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
                }
            }
            strings.Clear();
        }

        public void ItemReader(StreamReader streamReaderItems)
        {
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
                    if (strings[i][j] == 's')
                    {
                        itemList.Add(new Sabre(TextureManager.sabreTex, new Vector2(j * 91, i * 82)));
                    }
                }
            }
            strings.Clear();
        } // jag lade till en textfil för items. Kanske fiender också?
    }
}
