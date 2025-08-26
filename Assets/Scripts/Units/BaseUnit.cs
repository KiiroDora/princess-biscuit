using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    enum UnitStats { maxhp, hp, atk, def, movespd, atkspd };

    [SerializeField] protected int maxhp, hp, atk, def;
    [SerializeField] protected float movespd, atkspd;

    [SerializeField] protected  float baseAtkCooldown;

    bool isMoving = true;

    protected enum UnitState { Moving, Attacking, Dead };
    [SerializeField] protected UnitState unitState;

    private Rigidbody2D rb2d;

    public bool collisionCheck = true;  // TODO: dummy variable, remove this once collision is implemented


    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (isMoving)
        {
            // TODO: movement via rb2d (scales with movespd)
        }
    }


    public void TakeDamage(int enemyAtk)
    {
        int damageTaken = Mathf.Clamp((int)((float)enemyAtk * 100 / (def + 100)), 1, int.MaxValue);  // damage calculation
        hp = Mathf.Clamp(hp - damageTaken, 0, int.MaxValue);

        if (hp <= 0)
        {
            // TODO: death anim here (low priority)
            unitState = UnitState.Dead;
        }
    }


    public virtual void ExecuteAttack()
    {
        
    }


    IEnumerator ExecuteUnitBehavior()
    {
        Coroutine nextCoroutine;
        float cooldown = 0;

        if (unitState != UnitState.Dead)
        {
            if (collisionCheck) // TODO: if Collision check is true (current if condition is a placeholder)
            {
                unitState = UnitState.Attacking;
            }
            else
            {
                unitState = UnitState.Moving;
            }
        }

        switch (unitState)
        {
            case UnitState.Moving:
                // TODO: walk anim here (low priority)
                isMoving = true;
                nextCoroutine = StartCoroutine(ExecuteUnitBehavior());
                break;

            case UnitState.Attacking:
                // TODO: attack anim here (low priority)
                cooldown = baseAtkCooldown / atkspd;

                ExecuteAttack();

                nextCoroutine = StartCoroutine(ExecuteUnitBehavior());
                break;

            case UnitState.Dead:
                nextCoroutine = null;
                Destroy(gameObject);
                break;

            default:
                nextCoroutine = null;
                break;
        }

        if (cooldown > 0)
        {
            yield return new WaitForSeconds(cooldown);
        }

        yield return nextCoroutine;
    }
}
