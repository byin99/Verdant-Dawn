using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInitializable
{
    /// <summary>
    /// 실제 인벤토리 데이터
    /// </summary>
    Inventory inventory;

    /// <summary>
    /// 아이템을 줏을 수 있는 거리
    /// </summary>
    [SerializeField]
    float itemPickUpRange = 2.0f;

    /// <summary>
    /// 인벤토리를 읽기 위한 프로퍼티
    /// </summary>
    public Inventory Inventory => inventory;

    /// <summary>
    /// itemPickUpRange를 공유받을 프로퍼티(읽기 전용)
    /// </summary>
    public float ItemPickUpRange => itemPickUpRange;

    /// <summary>
    /// 인벤토리 초기화 함수
    /// </summary>
    public void Initialize()
    {
        PlayerStatus player = GetComponent<PlayerStatus>();
        inventory = new Inventory(player);
        UIManager.Instance.InventoryUI.InitializeInventory(inventory);
    }

    /// <summary>
    /// 주변 아이템을 줍는 입력이 들어왔을 때 실행될 함수
    /// </summary>
    public void GetPickUpItems()
    {
        // 주변에 있는 아이템(Item레이어로 되어있다)을 모두 획득해서 인벤토리에 추가하기
        Collider[] itemColliders = Physics.OverlapSphere(transform.position, itemPickUpRange, LayerMask.GetMask("Item"));
        foreach (Collider collider in itemColliders)
        {
            ItemObject item = collider.GetComponent<ItemObject>();
    
            if (item != null)   // null이 아니면 ItemObject라는 이야기
            {
                // 인베토리로 들어가는 아이템
                if (inventory.AddItem(item.ItemData.code))  // 인벤토리에 추가 시도
                {
                    item.ItemCollected();   // 추가에 성공했으면 비활성화시켜서 풀에 되돌리기
                }
            }
        }
    }
}
