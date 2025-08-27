using UnityEngine;

public class GameController : MonoBehaviour
{
    void Start()
    {
        PlayerSpawner.instance.InvokeRepeating(nameof(PlayerSpawner.instance.IncreaseIngredientCount), 5f, 5f);
    }

}
