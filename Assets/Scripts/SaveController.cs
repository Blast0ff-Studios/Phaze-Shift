using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    public GameObject player;
    public Vector3 savePoint;
    public bool commonOrParallel;

    public GameObject parallel, normal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void die()
    {
        player.transform.position = savePoint;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

        normal.SetActive(commonOrParallel);
        parallel.SetActive(!commonOrParallel);
    }
}
