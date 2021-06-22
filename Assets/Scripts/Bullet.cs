using UnityEngine;
using Cinemachine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float shootSpeed = 1.0f;

    private Rigidbody2D rigidbody2D;
    private Vector2 Direction;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody2D.velocity = Direction * shootSpeed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyMovement enemyMovement = collision.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            ScreenShakeController.Instance.ShakeCamera(0.3f, 0.5f);
            enemyMovement.Hit();
            DestroyBullet();
        }
    }

}
