using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 movement;
    public float speed;
    public float rotationSpeed;
    public Vector3 mousePos;
    public Camera camera;
    public Animator animator;
    public bool isRunning;
    public bool isShooting;
    //public Rigidbody rb;
   
    public CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        //cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        GetPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        MouseLook();
        Shooting();
        
        if(isRunning){
            animator.SetBool("IsRunning", true);
        }
        else{
            animator.SetBool("IsRunning", false);
        }
        if(isShooting){
            animator.SetBool("IsShooting", true);
        }
        else{
            animator.SetBool("IsShooting", false);
        }
    }

        private void FixedUpdate() {
        //Movement();
    }

    void Movement(){
        //Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical);
        transform.position += movement * speed * Time.deltaTime;
        Vector3 rotationMovement = new Vector3(horizontal, 0, 0);
        if (movement.magnitude > 0.01f) { // Use a small threshold to determine if moving
            isRunning = true;
        } else {
            isRunning = false;
        }


        //rotation
        /*if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);            
        }*/

    }
    
    void MouseLook(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position); // Assuming player is standing on XZ plane (y = 0)
        float distance;

        
        if (groundPlane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance);
            // 2. Determine the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(point - transform.position);
            // Optional: Make the rotation only about the Y axis
            targetRotation.x = 0;
            targetRotation.z = 0;
            // 3. Apply the rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10); // Smoothly rotate towards the target point
        }
    }

    void Shooting(){
        if(Input.GetButton("Fire1")){
            isShooting = true;
        }
        else{
            isShooting = false;
        }
    }

    public void GetPlayerData()
    {
        speed =  DataManager.LoadPlayerData().playerSpeed;
    }

    public void PlayerHit(){
        GameManager.Instance.GameOver();
    }

}
