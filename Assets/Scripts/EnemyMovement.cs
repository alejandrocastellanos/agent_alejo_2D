using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float EnemyCheckRadius = 10;
    public GameObject Player;
    public GameObject BulletPrefab;
    public Transform EnemyCheck;
    public LayerMask whatsIsPlayer;
    public ParticleSystem explosionRef;
    public float LastShoot;
    public Transform wallCheck;
    public float wallCheckDistance;

    private int Health = 5;
    private bool PlayerIsVisible;
    private Animator animator;
    private Vector3 direction2;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(EnemyCheck.position, EnemyCheckRadius);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(wallCheck.position, new Vector3(
       //     wallCheck.position.x + wallCheckDistance,
        //    wallCheck.position.y, wallCheck.position.z));
    }

    private void FixedUpdate()
    {
        PlayerIsVisible = Physics2D.OverlapCircle(EnemyCheck.position, EnemyCheckRadius, whatsIsPlayer);
        animator.SetBool("Walk", PlayerIsVisible);
    }

    void Update()
    {
        if (Player == null)
        {
            return;
        }
        if (PlayerIsVisible)
        {
            Vector3 direction = Player.transform.position - transform.position;
            if (direction.x >= 0.0f)
            {
                transform.localScale = new Vector3(0.25f, 0.25f, 1.0f);
                Vector2 vector2 = new Vector2(1, 0.0f);
                vector2 *= Time.deltaTime;
                transform.Translate(vector2);
            }
            else
            {
                transform.localScale = new Vector3(-0.25f, 0.25f, 1.0f);
                Vector2 vector2 = new Vector2(-1, 0.0f);
                vector2 *= Time.deltaTime;
                transform.Translate(vector2);
            }

            if (Time.time > LastShoot + 0.5f)
            {
                Shoot();
                LastShoot = Time.time;
            }
            
        }
    }

    public void Shoot()
    {
        if (transform.localScale.x > 0.0f)
        {
            direction2 = Vector3.right;

        }
        else if (transform.localScale.x < 0.0f)
        {
            direction2 = Vector3.left;

        }

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction2 * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletEnemy>().SetDirection(direction2);
    }

    public void Hit()
    {
        Health = Health - 1;
        Instantiate(explosionRef, transform.position, Quaternion.identity);
        if (Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
