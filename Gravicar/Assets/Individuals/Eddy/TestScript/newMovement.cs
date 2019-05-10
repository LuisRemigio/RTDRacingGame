using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newMovement : MonoBehaviour
{

    public float turnSpeed;
    public float moveSpeed;
    public float maxSpeed;
    public float torque = 70;
    Rigidbody rb;
    [SerializeField]
    private bool isGrounded;
    public AudioSource Flying;
    public GameObject escMenu;
    private bool escBool = false;
    public float breakMod = .98f;
    private bool canInput;
    private float startTimer = 3.5f;
    public GameObject Three;
    public GameObject Two;
    public GameObject One;
    public GameObject Go;
    public GameObject mirror;

    // Use this for initialization
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        Flying = GetComponent<AudioSource>();
        canInput = false;
        mirror.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escMenu.activeInHierarchy == false)
            {
                escMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else if (escMenu.activeInHierarchy == true)
            {
                escMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }

        startTimer -= Time.deltaTime;

        if (startTimer <= 3.2)
        {
            Three.SetActive(true);
        }   //Sets 3 on screen

        if (startTimer <= 2.1)
        {
            Three.SetActive(false);
            Two.SetActive(true);
        }   //Sets 2 on screen

        if (startTimer <= 1)
        {
            Two.SetActive(false);
            One.SetActive(true);
        }   //Sets 1 on screen

        if (startTimer <= 0)
        {
            One.SetActive(false);
            Go.SetActive(true);
            canInput = true;
        }  //Sets Go on screen, enables Movement

        if (startTimer <= -2)
        {
            Go.SetActive(false);
        } //Removes Go


    }
    private void FixedUpdate()
    {
        if (canInput == true)
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed;
            var z = Input.GetAxis("Vertical") * moveSpeed;


            if (isGrounded)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.velocity = rb.velocity * breakMod;
                }
            }
            rb.AddRelativeTorque(transform.up * x * torque, ForceMode.Acceleration);
            rb.AddForce(transform.forward * z, ForceMode.Acceleration);
            if (Input.GetKeyDown(KeyCode.W))
            {
                Flying.Play();
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                Flying.Stop();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Flying.Play();
                mirror.SetActive(true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                Flying.Stop();
                mirror.SetActive(false);
            }
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }


    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<Ground>())
    //    {
    //        isGrounded = true;
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<Ground>())
    //    {
    //        isGrounded = false;
    //    }
    //}

    public void resume()
    {
        escMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //This Method controls a public int manages the scenes in Unity
    public void LoadOrder(int LevelNumber)
    {
        SceneManager.LoadScene(LevelNumber);
    }
}