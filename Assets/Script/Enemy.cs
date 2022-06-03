using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform target;
    public GameObject deathEffect;
    private int pointIndex = 0;

    [HideInInspector]
    public float speed = 10f;

    public float health = 100;
    public float startHealth = 100;

    public int worth = 5;

    [Header("Unity Stuff")]
    public Transform healthBar;
    public Image healthBarFill;

    private bool isDead = false;
    void Start()
    {
        target = WayPoints.points[0];
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.LookAt(healthBar.position + Camera.main.transform.forward);
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(target.position, transform.position) <= 0.4f)
        {
            GetNextWayPoints();
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBarFill.fillAmount = health / startHealth;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }
    void Die()
    {
        PlayerStats.Money += worth;
        WaveSpawner.EnemiesAlive--;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
    private void GetNextWayPoints()
    {
        if (pointIndex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        pointIndex++;
        target = WayPoints.points[pointIndex];
    }

    private void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
