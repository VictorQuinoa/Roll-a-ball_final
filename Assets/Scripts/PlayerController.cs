using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
 
 private Rigidbody rb; 
 private int count;

 private float movementX;
 private float movementY;

 public float launchForce = 10f;

 public float speed = 0; 
 public TextMeshProUGUI countText;

 public GameObject winTextObject;


 void Start()
    {

        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winTextObject.SetActive(false);
    }
 

 void OnMove(InputValue movementValue)
    {

        Vector2 movementVector = movementValue.Get<Vector2>();


        movementX = - movementVector.x; 
        movementY = - movementVector.y; 
    }


 void setCountText()
 {
   countText.text = "Count:" + count.ToString();
   
   if(count >= 12)
   {
      winTextObject.SetActive(true);
   }
   
 }


 private void FixedUpdate() 
    {

        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);


       // rb.AddForce(movement * speed); 
       
       Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();
        
        dir *= Time.deltaTime;
        transform.Translate(dir * speed);
    }

     void OnTriggerEnter(Collider other) 
   {
     // other.gameObject.SetActive(false);
    

     if(other.gameObject.CompareTag("PickUp_I")){
        other.gameObject.SetActive(false);
       count ++;
        setCountText();
     }

     if(other.gameObject.CompareTag("PickUp")){
        other.gameObject.SetActive(false);
       count ++;
        setCountText();
     }

     if (other.gameObject.CompareTag("Enemigo")){
      rb.gameObject.SetActive(false);
      Destroy(rb);
      countText.text = "Has perdido";
     }

     if (other.gameObject.CompareTag("Agua")){
      rb.gameObject.SetActive(false);
      Destroy(rb);
      countText.text = "Aprende a nadar";
     }

    
}
private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rampa")) // Verifica si colisiona con una rampa
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
               Vector3 rampNormal = collision.contacts[0].normal;
                // Obtener la normal de la superficie de contacto (dirección de la rampa)
                 Vector3 launchDirection = Vector3.ProjectOnPlane(transform.forward, rampNormal).normalized;

                // Aplicar fuerza en la dirección de la rampa
                rb.AddForce(launchDirection.normalized * launchForce, ForceMode.Impulse);

                Debug.Log("Pelota lanzada en dirección: " + launchDirection);
            }
        }
    }
}
