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
        public Matrix transform;
        Viewport view;
        Vector2 centre;

        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(Vector2 playerPos, Rectangle pRec)
        {
            centre = new Vector2(playerPos.X + (pRec.Width / 2) - 640, 0);

            transform = Matrix.CreateScale(new Vector3(1, 1, 0))
                * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));
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
