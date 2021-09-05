using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class TimeHealth : MonoBehaviour
{
    [SerializeField] private float maxTimeHealth;
    [SerializeField] private float timeHealthDepletionSpeed;
    [SerializeField] private float maxShield;
    [SerializeField] private float shieldRegenSpeed;

    [SerializeField] private ScoreScript scoreScript;
    [SerializeField] private Text shieldText;
    
    private float health;
    private float shield;
    
    public float Shield
    {
        get => shield;
        set
        {
            shield = value;
            
            if (shield > maxShield)
                shield = maxShield;

            shieldText.text = $"Shield: {shield}";
        }
    }

    public float Health
    {
        get => health;
        set
        {
            health = value;

            if (health > maxTimeHealth)
                health = maxTimeHealth;

            if (health <= 0)
                FindObjectOfType<GameManager>().EndGame();
        }
    }

    void Start()
    {
        Health = maxTimeHealth;
        Shield = maxShield;
    }

    void Update()
    {
        Health -= timeHealthDepletionSpeed * Time.deltaTime;

        scoreScript.scoreValue = Health;
        
        Shield += shieldRegenSpeed * Time.deltaTime;
    }
    
    public void TakeDamage(int damage)
    {
        if (Shield > 0)
        {
            if (Shield > damage)
            {
                //can defense all the damage from by shield
                Shield -= damage;
            }
            else if (Shield < damage)
            {
                Shield = 0;
                Health -= damage - Shield;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy_bullet"))
        {
            // TODO: Should be equal to bullet's damage field
            TakeDamage(1);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("enemy"))
        {
            TakeDamage(1);
        }
    }
}
