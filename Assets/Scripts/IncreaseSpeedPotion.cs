using UnityEngine;

public class IncreaseSpeedPotion : MonoBehaviour
{
    private bool playerIsClose = false;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Inventory inventory;

    private void Update()
    {
        if (playerIsClose && playerController != null && playerController.pickPotion.WasPressedThisFrame())
        {
            if (inventory != null)
                inventory.AddSpeedPotion();

            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        playerIsClose = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        playerIsClose = false;
    }
}
