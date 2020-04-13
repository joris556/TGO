using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Tweede_Grid_Oorlog
{
    class MapParser
    {
        StreamReader streamReader;

        public MapParser()
        {
            
        }

        public TGOMap ParseMap(string mapname = "map1")
        {
            string realpath = "../../../../Assets/" + mapname + ".txt";
            streamReader = new StreamReader(realpath);
            TGOMap map = new TGOMap();

            map.Name = streamReader.ReadLine();
            string[] dim = streamReader.ReadLine().Split();
            map.Dimensions = new Point(int.Parse(dim[0]), int.Parse(dim[1]));
            TileType[,] tileGrid = new TileType[map.Dimensions.X, map.Dimensions.Y];

            for (int y = 0; y < map.Dimensions.Y; y++)
            {
                string s = streamReader.ReadLine();
                for (int x = 0; x < map.Dimensions.X; x++)
                    tileGrid[x, y] = LetterToType(s[x]);
            }
            map.MapGrid = tileGrid;

            Rectangle[,] tileSprites = new Rectangle[map.Dimensions.X, map.Dimensions.Y];
            for (int y = 0; y < map.Dimensions.Y; y++)
                for (int x = 0; x < map.Dimensions.X; x++)
                {
                    //Hiero de orientatie/ randomization doen
                }

            map.Spritebox = tileSprites;
            streamReader.Close();
            return map;
        }

        private TileType LetterToType(char c)
        {
            switch (c)
            {
                case 'g': return TileType.Grass;
                case 'b': return TileType.Bushes;
                case 'f': return TileType.Forest;
                case 'r': return TileType.Road;
                case 'h': return TileType.House;
            }

            return TileType.Grass;
        }


    }

    struct TGOMap
    {
        TileType[,] mapGrid;
        string name;
        Point dimensions;
        Rectangle[,] spritebox;

        public TileType[,] MapGrid { get => mapGrid; set => mapGrid = value; }
        public string Name { get => name; set => name = value; }
        public Point Dimensions { get => dimensions; set => dimensions = value; }
        public Rectangle[,] Spritebox { get => spritebox; set => spritebox = value; }
    }
}
