using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretSystem : MonoBehaviour
{
    public static TurretSystem instance;

    [Header("Essential Game Objects")]
    public AudioSource gunSource;
    public GameObject Turret;
    public GameObject Base;
    public GameObject Ring;
    public GameObject Flash;
    public GameObject Shot;
    public GameObject Muzzle;
    public Text shotData;

    [Header("Core Variables")]
    public float distance;
    public float azimuth;
    public int shot_count;
    public string newShotString = string.Empty;

    [Header("Automation")]
    public int desiredShotCount;
    public bool processingShot;
    public bool automateShotData = false;

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
        // if we want to generate some shot data
        if (automateShotData)
        {
            if (!processingShot && shot_count < desiredShotCount)
                StartCoroutine(RandomShots());
        }
        else// otherwise we want to manually test the system
        {
            HandleMouseRotation();
            if (Input.GetMouseButtonUp(0))
                HandleShot();
        }

    }


    IEnumerator RandomShots()
    {
        HandleAutoMouseRotation();
        SetTurretDistance();
        HandleShot();
        while (processingShot)
            yield return new WaitForSeconds(1f);

    }


    void HandleShot()
    {
        processingShot = true;
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
            yield return new WaitForSeconds(gunSource.clip.length);
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
        string dist = System.Math.Round(distance, 2).ToString();
        newShotString = prefix + azi + "_distance_" + dist;
    }

    float GetAngle()
    {
        // raw rotation
        float angle = Ring.transform.rotation.eulerAngles.y;
        angle = (angle > 360) ? angle - 360 : angle;
        return angle;
    }


    float GetDistance()
    {
        return Mathf.RoundToInt(Vector3.Distance(Ring.transform.position, Base.transform.position));
    }

    void HandleAutoMouseRotation()
    {
        float rotX = Random.Range(0, 359);
        Ring.transform.Rotate(Vector3.up, rotX);
    }

    void HandleMouseRotation()
    {
        float rotSpeed = 50;

        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        //float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

        if (Input.GetKey("space"))
            Turret.transform.Rotate(Vector3.up, rotX);
        else
            Ring.transform.Rotate(Vector3.up, rotX);
        //Ring.transform.Rotate(Vector3.right, rotY);
    }


    public void SetTurretDistance()
    {
        float minDistance = -25f;
        float maxDistance = -180f;
        float distance = Random.Range(maxDistance, minDistance);

        float newDistance = minDistance - distance;
        Base.transform.localPosition = new Vector3(newDistance, 0, 0);

    }
    public void SetTurretDistance(float distance)
    {
        float minDistance = -25f;
        float newDistance = minDistance - distance;
        Base.transform.localPosition = new Vector3(newDistance, 0, 0);

    }

}
