using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private TurretBluePrint turretToBuild;
    public GameObject standartTurretPrefab;
    public GameObject anotherTurretPrefab;
    public GameObject buildEffect;
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("More than one BuildManager in scene!");
    }
    public void  SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
    }
    internal void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.transform.position, Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.transform.position, Quaternion.identity);
        Destroy(effect, 3f);

        PlayerStats.Money -= turretToBuild.cost;
        Debug.Log("Turret build! Money left: " + PlayerStats.Money);

    }
}
