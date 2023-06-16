using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : PNGMonoBehaviour
{

    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float maxJumpHeight = 4f;
    [SerializeField] protected float maxJumpTime = 1f;
    [SerializeField] protected PlayerImpact playerImpact;

    private float inputAxis;
    private Vector2 velocity;
    private float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    private float gravity => (-2.5f * maxJumpHeight) / Mathf.Pow((maxJumpTime / 2f), 2);

   public bool grounded { get;   private set; }
    protected bool jumping { get;  private set; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayImpact();
    }
    private void Update()
    {

        this.HorizontalMovement();
        grounded = playerImpact.Rigidbody.Raycast(Vector2.down);
        if(grounded)
        {
            this.JumpMovement();
        }
        this.ApplyGravity();

    }
    private void FixedUpdate()
    {

        this.Moving();
       
    }


    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * speed, speed * Time.deltaTime);
    }
    protected virtual void JumpMovement()
    {

        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump");
            velocity.y = jumpForce;
            jumping = true;
           
        }
    }
    protected virtual void ApplyGravity()
    {
        bool falling = velocity.y < 0f || !Input.GetButtonDown("Jump");
        float multiplaier = falling ? 4f : 1f;
        velocity.y += gravity *multiplaier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }
    protected virtual void Moving()
    {
         Vector2 position = playerImpact.Rigidbody.position;
         position += velocity * Time.deltaTime;
         Vector2 leftEdge = GameController.Instance.MainCamera.ScreenToWorldPoint(Vector2.zero);
         Vector2 rightEdge = GameController.Instance.MainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
         position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);
        playerImpact.Rigidbody.MovePosition(position);
        
    }
    protected virtual void LoadPlayImpact()
    {
        if (playerImpact != null) return;
        this.playerImpact = transform.parent.GetComponent<PlayerImpact>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp")){
             if(transform.DotTest(collision.transform , Vector2.up))
            {
                velocity.y = 0f;
            }
        }
    }
}
