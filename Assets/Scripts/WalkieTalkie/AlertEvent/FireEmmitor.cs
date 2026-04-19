using UnityEngine;

public class FireEmmitor : MonoBehaviour
{
    public GameObject fireObj;
    
    void Start()
    {
        InvokeRepeating("Activate", 5, 5);
    }

    public void Activate()
    {
        Instantiate(fireObj, 
            new Vector3(Random.Range(-500,500), transform.position.y, Random.Range(-500, 500))
            , Quaternion.identity);
    }
}
