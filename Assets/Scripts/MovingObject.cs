using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [Header("이동 속도")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("달리기 배율")]
    [SerializeField] private float sprintMultiplier = 2f;

    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 movement;

    // 마지막으로 바라본 방향
    private Vector2 lastDirection = Vector2.down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement = Vector2.zero;

        // 위
        if (Input.GetKey(KeyCode.W))
        {
            movement = Vector2.up;
        }
        // 아래
        else if (Input.GetKey(KeyCode.S))
        {
            movement = Vector2.down;
        }
        // 왼쪽
        else if (Input.GetKey(KeyCode.A))
        {
            movement = Vector2.left;
        }
        // 오른쪽
        else if (Input.GetKey(KeyCode.D))
        {
            movement = Vector2.right;
        }

        // 움직일 때만 마지막 방향 변경
        if (movement != Vector2.zero)
        {
            lastDirection = movement;
        }

        // 애니메이션 설정
        if (animator != null)
        {
            animator.SetBool("Walking", movement != Vector2.zero);
            animator.SetFloat("DirX", lastDirection.x);
            animator.SetFloat("DirY", lastDirection.y);
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = moveSpeed;

        // Shift 누르고 있으면 빠르게
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed *= sprintMultiplier;
        }

        rb.MovePosition(
            rb.position + movement * currentSpeed * Time.fixedDeltaTime
        );
    }
}