using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;

    private Player _player;
    private Animator _animator;

    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) Debug.LogError("Player is NULL");

        _animator = GetComponent<Animator>();
        if (_animator == null) Debug.LogError("Animator is NULL");
    }

    // Update is called once per frame
    private void Update()
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
                break;
            }
            case "Laser":
            {
                Destroy(other.gameObject);
                if (_player != null) _player.AddScore(10);
                break;
            }
        }
        _animator.SetTrigger("OnEnemyDeath");
        _speed = 0;
        Destroy(gameObject, 2.4f);

    }
}