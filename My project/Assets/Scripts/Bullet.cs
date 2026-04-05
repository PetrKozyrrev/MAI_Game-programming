using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 25;

    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("PlayerBullet") && collision.gameObject.CompareTag("Player")) return;

        if (gameObject.CompareTag("EnemyBullet") && collision.gameObject.CompareTag("Enemy")) return;

        Health targetHealth = collision.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}