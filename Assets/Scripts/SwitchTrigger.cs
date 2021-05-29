using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTrigger : MonoBehaviour
{
    Inputs inputs;
    public bool PlayerNear;
    bool WasPlayerNear;

    public GameObject Text;
    public GameObject VisualSwitch;
    public string TextToAdd;

    public GameObject worldSwitchBelongsTo;
    

    public GameObject[] objectsToBeDisabled;
    public GameObject[] objectsToBeEnabled;


    // Start is called before the first frame update
    void Start()
    {
        inputs = new Inputs();
        inputs.Player.Enable();
        inputs.Player.E.started += _ => switchObjects();
    }

    void Update()
    {
        if (!worldSwitchBelongsTo.activeInHierarchy)
        {
            PlayerNear = false;
        }

        if (PlayerNear)
        {
            Text.SetActive(true);
            Text.GetComponentInChildren<Text>().text = TextToAdd;
        } else if (!PlayerNear && WasPlayerNear)
        {
            Text.SetActive(false);
            Text.GetComponentInChildren<Text>().text = "";
        }
    }
    private void LateUpdate()
    {
        WasPlayerNear = PlayerNear;

            VisualSwitch.SetActive(worldSwitchBelongsTo.activeInHierarchy);


    }
    void switchObjects()
    {
        if (PlayerNear && worldSwitchBelongsTo.activeInHierarchy)
        {
            for(int i = 0; i < objectsToBeDisabled.Length; i++)
            {
                objectsToBeDisabled[i].SetActive(!objectsToBeDisabled[i].activeInHierarchy);
            }
            for (int i = 0; i < objectsToBeEnabled.Length; i++)
            {
                objectsToBeEnabled[i].SetActive(!objectsToBeEnabled[i].activeInHierarchy);
            }
        }
    }

}
