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
    class PlayingState : GameState
    {
        Board board;

        public PlayingState() : base()
        {
            board = new Board();

        }

        public override void Update(GameTime gt)
        {
            board.Update(gt);

        }

        public override void HandleInput(InputHelper ih)
        {
            board.HandleInput(ih);

        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            board.Draw(gt, sb);

        }
    }
}
