using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public GameObject image;
    float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }
    public void End()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >=20f)
        {


            image.SetActive(true);
            
            //SceneManager.LoadScene(0);
        }
    }
}
