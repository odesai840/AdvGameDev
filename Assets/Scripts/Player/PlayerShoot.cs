using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public Image crosshair;
    public Image crosshairShooting;
    private bool isShooting = false;

    Transform cam;
    public float damage = 50f;
    public float range = 50f;
    public float fireRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        crosshairShooting.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateCrosshair()
    {
        if (isShooting)
        {
            crosshair.enabled = false;
            crosshairShooting.enabled = true;
        }
        else
        {
            crosshair.enabled = true;
            crosshairShooting.enabled = false;
        }
    }

    public void ProcessShoot(bool input)
    {
        if (input && !isShooting)
        {
            isShooting = true;
            UpdateCrosshair();
            StartCoroutine(ShootRoutine());
        }
        else if (!input && isShooting)
        {
            isShooting = false;
            UpdateCrosshair();
            StopCoroutine(ShootRoutine());
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, range))
        {
            Debug.DrawRay(cam.position, cam.forward * range, Color.red, 1f, true);
            Debug.Log(hit.collider.gameObject);
            SimpleAI enemy = hit.collider.gameObject.GetComponent<SimpleAI>();
            enemy?.TakeDamage(damage);
            GunEnemy gunEnemy = hit.collider.gameObject.GetComponent<GunEnemy>();
            gunEnemy?.TakeDamage(damage);
        }
    }

    IEnumerator ShootRoutine()
    {
        while(isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }
}
