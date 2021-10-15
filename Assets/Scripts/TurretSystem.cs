using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TurretSystem : MonoBehaviour
{
    public static TurretSystem instance;

    [Header("Essential Game Objects")]
    public TextMesh degreesText;
    public AudioSource gunSource;

    public GameObject GUN;
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

    private float shotRotation = 0f;

    // Start is called before the first frame update
    Camera _camera = null;  // cached because Camera.main is slow, so we only call it once.

    void Start()
    {
        instance = this;
        _camera = Camera.main;
        Flash.SetActive(false);
        shotData.text = "Angle: 0\nDistance: 0";

        SetTurretDistance(25);


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
            if (Input.GetKeyUp("space"))
                HandleShot();
        }

    }


    public void SetAmbientVolume(float newValue)
    {
        UniStorm.UniStormManager.Instance.SetWeatherVolume(newValue);
        UniStorm.UniStormManager.Instance.SetAmbienceVolume(newValue);
    }

    IEnumerator RandomShots()
    {
        HandleAutoMouseRotation();
        SetTurretDistance();
        HandleShot();
        SetAmbientVolume(0);
        while (processingShot)
            yield return new WaitForSeconds(1f);

    }

    IEnumerator RemoveShot(GameObject shot)
    {

        yield return new WaitForSeconds(gunSource.clip.length);
        Destroy(shot);
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
        StartCoroutine(RemoveShot(newShot));
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
        azimuth = Mathf.RoundToInt(GetAngle());
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
        return Mathf.RoundToInt(Vector3.Distance(Ring.transform.position, GUN.transform.position));
    }

    void HandleAutoMouseRotation()
    {
        float newRot = Random.Range(0, 359);
        Ring.transform.Rotate(Vector3.up, newRot);
        shotRotation = GetAngle();
        degreesText.text = Mathf.RoundToInt(shotRotation).ToString();
    }

    void HandleMouseRotation()
    {
        float rotSpeed = 5;

        float rotY = Input.GetAxis("Mouse X") * rotSpeed;

        Ring.transform.Rotate(Vector3.up, rotY);

        degreesText.text = System.Math.Round(GetAngle(), 2).ToString();
    }


    public void SetTurretDistance()
    {
        float minDistance = 0f;
        float maxDistance = 150f;
        float distance = Random.Range(minDistance, maxDistance);
        GUN.transform.localPosition = new Vector3(distance, 3, 0);

    }
    public void SetTurretDistance(float distance)
    {
        GUN.transform.localPosition = new Vector3(distance, 3, 0);

    }

}
