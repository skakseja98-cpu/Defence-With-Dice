using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed;
    public float jumpForce;
    public float mouseSensitivity = 2.0f;

    public float gravityScale = 1.0f;
    
    


    private Rigidbody playerRb;
    private float rotationX = 0f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.useGravity = false;

        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        // 2. 이동 (캐릭터 정면 기준)
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        
        Vector3 moveDir = (transform.forward * vInput) + (transform.right * hInput);
        transform.position += moveDir * playerSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerSpeed *= 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed /= 2;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJump();
        }

        /*
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate
        (Vector3.forward * verticalInput * playerSpeed * Time.deltaTime);

        transform.Translate
        (Vector3.right * horizontalInput * playerSpeed * Time.deltaTime); */
    }

    void FixedUpdate()
    {
        // 커스텀 중력을 수동으로 계산해서 더해줍니다.
        Vector3 gravity = -9.81f * gravityScale * Vector3.up;
        playerRb.AddForce(gravity, ForceMode.Acceleration);
    }


    void PlayerJump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
