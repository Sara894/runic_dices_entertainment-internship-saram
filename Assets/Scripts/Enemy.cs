using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum States
    {
        Idle = 0,
        Patrol = 1,
        Chase = 2,
        Attack = 3
    }

    [Header("References")]
    public Transform player;
    public Transform[] patrolPoints;
    public States currentState;
    private States previousState;

    [Header("Settings")]
    public float moveSpeed = 2f;
    public float chaseRange = 10f;
    public float attackRange = 1.5f;
    public float chaseStopRange = 12f;
    private int patrolIndex = 0;
    [SerializeField] private Animator animator;
    private float lastAttackTime = 0f;
    private int patrolDirection = 1;

    private void Start()
    {
        previousState = currentState;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        animator.SetInteger("State", (int)currentState);

        switch (currentState)
        {
            case States.Idle:
                if (distanceToPlayer < chaseRange)
                    currentState = States.Chase;
                else if (patrolPoints.Length > 0)
                    currentState = States.Patrol;
                break;

            case States.Patrol:
                Patrol();
                if (distanceToPlayer < chaseRange)
                    currentState = States.Chase;
                break;

            case States.Chase:
                Chase();
                if (distanceToPlayer < attackRange)
                    currentState = States.Attack;
                else if (distanceToPlayer > chaseStopRange)
                    currentState = patrolPoints.Length > 0 ? States.Patrol : States.Idle;
                break;

            case States.Attack:
                Attack();
                if (distanceToPlayer > attackRange)
                    currentState = States.Chase;
                break;
        }

        if (previousState != currentState)
        {
            previousState = currentState;
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length < 2) return;

        Transform target = patrolPoints[patrolIndex];

        float direction = target.position.x - transform.position.x;
        Flip(direction);

        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            if (patrolIndex == 0)
                patrolDirection = 1;
            else if (patrolIndex == patrolPoints.Length - 1)
                patrolDirection = -1;

            patrolIndex += patrolDirection;
        }
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        float direction = player.position.x - transform.position.x;
        Flip(direction);
    }

    private void Attack()
    {
        Debug.Log("Attacking player!");
    }

    private void Flip(float direction)
    {
        Vector3 scale = transform.localScale;
        if (direction > 0)
            scale.x = Mathf.Abs(scale.x);
        else if (direction < 0)
            scale.x = -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}