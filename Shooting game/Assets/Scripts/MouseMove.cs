using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    [SerializeField]
    private int moveSpeed = 8; 	// 비행기의 이동 속도
    
    private Rigidbody2D rigid2D; 	// 비행기의 강체(Rigidbody)
    private bool isFar = false;		// 터치와 비행기의 거리 체크
    private Vector3 inputPosition;	// 터치의 월드 포지션
    private Vector2 moveDir; 		// 화면 터치 시 비행기 이동 방향
    
    void Start () 
    {
    	rigid2D = GetComponent<Rigidbody2D>();
    }
    
    void Update ()
    {
    	if(rigid2D == null)	// 강체가 없거나 터치가 없으면 리턴
        	return;
        
        moveDir = Vector2.zero; // 방향 초기화
        
        if(Input.GetMouseButtonDown(0))
        {
        	inputPosition = GetInputPosition(Input.mousePosition);
            
            if (Vector3.Distance(transform.position, inputPosition) > .2f)
            {
            	isFar = true; // 비행기와 터치 사이의 거리가 먼 상태
            }
        }
        
        if(Input.GetMouseButtonUp(0))
        {
        	isFar = false;
        }
        
        if(Input.GetMouseButton(0))
        {
        	inputPosition = GetInputPosition(Input.mousePosition);            
            if (isFar) // 거리가 먼 상태
            {
            	moveDir = GetDirection(transform.position, inputPosition);                
                // 터치와 비행기 거리를 체크
                isFar = (Vector3.Distance(transform.position, inputPosition) > .2f);
            }
            else // 터치에 가까운 상태
            {
            	transform.position = inputPosition;
            }
        }        
        rigid2D.velocity = moveDir * moveSpeed; // 방향에 속도를 곱해서 강체에 적용
    }
    
    // 터치의 스크린 포지션을 월드 포지션으로 변경
    public Vector3 GetInputPosition(Vector3 position)
    {
    	Vector3 screenPosition = position + (Vector3.back * Camera.main.transform.position.z);
        return Camera.main.ScreenToWorldPoint(screenPosition);
    }
    
    // 두 포지션 사이의 방향 
    public Vector2 GetDirection (Vector2 from, Vector2 to)
    {
        Vector2 delta = to - from;
        float radian = Mathf.Atan2(delta.y, delta.x);

        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
}
