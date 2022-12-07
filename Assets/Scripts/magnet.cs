using UnityEngine;

public class magnet : MonoBehaviour
{
    public float speed = 0.1f;
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Pickup"))
        {
            other.transform.position = 
                Vector3.MoveTowards(other.transform.position, transform.position,speed*Time.deltaTime);
        }
    }
    
}
