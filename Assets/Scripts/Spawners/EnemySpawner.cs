using System.Collections;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [System.Serializable]
    class Wave
    {
        public BaseUnit.UnitType[] unitTypes;
        public float waveCooldown;
        public float spawnCooldown;
    }

    [SerializeField] Wave[] waves;

    [SerializeField] private bool isEndless = false;


    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (GameController.isGamePaused)
        {
            yield return null;
        }

        foreach (Wave wave in waves)  // for each wave
        {
            yield return new WaitForSeconds(wave.waveCooldown);  // wait for the interval to end
            foreach (BaseUnit.UnitType unitType in wave.unitTypes)  // spawn all enemies in the wave waiting inbetween
            {
                SpawnUnit(unitType);
                yield return new WaitForSeconds(wave.spawnCooldown);
            }
        }

        if (isEndless)
        {
            yield return StartCoroutine(SpawnEnemies());
        }
    }
}
