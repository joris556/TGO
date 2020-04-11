using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tweede_Grid_Oorlog
{
    class Board
    {
        Vector2 boardDimensions;
        Vector2 cameraOffset;
        float cameraSpeed;
        public List<GameObject> gameObjects;

        public Board()
        {
            boardDimensions = new Vector2(2000, 2000);
            gameObjects = new List<GameObject>();
        }

        public virtual void Update(GameTime gt)
        {
            foreach (GameObject g in gameObjects)
                g.Update(gt);
        }

        public virtual void HandleInput(InputHelper ih)
        {
            HandleCamera(ih);

            foreach (GameObject g in gameObjects)
                g.CameraOffset = cameraOffset;

            foreach (GameObject g in gameObjects) 
                g.HandleInput(ih);
        }

        public virtual void Draw(GameTime gt, SpriteBatch sb)
        {
            foreach (GameObject g in gameObjects)
                g.Draw(gt, sb);
        }


        public void HandleCamera(InputHelper ih)
        {
            if (ih.IsKeyDown(Keys.A) || ih.IsKeyDown(Keys.Left))
                cameraOffset.X += 5;

            if (ih.IsKeyDown(Keys.W) || ih.IsKeyDown(Keys.Up))
                cameraOffset.Y += 5;

            if (ih.IsKeyDown(Keys.D) || ih.IsKeyDown(Keys.Right))
                cameraOffset.X -= 5;

            if (ih.IsKeyDown(Keys.S) || ih.IsKeyDown(Keys.Down))
                cameraOffset.Y -= 5;

            MathHelper.Clamp(cameraOffset.X, 0, boardDimensions.X - Constants.screenSize.X);
            MathHelper.Clamp(cameraOffset.Y, 0, boardDimensions.Y - Constants.screenSize.Y);
        }
    }
}
