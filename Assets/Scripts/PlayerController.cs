using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [Header("FoodCounts")]
    public int foodCount;
    public Text foodText;

    [Header("Speeds")]
    public float movementSpeed;
    public float jumpForce;
    
    [Header("GroundCheck")]
    public bool isGrounded;
    public GameObject groundCheckA;
    public GameObject groundCheckB;
    private Rigidbody2D rb2D;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        rb2D.velocity = new Vector2(x*movementSpeed * Time.deltaTime, 0);

        if(isGrounded == true){
            rb2D.AddForce(new Vector2(0, jumpForce*y*Time.deltaTime));
        }
        
        foodText.text = foodCount.ToString();

        if(Input.GetKeyDown(KeyCode.R)){
            ResetScene();
        }
    }
    void FixedUpdate(){

        isGrounded = Physics2D.OverlapArea(groundCheckA.transform.position, groundCheckB.transform.position);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Food"){
            foodCount = foodCount +1;
            Destroy(col.gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Die"){
            ResetScene();
        }
    }

    void ResetScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
