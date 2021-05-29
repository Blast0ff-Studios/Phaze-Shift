using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.Equals(player))
        {
            GetComponentInParent<SaveController>().BroadcastMessage("die");

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
