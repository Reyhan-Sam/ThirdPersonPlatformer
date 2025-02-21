using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputManager inputManager;
    private Rigidbody playerRB;
    private Vector3 moveDirection = Vector3.zero;
    private bool isGrounded = true;
    private int jumpCount = 0;
    private bool canDash = false;
    private bool isDashing = false;
    private float dashTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        inputManager.OnMove.AddListener(Move);
        inputManager.OnSpacePressed.AddListener(Jump);
        inputManager.OnDash.AddListener(Dash);
    }   

    private void Move(Vector3 direction)
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        moveDirection = cameraForward * direction.z + cameraRight * direction.x;
 
    }

    private void Jump()
    {
        if (jumpCount < 2)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;

            if (jumpCount ==2){
                canDash = true;
            }
        }
        
    }
    private void Dash(){
        if (canDash && !isDashing)
        {
            isDashing = true;
            dashTimer = dashDuration;
            playerRB.linearVelocity = moveDirection * dashSpeed;
            canDash = false;
        }
    }


     void FixedUpdate()
    {
        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
            }
        }
        else
        {
            playerRB.linearVelocity = new Vector3(moveDirection.x * moveSpeed, playerRB.linearVelocity.y, moveDirection.z * moveSpeed);
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")  || other.gameObject.CompareTag("Platform"))
        {
            isGrounded = true;
            jumpCount = 0;
            canDash = false;
        }
    }
}
