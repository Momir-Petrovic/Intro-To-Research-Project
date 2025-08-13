using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Agent agent = other.GetComponent<Agent>();
        if (agent != null)
        {
            agent.health -= agent.damage;
            Destroy(gameObject);
        }
    }
}
