public class Box : SpawnableObject
{
    public override void OnClick()
    {
        base.OnClick();
        MainGameCanvasManager.instance.UpdateScore(score);
        GameManager.instance.NotifyObserver(UserAction.BoxClicked);
    }
}
