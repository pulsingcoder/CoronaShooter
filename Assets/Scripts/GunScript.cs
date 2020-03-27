
using System;
using System.Collections;
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
    public AudioSource gunSource;
    public int maxAmmo = 10;
    public int currentAmmo =10;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Animator Gunanimator;
  //  [SerializeField] Joystick gunJoystick;
    [SerializeField] Button buttonShoot;
    // Update is called once per frame

    
    void Start()
    {
        if (currentAmmo == -1)
        {
            currentAmmo = maxAmmo;
        }
    }

    void OnEnable()
    {
        
        if (currentAmmo == -1)
        {
            currentAmmo = maxAmmo;
        }
        isReloading = false;
        Gunanimator.SetBool("Reloading", false);
        
    }
   

    

    private IEnumerator Reload()
    {

        isReloading = true;
        Gunanimator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);
        Gunanimator.SetBool("Reloading", false);
       // yield return new WaitForSeconds(0.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    public void Shoot()
    {
        if (isReloading)
        {
            return;
        }
        currentAmmo--;
        if (currentAmmo <= 0)
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(Reload());
                return;
            }
        }
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
            gunSource.Play();
        }
    }
}
