using UnityEngine;

public class Laser : MonoBehaviour
{
  private float _speed = 12.0f;

  // Update is called once per frame
  void Update()
  {
    // move laser up after it is instantiated
    MoveNorth();

    // destroy laser object if it goes off screen
    if (transform.position.y > 8.0f)
    {
      Destroy(this.gameObject);
    }
  }

  private void MoveNorth()
  {
    transform.Translate(Vector3.up * _speed * Time.deltaTime);
  }
}
