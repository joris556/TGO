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
        protected Rectangle clickbox, spritebox;
        protected float rotation;
        protected bool visible = true;
        protected Point gridPosition;
        protected Vector2 origin;

        public Vector2 Position { get => position; set { position = value; clickbox.X = (int)position.X; clickbox.Y = (int)position.Y; } }
        public Texture2D Sprite { get => sprite; set => sprite = value; }
        public Rectangle Clickbox { get => clickbox; set => clickbox = value; }
        public Vector2 CameraOffset { get => cameraOffset; set => cameraOffset = value; }
        public Rectangle Spritebox { get => spritebox; set => spritebox = value; }
        public bool Visible { get => visible; set => visible = value; }
        public Point GridPosition { get => gridPosition; set => gridPosition = value; }
        public Vector2 Origin { get => origin; set => origin = value; }
        public float Rotation { get => rotation; set => rotation = value; }

        public GameObject()
        {
            clickbox = new Rectangle(0, 0, (int)Constants.tileSize.X, (int)Constants.tileSize.Y);
            origin = new Vector2(32, 32);
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
                sb.Draw(sprite, (position + cameraOffset + origin) * Constants.scale, spritebox, Color.White, rotation, origin , Constants.scale, SpriteEffects.None, 0f);
        }

        public void SetGridAndAdjust(Point p)
        {
            gridPosition = p;
            position = new Vector2(p.X * Constants.tileSize.X, p.Y * Constants.tileSize.Y);
            clickbox.X = (int)position.X;
            clickbox.Y = (int)position.Y;
        }
    }
}
