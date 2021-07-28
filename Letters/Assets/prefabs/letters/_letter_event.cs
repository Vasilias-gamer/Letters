using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _letter_event : MonoBehaviour
{
    [SerializeField]
    private int speed = 7;
    [HideInInspector]
    public string letter;
    [HideInInspector]
    public GameObject target;
    private GameObject mechanism;
    [HideInInspector]
    private float offset;
    public bool off;
    private bool move;
    [SerializeField]
    private float collider_size = 5f;

    private void Start()
    {
        target = transform.parent.gameObject;
        for (int i = 0; i < transform.parent.parent.childCount; i++)
        {
            if (transform.parent.parent.GetChild(i).name.Equals("Mechanism"))
            {
                mechanism = transform.parent.parent.GetChild(i).gameObject;
                break;
            }
        }
        letter = gameObject.name;
        move = false;
        off = false;
        offset = transform.localPosition.y;
        GetComponent<BoxCollider2D>().size = new Vector2(collider_size,collider_size);
    }

    private void Update()
    {

        if(move)
        {
            get_moving();
        }
    }

    private void OnMouseDown()
    {
        if (!move && !off)
        {
            if (target == transform.parent.gameObject)
            {
                target = mechanism.GetComponent<_mechanism>().taget_holder;
                mechanism.GetComponent<_mechanism>().hint_button_time=Time.time;
                target.GetComponent<_holder>().contant = gameObject;
            }
            else
            {
                target.GetComponent<_holder>().contant = null;
                target = transform.parent.gameObject;
            }
            move = true;
        }
    }

    private void get_moving()
    {
        float step = speed * Time.deltaTime;
        Vector3 target_pos = new Vector3(target.transform.position.x, target.transform.position.y + offset, target.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position,target_pos, step);
        if (transform.position == target_pos)
        {
            move = false;
        }
    }
}
