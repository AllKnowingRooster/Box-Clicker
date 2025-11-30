using UnityEngine;

public class Deadzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
        if (GameManager.instance.lives > 0 && other.gameObject.CompareTag("Box"))
        {
            GameManager.instance.lives--;
            MainGameCanvasManager.instance.ChangeHeartSpriteColor(GameManager.instance.lives);
        }
    }
}
