
using UnityEngine;

public class PlayerMovoment : MonoBehaviour
{
    [SerializeField] private AudioClip JUMP;

    [SerializeField] private float speed;
    [SerializeField] private float  jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anime;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float HorizontalInput;

     private void Awake() {
        
            //I otrimav silki 

        body = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

        private void Update() 
        {
            HorizontalInput = Input.GetAxis("Horizontal");
            
            //Povertal personaja v livo v pravo

        if (HorizontalInput > 0.01f)
            transform.localScale = new Vector3(3,3,3);
              else if  (HorizontalInput < -0.01f)
                 transform.localScale = new Vector3(-3,3,3); 


          


            // Animacii

            anime.SetBool("Run", HorizontalInput !=0);
            anime.SetBool("Grounded",isGrounded());


            if (wallJumpCooldown > 0.2f)
            {
            body.velocity = new Vector2 (HorizontalInput * speed, body.velocity.y);
            
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            body.gravityScale = 7;
            
                   if (Input.GetKey(KeyCode.Space))
                      Jump();
            }
            else 
             wallJumpCooldown += Time.deltaTime;
        }


        private void Jump () 
        {
            if (isGrounded())
            {
             body.velocity = new Vector2(body.velocity.x, jumpPower);
             anime.SetTrigger("Jump");
             SoundManager.instance.PlaySound(JUMP);
            }
            else if (onWall() && isGrounded())
            {
                if(HorizontalInput == 0)
                {
                   body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0); 
                    transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x),transform.localScale.y,transform.localScale.z);
                }
                else
                    
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
                wallJumpCooldown = 0;
            }
        }

    
        private bool isGrounded(){
           
           RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
            return raycastHit.collider != null;
        }
         private bool onWall(){
           
           RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0, new Vector2 (transform.localScale.x,0),0.1f,wallLayer);
            return raycastHit.collider != null;
        }

        public bool canAttack ()
        {
            return HorizontalInput == 0 && isGrounded() && !onWall();
        }
}
