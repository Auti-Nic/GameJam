using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;

    [SerializeField] private float acceleration;


    //Dash
    public float dashDuration = 0.2f;
    public int dashForce = 50;
    private Rigidbody rb;
    
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
        
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (rb.velocity.magnitude < maxSpeed)
            rb.AddForce(movement * acceleration * Time.deltaTime);
        
        GetComponentInChildren<Animator>().SetFloat("Speed",rb.velocity.magnitude);
        
      
        // Player Rotation
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, 1000))
        {
            Vector3 lookPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookPoint);
        }

        if(Input.GetKeyDown("space"))
        {
            StartCoroutine(Dash());
            
            //transform.Translate(Vector3.forward * 0 );
        }

        
    }

    IEnumerator Dash()
    {
        
        rb.AddForce(transform.forward * dashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;

    } 
    
}
