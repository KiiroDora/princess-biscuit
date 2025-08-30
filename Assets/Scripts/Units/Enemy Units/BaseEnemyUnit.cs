using UnityEngine;

public class BaseEnemyUnit : BaseUnit
{
    protected virtual int ingredientLootAmount { get; set; }

    public override void ExecuteAttack()
    {
        GameObject targetGameObject;
        GameObject detectedTower = null;
        // GameObject detectedAvatar = null;

        foreach (GameObject gameObject in objectsInAttackRange)
        {
            if (gameObject.GetComponent<PlayerTower>())
            {
                detectedTower = gameObject;
            }
            // else if (gameObject.GetComponent<AvatarUnit>())
            // {
            //     detectedAvatar = gameObject;
            // }
        }

        if (detectedTower != null)  // towers are #1 priority
        {
            targetGameObject = detectedTower;
        }
        // else if (detectedAvatar != null)  // avatar is #2 priority
        // {
        //     targetGameObject = detectedAvatar;
        // }
        else
        {
            targetGameObject = objectsInAttackRange[0];  // otherwise attack whoever you see first
        }

        if (!GameController.isGamePaused)
        {
            if (targetGameObject.GetComponent<PlayerTower>())
            {
                Tower target = targetGameObject.GetComponent<PlayerTower>();
                AudioPlayer.instance.PlayAudio("Hit");
                target.TakeDamage(atk);
            }
            else if (targetGameObject.GetComponent<BasePlayerUnit>())
            {
                BasePlayerUnit target = targetGameObject.GetComponent<BasePlayerUnit>();
                AudioPlayer.instance.PlayAudio("Hit");
                target.TakeDamage(atk);
            }
        }
        else
        {
            StopAllCoroutines();
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<EnemyTower>() && !collision.gameObject.GetComponent<BaseEnemyUnit>())
        {
            objectsInAttackRange.Add(collision.gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        // inheriting classes must override ingredientLootAmount and override this method to use that field instead
        PlayerSpawner.instance.IncreaseIngredientCountByAmount(1);
    }
}
