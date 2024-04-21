using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public GameObject cameraCenter;
    public GameObject cam;
    private float targetDistance;
    public float turnSpeed;
    private float rotX;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 0.0f;
    public float collisionSensitivity = 4.5f;

    Vector3 camMask;
    public LayerMask CamOcclusion;
    public float smooth;

    // Start is called before the first frame update
    void Start()
    {

        targetDistance = Vector3.Distance(transform.position, Player.transform.position);
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void LateUpdate()
    {


        //Mouse Inputs 
        float y = Input.GetAxis("Mouse X") * turnSpeed;
        rotX += Input.GetAxis("Mouse Y") * turnSpeed;



        rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);


        //Rotação da camera
        transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);

        smoothCamMethod();

        //movimentar a posição da camera
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position - (transform.forward * targetDistance), 0.5f);

        CheckCollision();
    }

    void smoothCamMethod()
    {

        smooth = 4f;
        transform.position = Vector3.Lerp(transform.position, Player.transform.position - (transform.forward * targetDistance), Time.deltaTime * smooth);
    }

    private void CheckCollision()
    {
        //declare a new raycast hit.
        RaycastHit wallHit = new RaycastHit();
        //linecast from your player (targetFollow) to your cameras mask (camMask) to find collisions.
        if (Physics.Linecast(Player.transform.position, cam.transform.position, out wallHit, CamOcclusion))
        {

            smooth = 10f;
            //the x and z coordinates are pushed away from the wall by hit.normal.
            //the y coordinate stays the same.
            transform.position = new Vector3(wallHit.point.x + wallHit.normal.x * 0.3f, transform.position.y, wallHit.point.z + wallHit.normal.z * 0.3f);
            Debug.DrawRay(transform.position, transform.position, Color.green);
        }

        if (Physics.Linecast(Player.transform.position, cam.transform.position, out wallHit, CamOcclusion))
        {

            smooth = 10f;
            //the x and z coordinates are pushed away from the wall by hit.normal.
            //the y coordinate stays the same.
            transform.position = new Vector3(wallHit.point.x + wallHit.normal.x * 0.3f, transform.position.y * 0.5f, wallHit.point.z + wallHit.normal.z * 0.3f);
            Debug.DrawRay(transform.position, transform.position, Color.green);
        }

    }

}
