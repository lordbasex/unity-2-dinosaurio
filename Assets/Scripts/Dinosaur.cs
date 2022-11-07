using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur : MonoBehaviour
{
    [SerializeField] private float upForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float radius;

 
    private Rigidbody2D dinoRb;
    private Animator dinoAnimator;

    public GameObject JumpSound;
    public GameObject Die;

    private bool gameOverCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        dinoRb = GetComponent<Rigidbody2D>();
        dinoAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, ground);
        dinoAnimator.SetBool("isGrounded",isGrounded);
  
        //salto - https://docs.unity3d.com/es/530/ScriptReference/KeyCode.html
        if (gameOverCheck == false && Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                dinoRb.AddForce(Vector2.up * upForce);
                Instantiate(JumpSound);
            }
            
        }

        //Se presionó la tecla de flecha hacia abajo. -  https://docs.unity3d.com/es/530/ScriptReference/KeyCode.html
        if (gameOverCheck == false && Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isGrounded){
                dinoAnimator.SetBool("agacharse", true);
            }
        }
            
        //Se soltó la tecla de flecha hacia abajo. -  https://docs.unity3d.com/es/530/ScriptReference/KeyCode.html
        if (gameOverCheck == false && Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (isGrounded){
                dinoAnimator.SetBool("agacharse", false);
            }
        }
            
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOverCheck = true;
            Instantiate(Die);
            GameManager.Instance.ShowGameOverScreen();
            dinoAnimator.SetTrigger("Die");
            Time.timeScale = 0f;
            Debug.Log(gameOverCheck);
        }    
    }
}
