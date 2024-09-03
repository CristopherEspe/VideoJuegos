using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public int damage = 10;
    public int points = 10;
    public GameObject explosionPrefab;
    private Transform player;
    public ScoreManager scoreManager;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
        if (gameManagerObject != null)
        {
            scoreManager = gameManagerObject.GetComponent<ScoreManager>();
        }
        else
        {
            Debug.LogError("GameManager no encontrado. Asegúrate de que el objeto GameManager tiene la etiqueta 'GameManager'.");
        }
    }

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Rotate towards player
        Vector3 lookDirection = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            scoreManager.OnEnemyDestroyed(points);

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }

            // Dead Sound
            AudioManager.instance.Play("Explosion");
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
