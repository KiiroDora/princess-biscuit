using UnityEngine;

public class BasePlayerUnit : BaseUnit
{
    public override void ExecuteAttack()
    {
        GameObject targetGameObject = objectsInAttackRange[0];
        GameObject detectedTower = null;

        foreach (GameObject gameObject in objectsInAttackRange)
        {
            if (gameObject.GetComponent<EnemyTower>())  //prioritize towers
            {
                detectedTower = gameObject;
            }
        }
        
        if (detectedTower != null)  // towers are #1 priority
        {
            targetGameObject = detectedTower;
        }

        else
        {
            int largestMaxHP = 0;
            foreach (GameObject gameObject in objectsInAttackRange)
            {
                if (gameObject.GetComponent<BaseUnit>().GetMaxHP() > largestMaxHP)
                {
                    targetGameObject = gameObject;
                    largestMaxHP = targetGameObject.GetComponent<BaseUnit>().GetMaxHP();
                }
            }
        }

        if (!GameController.isGamePaused)
        {
            if (targetGameObject.GetComponent<EnemyTower>())
            {
                Tower target = targetGameObject.GetComponent<EnemyTower>();
                AudioPlayer.instance.PlayAudio("Hit");
                target.TakeDamage(atk);
            }
            else if (targetGameObject.GetComponent<BaseEnemyUnit>())
            {
                BaseEnemyUnit target = targetGameObject.GetComponent<BaseEnemyUnit>();
                AudioPlayer.instance.PlayAudio("Hit");
                target.TakeDamage(atk);
            }
        }
    }
    
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<PlayerTower>() && !collision.gameObject.GetComponent<BasePlayerUnit>())
        {
            objectsInAttackRange.Add(collision.gameObject);
        }
    }
}
