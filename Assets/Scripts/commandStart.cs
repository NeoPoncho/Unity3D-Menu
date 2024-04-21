using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class commandStart : MonoBehaviour
{
    public RawImage rawImage;
    public float opacidadeRaw;
    private bool keyPressed = false;
    public FollowPlayer camera;
    private float turnCam;
    public TMP_Text text;


    void Start()
    {
        rawImage.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        Time.timeScale = 0;
        rawImage.color = new Color(1f, 1f, 1f, opacidadeRaw);
        text.color = new Color(1f, 1f, 1f, opacidadeRaw);
        turnCam = camera.turnSpeed;
        camera.turnSpeed = 0;
    }

    void Update()
    {
        if (Input.anyKey && !keyPressed)
        {
            rawImage.gameObject.SetActive(false);
            text.gameObject.SetActive(false);
            Time.timeScale = 1;
            camera.turnSpeed = turnCam;
        }

    }
}
