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
        List<string> strings = new List<string>();
        List<Environment> enviromentList = new List<Environment>();

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
                        enviromentList.Add(new Road(TextureManager.roadTex, new Vector2(j * 82, 10 + i * 82)));
                    }   
                    if(strings[i][j] == 'm')
                    {
                        enviromentList.Add(new ManHoleCover(TextureManager.manHoleCoverTex, new Vector2(j * 82, 10+i * 82)));                
                    }
                    if(strings[i][j] == 'c')
                    {
                        enviromentList.Add(new CityBackground(TextureManager.citybackgroundTex, new Vector2(j * 82, i * 82)));
                    }
                }
            }
            strings.Clear();
        }     
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Environment enviroment in enviromentList)
            {
                enviroment.Draw(spriteBatch);
            }           
        }
    }
}
