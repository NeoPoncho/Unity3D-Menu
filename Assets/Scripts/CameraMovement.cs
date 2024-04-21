using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject character;
    public GameObject cameraCenter;
    public float yOffset = 1f;
    public float sensitivity = 3f;
    public Camera cam;

    public float scrollSensitivity = 5f;
    public float scrollDampening = 6f;

    public float zoomMin = 3.5f;
    public float zoomMax = 15f;
    public float zoomDefault = 10f;
    public float zoomDistance;

    public float collisionSensivity = 4.5f;

    private RaycastHit camhit;
    private Vector3 camDist;


    private void Start()
    {
        camDist = cam.transform.localPosition;
        zoomDistance = zoomDefault;
        camDist.z = zoomDistance;

        Cursor.visible = false;
    }

    public void Update()
    {
        cameraCenter.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + yOffset, character.transform.position.z);

        var rotation = Quaternion.Euler(cameraCenter.transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity / 2, cameraCenter.transform.rotation.eulerAngles.x - Input.GetAxis("Mouse X") * sensitivity, cameraCenter.transform.rotation.eulerAngles.z);

        cameraCenter.transform.rotation = rotation;
    }
}
