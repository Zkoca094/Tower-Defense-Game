using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public GameObject turret;
    private Renderer render;
    private Color startColor;
    void Start()
    {
        render = GetComponent<Renderer>();
        startColor = render.material.color;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!BuildManager.instance.CanBuild)
            return;

        if (turret != null)
        {
            Debug.Log("Can't build there! - TODO : Display on screen.");
            return;
        }
        BuildManager.instance.BuildTurretOn(this);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!BuildManager.instance.CanBuild)
            return;
        if (BuildManager.instance.HasMoney)
            render.material.color = hoverColor;
        else
            render.material.color = notEnoughMoneyColor;
    }
    private void OnMouseExit()
    {
        render.material.color = startColor;
    }
}
