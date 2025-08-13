using UnityEngine;

public class Billboarding : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;       
    }
}
