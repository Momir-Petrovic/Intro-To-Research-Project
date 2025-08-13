//using System;
using Unity.VisualScripting;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public GameObject bulletPrefab;
    public SceneController controller;
    public int damage, health;
    public float fireingRange, spreadRadius, turnSpeed, moveSpeed, gapSize, fireRate, fireRateMulti, bulletSpeed, currentTime, nextFire, enemyDistance, rangeVariance, skill, enemyRandomnessChance;
    public bool isSideA, alive;
    public GameObject enemy;
    public Vector3 myDirection;

    private void Start()
    {
        currentTime = 0;
        nextFire = 0;
        alive = true;
        skill = Random.Range(0, rangeVariance);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            alive = false;
        }
        if(!alive)
        {
            return;
        }
        currentTime += Time.deltaTime;
        if (isSideA)
        {
            if(enemy == null || !enemy.GetComponent<Agent>().alive)
            {
                enemyDistance = float.MaxValue;
                enemy = null;
                bool skippedValid = false;
                foreach(GameObject sideBagent in controller.sideB)
                {
                    if(sideBagent.GetComponent<Agent>().alive && Vector3.Distance(transform.position, sideBagent.transform.position) < enemyDistance)
                    {
                        if(Random.Range(0, 1.0f) < enemyRandomnessChance)
                        {
                            skippedValid = true;
                            continue;
                        }
                        enemy = sideBagent;
                        enemyDistance = Vector3.Distance(transform.position, sideBagent.transform.position);
                    }
                }
                if(skippedValid) {
                    foreach (GameObject sideBagent in controller.sideB)
                    {
                        if (sideBagent.GetComponent<Agent>().alive && Vector3.Distance(transform.position, sideBagent.transform.position) < enemyDistance)
                        {
                            enemy = sideBagent;
                            enemyDistance = Vector3.Distance(transform.position, sideBagent.transform.position);
                        }
                    }
                }

            }
            if(enemy != null)
            {
                myDirection = enemy.transform.position - transform.position;
            }
            myDirection = Vector3.RotateTowards(transform.forward, myDirection, turnSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(myDirection);
            if (enemy != null && Vector3.Distance(enemy.transform.position, transform.position) < fireingRange && 
                Vector3.Distance(transform.forward, (enemy.transform.position - transform.position).normalized) < 0.01)
            {
                if(nextFire < currentTime)
                {
                    nextFire = currentTime + Random.Range(fireRate, fireRate * fireRateMulti);
                    GameObject bullet = Instantiate(bulletPrefab, transform.position + (transform.forward * 1.1f), transform.rotation);
                    bullet.GetComponent<Rigidbody>().rotation = 
                        Quaternion.Euler(bullet.transform.rotation.eulerAngles + new Vector3(Random.Range(-spreadRadius, spreadRadius), 0, 0));
                    bullet.GetComponent<Bullet>().moveSpeed = bulletSpeed;
                }
            }
            else
            {
                nextFire = currentTime + Random.Range(0, fireRate);
            }
        }
        else
        {
            if (enemy == null || !enemy.GetComponent<Agent>().alive)
            {
                enemyDistance = float.MaxValue;
                enemy = null;
                bool skippedValid = false;
                foreach (GameObject sideAagent in controller.sideA)
                {
                    if (sideAagent.GetComponent<Agent>().alive && Vector3.Distance(transform.position, sideAagent.transform.position) < enemyDistance)
                    {
                        if (Random.Range(0, 1.0f) < enemyRandomnessChance)
                        {
                            skippedValid = true;
                            continue;
                        }
                        enemy = sideAagent;
                        enemyDistance = Vector3.Distance(transform.position, sideAagent.transform.position);
                    }
                }
                if(skippedValid)
                {
                    foreach (GameObject sideAagent in controller.sideA)
                    {
                        if (sideAagent.GetComponent<Agent>().alive && Vector3.Distance(transform.position, sideAagent.transform.position) < enemyDistance)
                        {
                            enemy = sideAagent;
                            enemyDistance = Vector3.Distance(transform.position, sideAagent.transform.position);
                        }
                    }
                }

            }
            if(enemy != null)
            {
                myDirection = enemy.transform.position - transform.position;
            }
            else
            {
                myDirection = new Vector3(1, 0, 0);
            }
            myDirection = Vector3.RotateTowards(transform.forward, myDirection, turnSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(myDirection);
            if(enemy == null || Vector3.Distance(enemy.transform.position, transform.position) > fireingRange - skill)
            {
                transform.position += myDirection * moveSpeed * Time.deltaTime;
            }
            if (enemy != null && Vector3.Distance(enemy.transform.position, transform.position) < fireingRange &&
                Vector3.Distance(transform.forward, (enemy.transform.position - transform.position).normalized) < 0.01)
            {
                if (nextFire < currentTime)
                {
                    nextFire = currentTime + Random.Range(fireRate, fireRate * fireRateMulti);
                    GameObject bullet = Instantiate(bulletPrefab, transform.position + (transform.forward * 1.1f), transform.rotation);
                    bullet.GetComponent<Rigidbody>().rotation =
                        Quaternion.Euler(bullet.transform.rotation.eulerAngles + new Vector3(Random.Range(-spreadRadius, spreadRadius), 0, 0));
                    bullet.GetComponent<Bullet>().moveSpeed = bulletSpeed;
                }
            }
            else
            {
                nextFire = currentTime + Random.Range(fireRate, fireRate * fireRateMulti);
            }
        }
    }
}
