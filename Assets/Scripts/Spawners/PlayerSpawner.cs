using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : Spawner
{
    public enum IngredientType { flour, sugar, egg, butter, milk };

    public static int flourAmount, sugarAmount, eggAmount, butterAmount, milkAmount;

    public static float ingredientCount = 10; // this increases by time OR when enemies are killed 

    public static int ingredientsAdded = 0;

    public static bool isSoulAdded = false;

    [SerializeField] private TextMeshProUGUI flourAmountText, sugarAmountText, eggAmountText, butterAmountText, milkAmountText, bakeText, ingredientText;
    [SerializeField] private Button bakeButton;
    [SerializeField] private Button soulButton;
    [SerializeField] private Button[] ingredientButtons;

    [SerializeField] private Color regularIngredientButtonColor, soulIngredientButtonColor;

    public static PlayerSpawner instance;


    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        ingredientText.text = ((int)ingredientCount).ToString();
        isSoulAdded = false;
        soulButton.interactable = true;
        bakeButton.interactable = false;
        foreach (Button button in ingredientButtons)
        {
            button.interactable = true;
        }
        flourAmount = 0;
        sugarAmount = 0;
        eggAmount = 0;
        butterAmount = 0;
        milkAmount = 0;
        flourAmountText.text = "x" + flourAmount;
        sugarAmountText.text = "x" + sugarAmount;
        eggAmountText.text = "x" + eggAmount;
        butterAmountText.text = "x" + butterAmount;
        milkAmountText.text = "x" + milkAmount;
    }


    public void IncreaseIngredientCount()
    {
        ingredientCount += 1 + 0.1f * GameController.EnemiesSlayed;
        ingredientText.text = ingredientText.text = ((int)ingredientCount).ToString();
    }


    public void IncreaseIngredientCountByAmount(int amount)
    {
        ingredientCount += amount;
        ingredientText.text = ingredientText.text = ((int)ingredientCount).ToString();
    }


    public void AddIngredient(int ingredientType)
    {
        if (ingredientCount > 0)
        {
            if (ingredientsAdded < 5)
            {
                switch ((IngredientType)ingredientType)
                {
                    case IngredientType.flour:
                        flourAmount++;
                        flourAmountText.text = "x" + flourAmount;
                        break;
                    case IngredientType.sugar:
                        sugarAmount++;
                        sugarAmountText.text = "x" + sugarAmount;
                        break;
                    case IngredientType.egg:
                        eggAmount++;
                        eggAmountText.text = "x" + eggAmount;
                        break;
                    case IngredientType.butter:
                        butterAmount++;
                        butterAmountText.text = "x" + butterAmount;
                        break;
                    case IngredientType.milk:
                        milkAmount++;
                        milkAmountText.text = "x" + milkAmount;
                        break;
                }

                ingredientsAdded++;
                ingredientCount--;
                ingredientText.text = ((int)ingredientCount).ToString();

                if (ingredientsAdded >= 5)
                {
                    foreach (Button button in ingredientButtons)
                    {
                        button.interactable = false;
                    }
                    bakeText.text = "BAKE!";
                    bakeButton.interactable = true;
                }
                else
                {
                    bakeText.text = ingredientsAdded + "/" + 5;
                }
            }
        }
    }


    public void ToggleSoul()
    {
        if (isSoulAdded)
        {
            soulButton.GetComponentInChildren<TextMeshProUGUI>().text = "ADD SOUL";
            AudioPlayer.instance.PlayAudio("Soul Off");
            foreach (Button button in ingredientButtons)
            {
                button.GetComponent<Image>().color = regularIngredientButtonColor;
            }
        }
        else
        {
            soulButton.GetComponentInChildren<TextMeshProUGUI>().text = "REMOVE";
            AudioPlayer.instance.PlayAudio("Soul On");
            foreach (Button button in ingredientButtons)
            {
                button.GetComponent<Image>().color = soulIngredientButtonColor;
            }
        }

        isSoulAdded = !isSoulAdded;
    }


    public void Bake()
    {
        BaseUnit.UnitType unitTypeToSpawn;

        if (isSoulAdded)
        {
            unitTypeToSpawn = BaseUnit.UnitType.avatar;
            soulButton.interactable = false;
            soulButton.GetComponentInChildren<TextMeshProUGUI>().text = "CONJURED";
        }

        else
        {
            if (flourAmount == 1 && sugarAmount == 1 && eggAmount == 1 && butterAmount == 1 && milkAmount == 1)  // biscuit
            {
                unitTypeToSpawn = BaseUnit.UnitType.biscuit;
            }
            else if (flourAmount == 1 && sugarAmount == 0 && eggAmount == 2 && butterAmount == 0 && milkAmount == 2)  // crepe 
            {
                unitTypeToSpawn = BaseUnit.UnitType.crepe;
            }
            else if (flourAmount == 2 && sugarAmount == 2 && eggAmount == 0 && butterAmount == 1 && milkAmount == 0) // shortbread 
            {
                unitTypeToSpawn = BaseUnit.UnitType.shortbread;
            }
            else if (flourAmount == 1 && sugarAmount == 1 && eggAmount == 1 && butterAmount == 2 && milkAmount == 0) // jam biscuit
            {
                unitTypeToSpawn = BaseUnit.UnitType.jamBiscuit;
            }
            else
            {
                unitTypeToSpawn = BaseUnit.UnitType.failure;
            }
        }

        BaseUnit spawnedUnit = SpawnUnit(unitTypeToSpawn);

        if (spawnedUnit is AvatarUnit avatarUnit)  // determine avatarunit stats
        {
            avatarUnit.InitializeStats(maxhp: milkAmount * 100, atk: flourAmount * 10, def: eggAmount * 10, atkspd: sugarAmount * 10, movespd: butterAmount * 10);
        }

        // resetting everything
        bakeButton.interactable = false;
        foreach (Button button in ingredientButtons)
        {
            button.interactable = !isSoulAdded;  // if soul is added disable all buttons
        }
        foreach (Button button in ingredientButtons)
        {
            button.GetComponent<Image>().color = regularIngredientButtonColor;
        }
        ingredientsAdded = 0;
        flourAmount = 0;
        sugarAmount = 0;
        eggAmount = 0;
        butterAmount = 0;
        milkAmount = 0;
        flourAmountText.text = "x" + flourAmount;
        sugarAmountText.text = "x" + sugarAmount;
        eggAmountText.text = "x" + eggAmount;
        butterAmountText.text = "x" + butterAmount;
        milkAmountText.text = "x" + milkAmount;
        StartCoroutine(BakingResultTextChange(unitTypeToSpawn));
    }

    IEnumerator BakingResultTextChange(BaseUnit.UnitType unitType)
    {
        if (unitType != BaseUnit.UnitType.failure)
        {
            bakeText.text = "DONE!";
        }
        else
        {
            bakeText.text = "FAIL";
        }
        yield return new WaitForSeconds(1f);
        bakeText.text = ingredientsAdded + "/5";
    }
}
