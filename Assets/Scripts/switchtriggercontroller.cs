using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchtriggercontroller : MonoBehaviour
{
    public Collider player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.Equals(player))
        {
            GetComponentInParent<SwitchTrigger>().PlayerNear = true;

        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.Equals(player))
        {
            GetComponentInParent<SwitchTrigger>().PlayerNear = false;
        }
    }
    void onPlayerGone()
    {
        GetComponentInParent<SwitchTrigger>().PlayerNear = false;
    }
}
