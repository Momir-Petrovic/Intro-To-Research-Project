using UnityEngine;

public class Boid : MonoBehaviour
{
    public Swarm swarm;
    public float normalDistance, closeDistance, allignmentWeight, cohesionWeight, avoidanceWeight, centeringWeight, turnSpeed, moveSpeed;
    private int count;
    private Vector3 allignment, cohesion, avoidance, myDirection;


    // Update is called once per frame
    void Update()
    {
        allignment = Vector3.zero;
        cohesion = Vector3.zero;
        avoidance = Vector3.zero;
        count = 0;
        foreach(GameObject boid in swarm.boids)
        {
            if(Vector3.Distance(transform.position, boid.transform.position) <= normalDistance)
            {
                count++;
                cohesion += boid.transform.position;
                allignment += boid.transform.forward;
                if (Vector3.Distance(transform.position, boid.transform.position) <= closeDistance)
                {
                    avoidance += transform.position - boid.transform.position;
                }
            }
        }
        allignment = allignment.normalized;
        cohesion = cohesion/count;
        avoidance = avoidance.normalized;
        myDirection = allignmentWeight * allignment + cohesionWeight * cohesion + avoidanceWeight * avoidance - centeringWeight * transform.position;
        myDirection = Vector3.RotateTowards(transform.forward, myDirection, turnSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(myDirection);
        transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime;
    }
}
