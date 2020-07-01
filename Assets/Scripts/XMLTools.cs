using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class XMLTools
{
    public static Soldier GetSoldierByType(SoldierType soldierType,PlayerType player)
    {
        return new Soldier(10f, 3f, 3, soldierType, 3f) { playerType = player };
    }
}