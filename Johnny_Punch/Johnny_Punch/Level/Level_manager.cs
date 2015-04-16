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
        //StreamReader streamReader;
        
        List<string> strings = new List<string>();
        List<Road> roadList = new List<Road>();
        List<ManHoleCover> manHoleCoverList = new List<ManHoleCover>();

        Road road;
        ManHoleCover manHoleCover;

        public LevelManager(ContentManager Content)
        {
            StreamReader streamReader = new StreamReader(@"lvl1.txt");
            MapReader(streamReader);
            
        }
        
        public void MapReader(StreamReader streamReader)
        {
            while (!streamReader.EndOfStream)
            {
                strings.Add(streamReader.ReadLine());
            }
            streamReader.Close();

            for (int i = 0; i < strings.Count; i++)
            {
                for (int j = 0; j < strings[i].Length; j++)
                {
                    if (strings[i][j] == 'r')
                    {
                        road = new Road(TextureManager.roadTex, new Vector2(j * 82, 10+i * 82));
                        roadList.Add(road);
                    }   
                    if(strings[i][j] == 'm')
                    {
                        manHoleCover = new ManHoleCover(TextureManager.manHoleCoverTex, new Vector2(j * 82, 10+i * 82));
                        manHoleCoverList.Add(manHoleCover);
                    }

                }
            }
            strings.Clear();
        }     
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(ManHoleCover manHoleCover in manHoleCoverList)
            {
                manHoleCover.Draw(spriteBatch);
            }
            foreach(Road road in roadList)
            {
                road.Draw(spriteBatch);
            }
        }
    }
}
