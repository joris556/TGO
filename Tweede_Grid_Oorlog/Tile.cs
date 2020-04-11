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
    enum TileType { Grass, Bushes, Forest, Road, House };

    class Tile : GameObject
    {
        TileType type;
        
        public TileType Type { get => type; set => type = value; }

        public Tile(Point gridpos, TileType type = TileType.Grass)
        {
            gridPosition = gridpos;
            this.type = type;

        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }

        public override void HandleInput(InputHelper ih)
        {
            base.HandleInput(ih);
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            base.Draw(gt, sb);
            
        }

    }
}
