using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isJumping = false;
    public bool canMove = true;
    public bool canJump = false;
    public bool canShoot = false;
    public bool canSwim = false;
    public bool canDash = false;
    public bool isFacingRight = true;

    private Rigidbody2D myBody;
    private Animator anim;
    private Collision coll;
    private AudioSource audioSource;

    private GameManager gameManager;

    public float moveForce = 20f;
    public float jumpForce = 1f;
    public float maxVelocity = 4f;
    public float flyAmount = 1;

    public float shootSpeed = 10;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public AudioClip jumpSound;
    public AudioClip flySound;
    public AudioClip dieSound;
    // Start is called before the first frame update
    public enum PLAYER_STATE { GROUND, JUMP, FLY }
    public PLAYER_STATE currentMovementState = PLAYER_STATE.GROUND;

    public ParticleSystem deathParticle;
    public GameObject arrowPrefab;
    public Transform shootPosition;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collision>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        
        // Better Jump
        if (myBody.velocity.y < 0)
        {
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (myBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            myBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");

        if (x > 0 && canMove)
        {
            // Moving Right
            myBody.velocity = new Vector3(maxVelocity, myBody.velocity.y, 0f);
            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
            anim.SetBool("IsWalking", true);
            isFacingRight = true;
        }
        else if (x < 0 && canMove)
        {
            // Moving Right
            myBody.velocity = new Vector3(-maxVelocity, myBody.velocity.y, 0f);
            Vector3 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
            anim.SetBool("IsWalking", true);
            isFacingRight = false;
        }
        else
        {
            myBody.velocity = new Vector3(0f, myBody.velocity.y, 0f);
            anim.SetBool("IsWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1") && canMove)
        {
            if (coll.onGround && canJump)
            {
                anim.SetBool("IsJump", true);
                isJumping = true;
                myBody.velocity = new Vector3(myBody.velocity.x, jumpForce, 0f);
                audioSource.clip = jumpSound;
                audioSource.Play();
            }
        }

        if(Input.GetKeyDown(KeyCode.X) && canShoot)
        {
            anim.SetBool("IsShooting", true);
            StartCoroutine("ShootTheArrow");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enter " + collision.tag);
        if (collision.tag.Equals("Death"))
        {
            PlayerDeath();
            gameManager.GameOver();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Death"))
        {
            PlayerDeath();
        }

        if (collision.collider.tag.Equals("Ground") && isJumping)
        {
            Debug.Log("Go to Ground");
            anim.SetBool("IsJump", false);

        }

    }

    public void PlayerDeath()
    {
        if (deathParticle != null)
        {
            deathParticle.Play();
        }
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.5f, 1.5f, 14, 90, false, true);
        
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        canMove = false;
        audioSource.clip = dieSound;
        audioSource.Play();
        gameManager.GameOver();
    }

    IEnumerator ShootTheArrow()
    {
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.05f, 0.25f, 2, 5, false, false);

        canShoot = false;
        GameObject arrow = Instantiate(arrowPrefab, shootPosition.position, Quaternion.identity);
        GetComponent<AudioSource>().Play();
        Rigidbody2D myRigidBody = arrow.GetComponent<Rigidbody2D>();
        Vector2 currentVelocity = arrow.GetComponent<Rigidbody2D>().velocity;
        if (isFacingRight)
        {
            arrow.transform.position = shootPosition.position;
            arrow.transform.localScale = new Vector3(1, arrow.transform.localScale.y, arrow.transform.localScale.z);
            myRigidBody.velocity = new Vector2(shootSpeed, myRigidBody.velocity.y);
        }
        else
        {
            arrow.transform.position = shootPosition.position;
            arrow.transform.localScale = new Vector3(-1, arrow.transform.localScale.y, arrow.transform.localScale.z);
            myRigidBody.velocity = new Vector2(-shootSpeed, myRigidBody.velocity.y);
        }

        yield return new WaitForSeconds(1f);
        anim.SetBool("IsShooting", false);
        canShoot = true;


    }
}
