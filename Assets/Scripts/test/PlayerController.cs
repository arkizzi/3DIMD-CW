using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    private float wSpeed = 10.0f;
    private float wTurnSpeed = 20.0f;    
    private float rSpeed = 100.0f;
    private float rTurnSpeed = 100.0f;
    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       horizontalInput = Input.GetAxis("Horizontal");
       forwardInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //Move the Vehicle forward with inputs
            transform.Translate(Vector3.forward * Time.deltaTime * rSpeed * forwardInput);
            //rotates the car based on hrizontal inputs
            transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * rTurnSpeed);
            //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);
        }
        else
        {
            //Move the Vehicle forward with inputs
            transform.Translate(Vector3.forward * Time.deltaTime * wSpeed * forwardInput);
            //rotates the car based on hrizontal inputs
            transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * wTurnSpeed);
        }

        //Move the Vehicle forward with inputs
        transform.Translate(Vector3.forward * Time.deltaTime * wSpeed * forwardInput);
       //rotates the car based on hrizontal inputs
       transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * wTurnSpeed);
       //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);
    }
}
