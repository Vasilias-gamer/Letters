using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _object_contaner : MonoBehaviour
{
    [HideInInspector]
    public GameObject obj;
    // Start is called before the first frame update
    public void spwan(GameObject obj)
    {

        Debug.Log("o" + obj);
        Instantiate(obj, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
