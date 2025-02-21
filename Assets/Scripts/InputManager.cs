using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class InputManager : MonoBehaviour
{
    public UnityEvent OnSpacePressed = new UnityEvent();
    public UnityEvent<Vector3> OnMove = new UnityEvent<Vector3>();
    public UnityEvent OnDash = new UnityEvent();
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke();
        }

        Vector3 MoveDirection = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            MoveDirection += Vector3.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            MoveDirection += Vector3.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            MoveDirection += Vector3.back;
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveDirection += Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnDash?.Invoke();
        }

        OnMove?.Invoke(MoveDirection);
    }
}
