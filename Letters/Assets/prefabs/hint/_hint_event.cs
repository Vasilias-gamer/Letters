using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _hint_event : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void show_hint()
    {
        
        for (int i = 0; i < transform.parent.parent.childCount; i++)
        {
            if (transform.parent.parent.GetChild(i).name.Equals("Mechanism"))
            {
                if (transform.parent.parent.GetChild(i).GetComponent<_mechanism>().display_hint == false)
                    transform.parent.parent.GetChild(i).GetComponent<_mechanism>().display_hint = true;

                break;
            }
        }
    }
}
