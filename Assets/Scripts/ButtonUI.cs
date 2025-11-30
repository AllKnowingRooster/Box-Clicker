using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Image buttonImage;
    [SerializeField] private Color idleButtonText;
    [SerializeField] private Color idleButtonBackground;
    [SerializeField] private Color activeButtonText;
    [SerializeField] private Color activeButtonBackground;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.NotifyObserver(UserAction.Click);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = activeButtonText;
        }

        if (buttonImage != null)
        {
            buttonImage.color = activeButtonBackground;
        }
        GameManager.instance.NotifyObserver(UserAction.Hover);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = idleButtonText;
        }

        if (buttonImage != null)
        {
            buttonImage.color = idleButtonBackground;
        }
    }
}
