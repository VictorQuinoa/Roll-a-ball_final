using UnityEngine;

public class Ramp : MonoBehaviour
{
    [Tooltip("Dirección fija de lanzamiento (debe estar normalizada)")]
    public Vector3 launchDirection = new Vector3(1, 0.5f, 0);

    public float launchForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Asegurar que solo afecte a la pelota
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 finalLaunchDirection = launchDirection.normalized;
                rb.velocity = Vector3.zero;
                rb.AddForce(finalLaunchDirection * launchForce, ForceMode.Impulse);
                Debug.Log("Pelota lanzada en dirección: " + finalLaunchDirection);
            }
        }
    }
}
