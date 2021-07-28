using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _question_frame : MonoBehaviour
{
    [SerializeField]
    private GameObject[] letters;//store letters
    [HideInInspector]
    public GameObject obj;//store current frame object
    [HideInInspector]
    public Question question;//store question data
    [HideInInspector]
    public Data data;//data structure variable
    public bool data_updated;//is data updated
    //private _mechanism mechanic;
    [HideInInspector]
    public bool isactive = false;//is this frame sctive

    public struct Data
    {
        public GameObject obj;//store obj
        public string correct_answer;//store currect answer
        public string answer_got;//store answer got
        public int score;//store score
        public int no_of_hints;//store no of hint used
    }

    public struct Question
    {
        public GameObject obj;//question object
        public GameObject[] random_letters;//random letters of object
        public string answer;//answer of question
    }
    private void Awake()
    {
        data_updated = false;
    }

    private void Start()
    {
        data_updated = false;
        build_question(obj);
        setup_question_frame();
        isactive = true;
        data.obj = obj;
        data.answer_got = question.answer;
    }


    private Question build_question(GameObject obj)//building question from object
    {
        question.obj = obj;
        question.random_letters = letter_Randomizer(obj.name);
        question.answer = obj.name;
        return question;
    }
    private GameObject[] letter_Randomizer(string answer)//randomizing the letters
    {
        int k = 0;
        GameObject[] random_letters = new GameObject[answer.Length];
        GameObject[] correct_order_letters = new GameObject[answer.Length];
        foreach (char i in answer)
        {
            foreach (GameObject j in letters)
            {
                string t = i.ToString();
                if (j.name.Equals(t.ToLower()) || j.name.Equals(t.ToUpper()))
                {
                    random_letters[k] = j;
                    correct_order_letters[k++] = j;
                }
            }
        }
        
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("Spawner"))
            {
                transform.GetChild(i).GetComponent<_spawner>().letters_in_order = correct_order_letters;
            }
        }

        for (int i = 0; i < random_letters.Length - 1; i++)
        {
            int j = (int)Random.Range(i, random_letters.Length);
            GameObject temp = random_letters[i];
            random_letters[i] = random_letters[j];
            random_letters[j] = temp;
        }
        foreach (GameObject g in letters)
        {
            if(g.transform.GetChild(0).gameObject.activeSelf==false)
            {
                Debug.Log(g.transform.GetChild(0).gameObject.activeSelf);
                g.transform.GetChild(0).gameObject.SetActive(true);
            }
            g.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
           
        }
        return random_letters;
    }
    private void setup_question_frame()//setup this frame with question built
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Equals("Spawner"))
            {
                transform.GetChild(i).GetComponent<_spawner>().random_letters = question.random_letters;
            }
            if (transform.GetChild(i).name.Equals("object_contaner"))
            {
                transform.GetChild(i).GetComponent<_object_contaner>().spwan(obj);
            }
        }
    }
   
}
