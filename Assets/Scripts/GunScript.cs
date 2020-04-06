
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    public int index;
    [SerializeField]
    AudioClip scoreAudio;
    [SerializeField]
    AudioClip remindAudio;
    int score = 0;
    [SerializeField]
    Text scoreText;
    public Slider infectSlider;
    private int maxInfection = 10;
    public float damage = 10f;
    public float range = 200f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactForce = 30f;
    public AudioSource gunSource;
    public int maxAmmo = 10;
    public int currentAmmo =10;
    public int[] currentInfection = { 10, 10 };
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Animator Gunanimator;
    LineRenderer line;
  //  [SerializeField] Joystick gunJoystick;
    [SerializeField] Button buttonShoot;
    // Update is called once per frame

    
    void Start()
    {
       
        if (currentAmmo == -1)
        {
            currentAmmo = maxAmmo;
        }
        line = new LineRenderer();
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

        if (gameObject.activeSelf)
        {
            currentInfection[index]--;
            makeInfected();
            if (currentInfection[index] <= 2)
            {
                gunSource.PlayOneShot(remindAudio);
            }
            
        }
        if (currentAmmo <= 0)
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(Reload());
                return;
            }
        }
        if (currentInfection[0]<=0 || currentInfection[1] <= 0)
        {
            score -= 5;
            scoreText.text = score.ToString();
            gunSource.PlayOneShot(scoreAudio);
        }
        if (muzzleFlash != null)
        {
            
            
                muzzleFlash.Play();
            
        }
        RaycastHit hit;
       // Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward);
     
       
        // shoot direction in we're facing fpscam.transform.forward
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy !=null)
            {
                score += 10;
                scoreText.text = score.ToString();
                enemy.TakeDamage(damage);
                gunSource.PlayOneShot(scoreAudio);
            }
            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 2f);
            if (hit.rigidbody !=null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
            if (!gunSource.isPlaying)
            {


                gunSource.Play();
            }
        }
    }

    public void makeInfected()
    {
        
        float temp = (1.0f * (maxInfection - currentInfection[index])) / maxInfection;
        infectSlider.value = temp;
    }
}
