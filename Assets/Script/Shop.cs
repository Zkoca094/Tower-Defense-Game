using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standartTurret;
    public TurretBluePrint anotherTurret;

    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
        standartTurret.costText.text = string.Format("${0}", standartTurret.cost);
        anotherTurret.costText.text = string.Format("${0}", anotherTurret.cost);
    }

    public void SelectStandartTurret()
    {
        buildManager.SelectTurretToBuild(standartTurret);
    }

    public void SelectAnotherTurret()
    {
        buildManager.SelectTurretToBuild(anotherTurret);
    }
}
