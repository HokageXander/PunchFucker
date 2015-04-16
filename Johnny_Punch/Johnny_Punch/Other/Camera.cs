using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Johnny_Punch
{
    class Camera
    {
        Vector2 prevCenter;
        public Matrix transform;
        Viewport view;
        Vector2 centre;
        
        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(Vector2 playerPos, Rectangle pRec, GameWindow gameWindow)
        {

            //float dick = gameWindow.ClientBounds.Width ;
            //    Console.WriteLine(dick);
            //if(playerPos.X >=800)
            //{
                //if(playerPos.X >= prevCenter.X)
            
            centre = new Vector2(playerPos.X + (pRec.Width / 2) -800, 0);

            if (prevCenter.X < centre.X)
            {
                transform = Matrix.CreateScale(new Vector3(1, 1, 0))
                * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
            prevCenter = centre;
            }
           
            
            //}
            
        }

        public Matrix GetTransform
        {
            get
            {
                return transform;
            }
        }
        public Vector2 GetCameraPos
        {
            get
            {
                return centre;
            }
        }

    }
}
