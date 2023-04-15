using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 12.0f;
    private bool _isEnemyLaser;

    // Update is called once per frame
    private void Update()
    {
        
        if (IsEnemyLaser())
        {
            // enemy laser moves down
            MoveSouth();
        }
        else
        {
            // player laser moves up
            MoveNorth();
        }
    }

    private void MoveNorth()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        DestroyLaser();
    }

    private void MoveSouth()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        DestroyLaser();
    }

    private void DestroyLaser()
    {
        if (transform.position.y is <= 8.0f and >= -8.0f) return;

        // check and destroy the parent object
        if (transform.parent != null) Destroy(transform.parent.gameObject);

        // destroy laser object if it goes off screen
        Destroy(gameObject);
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    public bool IsEnemyLaser()
    {
        return _isEnemyLaser;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" || !IsEnemyLaser()) return;
        var player = other.GetComponent<Player>();
        player?.Damage();
    }
}