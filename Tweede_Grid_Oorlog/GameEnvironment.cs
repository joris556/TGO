using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Tweede_Grid_Oorlog
{
    class GameEnvironment
    {
        //Splitsing naar ui en bord
        GameState currentState;
        PlayingState playingState;
        MenuState menuState;

        public GameEnvironment()
        {
            playingState = new PlayingState();
            menuState = new MenuState();
            currentState = playingState;
        }

        public void HandleInput(InputHelper ih)
        {
            currentState.HandleInput(ih);
        }

        public void Update(GameTime gt)
        {
            currentState.Update(gt);
        }

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            currentState.Draw(gt, sb);
        }

    }
}
