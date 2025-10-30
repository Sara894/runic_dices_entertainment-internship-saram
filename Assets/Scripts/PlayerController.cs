using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction moveAction;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDir;
    [SerializeField] private Animator animator;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;

    private void Awake()
    {
        moveAction = playerInput.actions["Move"];
    }

    private void OnEnable() => moveAction.Enable();
    private void OnDisable() => moveAction.Disable();
    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.sqrMagnitude > 0.01f) lastMoveDir = moveInput;

        animator.SetFloat("MoveX", lastMoveDir.x);
        animator.SetFloat("MoveY", lastMoveDir.y);
        animator.SetFloat("Speed", moveInput.magnitude);
        Debug.Log(moveInput);
    }
    private void FixedUpdate()
    {
        Vector2 moveDir = moveInput.sqrMagnitude > 0.01f ? moveInput.normalized : Vector2.zero;

        Vector2 isoDir = new Vector2(moveDir.x + moveDir.y, (moveDir.y - moveDir.x) / 2f);
        isoDir = isoDir.normalized;
        Vector2 targetVelocity = isoDir * moveSpeed;

        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
    }
}