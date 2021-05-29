using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class WorldChanger : MonoBehaviour
{
    public GameObject normal, parellel, player, volume;
    Vector3 playerVelocity;
    public PlayerToAnimator PA;

    Inputs inputs;
    // Start is called before the first frame update
    void Start()
    {
        inputs = new Inputs();
        inputs.Player.Enable();
        inputs.Player.leftClick.started += _ => changeWorld();
    }
    private void LateUpdate()
    {
        //if (Time.timeScale != 1)
        //{
        //    Time.timeScale = 0.01f;

        //    player.GetComponent<Rigidbody>().isKinematic = true;
        //    player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //}
    }

    void changeWorld()
    {
        StartCoroutine( worldPause());
    }
    IEnumerator worldPause()
    {
        PA.Invoke("World", 0);
        Time.timeScale = 0.000001f;

        normal.SetActive(!normal.active);
        parellel.SetActive(!parellel.active);

        yield return new WaitForSeconds(0.4f * Time.timeScale);

        Time.timeScale = 1;
        PA.Invoke("UnWorld", 0);

    }
    void timeSet()
    {
        Time.timeScale = 1;
    }
}
