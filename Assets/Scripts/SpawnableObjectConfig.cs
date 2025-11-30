using UnityEngine;

[CreateAssetMenu(fileName = "SpawnableObjectConfig", menuName = "ScriptableObjects/SpawnableObjectConfig")]
public class SpawnableObjectConfig : ScriptableObject
{
    public SpawnableObject obj;
    [SerializeField] private int score;
    [SerializeField] private ParticleSystem destroyParticle;

    public void Setup(SpawnableObject obj)
    {
        obj.score = score;
        obj.destroyParticle = destroyParticle;
    }
}
