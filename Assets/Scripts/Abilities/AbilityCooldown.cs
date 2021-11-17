using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityCooldown : MonoBehaviour
{
    public Sprite icon;
    public TextMeshProUGUI cooldownText;
    public int slotNumber;

    // private AbilityManager abilityManager;

    // Start is called before the first frame update
    void Start()
    {
         // abilityManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AbilityManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.abilities[slotNumber] != null) {
            float cooldown = Controller.abilities[slotNumber].getCurrentCooldown();
            if (cooldown <= 0) {
                cooldownText.color = Color.black;
                cooldownText.text = Controller.slotToKey[slotNumber];
            } else {
                cooldownText.color = Color.red;
                cooldownText.text = System.Math.Truncate(cooldown + 1).ToString();
            }
        }
    }
}
