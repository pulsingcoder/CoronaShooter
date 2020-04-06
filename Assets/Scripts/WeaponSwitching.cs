
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] Button swapButton; 
    public int selectedWeapon = 0;
    public AudioSource swapSource;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();

    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                weapon.gameObject.transform.GetComponent<GunScript>().currentAmmo = 10;
                int index = weapon.gameObject.transform.GetComponent<GunScript>().index;
                weapon.gameObject.transform.GetComponent<GunScript>().currentInfection[index] = 9;
                weapon.gameObject.transform.GetComponent<GunScript>().makeInfected();
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    // Update is called once per frame
    public void SwapWeapon()
    {
        swapSource.Play();
        int prevSelectedWeapon = selectedWeapon;
        if (selectedWeapon >= transform.childCount -1)
        {
            selectedWeapon = 0;
        }
        else
        {
            selectedWeapon++;
        }
        if (prevSelectedWeapon != selectedWeapon)
        {
            
            SelectWeapon();


        }
    }
}
