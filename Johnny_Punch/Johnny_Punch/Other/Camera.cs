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
        public static Vector2 prevCentre;
        public Matrix transform;
        Viewport view;
        public Vector2 centre;

        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(Vector2 playerPos, Rectangle pRec, GameWindow gameWindow)
        {

            centre = new Vector2(playerPos.X + (pRec.Width / 2) - 800, 0);

            if (prevCentre.X < centre.X)
            {
                transform = Matrix.CreateScale(new Vector3(1, 1, 0))
                * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));

                prevCentre = centre;
            }
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
