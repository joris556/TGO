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
    class AssetHelper
    {
        ContentManager contentManager;

        public AssetHelper(ContentManager cm)
        {
            contentManager = cm;
        }

        public Texture2D GetSprite(string path)
        {
            return contentManager.Load<Texture2D>(path);
        }

        public SpriteFont GetFont(string path)
        {
            return contentManager.Load<SpriteFont>(path);
        }
    }
}
