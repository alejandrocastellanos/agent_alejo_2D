using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject Player;
    public GameObject BulletPrefab;

    private Vector3 direction;
    private float LastShoot;

    void Update()
    {
        Vector3 direction = Player.transform.position - transform.position;
        if (direction.x >= 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        if (Input.GetKeyDown("s") && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    public void Shoot()
    {
        if (transform.localScale.x > 0.0f)
        {
            direction = Vector3.right;
        }
        else if (transform.localScale.x < 0.0f)
        {
            direction = Vector3.left;
        }

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }
}
