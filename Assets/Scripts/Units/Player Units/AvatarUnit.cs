using System.Collections.Generic;
using UnityEngine;

public class AvatarUnit : BasePlayerUnit
{
    public override void ExecuteAttack()  // avatar's attacks are AoE
    {
        GameObject[] attackList = objectsInAttackRange.ToArray();
        foreach (GameObject gameObject in attackList)
        {
            if (gameObject.GetComponent<EnemyTower>())
            {
                Tower target = gameObject.GetComponent<EnemyTower>();
                AudioPlayer.instance.PlayAudio("Hit");
                target.TakeDamage(atk);
            }
            else if (gameObject.GetComponent<BaseEnemyUnit>())
            {
                BaseEnemyUnit target = gameObject.GetComponent<BaseEnemyUnit>();
                AudioPlayer.instance.PlayAudio("Hit");
                target.TakeDamage(atk);
            }
        }
    }


    public void InitializeStats(int maxhp, int atk, int def, int atkspd, int movespd)
    {
        this.maxhp = 100 + maxhp;
        hp = this.maxhp;
        this.atk = 5 + atk;
        this.def = 5 + def;
        this.atkspd = 5 + atkspd;
        this.movespd = 5 + movespd;
    }
}
