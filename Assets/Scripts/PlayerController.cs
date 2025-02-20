using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float force = 5f;
    [SerializeField] private InputManager inputManager;
    private Rigidbody playerRB;
    private Vector3 moveDirection = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        inputManager.OnMove.AddListener(Move);
        inputManager.OnSpacePressed.AddListener(Jump);
    }

    private void Move(Vector3 direction)
    {
        moveDirection = new Vector3(direction.x, 0, direction.z).normalized;

    }

    private void Jump()
    {
        playerRB.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

     void FixedUpdate()
    {
        playerRB.linearVelocity = new Vector3(moveDirection.x * force, playerRB.linearVelocity.y, moveDirection.z * force);
    }

}
