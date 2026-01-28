using UnityEngine;

public class Dice : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private bool hasStopped = false;

    // 주사위 각 면의 숫자 (당신의 주사위 모델에 맞게 수정!)
    [Header("각 방향에 해당하는 숫자 (주사위 모델에 맞게 설정)")]
    public int upNumber;       // local +Y 면
    public int downNumber;     // local -Y 면
    public int rightNumber;    // local +X 면
    public int leftNumber;     // local -X 면
    public int forwardNumber;  // local +Z 면
    public int backNumber;     // local -Z 면

    [Header("굴릴 때 사용할 랜덤 범위")]
    public float rollForceIndex = 10f;
    public float rolltorqueIndex = 10f;
    public float rollForceUp = 15f;

    [Header("정지 판정 설정")]
    public float stopThreshold = 0.05f; // 이 속도보다 낮으면 멈춘 것으로 간주
    public float waitTime = 0.5f;      // 이 시간 동안 유지되어야 함
    private float stopTimer = 0f;      // 시간을 잴 변수

    public float gravityScale = 1.0f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.useGravity = false;
    }

    // 외부에서 이 함수를 호출해서 주사위를 굴리면 됨
    public void Roll()
    {
        hasStopped = false;

        // 속도 초기화
        rb.linearVelocity = Vector3.zero;          // linearVelocity → velocity 로 수정
        rb.angularVelocity = Vector3.zero;

        // 랜덤 힘 / 토크 생성
        Vector3 rollForce = new Vector3(
            Random.Range(-rollForceIndex, rollForceIndex),
            rollForceUp,
            Random.Range(-rollForceIndex, rollForceIndex)
        );

        Vector3 rollTorque = new Vector3(
            Random.Range(-rolltorqueIndex, rolltorqueIndex),
            Random.Range(-rolltorqueIndex, rolltorqueIndex),
            Random.Range(-rolltorqueIndex, rolltorqueIndex)
        );

        rb.AddForce(rollForce, ForceMode.Impulse);
        rb.AddTorque(rollTorque, ForceMode.Impulse);
    }

    private void Update()
    {
        // 1. 스페이스바 입력 시 재시작 (기존 로직 유지)
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.isKinematic = false; 
            Roll();
            return; 
        }

        // 2. 이미 멈춘 상태라면 아래 로직 스킵
        if (hasStopped) return;

        // 3. 속도 체크 (기본 중력이나 커스텀 중력을 쓰더라도 동일하게 적용)
        // sqrMagnitude가 magnitude보다 연산 속도가 빨라 물리 체크에 유리합니다.
        if (rb.linearVelocity.sqrMagnitude < stopThreshold && rb.angularVelocity.sqrMagnitude < stopThreshold)
        {
            stopTimer += Time.deltaTime; // 속도가 낮으면 타이머 증가
        }
        else
        {
            stopTimer = 0f; // 조금이라도 움직이면 타이머 초기화 (찰나의 정지 방지)
        }

        // 4. 설정한 시간만큼 '계속' 멈춰 있었다면 최종 정지 처리
        if (stopTimer >= waitTime)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; 

            hasStopped = true;
            stopTimer = 0f; // 타이머 초기화

            int topNumber = GetTopNumber();
            Debug.Log($"주사위 결과: {topNumber}");
        }
    }

    void FixedUpdate()
    {
        // 커스텀 중력을 수동으로 계산해서 더해줍니다.
        Vector3 gravity = -9.81f * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    

    private int GetTopNumber()
    {
        // 월드 기준 위쪽 방향
        Vector3 worldUp = Vector3.up;

        // 주사위의 각 로컬 축이 월드에서 어느 방향인지
        Vector3 up = transform.up;
        Vector3 down = -transform.up;
        Vector3 right = transform.right;
        Vector3 left = -transform.right;
        Vector3 forward = transform.forward;
        Vector3 back = -transform.forward;

        // 각 방향과 월드 Up의 내적을 비교해서, 가장 위를 찾는다.
        float maxDot = -Mathf.Infinity;
        int number = 0;

        void CheckDir(Vector3 dir, int value)
        {
            float d = Vector3.Dot(dir, worldUp);
            if (d > maxDot)
            {
                maxDot = d;
                number = value;
            }
        }

        CheckDir(up,     upNumber);
        CheckDir(down,   downNumber);
        CheckDir(right,  rightNumber);
        CheckDir(left,   leftNumber);
        CheckDir(forward,forwardNumber);
        CheckDir(back,   backNumber);

        return number;
    }
}