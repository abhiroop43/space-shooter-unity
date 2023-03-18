using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    // ID for Powerup
    [SerializeField]
    private int powerupId;

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
                switch (powerupId)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeeBoostActive();
                        break;
                    case 2:
                        Debug.Log("Collected Shields Powerup");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
