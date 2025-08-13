using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Swarm : MonoBehaviour
{
    public List<GameObject> boids = new List<GameObject>();
    public GameObject boidPrefab;
    private GameObject newBoid;
    private Boid newBoidComponent;

    public int boidCount;
    [SerializeField] private float normalDistance, closeDistance, allignmentWeight, cohesionWeight, avoidanceWeight, centeringWeight, turnSpeed, moveSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < boidCount; i++)
        {
            newBoid = Instantiate(boidPrefab);
            newBoidComponent = newBoid.GetComponent<Boid>();
            newBoidComponent.swarm = this;
            newBoidComponent.normalDistance = normalDistance;
            newBoidComponent.closeDistance = closeDistance;
            newBoidComponent.allignmentWeight = allignmentWeight;
            newBoidComponent.cohesionWeight = cohesionWeight;
            newBoidComponent.avoidanceWeight = avoidanceWeight;
            newBoidComponent.centeringWeight = centeringWeight;
            newBoidComponent.turnSpeed = turnSpeed;
            newBoidComponent.moveSpeed = moveSpeed;
            newBoid.transform.rotation = Random.rotation;
            boids.Add(newBoid);
        }
    }

}

