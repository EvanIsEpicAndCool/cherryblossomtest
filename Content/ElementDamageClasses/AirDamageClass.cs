using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace cherryblossomtest.Content.ElementDamageClasses
{
    internal class AirDamageClass : DamageClass
    {
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Generic)
                return StatInheritanceData.Full;
            // else if (damageClass == DamageClass.Ranged)
            //    return new StatInheritanceData(
            //        damageInheritance: 1f,
            //        critChanceInheritance: 1f,
            //        attackSpeedInheritance: 1f,
            //        armorPenInheritance: 1f,
            //        knockbackInheritance: 1f
            //    );

            return StatInheritanceData.None;
        }
    }
}