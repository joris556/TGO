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
    class Constants
    {
        public static Point screenSize = new Point(1920, 1080);
        public static Vector2 tileSize = new Vector2(64, 64);
        public static float scale = 1f;
        public static float[] tileStealth = { 0.1f, 2f, 3f, 0.1f, 100f };
        
    }
}
