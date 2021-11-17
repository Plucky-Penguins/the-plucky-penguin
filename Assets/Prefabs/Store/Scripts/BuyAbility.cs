using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyAbility : MonoBehaviour
{
    [Header("Card Object")]
    public GameObject Card;
    public Text abilityName;

    [Header("Dialogue Object")]
    public GameObject BuyDialogue;
    public Text Prompt;

    private static AbilityInterface.IAbility selectedAbility;

    private const string PROMPT_PRE_TEXT = "Buy Ability: ";
    private const string PROMPT_POST_TEXT = "?";

    // To be added:
    // public Text FishRemaining

    public void SelectAbility() {
        Prompt.text = PROMPT_PRE_TEXT + abilityName.text + PROMPT_POST_TEXT;
        // print(Card.GetComponent<AbilityInterface.IAbility>().ToString());
        selectedAbility = Card.GetComponent<AbilityInterface.IAbility>();
        // print(selectedAbility.getName());
        if (!BuyDialogue.activeSelf) {
            BuyDialogue.SetActive(true);
        }
    }


    // Buying ability
    // Remove ability from available
    // AbilityManager add slot etc
    // Subtract and Update Total Fish
    // Leave Store
    public static void buyAbility()
    {
        if (selectedAbility != null) {
            print("Bought: " + selectedAbility.getName());
            AbilityStore.removeAbilityFromAvailableList(selectedAbility);
            Fish_Handler.total_fish -= selectedAbility.getCost();
        }
    }
}
