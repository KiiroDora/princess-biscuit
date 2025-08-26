using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : Spawner
{
    enum IngredientType { flour, sugar, egg, butter, milk };

    int flourAmount, sugarAmount, eggAmount, butterAmount, milkAmount;

    int ingredientsAdded = 0;

    bool isSoulAdded = false;

    [SerializeField] private TextMeshProUGUI flourAmountText, sugarAmountText, eggAmountText, butterAmountText, milkAmountText, soulAmountText, bakeText;
    [SerializeField] private Button bakeButton;
    [SerializeField] private Button soulButton;
    [SerializeField] private Button[] ingredientButtons;

    void AddIngredient(IngredientType ingredientType)
    {
        if (ingredientsAdded < 5)
        {
            switch (ingredientType)
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

            if (ingredientsAdded >= 5)
            {
                bakeText.text = "BAKE!";
                bakeButton.interactable = true;
            }
            else
            {
                bakeText.text = ingredientsAdded + "/" + 5;
            }

            // TODO: start cooldown for the button pressed
        }

        else
        {
            foreach (Button button in ingredientButtons)
            {
                button.interactable = false;
            }
        }
    }

    void AddSoul()
    {
        isSoulAdded = true;
        soulAmountText.text = "x1";
        soulButton.interactable = false;
    }

    void Bake()
    {
        isSoulAdded = false;
        soulButton.interactable = true;
        bakeButton.interactable = false;
        foreach (Button button in ingredientButtons)
        {
            button.interactable = true;
        }

        BaseUnit.UnitType unitTypeToSpawn;

        // TODO: if soul is added make avatar

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

        BaseUnit spawnedUnit = SpawnUnit(unitTypeToSpawn);
        spawnedUnit.InitializeUnitStats();
    }
}
