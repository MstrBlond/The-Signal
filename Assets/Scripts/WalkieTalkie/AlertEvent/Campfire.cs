using UnityEngine;

public class Campfire : MonoBehaviour, IDanger
{
    public GameObject _obj;

    private void Start()
    {
        GetComponent<Collider>().enabled = false;
    }
    public void Activate()
    {
        GetComponent<Collider>().enabled = true;
        _obj.SetActive(true);
    }
}
