using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    [Header("Vida")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("Spawn")]
    public Transform spawnPoint;

    private bool isDead;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;

        if (HealthUI.Instance != null)
            HealthUI.Instance.UpdateBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        HealthUI.Instance.UpdateBar(currentHealth, maxHealth);

        StartCoroutine(DamageManager.Instance.DamageSequence());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Respawn()
    {
        CharacterController cc = GetComponent<CharacterController>();

        cc.enabled = false;

        transform.position = spawnPoint.position;

        cc.enabled = true;
    }
    void Die()
    {
        isDead = true;

        Debug.Log("Game Over");

        // Aquí luego puedes abrir pantalla de derrota
    }
}