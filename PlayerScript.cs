using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : Strength_Provider 
{
    
    private Rigidbody2D body;
    private bool grounded;
    private Animator anime;
    public Text EndText;
    public Text GameOverText;
    public GameObject PlayAgain;
    // Start is called before the first frame update
    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float horiInput = Input.GetAxis("Horizontal");
        body.velocity=new Vector2( horiInput* speed ,body.velocity.y);

        //Flip player turn reight and left
        if(horiInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horiInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Jumping 
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            
                Jump();

        }

        anime.SetBool("run", horiInput != 0);
        anime.SetBool("grounded", grounded);
        
    }
    public override void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anime.SetTrigger("jump");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
       
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "End")
        {
            EndText.gameObject.SetActive(true);
            Time.timeScale = 0;
            PlayAgain.SetActive(true);

        }
        if (collision.gameObject.tag == "block")
        {
            GameOverText.gameObject.SetActive(true);
            Time.timeScale = 0;
            PlayAgain.SetActive(true);

        }
    }
}
