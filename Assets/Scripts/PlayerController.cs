using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction attackAction;
    public InputAction pickPotion;
    public InputAction openInventoryCanvas;

    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDir;
    [SerializeField] private Animator animator;
    [SerializeField] private HealthController selfHealthController;
    [SerializeField] private HealthController enemyHealthController;

    [SerializeField] private float acceleration = 5f;
    private float attackCooldown = 0.5f;
    private float lastAttackTime;
    private bool isAttacking = false;

    [SerializeField] private float moveSpeed = 3f;

    private void Awake()
    {
        moveAction = playerInput.actions["Move"];
        attackAction = playerInput.actions["Attack"];
        pickPotion = playerInput.actions["Interact"];
        openInventoryCanvas = playerInput.actions["Submit"];
    }

    private void OnEnable() 
    {
        moveAction.Enable();
        attackAction.Enable();
        attackAction.performed += OnAttack;
    }
    private void OnDisable() 
    {
        moveAction.Disable();
        attackAction.Disable();
        attackAction.performed -= OnAttack;
    }
    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.sqrMagnitude > 0.01f) lastMoveDir = moveInput;

        animator.SetFloat("MoveX", lastMoveDir.x);
        animator.SetFloat("MoveY", lastMoveDir.y);
        animator.SetFloat("Speed", moveInput.magnitude);
        Debug.Log(moveInput);
        Debug.Log("Speed: " + moveInput.magnitude);
    }
    private void FixedUpdate()
    {
        Vector2 moveDir = moveInput.sqrMagnitude > 0.01f ? moveInput.normalized : Vector2.zero;

        Vector2 isoDir = new Vector2(moveDir.x + moveDir.y, (moveDir.y - moveDir.x) / 2f);
        isoDir = isoDir.normalized;
        Vector2 targetVelocity = isoDir * moveSpeed;

        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
    }
    private void OnAttack(InputAction.CallbackContext context)
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;
        animator.SetTrigger("Attack");
        StartCoroutine(AttackWindow());
    }
    private IEnumerator AttackWindow()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isAttacking) return;
        if (!collision.gameObject.CompareTag("Enemy")) return;

        enemyHealthController.DecreaseHealth(10f);
    }
    public void AddMoveSpeed(float value)
    {
        moveSpeed += value;
    }
}