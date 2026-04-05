using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAI : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireRate = 2f;
    public float stopDistance = 10f;

    private Transform player;
    private Rigidbody playerRb;
    private NavMeshAgent agent;
    private float nextFireTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject pObj = GameObject.FindGameObjectWithTag("Player");
        if (pObj != null)
        {
            player = pObj.transform;
            playerRb = pObj.GetComponent<Rigidbody>();
        }
        
        agent.stoppingDistance = stopDistance;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        agent.SetDestination(player.position);

        Vector3 aimPoint = PredictPosition(distance);
        transform.LookAt(new Vector3(aimPoint.x, transform.position.y, aimPoint.z));

        if (distance <= stopDistance + 2f && Time.time >= nextFireTime)
        {
            Shoot(aimPoint);
            nextFireTime = Time.time + fireRate;
        }
    }

    Vector3 PredictPosition(float distance)
    {
        Vector3 playerPos = player.position;
        Vector3 playerVel = playerRb != null ? playerRb.linearVelocity : Vector3.zero;

        float travelTime = distance / bulletSpeed;

        return playerPos + (playerVel * travelTime);
    }

    void Shoot(Vector3 targetPos)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        
        Vector3 shootDir = (targetPos - firePoint.position).normalized;
        rb.AddForce(shootDir * bulletSpeed, ForceMode.Impulse);
        
        Destroy(bullet, 3f);
    }
}