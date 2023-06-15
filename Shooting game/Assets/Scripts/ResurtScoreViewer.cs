using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResurtScoreViewer : MonoBehaviour
{
    private TextMeshProUGUI textResultScore;

    private void Awake() {
        
        textResultScore = GetComponent<TextMeshProUGUI>();
        // stage에서 저장한 점수를 불러와서 score 변수에 저장
        int score = PlayerPrefs.GetInt("Score");
        // textResult
    }
}
