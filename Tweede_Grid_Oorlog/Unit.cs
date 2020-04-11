﻿using System;
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

        public Unit()
        {

        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            base.Draw(gt, sb);
        }

        public override void HandleInput(InputHelper ih)
        {
            base.HandleInput(ih);
        }

        public override void Update(GameTime gt)
        {
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

            //Animatie
        }

        public bool CanSee(Unit other, Board board)
        {
            float dist = Vector2.Distance(gridPosition.ToVector2(), other.GridPosition.ToVector2());
            if (isEnemy == other.IsEnemy || dist > range * 2)
                return false;

            if (Math.Abs(gridPosition.X - other.GridPosition.X) == Math.Abs(gridPosition.Y - other.GridPosition.Y))
            {
                
            }
            return true;
        }

        private List<Point> TilesOnLine(Vector2 start, Vector2 end)
        {
            List<Point> ptList = new List<Point>();
            Vector2 startTile = new Vector2((float)Math.Floor(start.X / Constants.tileSize.X), (float)Math.Floor(start.Y / Constants.tileSize.Y));
            Vector2 direction = end - start;
            direction.Normalize();
            float dtx = (startTile.X * Constants.tileSize.X + Constants.tileSize.X / 2f) / direction.X;
            float dty = (startTile.Y * Constants.tileSize.Y + Constants.tileSize.Y / 2f) / direction.Y;

            return ptList;

        }

    }
}