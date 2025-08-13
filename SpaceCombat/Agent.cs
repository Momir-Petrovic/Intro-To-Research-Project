//using System;
using Unity.VisualScripting;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public GameObject bulletPrefab;
    public SceneController controller;
    public int damage, health;
    public float fireingRange, turnSpeed, moveSpeed, fireRate, bulletSpeed, currentTime, nextFire, enemyDistance;
    public bool isSideA;
    public GameObject enemy;
    public Vector3 targetLogation, vectorToTarget, thrust, rotate;
    public Rigidbody rb;

    private void Start()
    {
        currentTime = 0;
        nextFire = 0;
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if(health < 0)
        {
            return;
        }
        enemy = null;
        enemyDistance = float.MaxValue;
        currentTime += Time.deltaTime;
        if (isSideA)
        {
            foreach (GameObject sideBagent in controller.sideB)
            {
                if (sideBagent.GetComponent<Agent>().health > 0 && Vector3.Distance(transform.position, sideBagent.transform.position) < enemyDistance)
                {
                    enemy = sideBagent;
                    enemyDistance = Vector3.Distance(transform.position, sideBagent.transform.position);
                }
            }
        }
        else
        {
            foreach (GameObject sideAagent in controller.sideA)
            {
                if (sideAagent.GetComponent<Agent>().health > 0 && Vector3.Distance(transform.position, sideAagent.transform.position) < enemyDistance)
                {
                    enemy = sideAagent;
                    enemyDistance = Vector3.Distance(transform.position, sideAagent.transform.position);
                }
            }

        }
        

        if (enemy != null)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) > 20)
            {
                targetLogation = enemy.transform.position * 0.9f;
            }
            else
            {
                targetLogation = transform.position - enemy.transform.position;
            }
        } else
        {
            targetLogation = Vector3.zero;
        }
        vectorToTarget = (targetLogation - transform.position).normalized;
        thrust = transform.forward;
        rotate = Vector3.Cross(transform.forward, vectorToTarget);
        
        rb.AddForce(thrust * moveSpeed);
        rb.AddTorque(rotate * turnSpeed);

        Debug.DrawLine(transform.position, transform.position + transform.forward);
        Debug.DrawLine(transform.position, transform.position + vectorToTarget, Color.blue);
        Debug.DrawLine(transform.position, transform.position + rotate, Color.red);

        if (enemy != null && Vector3.Distance(enemy.transform.position, transform.position) < fireingRange)
        {
            if (nextFire < currentTime)
            {
                nextFire = currentTime + fireRate;
                Vector3 aimPosition = targetLogation - transform.position + 
                    (enemy.GetComponent<Rigidbody>().linearVelocity * Vector3.Distance(targetLogation, transform.position)/bulletSpeed ); 
                GameObject bullet = Instantiate(bulletPrefab, transform.position + (transform.forward * 1.1f), 
                    Quaternion.LookRotation(aimPosition));
                bullet.GetComponent<Bullet>().moveSpeed = bulletSpeed;
            }
        }
        else
        {
            nextFire = currentTime + Random.Range(0, fireRate);
        }
    }

    // Update is called once per frame
    /*
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
    }*/
}
