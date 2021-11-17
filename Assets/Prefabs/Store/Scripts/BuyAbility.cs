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

    public static AbilityInterface.IAbility selectedAbility;

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
    public void deselectAbility()
    {
        selectedAbility = null;
    }

    // Buying ability
    // Remove ability from available
    // AbilityManager add slot etc
    // Subtract and Update Total Fish
    // Leave Store
    public static void buyAbility()
    {
        foreach (AbilityInterface.IAbility ability in Controller.abilities) {
            int i = 0;
            print("Listing: " + i + " " + ability);
            i++;
        }

        if (selectedAbility != null) {
            print("Bought: " + selectedAbility.getName());
            AbilityStore.removeAbilityFromAvailableList(selectedAbility);
            Fish_Handler.total_fish -= selectedAbility.getCost();
            // AbilityManager.abilities.Add(selectedAbility);
            for (int i = 0; i < Controller.abilities.Count; i++)
            {
                print("Check:" + Controller.abilities[i]);
                if (Controller.abilities[i] == null)
                {
                    Controller.abilities[i] = selectedAbility;
                    //AbilityManager.addIndexAbility(i, selectedAbility.getName());
                    break;
                }
            }
        }
    }
}
