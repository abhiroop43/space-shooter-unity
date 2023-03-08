using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField]
  private float _speed = 3.5f;
  private float _playerHorizontalBound = 11f;
  private float _playerVerticalLowerBound = -3.8f;
  private float _playerVerticalUpperBound = 0f;
  private float _canFire = -1f;

  [SerializeField]
  private float _fireRate = 0.15f;

  [SerializeField]
  private GameObject _laserPrefab;

  [SerializeField]
  private int _lives = 3;
  private SpawnManager _spawnManager;


  // Start is called before the first frame update
  void Start()
  {
    // take the current position and assign a start position
    transform.position = new Vector3(0, 0, 0);
    _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

    if (_spawnManager == null)
    {
      Debug.LogError("SpawnManager is NULL");
    }
  }

  // Update is called once per frame
  void Update()
  {
    MovePlayer();
    ConstrainPlayer();
    if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
    {
      FireLaser();
    }
  }

  private void MovePlayer()
  {
    // Get the horizontal and vertical inputs.
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    // Create a vector to store the direction we want to move in.
    var direction = new Vector3(horizontalInput,
                                verticalInput,
                                0);

    // Move the player in that direction.
    transform.Translate(direction * _speed * Time.deltaTime);
  }

  private void ConstrainPlayer()
  {
    // Horizontal bounds
    if (transform.position.x > _playerHorizontalBound)
    {
      transform.position = new Vector3(-_playerHorizontalBound,
                                        transform.position.y,
                                        transform.position.z);
    }
    else if (transform.position.x < -_playerHorizontalBound)
    {
      transform.position = new Vector3(_playerHorizontalBound,
                                        transform.position.y,
                                        transform.position.z);
    }

    // Vertical bounds
    transform.position = new Vector3(transform.position.x,
                                      Mathf.Clamp(transform.position.y,
                                                  _playerVerticalLowerBound,
                                                  _playerVerticalUpperBound),
                                      transform.position.z);
  }

  private void FireLaser()
  {
    _canFire = Time.time + _fireRate;

    // Instantiate the laser prefab
    Instantiate(_laserPrefab,
                  transform.position + new Vector3(0, 0.8f, 0),
                  Quaternion.identity);
  }

  public void Damage()
  {
    _lives--;
    if (_lives < 1)
    {
      _spawnManager.OnPlayerDeath();
      Destroy(this.gameObject);
    }
  }

}
