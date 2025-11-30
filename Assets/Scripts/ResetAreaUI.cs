using UnityEngine;

public class ResetAreaUI : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Vector3 curPos = other.transform.position;
        curPos.y = 7;
        curPos.x = Random.Range(-1.0f, 1.0f);
        other.gameObject.transform.position = curPos;
        other.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
    }
}
