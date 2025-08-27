using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerUnit : BaseUnit
{
    public override void ExecuteAttack()
    {
        GameObject targetGameObject  = objectsInAttackRange[0];

        foreach (GameObject gameObject in objectsInAttackRange)
        {
            if (gameObject.GetComponent<EnemyTower>())  //prioritize towers
            {
                targetGameObject = gameObject;
            }
        }

        if (targetGameObject.GetComponent<EnemyTower>())
        {
            Tower target = targetGameObject.GetComponent<EnemyTower>();
            target.TakeDamage(atk);
        }
        else if (targetGameObject.GetComponent<BaseEnemyUnit>())
        {
            BaseEnemyUnit target = targetGameObject.GetComponent<BaseEnemyUnit>();
            target.TakeDamage(atk);
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
