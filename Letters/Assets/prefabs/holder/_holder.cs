using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _holder : MonoBehaviour
{
    [SerializeField]
    private Sprite bg_image;
    [HideInInspector]
    public bool off;
    [HideInInspector]
    public GameObject contant=null;
    // Start is called before the first frame update
    void Start()
    {
        off = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = bg_image;
    }
    private void Update()
    {
        if (contant != null)
        {
            contant.GetComponent<_letter_event>().off = off;
        }
    }
}
