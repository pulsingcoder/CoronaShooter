using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
public class GPS : MonoBehaviour
{
    public bool isUpdating;
    public float latitude;
    public float longitude;

    IEnumerator coroutine;
    public static GPS Instance { set; get; }
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
         Instance = this;
        //  DontDestroyOnLoad(gameObject);
        coroutine = updateGPS();
        if (!Input.location.isEnabledByUser)
        {
            latitude = -2;
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
            Debug.Log("User has not enables GPS");
            yield break;
        }
        Input.location.Start(5,2);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            latitude = -1;
            maxWait--;
        }
        if (maxWait <= 0)
        {
            Debug.Log("Timed out");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("unable to determine device location");
            yield break;
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        StartCoroutine(coroutine);
        isUpdating = true;


        //StartCoroutine(StartLocationService());
    }

    private IEnumerator updateGPS()
    {
        float updateTime = 3f;
        WaitForSeconds update_Time = new WaitForSeconds(updateTime);
        while(true)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            yield return update_Time;
        }
       

    }
    /*
    private IEnumerator StartLocationService()
    {
        /*
        if (!Input.location.isEnabledByUser)
        {
            latitude = -2;
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
            Debug.Log("User has not enables GPS");
            yield break;
        }
        Input.location.Start(0.01f,0.01f);
      
        int maxWait = 20;
        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            latitude = -1;
            maxWait--;
        }
        if (maxWait <=0)
        {
            Debug.Log("Timed out");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("unable to determine device location");
            yield break;
        }
        
        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        isUpdating = true;
        
     
        yield break;


    }


    // Update is called once per frame
    void Update()
    {
        if (!isUpdating)
        {


            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        }
    }
    */
    void stopGPS()
    {
        Input.location.Stop();
        StopCoroutine(coroutine);

    }
    void OnDisable()
    {
        stopGPS();
    }
}
