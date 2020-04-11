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
    class ArmoredUnit : Unit
    {
        public ArmoredUnit()
        {
            this.UnitClass = UnitClass.Armored; //Arbitrair maar werkt wel prettig.
        }

        public override void Attack(Unit u)
        {
            if (isEnemy == u.IsEnemy)
                return;

            if (unitType == UnitType.LightTank && u.UnitClass == UnitClass.Infantry)
            {
                //Damage = AP > Armor = 100% anders AP / (Armor + 1)
                float dmg = 2f * damage * (armorPierce > u.Armor ? 1f : armorPierce / (u.Armor + 1f));
                u.Health -= dmg;
                if (u.Health <= 0)
                    u.TriggerDeath();

                //Animatie
            }
            else
                base.Attack(u);
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            base.Draw(gt, sb);
        }

        public override void HandleInput(InputHelper ih)
        {
            base.HandleInput(ih);
        }

        public override void TriggerDeath()
        {
            base.TriggerDeath();
        }

        public override void Update(GameTime gt)
        {
            base.Update(gt);
        }
    }
}
