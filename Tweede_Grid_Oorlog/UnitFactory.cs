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
    enum UnitType { Gunner, Sniper, Bazooka, LightTank, MediumTank, Artillery };

    class UnitFactory
    {
        //  Health / Damage / Armor / Armorpierce / Range / Visibility / Movement
        float[,] unitVars = {
            { 10f, 4f, 1f, 1f, 3f, 2f, 3f }, //Gunner
            { 7f, 6f, 1f, 2f, 4f, 1f, 2f }, //Sniper
            { 8f, 3f, 1f, 5f, 2.5f, 2f, 2f }, //Bazooka
            { 12f, 4f, 3f, 2f, 3f, 3f, 3f }, //LightTank
            { 15f, 5f, 5f, 5f, 4f, 5f, 2f }, //MediumTank
            { 8f, 3f, 2f, 5f, 6f, 5f, 2f } //Artillery
        };


        public UnitFactory()
        {

        }

        public void MakeUnit(ref Unit unit, UnitType type)
        {
            int nr = (int)type;
            unit.Health = unitVars[nr, 0];
            unit.Damage = unitVars[nr, 1];
            unit.Armor = unitVars[nr, 2];
            unit.ArmorPierce = unitVars[nr, 3];
            unit.Range = unitVars[nr, 4];
            unit.Visibility = unitVars[nr, 5];
            unit.Movement = unitVars[nr, 0];
            unit.UnitClass = (nr > 2 ? UnitClass.Armored : UnitClass.Infantry);
        }

    }
}
