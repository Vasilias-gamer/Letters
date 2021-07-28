using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class _controller : MonoBehaviour
{
    [SerializeField]
    private GameObject Question_frame;//taking prefab of question frame
    [SerializeField]
    //private List<GameObject> Objects;//complete set of objects
    private List<GameObject> objects;//copy of "Objects" for each gameplay
    private Queue<GameObject> obj = new Queue<GameObject>();//obj holds "objects" of wrong answered

    [SerializeField]
    private GameObject next;//holds next butten
    [SerializeField]
    private _data_manager data_Manager;//manage data to be stored and loaded

    private GameObject active_frame;//"question frame" currently running
    private GameObject next_frame;//"question frame" to be shown next

    public Data data;//store data from each iteration
    private bool data_updated;//is tha data ia updated for current frame
    private int random_no_of_objs;//no of random frame to be shown before next wrong frame

    public struct Data
    {
        public GameObject obj;//holdes object of current frame
        public string correct_answer;//holdes currect answer of current frame
        public string answer_got;//holdes answer of current frame
        public int score; //holdes score of current frame
        public int no_of_hints;//holdes no of hint used in current frame
        public int total_correct_ans;//holdes total currect answer
        public int total_incorrect_ans;//holdes total incorrect answer
    }
    private void Start()//for initial value
    {
        //objects = Objects;
        random_no_of_objs = Random.Range(1,6);
        data_updated = false;
        data_Manager.Load();
        data.answer_got = data_Manager.data.child_ans;
        data.correct_answer = data_Manager.data.correct_ans;
        data.score = data_Manager.data.total_score;
        data.no_of_hints = data_Manager.data.hint_used;
        data.total_correct_ans = data_Manager.data.total_correct_ans;
        data.total_incorrect_ans = data_Manager.data.total_incorrect_ans;
        next.SetActive(false);
        active_frame = Instantiate(prepar_question_frame(get_random_obj()), transform.position, transform.rotation);
    }

    private void Update()//for each frame update
    {
        _question_frame current_frame= active_frame.GetComponent<_question_frame>();//get script "_question_frame" of current frame

        if (!current_frame.isactive)//is current frame active
        {
            if(current_frame.data_updated==false)//is data if current frame is updated
                update_data(current_frame);//if not update

            next.SetActive(true);//activate next button
        }
        else
        {
            next.SetActive(false);//if frame is active next button off 
            data_updated = false;//data is not updated
        }

        if (Input.GetKeyDown(KeyCode.Escape))//if escape, get to previous scene
            SceneManager.LoadScene(0);
    }

    private GameObject next_obj()//to get next object for the frame
    {
        if (!data.answer_got.Equals(data.correct_answer))
        {
            put_obj(data.obj);
        }
        return get_obj();
    }

    public void on_click_back()//get back to perivious scene
    {
        SceneManager.LoadScene(0);
    }

    
    public void onClick_next()//get to next question
    {
        //data.obj = active_frame.GetComponent<_question_frame>().obj;
        Destroy(active_frame);
        if (objects.Count > 0)
            objects.Remove(data.obj);
        else
            SceneManager.LoadScene(0);
        active_frame = Instantiate(prepar_question_frame(next_obj()), transform.position, transform.rotation);//instantiating next question frame 
    }

    private void update_data(_question_frame frame)//update frame data
    {
        //if (data.obj != frame.obj)
        {
            data.correct_answer = frame.data.correct_answer;
            data.answer_got = frame.data.answer_got;
            data.obj = frame.data.obj;
            data.score += frame.data.score;
            data.no_of_hints += frame.data.no_of_hints;

            Debug.Log("score-"+data.score);
            if (data.correct_answer == data.answer_got)
            {
                data.total_correct_ans++;

                //Debug.Log("score-" + data.total_correct_ans);
            }
            else
                data.total_incorrect_ans++;
            save_data();
            frame.data_updated = true;
        }
    }

    private void save_data()//save data in json file
    {
        data_Manager.data.correct_ans = string.Concat(data_Manager.data.correct_ans, data.correct_answer);
        data_Manager.data.correct_ans = string.Concat(data_Manager.data.correct_ans, ",");
        data_Manager.data.child_ans = string.Concat(data_Manager.data.child_ans, data.answer_got);
        data_Manager.data.child_ans = string.Concat(data_Manager.data.child_ans, ",");
        data_Manager.data.total_score = data.score;
        data_Manager.data.hint_used = data.no_of_hints;
        data_Manager.data.total_correct_ans = data.total_correct_ans;
        data_Manager.data.total_incorrect_ans = data.total_incorrect_ans;
        data_Manager.Save();
    }

    private GameObject prepar_question_frame(GameObject obj)//preparing the next frame 
    {
        GameObject frame = Question_frame;
        frame.GetComponent<_question_frame>().obj = obj;
        return frame;
    }

    private GameObject get_random_obj()//gives random objects for the frame
    {
        return objects[(int)Random.Range(0, objects.Count)];
    }
    private GameObject get_obj()//gives next object
    {
        if (obj.Count != 0 && random_no_of_objs == 0)
        {
            random_no_of_objs = Random.Range(1, 6);
            return obj.Dequeue();
        }
        else
        {
            random_no_of_objs--;
            return get_random_obj();
        }
    }
    private void put_obj(GameObject value)//store wrong answered object
    {
        obj.Enqueue(value);
    }
}