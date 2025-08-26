using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    public enum UnitType
    {
        biscuit, shortbread, jamBiscuit, crepe, cake, avatar,
        blackTea, herbalTea, bubbleTea, milkTea, matcha, failure
    };
    UnitType unitType;

    enum UnitStats { maxhp, hp, atk, def, movespd, atkspd };

    [SerializeField] protected int maxhp, hp, atk, def;
    [SerializeField] protected float movespd, atkspd;

    [SerializeField] protected float baseAtkCooldown;

    bool isMoving = false;

    protected enum UnitState { Moving, Attacking, Dead };
    [SerializeField] protected UnitState unitState;

    [SerializeField] protected List<GameObject> objectsInAttackRange = new();


    void Start()
    {
        StartCoroutine(ExecuteUnitBehavior());
        hp = maxhp;
    }


    void Update()
    {
        if (isMoving)
        {
            transform.position = new Vector2(transform.position.x + movespd / 4000, transform.position.y);
        }
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        objectsInAttackRange.Add(collision.gameObject);
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        objectsInAttackRange.Remove(collision.gameObject);
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
        IEnumerator nextCoroutine = null;
        float cooldown = 0.1f;

        if (unitState != UnitState.Dead)
        {
            if (objectsInAttackRange.Count > 0)
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

                nextCoroutine = ExecuteUnitBehavior();
                break;

            case UnitState.Attacking:
                // TODO: attack anim here (low priority)
                cooldown = baseAtkCooldown / atkspd;
                isMoving = false;

                ExecuteAttack();

                nextCoroutine = ExecuteUnitBehavior();
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

        yield return StartCoroutine(nextCoroutine);
    }

    public virtual void InitializeUnitStats()
    {
        maxhp = 1000;
        atk = 10;
        def = 10;
        atkspd = 5;
        movespd = 5;
    }

    public virtual void SetUnitStats(int maxhp, int atk, int def, int atkspd, int movespd)
    {
        this.maxhp = maxhp;
        this.atk = atk;
        this.def = def;
        this.atkspd = atkspd;
        this.movespd = movespd;
    }
}
