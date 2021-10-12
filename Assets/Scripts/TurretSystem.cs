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

    public bool processingShot;

    // Start is called before the first frame update
    Camera _camera = null;  // cached because Camera.main is slow, so we only call it once.

    void Start()
    {
        instance = this;
        _camera = Camera.main;
        Flash.SetActive(false);
        shotData.text = "Angle: 0\nDistance: 0";

        // start the first shot

    }

    // Update is called once per frame
    void Update()
    {
        // monitor successive shots
        //if (Input.GetKeyUp("space"))
        if (!processingShot)
            StartCoroutine(RandomShots());

    }


    IEnumerator RandomShots()
    {
        HandleMouseRotation();
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

    void HandleMouseRotation()
    {
        float rotX = Random.Range(0, 359);
        Ring.transform.Rotate(Vector3.up, rotX);
    }


    public void SetTurretDistance()
    {
        float distance = Random.Range(-80, -25);
        float minDistance = -25f;
        float newDistance = minDistance - distance;
        Base.transform.localPosition = new Vector3(newDistance, 0, 0);

    }

}
