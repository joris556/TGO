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
    enum UnitClass { Infantry, Armored };

    class Unit : GameObject
    {
        protected float visibilityRange;
        protected float armor;
        protected float armorPierce;
        protected float range;
        protected float health;
        protected float damage;
        protected float movement;
        protected float visibility;
        protected UnitClass unitClass;
        protected UnitType unitType;
        protected bool isDead;
        protected float movementLeft;
        protected bool canAttack;
        protected bool isEnemy;

        //Pathfinding and moving
        protected bool isMoving;
        protected List<Point> pathToMove;
        protected Vector2 currentDirection;
        protected Vector2 currentGoal;
        protected Point gridGoal;

        public float Armor { get => armor; set => armor = value; }
        public float ArmorPierce { get => armorPierce; set => armorPierce = value; }
        public float Range { get => range; set => range = value; }
        public float Health { get => health; set => health = value; }
        public float Damage { get => damage; set => damage = value; }
        public float Movement { get => movement; set => movement = value; }
        public float Visibility { get => visibility; set => visibility = value; }
        public float VisibilityRange { get => visibilityRange; set => visibilityRange = value; }
        public UnitClass UnitClass { get => unitClass; set => unitClass = value; }
        public bool IsDead { get => isDead; set => isDead = value; }
        public float MovementLeft { get => movementLeft; set => movementLeft = value; }
        public bool CanAttack { get => canAttack; set => canAttack = value; }
        public UnitType UnitType { get => unitType; set => unitType = value; }
        public bool IsEnemy { get => isEnemy; set => isEnemy = value; }
        public bool IsMoving { get => isMoving; set => isMoving = value; }
        public List<Point> PathToMove { get => pathToMove; set => pathToMove = value; }

        public Unit()
        {
            sprite = Game1.AssetHelper.GetSprite("Sprites/unitPlaceholder");
        }

        public virtual void TurnReset()
        {
            canAttack = true;
            movementLeft = movement;
            if (isMoving)
            {
                MoveFast();
            }
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            base.Draw(gt, sb);
        }

        public override void HandleInput(InputHelper ih)
        {
            base.HandleInput(ih);
            if (ih.KeyPressed(Keys.Space) && isMoving)
            {
                MoveFast();
            }
        }

        public override void Update(GameTime gt)
        {
            if (isMoving)
                Move();

            base.Update(gt);
        }

        public virtual void TriggerDeath()
        {
            isDead = true;
        }

        public virtual void Attack(Unit u)
        {
            if (isEnemy == u.IsEnemy)
                return;

            //Damage = AP > Armor = 100% anders AP / (Armor + 1)
            float dmg = damage * (armorPierce > u.Armor ? 1f : armorPierce / (u.Armor + 1f));
            u.Health -= dmg;
            if (u.Health <= 0)
                u.TriggerDeath();

            canAttack = false;

            //Animatie
        }

        public bool CanSee(Unit other, Board board)
        {
            float dist = Vector2.Distance(gridPosition.ToVector2(), other.GridPosition.ToVector2());
            if (isEnemy == other.IsEnemy || dist > range * 2) // Out of range or on same team.
                return false;

            float stealthRange = other.Visibility / Constants.tileStealth[(int)board.TileTypeAtPosition(other.gridPosition)] + 1f;
            if (stealthRange < dist) //Unit is hidden
                return false;

            return board.UnobstructedLine(gridPosition, other.GridPosition);

        }

        public bool AttackInRange(Unit other, Board board)
        {
            float dist = Vector2.Distance(gridPosition.ToVector2(), other.GridPosition.ToVector2());
            if (dist > range)
                return false;

            return board.UnobstructedLine(gridPosition, other.GridPosition);
        }

        public virtual void MoveTo(List<Point> path)
        {
            isMoving = true;
            pathToMove = path;
            currentGoal = gridPosition.ToVector2() * Constants.tileSize;
            currentDirection = currentGoal - position;
            currentDirection.Normalize();
        }

        protected virtual void Move()
        {
            //Vector2 dir = currentGoal - position;
            //dir.Normalize();
            //if (dir != currentDirection) //aka je bent er voorbij
            if (Vector2.Distance(currentGoal, position) < 3f)
            {
                if (pathToMove.Count == 0)
                {
                    isMoving = false;
                    gridPosition = gridGoal;
                    SetGridAndAdjust(gridGoal);
                    return;
                }

                position = currentGoal;
                gridGoal = pathToMove[0];
                pathToMove.RemoveAt(0);

                currentGoal = gridGoal.ToVector2() * Constants.tileSize;
                currentDirection = currentGoal - position;
                currentDirection.Normalize();
            }
            position += currentDirection * 3;
        }

        public void MoveFast()
        {
            if (pathToMove.Count > 0)
            {
                Point end = pathToMove[PathToMove.Count - 1];
                gridPosition = end;
                SetGridAndAdjust(end);
                position = end.ToVector2() * Constants.tileSize;
            }
        }
    }
}
