using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSystem : MonoBehaviour
{
    public AudioSource gunSource;
    public GameObject Turret;
    public GameObject Base;
    public GameObject Ring;
    public GameObject Flash;
    public GameObject Shot;
    public GameObject Muzzle;

    // Start is called before the first frame update
    Camera _camera = null;  // cached because Camera.main is slow, so we only call it once.

    void Start()
    {
        _camera = Camera.main;
        Flash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        HandleMouseRotation();
        if (Input.GetMouseButtonUp(0))
            HandleShot();





    }

    void HandleShot()
    {
        gunSource.Play();
        StartCoroutine(ShotFlash());
        GameObject newShot = Instantiate(Shot, Muzzle.transform.position, Quaternion.LookRotation(Muzzle.transform.forward));
        newShot.transform.parent = null;
    }

    IEnumerator ShotFlash()
    {
        Flash.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Flash.SetActive(false);
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


}
