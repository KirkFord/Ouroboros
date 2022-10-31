using UnityEngine;

public class GroundSegment : MonoBehaviour
{
    private GroundController gc;
    [SerializeField] private int length = 1;
    private void Start()
    {
        gc = GameObject.Find("GROUND").GetComponent<GroundController>();
        Destroy(gameObject, length * 25);
    }

    private void OnDestroy()
    {
        gc.RemoveSegment(gameObject);
    }

    public int GetLength()
    {
        return length;
    }
    
}
