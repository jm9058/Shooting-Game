using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScoreViewer2 : MonoBehaviour
{
    private TextMeshProUGUI textResultScore2;

    private void Awake() 
    {        
        textResultScore2 = GetComponent<TextMeshProUGUI>();
        // stage에서 저장한 점수를 불러와서 score 변수에 저장
        int score2 = PlayerPrefs.GetInt("Score2");
        // textResultScore UI에 점수 갱신
        textResultScore2.text = "Score2 " + score2;
    }
}
