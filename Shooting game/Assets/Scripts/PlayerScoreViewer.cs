using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScoreViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerController PlayerController;
    private TextMeshProUGUI  textScore;

    private void Awake() 
    {
        textScore = GetComponent<TextMeshProUGUI>();        
    }

    private void Update()
    {
        // text UI에 현재 점수 정보를 업데이트
        textScore.text = "Score "+PlayerController.Score;
    }
}


/*
 * File : PlayerScoreViewer.cs
 * Desc
 * : 플레이어의 점수 정보를 Text UI에 업데이트
 *
 */