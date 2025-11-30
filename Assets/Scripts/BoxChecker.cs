using UnityEngine;
using UnityEngine.InputSystem;

public class BoxChecker : MonoBehaviour
{
    Camera cam;
    public static Vector3 point;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        if (GameManager.instance.lives > 0 && Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray camRay = cam.ScreenPointToRay(mousePos);
            if (Physics.Raycast(camRay, out hit, Mathf.Infinity, LayerMask.GetMask("SpawnableObject")))
            {
                point = hit.point;
                SpawnableObject spawnableObject = hit.collider.gameObject.GetComponent<SpawnableObject>();
                spawnableObject.OnClick();
            }
        }
    }
}
