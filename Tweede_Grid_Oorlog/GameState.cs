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
    abstract class GameState
    {

        public GameState()
        {
            gameObjects = new List<GameObject>();
        }

        public abstract void Update(GameTime gt);

        public abstract void HandleInput(InputHelper ih);

        public abstract void Draw(GameTime gt, SpriteBatch sb);

    }
}
