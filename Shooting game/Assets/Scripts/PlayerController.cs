using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private string          nextSceneName;
    [SerializeField]
    private StageData       stageData;
    [SerializeField]
    //private KeyCode     keyCodeAttack = KeyCode.Space;
    private KeyCode         keyCodeAttack = KeyCode.Mouse0;
    [SerializeField]
    private KeyCode         keyCodeBoom = KeyCode.Z;
    private bool            isDie = false;
    private Movement2D      movement2D;
    private Weapon          weapon;
    private Animator        animator;
    [SerializeField]
    private FixedJoystick   joystick;
    private bool            isButtonB;
    [SerializeField]
    private float           Speed = 3.0f;

    private int score;
    public  int Score
    {
        // score 값이 음수가 되지 않도록
        set => score = Mathf.Max(0, value);
        get => score;        
    }    

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();
        weapon     = GetComponent<Weapon>();
        animator   = GetComponent<Animator>();
        joystick   = FindObjectOfType<FixedJoystick>();
    }

    private void Update()
    {
        Move();
        // 플레이어가 사망 애니메이션 재생 중일 때 이동, 공격이 불가능하게 설정
        if ( isDie == true ) return;

        // 방향 키를 눌러 이동 방향 설정
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        // 조이스틱을 이용하여 이동 방향 설정
        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        movement2D.MoveTo(new Vector3(x, y, 0));

        // 공격 키를 Down/Up으로 공격 시작/종료 (키보드)
        if (Input.GetKeyDown(keyCodeAttack) )
        {
            weapon.StartFiring();
        }
        else if (Input.GetKeyUp(keyCodeAttack) )
        {
            weapon.StopFiring();
        }

        //공격 키를 Down/Up으로 공격 시작/종료 (마우스)
        if (Input.GetKeyDown(KeyCode.Mouse0) )
        {
            weapon.StartFiring();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) )
        {
            weapon.StopFiring();
        }

        // 폭탄 키를 눌러 폭탄 생성 (키보드)
        if ( Input.GetKeyDown(keyCodeBoom))
        {
            weapon.StartBoom();
        }
    }

    public void boom()
    {
        /*폭탄 키를 눌러 폭탄 생성 (마우스)
        if ( Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.StartBoom();
        }*/
        
        //폭탄 키를 눌러 폭탄 생성 (버튼)
        if (!isButtonB)
        {
            weapon.StartBoom();
        }
    }

    private void Move()
    {
        // 키보드 방향 키를 눌러 이동 방향 설정
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
    }
    
    private void LateUpdate() 
    {
        // 플레이어 캐릭터가 화면 범위 바깥으로 나가지 못하도록 함
        transform.position = new Vector3
        (Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }

    public void OnDie() 
    {
        // 이동 방향 초기화
        movement2D.MoveTo(Vector3.zero);
        // 사망 애니메이션 재생
        animator.SetTrigger("onDie");
        // 적들과 충돌하지 않도록 충돌 박스 삭제
        Destroy(GetComponent<CircleCollider2D>());
        // 사망 시 키 플레이어 조작 등을 하지 못하게 하는 변수
        isDie = true;
    }

    public void OnDieEvent() 
    {
        // 디바이스에 획득한 점수 score 저장
        PlayerPrefs.SetInt("Score", score);
        // 플레이어 사망 시 nextSceneName 씬으로 이동
        SceneManager.LoadScene(nextSceneName);
    }
}


/*
 * File : PlayerController.cs
 * Desc
 * : 플레이어 캐릭터에 부착해서 사용
 *
 * Functions
 *
 */
