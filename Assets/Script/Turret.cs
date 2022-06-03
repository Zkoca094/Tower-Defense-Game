using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Turret : MonoBehaviour
{
    [Header("Attributes")]
    public float range;    
    public float fireRate = 1f;
    public float turnSpeed = 10f;
    private float fireCountDown = 0f;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";    
    private Transform target;
    public Transform partToRotate;    
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Vector3 distance;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortesDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortesDistance)
            {
                shortesDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortesDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }
    void Update()
    {
        if (target == null)
        {
            partToRotate.rotation = Quaternion.Lerp(partToRotate.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * turnSpeed);
            return;
        }

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;

    }
    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seek(target);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
