using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _score : MonoBehaviour
{
    [HideInInspector]
    public int got_score=0;
    [HideInInspector]
    public int total_score=0;
    private void Start()
    {
        //display_score(got_score, total_score);
    }
    public int display_score(float got_score, float total_score)
    {
        int score = Mathf.RoundToInt((got_score / total_score) * 3);
        for (int i=0; i<score; i++)
        {
            this.gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
        return score;
    }
}
