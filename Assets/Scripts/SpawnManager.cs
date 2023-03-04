using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  [SerializeField]
  private GameObject _enemyPrefab;

  [SerializeField]
  private GameObject _enemyContainer;

  private bool _stopSpawning = false;

  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(SpawnEnemyRoutine());
  }

  // Update is called once per frame
  void Update()
  {

  }

  // spawn game assets every 5 seconds
  // spawn enemy ships
  IEnumerator SpawnEnemyRoutine()
  {
    while (_stopSpawning == false)
    {
      float xPosition = Random.Range(-8f, 8f);
      Vector3 positionToSpawn = new Vector3(xPosition, 7, 0);
      GameObject newEnemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
      newEnemy.transform.parent = _enemyContainer.transform;
      yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
    }
  }

  public void OnPlayerDeath()
  {
    _stopSpawning = true;
  }

  // spawn powerups
}
