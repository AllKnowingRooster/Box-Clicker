using System.Collections;
using UnityEngine;

public class SlowPowerUp : PowerUp
{
    public override void UsePowerUp()
    {
        GameManager.instance.NotifyObserver(UserAction.SlowCLicked);
        if (GameManager.instance.slowCoroutine != null)
        {
            StopCoroutine(GameManager.instance.slowCoroutine);
            GameManager.instance.slowCoroutine = null;
        }
        GameManager.instance.slowCoroutine = StartCoroutine(SlowCoroutine());
        gameObject.SetActive(false);
    }

    private IEnumerator SlowCoroutine()
    {
        float duration = 15.0f;
        Physics.gravity = new Vector3(0, -0.2f, 0);
        while (duration > 0.0f)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        Physics.gravity = new Vector3(0, -1.5f, 0);
        GameManager.instance.slowCoroutine = null;
    }

}
