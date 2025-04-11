using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float moveSpeed;
    [SerializeField] private float standSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float dashSpeed;
    private Rigidbody2D myRigidBody;
    private PolygonCollider2D standingCollider;
    private BoxCollider2D crouchingCollider;
    [SerializeField] private float jumpSpeed;
    private Animator myAnim;
    [SerializeField] private GameObject projectile;

    //Checks
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform roofCheck;
    [SerializeField] private float checkRadius; // Radius of groundcheck
    [SerializeField] private LayerMask whatIsGround; // What layer can the player jump 
    [SerializeField] private bool isGrounded; //Is the player grounded
    [SerializeField] private bool canUncrouch;
    [SerializeField] private bool isCrouching;
    [SerializeField] private int jumpCount;
    [SerializeField] private int maxJump;

    //Health
    [SerializeField] private int curHealth;
    [SerializeField] private int maxHealth;


    [SerializeField] private float knockback; //power
    public float knockbackLength; //how long we going to get pushed back
    public float knockbackCount;
    public bool knockFromRight; //direction where we going to get pushed

    //Sound
    [SerializeField] private AudioClip JumpSound;
    [SerializeField] private AudioClip CrouchSound;
    private AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        curHealth = maxHealth;
        standingCollider = GetComponent<PolygonCollider2D>();
        crouchingCollider = GetComponent<BoxCollider2D>();
        isCrouching = false;
        moveSpeed = standSpeed;
    }


    void Update()
    {
        //Jump

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            if(!isGrounded)
            {
                Instantiate(projectile, groundCheck.position, Quaternion.Euler(0,0, 180));
            }
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
            //source.PlayOneShot(JumpSound, 1F);
            jumpCount++;
        }

        //Crouch
        canUncrouch = !Physics2D.OverlapCircle(roofCheck.position, checkRadius, whatIsGround);
        if(isGrounded)
        {
            if (Input.GetButtonDown("Crouch")){
                if(!isCrouching){
                    //source.PlayOneShot(CrouchSound, 1F);
                    Crouch();
                }
                else if(canUncrouch){
                    //source.PlayOneShot(CrouchSound, 1F);
                    Uncrouch();
                }
                else{
                    Debug.Log("cant't uncrouch");
                }
            }
            jumpCount = 0;
        }
        else if(isCrouching && canUncrouch)
        {
            Uncrouch();
        }
    }

    private void Uncrouch()
    {
        standingCollider.enabled = true;
        crouchingCollider.enabled = false;
        moveSpeed = standSpeed;
        isCrouching = false;
    }

    private void Crouch()
    {
        crouchingCollider.enabled = true;
        standingCollider.enabled = false;
        moveSpeed = crouchSpeed;
        isCrouching = true;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {

        myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);
        myAnim.SetBool("Crouched", isCrouching);


        if (knockbackCount <= 0)
        // Player moving right
        {
            if(Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);  
                transform.localScale = new Vector2(1f, 1f);
            }

            // Player moving left

            else if(Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRigidBody.velocity = new Vector2(-moveSpeed, myRigidBody.velocity.y);
                transform.localScale = new Vector2(-1f, 1f);

            }

            // No slide
            else
            {
                myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
            }
        }

        //knockback
        else
        {
            if (knockFromRight)

                myRigidBody.velocity = new Vector2(-knockback, knockback);

            if (!knockFromRight)
                myRigidBody.velocity = new Vector2(knockback, knockback);
            knockbackCount -= Time.deltaTime;

        }

        //Health
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;

        }
        if (curHealth <= 0)
        {
            Die();
        }
    }
    
    //Death
    void Die()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        curHealth = maxHealth;
    }

    //Player Damage
    public void Damage(int dmg)
    {
        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("Player_redflash");
    }

    //  Parent player to moving platform
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
            transform.parent = other.transform;
    }


    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "MovingPlatform")
            transform.parent = null;
    }
}



