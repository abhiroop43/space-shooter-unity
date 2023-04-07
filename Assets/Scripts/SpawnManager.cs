using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;

    [SerializeField] private GameObject[] powerups;


    [SerializeField] private GameObject _enemyContainer;

    private bool _stopSpawning = false;


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    // spawn game assets every 5 seconds
    // spawn enemy ships
    private IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            var xPosition = Random.Range(-8f, 8f);
            var positionToSpawn = new Vector3(xPosition, 7, 0);
            var newEnemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
    }

    // spawn powerup every 30-45 seconds
    private IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            var xPosition = Random.Range(-8f, 8f);
            var randomPowerup = Random.Range(0, 3);
            var positionToSpawn = new Vector3(xPosition, 7, 0);
            Instantiate(powerups[randomPowerup], positionToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10.0f, 15.0f));
        }
    }


    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}