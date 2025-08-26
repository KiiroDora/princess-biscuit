using UnityEngine;

public class Tower : MonoBehaviour
{
    private int hitPoints;
    [SerializeField] private int hitPointsMax = 1000;

    enum TowerState { Full, Damaged, VeryDamaged, Destroyed };
    [SerializeField] private TowerState towerState;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;


    void Start()
    {
       spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); 
    }


    public void TakeDamage(int damageTaken)
    {
        hitPoints = Mathf.Clamp(hitPoints - damageTaken, 0, int.MaxValue);  // clamp the reduced hitpoint to 0 if it goes negative

        // set the tower state according to current hp
        if (hitPoints <= 0 && towerState != TowerState.Destroyed)
        {
            towerState = TowerState.Destroyed;
            spriteRenderer.sprite = sprites[3];

            if (this is PlayerTower)
            {
                Debug.Log("player win");
                // TODO: add win screen etc.
            }
            else if (this is EnemyTower)
            {
                Debug.Log("game over");
                // TODO: add gameover screen etc.
            }
        }
        else if (hitPoints <= hitPointsMax / 3 && towerState != TowerState.VeryDamaged)
        {
            towerState = TowerState.VeryDamaged;
            spriteRenderer.sprite = sprites[2];
        }
        else if (hitPoints <= hitPointsMax * 2 / 3 && towerState != TowerState.Damaged)
        {
            towerState = TowerState.Damaged;
            spriteRenderer.sprite = sprites[1];
        }
        else if (towerState != TowerState.Full)
        {
            towerState = TowerState.Full;
            spriteRenderer.sprite = sprites[0];
        }
    }
}
