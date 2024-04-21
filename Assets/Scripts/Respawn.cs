using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject player;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Se precionar a letra R dá respawn
        if (Input.GetKey(KeyCode.R))
        {
            RespawnPoint();
        }
    }

    void RespawnPoint()
    {
        transform.position = spawnPoint.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        spawnPoint.transform.position = other.transform.position;
    }
}
