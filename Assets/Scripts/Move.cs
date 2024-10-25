using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Variável pública para definir a velocidade do movimento
    public float velocidade = 5f;
    public float jumpForce = 5f;
    public float groundCheckSize = 0.1f;
    public bool canMove = true;
    public bool canJump = true;

    Animator anim;

    public GameObject groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;
    float movimentoHorizontal;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Movement();
            if(canJump)
                Jump();
        }
        CheckGround();
        FlipSprite();
        anim.SetBool("isJumping", !isGrounded);
    }

    void CheckGround()
    {
        // Verifica se o personagem está no chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckSize, groundLayer);
    }

    void Movement()
    {
        movimentoHorizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(movimentoHorizontal * velocidade * Time.deltaTime, 0, 0));

        // Atualiza o estado da animação de movimento
        if (movimentoHorizontal != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetTrigger("takeOff");
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    void FlipSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (movimentoHorizontal < 0)
        {
            spriteRenderer.flipX = true;
            //isFacingRight = false;
        }
        else if (movimentoHorizontal > 0)
        {
            spriteRenderer.flipX = false;
            //isFacingRight = true;
        }
    }


    // Desenha o OverlapCircle no editor
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckSize);
        }
    }
}