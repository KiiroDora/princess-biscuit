using UnityEngine;

public class Tower : MonoBehaviour
{
    public int hitPointsMax = 1000;
    public int hitPoints;
    [SerializeField] private HitPointBarBehavior hitPointBar;

    enum TowerState { Full, Damaged, VeryDamaged, Destroyed };
    [SerializeField] private TowerState towerState;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        hitPoints = hitPointsMax;       
    }


    public void TakeDamage(int damageTaken)
    {
        hitPoints = Mathf.Clamp(hitPoints - damageTaken, 0, int.MaxValue);  // clamp the reduced hitpoint to 0 if it goes negative
        hitPointBar.UpdateHitPointBar(this);

        // set the tower state according to current hp
        if (hitPoints <= 0)
        {
            if (towerState != TowerState.Destroyed)
            {
                towerState = TowerState.Destroyed;
                spriteRenderer.sprite = sprites[3];
            }

            if (this is EnemyTower)
            {
                GameController.instance.EndGame(isGameWon: true);
            }
            else if (this is PlayerTower)
            {
                GameController.instance.EndGame(isGameWon: false);
            }
        }
        else if (hitPoints <= hitPointsMax / 3)
        {
            if (towerState != TowerState.VeryDamaged)
            {
                towerState = TowerState.VeryDamaged;
                spriteRenderer.sprite = sprites[2];
            }
        }
        else if (hitPoints <= hitPointsMax * 2 / 3)
        {
            if (towerState != TowerState.Damaged)
            {
                towerState = TowerState.Damaged;
                spriteRenderer.sprite = sprites[1];
            }
        }
        else if (towerState != TowerState.Full)
        {
            towerState = TowerState.Full;
            spriteRenderer.sprite = sprites[0];
        }
    }
}
