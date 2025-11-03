using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public bool isDead = false;
    public float health = 100;
    public bool canTakeDamage = true;

    [SerializeField] Image healthBar;

    void Start() => IntializeHealth();
    private void IntializeHealth()
    {
        health = 100;
    }

    public void DecreaseHealth(float value)
    {
        if (!canTakeDamage) return;
        if (health - value <= 0)
        {
            health = 0;
            isDead = true;
        }
        else
        {
            health = health - value;
            healthBar.fillAmount = health / 100f;
        }
    }

    public void IncreaseHealth()
    {
        if (health + 25 >= 100)
        {
            health = 100;
        }
        else
        {
            health += 25;
            healthBar.fillAmount = health / 100f;
        }
    }
}
