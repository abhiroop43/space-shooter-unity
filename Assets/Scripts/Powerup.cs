using System;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;

    // ID for Powerup
    [SerializeField] private int powerupId;
    [SerializeField] private AudioClip _clip;

    // Update is called once per frame
    private void Update()
    {
        // move down at 3m/s
        MoveDown();

        // when leaving the screen, destroy this object
        if (transform.position.y < -6f) Destroy(gameObject);
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position, 1f);
            if (player != null)
                switch (powerupId)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                }

            Destroy(gameObject);
        }
    }
}