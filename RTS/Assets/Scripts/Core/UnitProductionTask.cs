using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProductionTask : IUnitProductionTask
{
    public Sprite Icon { get; }
    public float TimeLeft { get; set; }
    public float ProductionTime { get; }
    public string UnitName { get; }
    public GameObject UnitPrefab { get; }

    public UnitProductionTask(Sprite icon, float productionTime, string unitName, GameObject unitPrefab)
    {
        Icon = icon;
        TimeLeft = productionTime;
        ProductionTime = productionTime;
        UnitName = unitName;
        UnitPrefab = unitPrefab;
    }
}
