using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchStep1 : MonoBehaviour
{
    public bool PlayerNear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInParent<SwitchTrigger>().PlayerNear = PlayerNear;
    }
}
