using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretSystem : MonoBehaviour
{
    public static TurretSystem instance;
    public AudioSource gunSource;
    public GameObject Turret;
    public GameObject Base;
    public GameObject Ring;
    public GameObject Flash;
    public GameObject Shot;
    public GameObject Muzzle;

    public Text shotData;

    public float distance;
    public float azimuth;
    public int shot_count;

    public string newShotString = string.Empty;


    // Start is called before the first frame update
    Camera _camera = null;  // cached because Camera.main is slow, so we only call it once.

    void Start()
    {
        instance = this;
        _camera = Camera.main;
        Flash.SetActive(false);
        shotData.text = "Angle: 0\nDistance: 0";
    }

    // Update is called once per frame
    void Update()
    {

        HandleMouseRotation();
        if (Input.GetKeyUp("space"))
            HandleShot();





    }

    void HandleShot()
    {
        shot_count++;
        print(shot_count);
        ProcessShotString();
        BasicAudio.instance.ListenerStart();
        gunSource.Play();

        StartCoroutine(ShotFlash());
        StartCoroutine(WaitforClipToEnd());
        GameObject newShot = Instantiate(Shot, Muzzle.transform.position, Quaternion.LookRotation(Muzzle.transform.forward));
        newShot.transform.parent = null;

    }

    IEnumerator ShotFlash()
    {
        Flash.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Flash.SetActive(false);
    }

    IEnumerator WaitforClipToEnd()
    {
        while (gunSource.isPlaying)
            yield return new WaitForSeconds(0.5f);
        // gunsource has stopped    
        BasicAudio.instance.ListenerStop(newShotString);
    }
    void ProcessShotString()
    {
        azimuth = GetAngle();
        distance = GetDistance();
        shotData.text = $"Angle: {azimuth}\nDistance: {distance}m";
        string prefix = "ShotAzimuth_";
        string azi = Mathf.RoundToInt(azimuth).ToString();
        newShotString = prefix + azi;

    }

    float GetAngle()
    {
        // raw rotation
        float angle = Ring.transform.rotation.eulerAngles.y;

        angle = (angle > 360) ? angle - 360 : angle;

        return angle;
        //float rValue = Vector3.Dot(Ring.transform.position.normalized, Base.transform.position.normalized);
        // return Mathf.Acos(rValue) * Mathf.Rad2Deg;
    }


    float GetDistance()
    {
        return Mathf.RoundToInt(Vector3.Distance(Ring.transform.position, Base.transform.position));
    }

    void HandleMouseRotation()
    {
        float rotSpeed = 50;

        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        //float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

        // if (Input.GetKey("space"))
        //     Turret.transform.Rotate(Vector3.up, rotX);
        // else
        Ring.transform.Rotate(Vector3.up, rotX);
        //Ring.transform.Rotate(Vector3.right, rotY);
    }


    public void SetTurretDistance(float distance)
    {
        float minDistance = -25f;
        float newDistance = minDistance - distance;
        Base.transform.localPosition = new Vector3(newDistance, 0, 0);

    }

}
