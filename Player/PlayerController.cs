using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    PlayerInputs inputActions;
    public float speed = 2.7f;
    public float jumpForce = 5;
    public GameObject shotPrefab;
    public float shotForce = 10f;

    bool canJump = true;
    bool canAttack = true;

    SpriteRenderer sprite;
    Animator animator;
    Rigidbody2D body;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }
    void Awake()
    {
        inputActions = new PlayerInputs();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        var moveInputs = inputActions.Player_Map.Movement.ReadValue<Vector2>();
        transform.position += speed * Time.deltaTime * new Vector3(moveInputs.x, moveInputs.y, 0);

        animator.SetBool("b_isWalking", moveInputs.x != 0);


        if (moveInputs.x != 0)
        {
            sprite.flipX = moveInputs.x < 0;
        }

        canJump = Mathf.Abs(body.velocity.y) <= 0.001f;

        HandlerJumpAction();
        HandleAttack();
    }

    void HandlerJumpAction()
    {
        var jumpPressed = inputActions.Player_Map.Jump.IsPressed();

        if (canJump && jumpPressed)
        {
            body.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    void HandleAttack()
    {
        var attackPressed = inputActions.Player_Map.Attack.IsPressed();

        if (canAttack && attackPressed)
        {
            canAttack = false;

            animator.SetTrigger("t_attack");

           
        }
    }

    public void ShotNewEgg()
    {
            var newShot = GameObject.Instantiate(shotPrefab);
            newShot.transform.position = transform.position;

            var isLookRight = !sprite.flipX;
            Vector2 shotDirection = shotForce * new Vector2(isLookRight ? -1 : 1, 0);
            newShot.GetComponent<Rigidbody2D>().AddForce(shotDirection, ForceMode2D.Impulse);
            newShot.GetComponent<SpriteRenderer>(). flipY = !isLookRight;
    }

    public void SetCanAttack()
    {
        canAttack = true;
    }
}
