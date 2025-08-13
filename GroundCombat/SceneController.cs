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
    [SerializeField] private float fireingRange, spreadRadius, fireRate, fireRateMulti, turnSpeed, moveSpeed, bulletSpeed, gapSize, sideAPosition, sideBPosition, sideBVarience, rangeVariance, enemyRandomnessChance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < sideACount; i++)
        {
            newAgent = Instantiate(agentPrefab);
            newAgentComponent = newAgent.GetComponent<Agent>();
            newAgentComponent.controller = this;
            newAgentComponent.fireingRange = fireingRange;
            newAgentComponent.spreadRadius = spreadRadius;
            newAgentComponent.health = health;
            newAgentComponent.damage = damage;
            newAgentComponent.turnSpeed = turnSpeed;
            newAgentComponent.moveSpeed = moveSpeed;
            newAgentComponent.fireRate = fireRate;
            newAgentComponent.fireRateMulti = fireRateMulti;
            newAgentComponent.bulletSpeed = bulletSpeed;
            newAgentComponent.isSideA = true;
            newAgentComponent.enemyRandomnessChance = enemyRandomnessChance;
            newAgentComponent.transform.position = new Vector3(sideAPosition, (i-(sideACount/2)) * gapSize ,0);
            sideA.Add(newAgent);
        }

        for (int i = 0; i < sideBCount; i++)
        {
            newAgent = Instantiate(agentPrefab);
            newAgentComponent = newAgent.GetComponent<Agent>();
            newAgentComponent.controller = this;
            newAgentComponent.fireingRange = fireingRange;
            newAgentComponent.spreadRadius = spreadRadius;
            newAgentComponent.health = health;
            newAgentComponent.damage = damage;
            newAgentComponent.turnSpeed = turnSpeed;
            newAgentComponent.moveSpeed = moveSpeed;
            newAgentComponent.fireRate = fireRate;
            newAgentComponent.fireRateMulti = fireRateMulti;
            newAgentComponent.bulletSpeed = bulletSpeed;
            newAgentComponent.isSideA = false;
            newAgentComponent.rangeVariance = rangeVariance;
            newAgentComponent.enemyRandomnessChance = enemyRandomnessChance;
            newAgentComponent.transform.position = new Vector3(sideBPosition - Random.Range(0, sideBVarience), Random.Range(-sideACount/2*gapSize, sideACount/2*gapSize), 0);
            sideB.Add(newAgent);
        }
    }

}

