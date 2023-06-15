using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { PowerUp = 0, Boom, HP }

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemType    itemType;
    private Movement2D  movement2D;
    [SerializeField]
    private StageData   stageData;

    private void Awake() 
    {
        movement2D = GetComponent<Movement2D>();

        float x = Random.Range(-1.0f, 1.0f);
        float y = Random.Range(-1.0f, 1.0f);

        movement2D.MoveTo(new Vector3(x, y, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if ( collision.CompareTag("Player") )
        {
            // 아이템 획득 시 효과
            Use(collision.gameObject);
            // 아이템 오브젝트 삭제
            Destroy(gameObject);
        }
    }

    public void Use(GameObject player)
    {
        switch ( itemType )
        {
            case ItemType.PowerUp:
                player.GetComponent<Weapon>().AttackLevel ++;
                break;
            case ItemType.Boom:
                player.GetComponent<Weapon>().BoomCount ++;
                break;
            case ItemType.HP:
                player.GetComponent<PlayerHP>().CurrentHP += 2;
                break;
        }  
    }     

    private void LateUpdate() 
    {
        // 플레이어 캐릭터가 화면 범위 바깥으로 나가지 못하도록 함
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                                         Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }
}