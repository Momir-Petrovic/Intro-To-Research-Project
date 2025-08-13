using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public List<GameObject> sideA = new List<GameObject>();
    public List<GameObject> sideB = new List<GameObject>();
    public GameObject agentPrefab;
    private GameObject newAgent;
    private Agent newAgentComponent;

    public int sideACount, sideBCount;
    [SerializeField] private int damage, health;
    [SerializeField] private float fireingRange, fireRate, turnSpeed, moveSpeed, bulletSpeed, gapSize;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < sideACount; i++)
        {
            newAgent = Instantiate(agentPrefab, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)  ), 
                Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)) ));
            newAgentComponent = newAgent.GetComponent<Agent>();
            newAgentComponent.controller = this;
            newAgentComponent.fireingRange = fireingRange;
            newAgentComponent.health = health;
            newAgentComponent.damage = damage;
            newAgentComponent.turnSpeed = turnSpeed;
            newAgentComponent.moveSpeed = moveSpeed;
            newAgentComponent.fireRate = fireRate;
            newAgentComponent.bulletSpeed = bulletSpeed;
            newAgentComponent.isSideA = true;
            sideA.Add(newAgent);
        }

        for (int i = 0; i < sideBCount; i++)
        {
            newAgent = Instantiate(agentPrefab, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)),
                Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            newAgentComponent = newAgent.GetComponent<Agent>();
            newAgentComponent.controller = this;
            newAgentComponent.fireingRange = fireingRange;
            newAgentComponent.health = health;
            newAgentComponent.damage = damage;
            newAgentComponent.turnSpeed = turnSpeed;
            newAgentComponent.moveSpeed = moveSpeed;
            newAgentComponent.fireRate = fireRate;
            newAgentComponent.bulletSpeed = bulletSpeed;
            newAgentComponent.isSideA = false;
            sideB.Add(newAgent);
        }
    }

}

