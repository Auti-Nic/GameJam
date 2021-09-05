using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;

    [SerializeField] private float acceleration;

    private Rigidbody rb;
    public int TTL = 30;
    public int shield = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (rb.velocity.magnitude < maxSpeed)
            rb.AddForce(movement * acceleration * Time.deltaTime);

        // Player Rotation
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, 1000))
        {
            Vector3 lookPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookPoint);
        }
    }

    public void TakeDamage(int damage)
    {
        if (shield > 0) {
            if (shield > damage)
            {
                //can defense all the damage from by shield
                shield -= damage;
            } else if (shield < damage) {
                shield = 0;
                TTL -= (damage - shield);
            }
        }


        // Removed delay for now so it feels more responsive
        //if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
        if (TTL <= 0) GameOver();
    }

    public void GameOver()
    {
        //TODO: Game Over Scene
        Debug.Log("Game Overr");

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "bullet" || other.tag == "enemy")
            TakeDamage(1);
    }
}
