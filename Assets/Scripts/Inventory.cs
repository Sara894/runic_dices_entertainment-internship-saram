using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int healthPotions = 0;
    public int speedPotions = 0;
    public int immunityPotions = 0;

    public void AddHealthPotion() => healthPotions++;
    public void AddSpeedPotion() => speedPotions++;
    public void AddImmunityPotion() => immunityPotions++;

    public bool UseHealthPotion(HealthController playerHealth)
    {
        if (healthPotions <= 0) return false;
        healthPotions--;
        playerHealth.IncreaseHealth();
        return true;
    }

    public bool UseSpeedPotion(PlayerController player)
    {
        if (speedPotions <= 0) return false;
        speedPotions--;
        player.AddMoveSpeed(2f);
        return true;
    }

    public bool UseImmunityPotion(HealthController playerHealth)
    {
        if (immunityPotions <= 0) return false;
        immunityPotions--;
        playerHealth.canTakeDamage = false;
        playerHealth.StartCoroutine(RestoreImmunity(playerHealth));
        return true;
    }

    private System.Collections.IEnumerator RestoreImmunity(HealthController playerHealth)
    {
        yield return new WaitForSeconds(5f);
        playerHealth.canTakeDamage = true;
    }
}
