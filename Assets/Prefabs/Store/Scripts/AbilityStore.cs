using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/***
 * When a new ability is added.
 * The new ability will require for:
 * - a new slot to be added to the available ability list
 * - script must be dragged into the canvas as a component
 * - added to the availability list in the code
 */
public class AbilityStore : MonoBehaviour
{
    [Header("Ability 1")]
    public GameObject abilityCard1;
    public Text abilityName1;
    public Text abilityCost1;
    public Text abilityDescription1;

    [Header("Ability 2")]
    public GameObject abilityCard2;
    public Text abilityName2;
    public Text abilityCost2;
    public Text abilityDescription2;

    [Header("Ability 3")]
    public GameObject abilityCard3;
    public Text abilityName3;
    public Text abilityCost3;
    public Text abilityDescription3;

    private static List<AbilityInterface.IAbility> availableAbilityList;
    private List<AbilityInterface.IAbility> selectedAbility;

    private const string CARD_COST = "Cost: ";
    private const string FISH = " Fish";

    private void Start()
    {
        setUp();
        selectRandomAbilitiesAndPopulateText();
        /*removeAbilityFromAvailableList(GetComponent<BombAbility>());*/
    }

    private void setUp() {
        if (availableAbilityList == null) {
            availableAbilityList = new List<AbilityInterface.IAbility>();
            for (int i = 0; i < 5; i++) {
                availableAbilityList.Add(null);
            }
            availableAbilityList[0] = GetComponent<BombAbility>();
            availableAbilityList[1] = GetComponent<ShieldAbility>();
            availableAbilityList[2] = GetComponent<SpeedAbility>();
            availableAbilityList[3] = GetComponent<BurstAbility>();
            availableAbilityList[4] = GetComponent<ProjectileAbility>();
        }
    }

    private void selectRandomAbilitiesAndPopulateText() {
        selectedAbility = new List<AbilityInterface.IAbility>();

        HashSet<int> setOfIndexs = new HashSet<int>();
        for (int i = 0; setOfIndexs.Count < 3; i++) {
            int randomIndex = Random.Range(0, availableAbilityList.Count);
            setOfIndexs.Add(randomIndex);
        }

        int[] intIndexArray = new int[] { 0, 0, 0 };
        setOfIndexs.CopyTo(intIndexArray, 0, 3);
        /*print(intIndexArray[0].ToString());
        print(intIndexArray[1].ToString());
        print(intIndexArray[2].ToString());*/

        for (int i = 0; i < 3; i++)
        {
            selectedAbility.Add(null);
            selectedAbility[i] = availableAbilityList[intIndexArray[i]];
            /*print(selectedAbility[i].getName());*/
        }


        // Ability 1
        System.Type typeAbility1 = selectedAbility[0].GetType();
        abilityCard1.AddComponent(typeAbility1);
        abilityName1.text = selectedAbility[0].getName();
        abilityCost1.text = CARD_COST + selectedAbility[0].getCost().ToString() + FISH;
        abilityDescription1.text = selectedAbility[0].getDescription();

        // Ability 2
        System.Type typeAbility2 = selectedAbility[1].GetType();
        abilityCard2.AddComponent(typeAbility2);
        abilityName2.text = selectedAbility[1].getName();
        abilityCost2.text = CARD_COST + selectedAbility[1].getCost().ToString() + FISH;
        abilityDescription2.text = selectedAbility[1].getDescription();

        // Ability 3
        System.Type typeAbility3 = selectedAbility[2].GetType();
        abilityCard3.AddComponent(typeAbility3);
        abilityName3.text = selectedAbility[2].getName();
        abilityCost3.text = CARD_COST + selectedAbility[2].getCost().ToString() + FISH;
        abilityDescription3.text = selectedAbility[2].getDescription();
    }

    public void removeAbilityFromAvailableList(AbilityInterface.IAbility ability) {
        availableAbilityList.Remove(ability);
        /*print("Removed: " + ability.getName());
        for (int i = 0; i < availableAbilityList.Count; i++) {
            print(availableAbilityList[i].getName());
        }*/
    }

    // Buying ability
    // Remove ability from available
    // AbilityManager add slot etc
    // Subtract and Update Total Fish
    // Leave Store
}
