using UnityEngine;
using UnityEngine.UIElements;

public class WeaponSystem : MonoBehaviour
{

    public Transform muzzle;
    public GameObject bulletPrefab;
    public ParticleSystem fireParticlePrefab;
    public float fireRate;
    public AudioSource audioSource;
    public AudioClip fireSound;


    private float nextFireTime;

    void Start()
    {
        
    }

    void Update()
    {
        // 1. GetMouseButton(0)은 마우스를 누르고 있는 동안 '계속' true를 반환합니다.
        // 2. 현재 시간(Time.time)이 다음 발사 가능 시간보다 커졌는지 확인합니다.
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Fire();
            // 3. 다음 발사가 가능한 시각을 (현재 시간 + 발사 간격)으로 갱신합니다.
            nextFireTime = Time.time + fireRate;
        }
    }
    private void Fire()
    {
        Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation);
        Instantiate(fireParticlePrefab, muzzle.transform.position, muzzle.transform.rotation);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(fireSound);
    }
}
