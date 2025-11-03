using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem;
public class PotionTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Tooltip Setup")]
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Vector3 offset = new Vector3(10f, -10f, 0f);

    public enum PotionType { Health, Speed, Immunity }
    public PotionType potionType;

    private void Update()
    {
        if (tooltipPanel.activeSelf)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            tooltipPanel.transform.position = mousePos + (Vector2)offset;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!tooltipPanel.transform.root.gameObject.activeInHierarchy)
            return;

        tooltipPanel.SetActive(true);
        tooltipPanel.transform.SetAsLastSibling();

        switch (potionType)
        {
            case PotionType.Health:
                tooltipText.text = "Health Potion: " + inventory.healthPotions + " left";
                break;
            case PotionType.Speed:
                tooltipText.text = "Speed Potion: " + inventory.speedPotions + " left";
                break;
            case PotionType.Immunity:
                tooltipText.text = "Immunity Potion: " + inventory.immunityPotions + " left";
                break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipPanel.SetActive(false);
    }
}
