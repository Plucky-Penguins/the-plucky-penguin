using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMControlsButton : MonoBehaviour
{
    public container ControlsContainer;
    public container OptionsContainer;

    public void Start()
    {
        if (ControlsContainer.isActive)
        {
            ControlsContainer.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            ControlsContainer.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void OpenCloseOptions()
    {
        ControlsContainer.isActive = !ControlsContainer.isActive;
        if (ControlsContainer.isActive)
        {
            if (OptionsContainer.isActive)
            {
                OptionsContainer.isActive = false;
                OptionsContainer.transform.localScale = new Vector3(0, 0, 0);
            }
            ControlsContainer.transform.localScale = new Vector3(1, 1, 1);
        } else
        {
            ControlsContainer.transform.localScale = new Vector3(0, 0, 0);
        }
    }
    
}