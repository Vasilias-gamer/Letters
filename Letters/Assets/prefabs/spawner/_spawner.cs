using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _spawner : MonoBehaviour
{
    [SerializeField]
    private Transform holder_axis_pos;//where holders should spwan
    [SerializeField]
    private Transform letters_axis_pos;//where letters should spwan
    [SerializeField]
    private Transform correct_letters_axis_pos;//where correct ans to be shone
    [HideInInspector]
    public GameObject[] random_letters;//holds random letters
    [HideInInspector]
    public GameObject[] letters_in_order;//holds correct letters
    [SerializeField]
    private GameObject holder;//holder prefab
    [HideInInspector]
    public GameObject[] holders;//holder array
    // Start is called before the first frame update
    void Start()
    {
        random_letters = insiate_letters(random_letters,letters_axis_pos);
        letters_in_order = insiate_letters(letters_in_order,correct_letters_axis_pos);
        holders = insiate_holder(random_letters.Length, holder);
        foreach(GameObject letter in letters_in_order)
        {
            letter.gameObject.SetActive(false);
        }

    }
    
    public GameObject[] insiate_letters(GameObject[] letters,Transform axis_pos)
    {
        int no_of_letters = letters.Length;
        float[] letters_pos = item_pos(no_of_letters);
        for (int i = 0; i < no_of_letters; i++)
        {
            letters[i] = Instantiate(letters[i], new Vector3(letters_pos[i] + axis_pos.position.x, axis_pos.position.y, 0), axis_pos.rotation);
            letters[i].transform.parent = this.transform.parent;
        }
        return letters;
    }//spwan the letters

    public GameObject[] insiate_holder(int no_of_holder, GameObject holder)
    {
        GameObject[] holders = new GameObject[no_of_holder];
        float[] holders_pos = item_pos(no_of_holder);
        for (int i = 0; i < no_of_holder; i++)
        {
            holders[i] = Instantiate(holder, new Vector3(holders_pos[i] + holder_axis_pos.position.x, holder_axis_pos.position.y, transform.position.z), transform.rotation);
            holders[i].transform.parent = this.transform.parent;

            GameObject temp = Instantiate(letters_in_order[i], new Vector3(holders_pos[i] + holder_axis_pos.position.x, holder_axis_pos.position.y, transform.position.z), transform.rotation);
            temp.transform.GetChild(0).GetComponent<SpriteRenderer>().color=new Color(1f,1f,1f,.5f);
            temp.transform.GetChild(0).gameObject.SetActive(false);
            temp.transform.parent = holders[i].transform;
        }
        return holders;
    }//spwan holders
    
    public float[] item_pos(int no_of_items)//gives the position where to be spwaned
    {

        //float screen_width = Screen.width / 100;
        float gap = 1; //screen_width / no_of_items;
        //gap *= 1.6f;

        float [] items_pos=new float[no_of_items];
        float temp = (gap * (no_of_items - 1));
        temp /= 2;
        for(int i=0;i<no_of_items;i++)
        {
            items_pos[i] = gap * i-temp;
        }
        return items_pos;
    }
}
