using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private GameObject _enemyLaserPrefab;
    [SerializeField] private float _enemyFireRate = 3f;
    [SerializeField] private float _canFire = -1f;

    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) Debug.LogError("Player is NULL");

        _animator = GetComponent<Animator>();
        if (_animator == null) Debug.LogError("Animator is NULL");

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateMovement();

        if (!(Time.time > _canFire)) return;
        _enemyFireRate = Random.Range(3f, 7f);
        _canFire = Time.time + _enemyFireRate;
        var enemyGameObject = Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
        var enemyLasers = enemyGameObject.GetComponentsInChildren<Laser>();

        foreach (var enemyLaser in enemyLasers) enemyLaser.AssignEnemyLaser();

        // Debug.Break();
    }

    private void CalculateMovement()
    {
        // move down at 4 m/s
        var direction = new Vector3(0, -1, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        // if bottom of screen
        // respawn at top with a new random x position
        if (transform.position.y < -5.5f)
        {
            var randomX = Random.Range(-9.5f, 9.5f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player":
            {
                var player = other.transform.GetComponent<Player>();
                if (player != null) player.Damage();
                _animator.SetTrigger("OnEnemyDeath");
                _speed = 0;
                _audioSource.Play();
                Destroy(gameObject, 2.4f);
                break;
            }
            case "Laser":
            {
                var laser = other.transform.GetComponent<Laser>();
                if (!laser.IsEnemyLaser())
                {
                    Destroy(other.gameObject);
                    if (_player != null) _player.AddScore(10);
                    _animator.SetTrigger("OnEnemyDeath");
                    _speed = 0;
                    _audioSource.Play();

                    // fix for destroyed enemy being attacked issue
                    Destroy(GetComponent<Collider2D>());

                    Destroy(gameObject, 2.4f);
                }

                break;
            }
        }
    }
}