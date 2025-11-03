using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public bool isDead = false;
    public float health = 100;

    [SerializeField] Image healthBar;

    void Start() => IntializeHealth();
    private void IntializeHealth()
    {
        health = 100;
    }

    public void DecreaseHealth(float value)
    {
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
}
