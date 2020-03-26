
using System;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactForce = 30f;
  //  [SerializeField] Joystick gunJoystick;
    [SerializeField] Button buttonShoot;
    // Update is called once per frame
    void Update()
    {
       // buttonShoot.onClick.AddListener(TaskOnClick);
     
    }

    private void TaskOnClick()
    {
        Shoot();
    }

    public void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        // shoot direction in we're facing fpscam.transform.forward
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy !=null)
            {
                enemy.TakeDamage(damage);

            }
            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 2f);
            if (hit.rigidbody !=null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }
}
