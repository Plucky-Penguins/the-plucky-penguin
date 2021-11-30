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

    [Header("Other Text")]
    public Text Leave;

    private static List<AbilityInterface.IAbility> availableAbilityList;
    private List<AbilityInterface.IAbility> randomSelectedAbilityList;

    private const string CARD_COST = "Cost: ";
    private const string FISH = " Fish";
    private const string NOT_ENOUGH = "Not enough fish...\nLeave to next level...";

    private void Start()
    {
        setUp();
        // Uncomment to test remove ability before loading the shop
        /*removeAbilityFromAvailableList(GetComponent<BombAbility>());*/
        selectRandomAbilitiesAndPopulateText();

        // Uncomment to test remove ability after loading the shop
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
        else
        {
            for (int i = 0; i < availableAbilityList.Count; i++)
            {
                foreach (AbilityInterface.IAbility y in Controller.abilities)
                {
                    if (availableAbilityList[i] != null && y != null)
                    {
                        if (availableAbilityList[i].getName() == y.getName())
                        {
                            availableAbilityList[i] = null;
                        }
                    }

                }
            }
            availableAbilityList.RemoveAll(item => item == null);
        }
        
    }

    private void selectRandomAbilitiesAndPopulateText() {
        randomSelectedAbilityList = new List<AbilityInterface.IAbility>();

        HashSet<int> setOfIndexs = new HashSet<int>();
        for (int i = 0; setOfIndexs.Count < 3; i++) {
            int randomIndex = Random.Range(0, availableAbilityList.Count);
            setOfIndexs.Add(randomIndex);
        }

        int[] intIndexArray = new int[] { 0, 0, 0 };
        setOfIndexs.CopyTo(intIndexArray, 0, 3);

        for (int i = 0; i < 3; i++)
        {
            randomSelectedAbilityList.Add(null);
            randomSelectedAbilityList[i] = availableAbilityList[intIndexArray[i]];
        }


        // Ability 1
        System.Type typeAbility1 = randomSelectedAbilityList[0].GetType();
        abilityCard1.AddComponent(typeAbility1);
        abilityName1.text = randomSelectedAbilityList[0].getName();
        abilityCost1.text = CARD_COST + randomSelectedAbilityList[0].getCost().ToString() + FISH;
        abilityDescription1.text = randomSelectedAbilityList[0].getDescription();

        // Ability 2
        System.Type typeAbility2 = randomSelectedAbilityList[1].GetType();
        abilityCard2.AddComponent(typeAbility2);
        abilityName2.text = randomSelectedAbilityList[1].getName();
        abilityCost2.text = CARD_COST + randomSelectedAbilityList[1].getCost().ToString() + FISH;
        abilityDescription2.text = randomSelectedAbilityList[1].getDescription();

        // Ability 3
        System.Type typeAbility3 = randomSelectedAbilityList[2].GetType();
        abilityCard3.AddComponent(typeAbility3);
        abilityName3.text = randomSelectedAbilityList[2].getName();
        abilityCost3.text = CARD_COST + randomSelectedAbilityList[2].getCost().ToString() + FISH;
        abilityDescription3.text = randomSelectedAbilityList[2].getDescription();


        // DISABLE BUTTONS IF TOTAL FISH IS NOT ENOUGH TO BUY
        if (Fish_Handler.total_fish < randomSelectedAbilityList[0].getCost())
        {
            abilityCard1.GetComponent<Button>().interactable = false;
        }
        if (Fish_Handler.total_fish < randomSelectedAbilityList[1].getCost())
        {
            abilityCard2.GetComponent<Button>().interactable = false;
        }
        if (Fish_Handler.total_fish < randomSelectedAbilityList[2].getCost())
        {
            abilityCard3.GetComponent<Button>().interactable = false;
        }

        if (Fish_Handler.total_fish < randomSelectedAbilityList[0].getCost() &&
            Fish_Handler.total_fish < randomSelectedAbilityList[1].getCost() &&
            Fish_Handler.total_fish < randomSelectedAbilityList[2].getCost()) {
            Leave.text = NOT_ENOUGH;
        }
        

    }

    public static void removeAbilityFromAvailableList(AbilityInterface.IAbility ability) {
        availableAbilityList.Remove(ability);
    }
}
