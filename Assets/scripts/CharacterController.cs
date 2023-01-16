using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//used "https://www.youtube.com/watch?v=1tuQqm3gQBI&ab_channel=DaniKrossing" for character controller and added my own features to it.


public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float moveSpeed, jumpForce, moveHorizontal, moveVertical;
    private bool isJumping;
    private bool isRunning;

    public GameObject DeathMenu;
    public GameObject PauseMenu;

    public GameObject camera;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 2f;
        jumpForce = 40f;
        isJumping = false;
        Time.timeScale = 1f;
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
    }
    void FixedUpdate()
    {
        if ((moveHorizontal > 0.1f || moveHorizontal < -.1f))
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }
        if (moveVertical > 0.1f && isJumping != true)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);

        }
        if (moveVertical < -.1f || (this.transform.position.x > 70.5 && this.transform.position.y > 4.3) || (this.transform.position.x > 109f && this.transform.position.x < 113.7f))
        {
            this.transform.localScale = new Vector3(1, 1, 0);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 2, 0);
        }
        if (isRunning == true)
        {
            moveSpeed = 1f;
        }
        else
        {
            moveSpeed = 2f;
        }
        
    } 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Platform")
        {
            isJumping = false;
            isRunning = false;
        }
        if(collider.gameObject.tag == "spikes" || collider.gameObject.tag == "enemy")
        {
            die();
        }
    }
    private void OnTriggerExit2D()
    {
        isJumping = true;
        isRunning = true;
    }
    public void die()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        DeathMenu.SetActive(true);
        Debug.Log("Player Has Died");
    }
}