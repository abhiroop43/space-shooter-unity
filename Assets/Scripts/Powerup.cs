using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
  [SerializeField]
  private float _speed = 3f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    // move down at 3m/s
    MoveDown();

    // when leaving the screen, destroy this object
    if (transform.position.y < -6f)
    {
      Destroy(this.gameObject);
    }
  }

  private void MoveDown()
  {
    transform.Translate(Vector3.down * _speed * Time.deltaTime);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "Player")
    {
      Player player = other.GetComponent<Player>();
      if (player != null)
      {
        player.TripleShotActive();
      }
      Destroy(this.gameObject);
    }
  }
}
