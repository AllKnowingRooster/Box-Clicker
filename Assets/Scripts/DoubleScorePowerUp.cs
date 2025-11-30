using System.Collections;
using UnityEngine;

public class DoubleScorePowerUp : PowerUp
{

    public override void UsePowerUp()
    {
        GameManager.instance.NotifyObserver(UserAction.DoubleScoreClicked);
        if (GameManager.instance.doubleScoreCoroutine != null)
        {
            StopCoroutine(GameManager.instance.doubleScoreCoroutine);
            GameManager.instance.doubleScoreCoroutine = null;
        }
        GameManager.instance.doubleScoreCoroutine = StartCoroutine(DoubleMultiplier());
        gameObject.SetActive(false);
    }

    private IEnumerator DoubleMultiplier()
    {
        float duration = 20.0f;
        GameManager.instance.multiplier = 2;
        MainGameCanvasManager.instance.UpdateMultiplierText();
        while (duration > 0.0f)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        GameManager.instance.multiplier = 1;
        MainGameCanvasManager.instance.UpdateMultiplierText();
        GameManager.instance.doubleScoreCoroutine = null;
    }


}
