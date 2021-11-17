using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterLeaveStore : MonoBehaviour
{
    [Header("UpdateText")]
    public Text fish;
    
    // Start is called before the first frame update
    void Start()
    {
        // uncomment for testing with stocked fish
        //Fish_Handler.total_fish += 40;

        fish.text = (Fish_Handler.total_fish.ToString() + " Fish");
    }

}
