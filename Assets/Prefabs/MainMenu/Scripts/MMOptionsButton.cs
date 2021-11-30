using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMOptionsButton : MonoBehaviour
{
    public container ControlsContainer;
    public container OptionsContainer;

    public void Start()
    {
        if (OptionsContainer.isActive)
        {
            OptionsContainer.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            OptionsContainer.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void OpenCloseOptions()
    {
        OptionsContainer.isActive = !OptionsContainer.isActive;
        if (OptionsContainer.isActive)
        {
            if (ControlsContainer.isActive)
            {
                ControlsContainer.isActive = false;
                ControlsContainer.transform.localScale = new Vector3(0, 0, 0);
            }
            OptionsContainer.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            OptionsContainer.transform.localScale = new Vector3(0, 0, 0);
        }

        
    }

}