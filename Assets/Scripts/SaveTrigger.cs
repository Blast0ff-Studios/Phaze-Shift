using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    public Collider player;
    public Vector3 SavePoint;
    public bool commonOrParallel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(player))
        {
            GetComponentInParent<SaveController>().savePoint = SavePoint;
            GetComponentInParent<SaveController>().commonOrParallel = commonOrParallel;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.Equals(player))
        {
            GetComponentInParent<SaveController>();
        }
    }
}
