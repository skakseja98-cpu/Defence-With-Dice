using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float enemyMoveSpeed;
    public float enemyHP;


    private Transform playerTransform;
    private Rigidbody enemyRb;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // 1. 플레이어를 향한 방향 벡터 계산 (Y축 제외)
        Vector3 dir = (playerTransform.position - transform.position).normalized;
        dir.y = 0; // 이 값이 0이어야 바닥을 따라 이동합니다.

        // 2. 고개 꺾임 방지: 적과 같은 높이의 가상 목표 지점 설정
        Vector3 targetPostion = new Vector3(playerTransform.position.x,
        transform.position.y, playerTransform.position.z);
        
        // 3. 가상의 지점을 바라보게 하여 수평 회전만 수행
        transform.LookAt(targetPostion);

        // 4. 이동은 기존과 동일하게 리지드바디 속도 제어
        enemyRb.linearVelocity = new Vector3(
            dir.x * enemyMoveSpeed, 
            enemyRb.linearVelocity.y, 
            dir.z * enemyMoveSpeed
        );
    }



    public void TakeDamage(float amount)
    {
        enemyHP -=amount;
        Debug.Log("적 체력 : " + enemyHP);

        if (enemyHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameObject.CompareTag("Boss"))
        {
            StageManager stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

            stageManager.OnBossDeath(gameObject.transform.position);
            Destroy(gameObject);
        } else Destroy(gameObject);
    }
}
