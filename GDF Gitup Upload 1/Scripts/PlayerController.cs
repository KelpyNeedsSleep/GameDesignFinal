using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed;
    public float standSpeed;
    public float crouchSpeed;
    private Rigidbody2D myRigidBody;
    private CircleCollider2D standingCollider;
    private PolygonCollider2D crouchingCollider;
    public float jumpSpeed;

    private Animator myAnim;

    //Checks
    public Transform groundCheck;
    public Transform roofCheck;
    public float checkRadius; // Radius of groundcheck
    public LayerMask whatIsGround; // What layer can the player jump 
    public bool isGrounded; //Is the player grounded
    public bool canUncrouch;
    public bool isCrouching;

    //Health
    public int curHealth;
    public int maxHealth = 100;


    public float knockback; //power
    public float knockbackLength; //how long we going to get pushed back
    public float knockbackCount;
    public bool knockFromRight; //direction where we going to get pushed


    private LevelManager theLevelManager;

    //Coin
    public int coinValue;

    //Sound

    public AudioClip JumpSound;
    public AudioClip CoinSound;
    private AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        theLevelManager = FindObjectOfType<LevelManager>();
        myAnim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        curHealth = maxHealth;
        standingCollider = GetComponent<CircleCollider2D>();
        crouchingCollider = GetComponent<PolygonCollider2D>();
        isCrouching = false;
        moveSpeed = standSpeed;
    }


    void Update()
    {
        //Jump

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
            source.PlayOneShot(JumpSound, 1F);
        }

        //Crouch

        canUncrouch = !Physics2D.OverlapCircle(roofCheck.position, checkRadius, whatIsGround);

        if(isGrounded){
            if (Input.GetButtonDown("Crouch")){
                if(!isCrouching){
                    //source.PlayOneShot(CrouchSound, 1F);
                    crouchingCollider.enabled = true;
                    standingCollider.enabled = false;
                    moveSpeed = crouchSpeed;
                    Debug.Log("crouching");
                    isCrouching = true;
                }
                else if(canUncrouch){
                    //source.PlayOneShot(CrouchSound, 1F);
                    standingCollider.enabled = true;
                    crouchingCollider.enabled = false;
                    moveSpeed = standSpeed;
                    Debug.Log("standing");
                    isCrouching = false;
                }
                else{
                    Debug.Log("cant't uncrouch");
                }
            }
        }
        else if(isCrouching && canUncrouch){
            standingCollider.enabled = true;
            crouchingCollider.enabled = false;
            moveSpeed = standSpeed;
            Debug.Log("standing");
            isCrouching = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

            myAnim.SetFloat("Speed", Mathf.Abs(myRigidBody.velocity.x));
            myAnim.SetBool("Grounded", isGrounded);


        if (knockbackCount <= 0)

        // Player moving right
        {
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);  
                transform.localScale = new Vector2(1f, 1f);
            }

            // Player moving left

            else if (Input.GetAxisRaw("Horizontal") < 0f)
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

    //Kill border
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Kill Plane")
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        else if (other.tag == "Coin")
        {
            theLevelManager.AddCoins(coinValue);
            source.PlayOneShot(CoinSound, 1F);
            Destroy(other.gameObject);
        }


    }

}



