using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    
    void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }
}
