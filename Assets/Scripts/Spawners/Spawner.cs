using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected BaseUnit biscuit, shortbread, jamBiscuit, crepe, cake, avatar, blackTea, herbalTea, bubbleTea, milkTea, matcha;
    [SerializeField] private GameObject enemyParent, playerParent;


    protected BaseUnit SpawnUnit(BaseUnit.UnitType unitType)
    {
        Transform parentTransform;

        BaseUnit unitToSpawn = unitType switch
        {
            // player unit types
            BaseUnit.UnitType.biscuit => biscuit,
            BaseUnit.UnitType.shortbread => shortbread,
            BaseUnit.UnitType.jamBiscuit => jamBiscuit,
            BaseUnit.UnitType.crepe => crepe,
            BaseUnit.UnitType.cake => cake,
            BaseUnit.UnitType.avatar => avatar,

            // enemy unit types
            BaseUnit.UnitType.blackTea => blackTea,
            BaseUnit.UnitType.herbalTea => herbalTea,
            BaseUnit.UnitType.bubbleTea => bubbleTea,
            BaseUnit.UnitType.milkTea => milkTea,
            BaseUnit.UnitType.matcha => matcha,
            _ => null
        };

        if (unitToSpawn != null)
        {

            if (this is EnemySpawner)
            {
                parentTransform = enemyParent.transform;
            }
            else
            {
                parentTransform = playerParent.transform;
            }

            return Instantiate(unitToSpawn, transform.position, Quaternion.identity, parentTransform);
        }
        else
        {
            return null;
            // TODO: failure indicator
        }
    }
}
