using UnityEngine;

public class WallMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 1.5f * Time.deltaTime);

    }
}
