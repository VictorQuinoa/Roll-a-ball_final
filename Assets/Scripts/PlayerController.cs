using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private Animator anim;
    private Renderer playerRenderer;
    private bool isInvulnerable = false;

    public float speed = 10f;
    public float invulnerabilityDuration = 5f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);

        anim = GetComponent<Animator>();
        playerRenderer = GetComponent<Renderer>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = -movementVector.x;
        movementY = -movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 14)
        {
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;
        transform.Translate(dir * speed);

        UpdateAnimationAndColor();
    }

    void UpdateAnimationAndColor()
    {
        if (isInvulnerable)
        {
            anim.SetBool("isInvulnerable", true);
            anim.SetBool("isMoviendose", false);
           
            playerRenderer.material.color = Color.yellow;
            return;
        }

        else if (isMoviendose())
        {
            anim.SetBool("isMoviendose", true);
            
            anim.SetBool("isInvulnerable", false);
            playerRenderer.material.color = Color.blue;
        }
        else
        {
            anim.SetBool("isMoviendose", false);
        
            anim.SetBool("isInvulnerable", false);
            playerRenderer.material.color = Color.red;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }

        if (other.gameObject.CompareTag("PickUp_I"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            StartCoroutine(BecomeInvulnerable());
        }

        if (other.gameObject.CompareTag("Enemigo") && !isInvulnerable)
        {
            rb.gameObject.SetActive(false);
            Destroy(rb);
            countText.text = "Has perdido";
        }

        if (other.gameObject.CompareTag("Agua"))
        {
            rb.gameObject.SetActive(false);
            Destroy(rb);
            countText.text = "Aprende a nadar";
        }
    }

    IEnumerator BecomeInvulnerable()
    {
        isInvulnerable = true;
        UpdateAnimationAndColor();
        Debug.Log("Jugador invulnerable");

        yield return new WaitForSeconds(invulnerabilityDuration);

        isInvulnerable = false;
        UpdateAnimationAndColor();
        Debug.Log("Jugador vuelve a ser vulnerable");
    }

    bool isMoviendose()
    {
        return rb.velocity.magnitude > 0.1f;
    }
}