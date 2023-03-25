using UnityEngine;

public class Enemy : MonoBehaviour
{
  [SerializeField]
  private float _speed = 4f;

  private Player _player;
  // Start is called before the first frame update
  void Start()
  {
    _player = GameObject.Find("Player").GetComponent<Player>();
    if (_player == null)
    {
      Debug.LogError("Player is NULL");
    }
  }

  // Update is called once per frame
  void Update()
  {
    // move down at 4 m/s
    var direction = new Vector3(0, -1, 0);
    transform.Translate(direction * _speed * Time.deltaTime);

    // if bottom of screen
    // respawn at top with a new random x position
    if (transform.position.y < -5.5f)
    {
      float randomX = Random.Range(-9.5f, 9.5f);
      transform.position = new Vector3(randomX, 7, 0);
    }
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      Player player = other.transform.GetComponent<Player>();
      if (player != null)
      {
        player.Damage();
      }
      Destroy(this.gameObject);
    }
    if (other.tag == "Laser")
    {
      Destroy(other.gameObject);
      if (_player != null)
      {
        _player.AddScore(10);
      }
      Destroy(this.gameObject);
    }
  }
}
