using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _mechanism : MonoBehaviour
{
    private GameObject[] holders;// letter holders
    private GameObject[] correct_letters;//currect letters array
    [SerializeField]
    private GameObject hint;//hint button
    [HideInInspector]
    public GameObject taget_holder;//next holder for letter
    [HideInInspector]
    public bool display_hint;//to display hint or not
    [SerializeField]
    private GameObject correct_popup;// popup correct if answer correct 
    [SerializeField]
    private GameObject wrong_popup;//popup if answer wong
    private bool answer_complet; // si the answer complete
    private string answer_recived = null;//what ans recived
    private bool correct_ans;//is the answer correct
    private _question_frame question_frame;//question frame script
    private float hint_time;//time after hint turned off
    private bool hint_button_active;//is hint button active
    private int score;//current score
    private int hint_count;//total no of hint used in this frame
    public float hint_button_time;//timeafter hint button activated
    private float correct_ans_time;//currect ans gets off by this timee
    private bool correct_ans_on_screen;
    private bool correct_ans_shown;

    // Start is called before the first frame update
    void Start()
    {
        answer_recived = null;
        question_frame = GetComponentInParent<_question_frame>();
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).name.Equals("Spawner"))
            {
                holders = transform.parent.GetChild(i).GetComponent<_spawner>().holders;
                correct_letters = transform.parent.GetChild(i).GetComponent<_spawner>().letters_in_order;
                break;
            }
        }
        
        display_hint = false;
        hint_button_time=Time.time;
        hint_time = 0;
        hint_button_active = false;
        hint_count = 0;
        correct_ans = false;
        answer_complet = false;
        correct_ans_time = 0;
        correct_ans_on_screen = false;
        correct_ans_shown = false;
        correct_popup.SetActive(false);
        wrong_popup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!answer_complet)
        {
            if (Time.time-hint_button_time > 3 && hint_button_active == false)
            {
                activate_hint(true);
            }

            if (display_hint)
            {
                if (hint_time > 0)
                {
                    if (Time.time - hint_time > 2)
                    {
                        hint_time = 0;
                        show_hint(false);
                    }
                }
                else
                {
                    hint_time = Time.time;
                    if(hint_count<correct_letters.Length)
                        hint_count++;
                    show_hint(true);
                }
            }

            answer_recived = get_ans();
        }
        else
        {
            correct_ans = check_answer();
            display_score();
            if (!correct_ans)
            {
                if (correct_ans_shown == false)
                {
                    if (!correct_ans_on_screen)
                    {
                        correct_ans_time = Time.time;
                        display_correct_ans(true);
                        correct_ans_on_screen = true;
                    }
                    else if (Time.time - correct_ans_time > 2 && correct_ans_on_screen == true)
                    {
                        correct_ans_time = 0;
                        display_correct_ans(false);
                        correct_ans_on_screen = false;
                        correct_ans_shown = true;
                    }
                }
            }
            if(correct_ans || correct_ans_shown)
                update_data();
        }
    }

    private void activate_hint(bool active)//activate hint button
    {
        hint.SetActive(active);
        hint_button_active = true;
    }

    private void show_hint(bool active)//show the hint
    {
        for(int j=0; j < hint_count; j++)
        {
            for(int i = 0; i < correct_letters[j].transform.childCount; i++)
            {
                Destroy(correct_letters[i].transform.GetChild(0).GetComponent<_letter_event>());
                correct_letters[j].SetActive(active);
            }
        }
        display_hint = active;
    }

    private string get_ans()//get the answers user enters
    {
        bool flag = true;
        string ans = "";
        foreach (GameObject holder in holders)
        {
            if (holder.GetComponent<_holder>().contant == null)
            {
                taget_holder = holder;
                flag = false;
                break;
            }
            ans = string.Concat(ans, holder.GetComponent<_holder>().contant.name);
        }
        if (flag && ans!="")
        {
            answer_complet = true;
            foreach (GameObject holder in holders)
            {
                holder.GetComponent<_holder>().off = true;
            }
            activate_hint(false);
        }
        return ans;
    }

    private bool check_answer()//chect answer is correct or not
    {
        score = 0;
        if (answer_recived == question_frame.question.answer)
        {
            correct_popup.SetActive(true);
            score = question_frame.question.answer.Length;
            return true;
        }
        else
        {
            for (int i = 0; i < question_frame.question.answer.Length; i++)
            {
                if (answer_recived[i] == question_frame.question.answer[i])
                    score++;
            }
            wrong_popup.SetActive(true);
            return false;
        }

        
    }

    private void display_score()// display the score star
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).name.Equals("score"))
            {
                transform.parent.GetChild(i).GetComponent<_score>().display_score(score, question_frame.question.answer.Length);
            }
        }
    }

    private void display_correct_ans(bool active)//display the correct answer
    {
        foreach(GameObject letters in correct_letters)
        {
            letters.SetActive(active);
        }
    }

    private void update_data()//update the data in question frame
    {
        question_frame.data.obj = question_frame.question.obj;
        question_frame.data.correct_answer = question_frame.question.answer;
        question_frame.data.answer_got = answer_recived;
        question_frame.data.score = score;
        question_frame.data.no_of_hints = hint_count;
        question_frame.isactive = false;
    }
}
