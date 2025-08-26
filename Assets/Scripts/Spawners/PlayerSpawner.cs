using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : Spawner
{
    public enum IngredientType { flour, sugar, egg, butter, milk };

    int flourAmount, sugarAmount, eggAmount, butterAmount, milkAmount;

    int ingredientCount = 10; // TODO: this increases by time OR when enemies are killed 

    int ingredientsAdded = 0;

    bool isSoulAdded = false;

    [SerializeField] private TextMeshProUGUI flourAmountText, sugarAmountText, eggAmountText, butterAmountText, milkAmountText, soulAmountText, bakeText, ingredientText;
    [SerializeField] private Button bakeButton;
    [SerializeField] private Button soulButton;
    [SerializeField] private Button[] ingredientButtons;


    void Start()
    {
        ingredientText.text = "Ingredients: " + ingredientCount;
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


    public void AddIngredient(int ingredientType)
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
            ingredientText.text = "Ingredients: " + ingredientCount;

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

    public void AddSoul()
    {
        isSoulAdded = true;
        soulAmountText.text = "x1";
        soulButton.interactable = false;
    }

    public void Bake()
    {
        BaseUnit.UnitType unitTypeToSpawn;

        if (isSoulAdded)
        {
            // TODO: if soul is added make avatar instead
            unitTypeToSpawn = BaseUnit.UnitType.avatar;
        }

        else
        {
            if (flourAmount == 2 && sugarAmount == 1 && eggAmount == 1 && butterAmount == 1 && milkAmount == 0)  // biscuit
            {
                unitTypeToSpawn = BaseUnit.UnitType.biscuit;
            }
            else if (flourAmount == 1 && sugarAmount == 1 && eggAmount == 1 && butterAmount == 0 && milkAmount == 2)  // crepe 
            {
                unitTypeToSpawn = BaseUnit.UnitType.crepe;
            }
            else if (flourAmount == 2 && sugarAmount == 2 && eggAmount == 0 && butterAmount == 1 && milkAmount == 0) // shortbread 
            {
                unitTypeToSpawn = BaseUnit.UnitType.shortbread;
            }
            else if (flourAmount == 1 && sugarAmount == 2 && eggAmount == 1 && butterAmount == 1 && milkAmount == 0) // jam biscuit
            {
                unitTypeToSpawn = BaseUnit.UnitType.jamBiscuit;
            }
            else if (flourAmount == 1 && sugarAmount == 1 && eggAmount == 1 && butterAmount == 1 && milkAmount == 1) // cake
            {
                unitTypeToSpawn = BaseUnit.UnitType.cake;
            }
            else
            {
                unitTypeToSpawn = BaseUnit.UnitType.failure;
            }
        }

        BaseUnit spawnedUnit = SpawnUnit(unitTypeToSpawn);
        if (unitTypeToSpawn != BaseUnit.UnitType.failure) { spawnedUnit.InitializeUnitStats(); }

        // resetting everything
        isSoulAdded = false;
        soulButton.interactable = true;
        bakeButton.interactable = false;
        foreach (Button button in ingredientButtons)
        {
            button.interactable = true;
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
    }
}
