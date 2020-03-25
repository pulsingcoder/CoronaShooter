using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGpsText : MonoBehaviour
{
    float m_lat;
    float m_lon;
    Rigidbody m_Rigidbody;
    public Text coordinates;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_lat = GPS.Instance.latitude;
        m_lon = GPS.Instance.longitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_lat!=GPS.Instance.latitude || m_lon != GPS.Instance.longitude)
        {
            /*
            Vector3 angles = UnityEditor.TransformUtils.GetInspectorRotation(transform);
            Vector3 moveDirection = new Vector3(Mathf.Cos(angles.x),0, Mathf.Sin(angles.x));
            m_Rigidbody.AddForce(moveDirection * 5, ForceMode.Impulse);
            m_lat = GPS.Instance.latitude;
            m_lon = GPS.Instance.longitude;*/
        }
        else
        {
           // m_Rigidbody.velocity = new Vector3(0, 0, 0);
        }
        coordinates.text = "LAT: " + GPS.Instance.latitude.ToString() + " LON:" + GPS.Instance.longitude.ToString(); 
    }
}
