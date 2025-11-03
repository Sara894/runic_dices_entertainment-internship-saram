using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private HealthController playerHealth;
    [SerializeField] private GameObject healthCanvas;
    [SerializeField] private GameObject InventoryCanvas;

    private void Update()
    {
        if (playerController.openInventoryCanvas.WasPressedThisFrame())
        {
            InventoryCanvas.SetActive(!InventoryCanvas.activeSelf);
            healthCanvas.SetActive(InventoryCanvas.activeSelf ? false : true);
            if (InventoryCanvas.activeSelf)
            {
                Time.timeScale = 0f;
                playerController.enabled = false;
            }
            else
            {
                Time.timeScale = 1f;
                playerController.enabled = true;
            }
        }
    }

    public void UseHealthPotion()
    {
        playerInventory.UseHealthPotion(playerHealth);
    }

    public void UseSpeedPotion()
    {
        playerInventory.UseSpeedPotion(playerController);
    }

    public void UseImmunityPotion()
    {
        playerInventory.UseImmunityPotion(playerHealth);

    }
}
