using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed;
    public float bulletDamage;
    public ParticleSystem hitParticlePrefab;

    
    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            Instantiate(hitParticlePrefab, transform.position, transform.rotation);
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            Destroy(gameObject);
            enemy.TakeDamage(bulletDamage);
        }
    }
}
