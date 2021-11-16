using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public Transform healthbar_full;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updateHealth(int health, int maxhealth)
    {
        healthbar_full.localScale = new Vector3(((float)health/(float)maxhealth)*50, healthbar_full.localScale.y, healthbar_full.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
