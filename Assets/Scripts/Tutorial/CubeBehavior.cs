using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeBehaviour : MonoBehaviour
{
    
    public int xrotateSpeed=125;
    public int zrotateSpeed=125;
    public int yrotateSpeed=125;

    //Quaternion newrotation = new Quaternion(.9f, .5f, .6f, 1);
    Quaternion newrotation = new Quaternion();
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = newrotation;
    }

    // Update is called once per frame
    void Update()
    {   // Quaternion.Euler(x, y, z), using time delta to multiply the spin
        Quaternion newrotation = Quaternion.Euler(xrotateSpeed * Time.deltaTime, yrotateSpeed * Time.deltaTime, zrotateSpeed * Time.deltaTime);
        transform.rotation = transform.rotation * newrotation;
    }
}
