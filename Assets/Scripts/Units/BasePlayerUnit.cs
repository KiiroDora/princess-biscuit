using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerUnit : BaseUnit
{
    public override void ExecuteAttack()
    {
        GameObject targetGameObject = null;  // TODO: add targeting here according to collision
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
}
