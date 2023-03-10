using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//used "https://www.youtube.com/watch?v=1tuQqm3gQBI&ab_channel=DaniKrossing" for character controller and added my own features to it.


public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float moveSpeed, jumpForce, moveHorizontal, moveVertical;
    private bool isJumping;
    public GameObject DeathMenu;
    public GameObject PauseMenu;
    public GameObject camera;
    public AudioClip weaponClip;
    public SpriteRenderer sprite;
    public Animator anim;
    public int health;
    public GameObject heart1;
    public GameObject heart2;
    private float nextInvincible;
    private float fireRate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 2f;
        jumpForce = 40f;
        isJumping = false;
        Time.timeScale = 1f;
        health = 2;
        heart1.SetActive(true);
        heart2.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        Vector2 position = transform.position;
        Vector2 direction =  Vector2.down;
        //this text is for pausing the game
        if (Input.GetKeyDown("escape") && Time.timeScale != 0)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PauseMenu.SetActive(true);
        }
        camera.transform.position = new Vector3(this.transform.position.x, 0, -10);
        
        if(isJumping == true)
        {
            moveSpeed = 1f;
        }
        else
        {
            moveSpeed = 2f;
        }
    }
    void FixedUpdate()
    {
        if ((moveHorizontal > 0.1f || moveHorizontal < -.1f))
        {
            anim.SetBool("isWalking", true);
            if(moveHorizontal > .1f)
            {
                sprite.flipX = false;
            }
            else if (moveHorizontal < -.1f)
            {
                sprite.flipX = true;
            }
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if (moveVertical > 0.1f && isJumping != true)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
        if (moveVertical < -.1f || (this.transform.position.x > 70.5 && this.transform.position.y > 4.3) || (this.transform.position.x > 109f && this.transform.position.x < 113.7f))
        {
            this.transform.localScale = new Vector3(1, .5f, 0);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 0);
        }
    } 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Platform" || collider.gameObject.tag == "spikes")
        {
            isJumping = false;
            
        }
        if (collider.gameObject.tag == "spikes" || collider.gameObject.tag == "enemy")
        {
            if (Time.time > nextInvincible) {
                nextInvincible = Time.time + fireRate;
                StartCoroutine(colorChange());
                health -= 1;
                heart2.SetActive(false);
                if (health < 1) {
                    heart1.SetActive(false);
                    die();
                }
            }
            
        }
        
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag != "enemy")
        {
            isJumping = true;
        }
    }
    public void die()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        DeathMenu.SetActive(true);
        Debug.Log("Player Has Died");
    }
    IEnumerator colorChange()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(.5f);
        sprite.color = Color.white;
    }
}