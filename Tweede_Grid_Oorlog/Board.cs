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
        MapParser mapParser;
        Vector2 boardDimensions;
        UnitFactory unitFactory;
        Vector2 cameraOffset;
        Point boardWH;
        TGOMap currentMap;
        Rectangle bRect;
        Texture2D tiles;
        float cameraSpeed;
        List<Point> possibleMoves;
        Unit selectedUnit;
        public List<GameObject> gameObjects;

        public Board()
        {
            possibleMoves = new List<Point>();

            cameraSpeed = 5f;
            mapParser = new MapParser();
            currentMap = mapParser.ParseMap("map1");
            boardWH = currentMap.Dimensions;
            bRect = new Rectangle(0, 0, boardWH.X, boardWH.Y);
            boardDimensions = new Vector2(boardWH.X * Constants.tileSize.X, boardWH.Y * Constants.tileSize.Y);
            gameObjects = new List<GameObject>();
            tiles = Game1.AssetHelper.GetSprite("Sprites/tilePlaceholders");
            unitFactory = new UnitFactory();
        }

        public virtual void Update(GameTime gt)
        {
            foreach (GameObject g in gameObjects)
                g.Update(gt);
        }

        public virtual void HandleInput(InputHelper ih)
        {
            HandleCamera(ih);

            CheckUnitSelect(ih);

            CheckUnitMoveTo(ih);

            if (ih.MouseLeftButtonPressed() && ih.IsKeyDown(Keys.LeftShift))
            {
                Unit u = new InfantryUnit();
                Point pos = AdjustedMousePosition(ih.MousePosition);
                u.SetGridAndAdjust(pos);
                unitFactory.MakeUnit(ref u, UnitType.Gunner);
                gameObjects.Add(u);
            }
          

            foreach (GameObject g in gameObjects)
                g.CameraOffset = cameraOffset;

            foreach (GameObject g in gameObjects) 
                g.HandleInput(ih);
        }

        private Point AdjustedMousePosition(Vector2 mousepos)
        {
            return new Point((int)((mousepos.X - cameraOffset.X) / Constants.tileSize.X), (int)((mousepos.Y - cameraOffset.Y) / Constants.tileSize.Y));
        }

        private void CheckUnitSelect(InputHelper ih)
        {
            if (ih.MouseLeftButtonPressed())
            {
                selectedUnit = null;
                foreach (Unit u in gameObjects)
                {
                    if (u.Clickbox.Contains(ih.MousePosition - cameraOffset))
                    {
                        possibleMoves = MovesInRange(u.GridPosition, u.Range);
                        selectedUnit = u;
                    }
                }

                if (selectedUnit == null)
                    possibleMoves = null;
            }
        }

        private void CheckUnitMoveTo(InputHelper ih)
        {
            if (selectedUnit == null)
                return;

            if (ih.MouseRightButtonPressed())
            {
                if (possibleMoves.Contains(AdjustedMousePosition(ih.MousePosition)))
                {
                    List<Point> path = FindPath(selectedUnit.GridPosition, AdjustedMousePosition(ih.MousePosition));
                    selectedUnit.MoveTo(path);
                }
            }
        }

        public virtual void Draw(GameTime gt, SpriteBatch sb)
        {
            DrawBoard(gt, sb);

            if (possibleMoves != null)
                if (possibleMoves.Count > 0)
                    foreach (Point p in possibleMoves)
                        sb.Draw(tiles, p.ToVector2() * Constants.tileSize, new Rectangle(0,0,64,64), Color.Red);

            foreach (GameObject g in gameObjects)
                g.Draw(gt, sb);
        }


        public void HandleCamera(InputHelper ih)
        {
            if (ih.IsKeyDown(Keys.A) || ih.IsKeyDown(Keys.Left))
                cameraOffset.X += cameraSpeed;

            if (ih.IsKeyDown(Keys.W) || ih.IsKeyDown(Keys.Up))
                cameraOffset.Y += cameraSpeed;

            if (ih.IsKeyDown(Keys.D) || ih.IsKeyDown(Keys.Right))
                cameraOffset.X -= cameraSpeed;

            if (ih.IsKeyDown(Keys.S) || ih.IsKeyDown(Keys.Down))
                cameraOffset.Y -= cameraSpeed;

            cameraOffset.X = MathHelper.Clamp(cameraOffset.X, Constants.screenSize.X - boardDimensions.X, 0);
            cameraOffset.Y = MathHelper.Clamp(cameraOffset.Y, Constants.screenSize.Y - boardDimensions.Y, 0);
        }

        public TileType TileTypeAtPosition(Point pt)
        {
            return currentMap.MapGrid[pt.X, pt.Y];
        }

        //Double line iteration
        public bool UnobstructedLine (Point start, Point end)
        {
            Vector2 startCenter = new Vector2(start.X * Constants.tileSize.X + Constants.tileSize.X / 2f, start.Y * Constants.tileSize.Y + Constants.tileSize.Y / 2f);
            Vector2 endCenter = new Vector2(end.X * Constants.tileSize.X + Constants.tileSize.X / 2f, end.Y * Constants.tileSize.Y + Constants.tileSize.Y / 2f);
            Vector2 dir = endCenter - startCenter;
            dir.Normalize();
            float dist = Vector2.Distance(startCenter, endCenter);

            Vector2 adjLine1 = new Vector2(startCenter.X + dir.Y * 3f, startCenter.Y - dir.X * 3f);
            Vector2 adjLine2 = new Vector2(startCenter.X - dir.Y * 3f, startCenter.Y + dir.X * 3f);


            List<Point> ptList = new List<Point>();
            for (float i = 0; i < dist; i += 2f)
            {
                Vector2 t1 = adjLine1 + dir * i;
                Vector2 t2 = adjLine2 + dir * i;

                Point p1 = new Point((int)(t1.X / Constants.tileSize.X), (int)(t1.Y / Constants.tileSize.Y));
                Point p2 = new Point((int)(t2.X / Constants.tileSize.X), (int)(t2.Y / Constants.tileSize.Y));

                NoDubAdd(ptList, p1);
                NoDubAdd(ptList, p2);
            }

            foreach (Point p in ptList)
                if (TileTypeAtPosition(p) == TileType.Forest || TileTypeAtPosition(p) == TileType.House)
                    return false;

            return true;
        }

        private void NoDubAdd(List<Point> ptList, Point pt)
        {
            if (ptList.Contains(pt))
                return;
            ptList.Add(pt);
        }

        private List<Point> Neighbours(Point pt, bool allowForest)
        {
            List<Point> nb = new List<Point>();

            Point left = new Point(pt.X - 1, pt.Y);
            if (bRect.Contains(left))
                if (TileTypeAtPosition(left) != TileType.House && (TileTypeAtPosition(left) != TileType.Forest || allowForest))
                    nb.Add(left);

            Point right = new Point(pt.X + 1, pt.Y);
            if (bRect.Contains(right))
                if (TileTypeAtPosition(right) != TileType.House && (TileTypeAtPosition(right) != TileType.Forest || allowForest))
                    nb.Add(right);

            Point up = new Point(pt.X, pt.Y - 1);
            if (bRect.Contains(up))
                if (TileTypeAtPosition(up) != TileType.House && (TileTypeAtPosition(up) != TileType.Forest || allowForest))
                    nb.Add(up);

            Point down = new Point(pt.X, pt.Y + 1);
            if (bRect.Contains(down))
                if (TileTypeAtPosition(down) != TileType.House && (TileTypeAtPosition(down) != TileType.Forest || allowForest))
                    nb.Add(down);

            return nb;
        }

        public List<Point> FindPath(Point start, Point end, bool allowForest = true)
        {
            List<Point> openlist = new List<Point>();
            List<Point> closedlist = new List<Point>();
            Dictionary<Point, Point> cameFrom = new Dictionary<Point, Point>();
            Dictionary<Point, float> cost = new Dictionary<Point, float>();

            cameFrom.Add(start, start);
            cost.Add(start, 0f);
            openlist.Add(start);

            Point check = start;
            while (openlist.Count > 0)
            {
                check = openlist[0];
                openlist.Remove(check);

                if (check == end)
                    break;

                List<Point> nb = Neighbours(check, allowForest);
                foreach (Point p in nb)
                {
                    if (openlist.Contains(p))
                    {
                        if (cost[check] + 1f < cost[p])
                        {
                            cost[p] = cost[check] + 1f;
                            cameFrom[p] = check;
                        }
                    }
                    else
                    {
                        openlist.Add(p);
                        cost[p] = cost[check] + 1f;
                        cameFrom[p] = check;
                    }
                }

                closedlist.Add(check);
            }

            List<Point> reversedpath = new List<Point>();
            List<Point> path = new List<Point>();
            while (cameFrom[check] != check)
            {
                reversedpath.Add(check);
                check = cameFrom[check];
            }

            while (reversedpath.Count > 0)
            {
                Point pt = reversedpath[reversedpath.Count - 1];
                reversedpath.Remove(pt);
                path.Add(pt);
            }

            return path;
        }

        public List<Point> MovesInRange(Point start, float range, bool allowForest = true)
        {
            List<Point> openlist = new List<Point>();
            List<Point> closedlist = new List<Point>();
            Dictionary<Point, float> cost = new Dictionary<Point, float>();

            cost.Add(start, 0f);
            openlist.Add(start);

            Point check = start;
            while (openlist.Count > 0)
            {
                check = openlist[0];
                openlist.Remove(check);

                if (cost[check] >= range)
                    continue;

                List<Point> nb = Neighbours(check, allowForest);
                foreach (Point p in nb)
                {
                    if (openlist.Contains(p))
                    {
                        if (cost[check] + 1f < cost[p])
                        {
                            cost[p] = cost[check] + 1f;
                        }
                    }
                    else
                    {
                        openlist.Add(p);
                        cost[p] = cost[check] + 1f;
                    }
                    
                }

                closedlist.Add(check);
            }

            return closedlist;
        }

        private void DrawBoard(GameTime gt, SpriteBatch sb)
        {
            //Niet geculled maar boeie
            for (int y = 0; y < currentMap.Dimensions.Y; y++)
                for (int x = 0; x < currentMap.Dimensions.X; x++)
                    sb.Draw(tiles, (new Vector2(x * Constants.tileSize.X, y * Constants.tileSize.Y) + cameraOffset) * Constants.scale, 
                        new Rectangle((int)Constants.tileSize.X * (int)currentMap.MapGrid[x,y], 0, 64, 64), Color.White, 0f, Vector2.Zero, Constants.scale, SpriteEffects.None, 0f);

        }
    }


}
