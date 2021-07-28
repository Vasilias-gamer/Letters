using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _show_data : MonoBehaviour
{
    [SerializeField]
    public Text score;
    [SerializeField]
    public Text hint_used;
    [SerializeField]
    public Text total_correct_ans;
    [SerializeField]
    public Text total_incorrect_ans;

    [SerializeField]
    private _data_manager data_Manager;
    private Data data;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    public struct Data
    {
        public string correct_answer;
        public string answer_got;
        public int score;
        public int no_of_hints;
        public int total_correct_ans;
        public int total_incorrect_ans;
    }

    private void Start()
    {
        data_Manager.Load();
        data.score = data_Manager.data.total_score;
        data.no_of_hints = data_Manager.data.hint_used;
        data.total_incorrect_ans = data_Manager.data.total_incorrect_ans;
        data.total_correct_ans = data_Manager.data.total_correct_ans;

        score.text ="Score: "+data.score.ToString();
        hint_used.text ="Hint used: "+data.no_of_hints.ToString();
        total_correct_ans.text ="Correct ans: "+data.total_correct_ans.ToString();
        total_incorrect_ans.text ="Wrong ans: "+data.total_incorrect_ans.ToString();
    }

    public void on_click_back()
    {
        SceneManager.LoadScene(0);
    }
}
