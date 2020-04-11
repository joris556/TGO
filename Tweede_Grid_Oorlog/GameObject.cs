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
    class GameObject
    {
        protected Vector2 position, cameraOffset;
        protected Texture2D sprite;
        protected Rectangle clickBox, spriteBox;
        protected bool visible = true;
        protected Point gridPosition;

        public Vector2 Position { get => position; set => position = value; }
        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public Rectangle ClickBox { get => clickBox; set => clickBox = value; }
        public Vector2 CameraOffset { get => cameraOffset; set => cameraOffset = value; }
        public Rectangle SpriteBox { get => spriteBox; set => spriteBox = value; }
        public bool Visible { get => visible; set => visible = value; }
        public Point GridPosition { get => gridPosition; set => gridPosition = value; }


        public GameObject()
        {

        }

        public virtual void Update(GameTime gt)
        {

        }

        public virtual void HandleInput(InputHelper ih)
        {

        }

        public virtual void Draw(GameTime gt, SpriteBatch sb)
        {
            if (visible)
                sb.Draw(sprite, (position + cameraOffset) * Constants.scale, spriteBox, Color.White, 0f, Vector2.Zero, Constants.scale, SpriteEffects.None, 0f);
        }

    }
}
