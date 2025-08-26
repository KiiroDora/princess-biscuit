using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyUnit : BaseUnit
{
    public override void ExecuteAttack()
    {
        GameObject targetGameObject = objectsInAttackRange[0];
        if (targetGameObject.GetComponent<PlayerTower>())
        {
            Tower target = targetGameObject.GetComponent<PlayerTower>();
            target.TakeDamage(atk);
        }
        else if (targetGameObject.GetComponent<BasePlayerUnit>())
        {
            BasePlayerUnit target = targetGameObject.GetComponent<BasePlayerUnit>();
            target.TakeDamage(atk);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<EnemyTower>() && !collision.gameObject.GetComponent<BaseEnemyUnit>())
        {
            objectsInAttackRange.Add(collision.gameObject);
        }
    }

}
