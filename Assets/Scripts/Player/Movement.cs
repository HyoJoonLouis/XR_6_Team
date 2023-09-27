using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Animator animator;
    bool checkWalk;

    private Vector2 moveDirection;
    public Vector3 mousePosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(float speed)
    {
        if (moveDirection.x == 0 && moveDirection.y == 0)
            checkWalk = false;
        else
            checkWalk = true;

        animator.SetBool("isWalk", checkWalk);
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnMove(InputValue value)
    {
        Vector3 input = value.Get<Vector2>();
        
        moveDirection = new Vector2(input.x, input.y);
    }
}
