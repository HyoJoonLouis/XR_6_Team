using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Animator animator;
    bool checkWalk;
    bool isFront, isBack;

    private Vector2 moveDirection;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(float speed)
    {
        if (moveDirection.x != 0)
        {
            animator.SetBool("isWalk", true);
        }

        if (moveDirection.x < 0)
        {
            animator.SetBool("isBack", true);
            animator.SetBool("isFront", false);
        }
        else if (moveDirection.x > 0)
        {
            animator.SetBool("isBack", false);
            animator.SetBool("isFront", true);
        }

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        
        moveDirection = new Vector2(input.x, input.y);
        Debug.Log(moveDirection.x);
    }
}
