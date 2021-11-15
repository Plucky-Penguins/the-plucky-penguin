using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MMOptionsButton : MonoBehaviour
{
    public GameObject OptionsContainer;

    public void OpenCloseOptions() {
        OptionsContainer.SetActive(!OptionsContainer.activeSelf);
    }
}