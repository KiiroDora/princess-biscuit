using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyUnit : BaseUnit
{
    public override void ExecuteAttack()
    {
        GameObject targetGameObject = null;  // TODO: add targeting here according to collision
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
}
