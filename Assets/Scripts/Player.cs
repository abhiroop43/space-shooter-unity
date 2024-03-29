using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 3.5f;
    private float _playerHorizontalBound = 11f;
    private float _playerVerticalLowerBound = -3.8f;
    private float _playerVerticalUpperBound = 0f;
    private float _canFire = -1f;

    [SerializeField] private float _fireRate = 0.15f;

    [SerializeField] private GameObject _laserPrefab;

    [SerializeField] private GameObject _tripleShotPrefab;

    [SerializeField] private GameObject _shieldVisualizer;

    [SerializeField] private GameObject _leftEngine;
    [SerializeField] private GameObject _rightEngine;

    [SerializeField] private int _lives = 3;
    private SpawnManager _spawnManager;

    [SerializeField] private bool _isTripleShotActive = false;

    private float _speedMultiplier = 2.0f;

    [SerializeField] private bool _isSpeedBoostActive = false;

    [SerializeField] private bool _isShieldActive = false;

    [SerializeField] private int _score;

    private UIManager _uiManager;

    [SerializeField] private AudioClip _fireLaserAudioClip;
    
    private AudioSource _audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        // take the current position and assign a start position
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null) Debug.LogError("SpawnManager is NULL");

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null) Debug.LogError("UIManager is NULL");

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource on the Player is NULL");
        }
        else
        {
            _audioSource.clip = _fireLaserAudioClip;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        MovePlayer();
        ConstrainPlayer();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) FireLaser();
    }

    private void MovePlayer()
    {
        // Get the horizontal and vertical inputs.
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

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
            transform.position = new Vector3(-_playerHorizontalBound,
                transform.position.y,
                transform.position.z);
        else if (transform.position.x < -_playerHorizontalBound)
            transform.position = new Vector3(_playerHorizontalBound,
                transform.position.y,
                transform.position.z);

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

        if (_isTripleShotActive)
            // Instantiate the triple shot prefab
            Instantiate(_tripleShotPrefab,
                transform.position + new Vector3(-0.79f, 1.05f, 0),
                Quaternion.identity);
        else
            // Instantiate the laser prefab
            Instantiate(_laserPrefab,
                transform.position + new Vector3(0, 1.05f, 0),
                Quaternion.identity);

        // play laser audio clip
        _audioSource.Play();
    }

    public void Damage()
    {
        // if shields are active then don't subtract lives but deactivate shields
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);
        
        // set damage visualizer
        var engineSide = (new System.Random()).Next(0, 2);

        if (_leftEngine.activeInHierarchy)
        {
            _rightEngine.SetActive(true);
        }
        else if (_rightEngine.activeInHierarchy)
        {
            _leftEngine.SetActive(true);
        }
        else
        {
            if (engineSide == 0)
            {
                _leftEngine.SetActive(true);
            }
            else
            {
                _rightEngine.SetActive(true);
            }
        }
        
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
        _isSpeedBoostActive = false;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer = transform.GetChild(0).gameObject;
        _shieldVisualizer.SetActive(true);
        // StartCoroutine(ShieldPowerDownRoutine());
    }

    // not used
    private IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _isShieldActive = false;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}