using System;
using UnityEngine;
public class InvenSlot
{
    /// <summary>
    /// 인벤토리에서 몇번째 슬롯인지를 나타내는 변수
    /// </summary>
    uint slotIndex;

    /// <summary>
    /// 이 슬롯에 들어있는 아이템 종류(null이면 슬롯은 비어있다)
    /// </summary>
    ItemData slotItemData = null;

    /// <summary>
    /// 이 슬롯에 들어있는 아이템 갯수
    /// </summary>
    uint itemCount = 0;

    /// <summary>
    /// 슬롯의 인덱스를 확인하기 위한 프로퍼티
    /// </summary>
    public uint Index => slotIndex;

    /// <summary>
    /// 슬롯에 들어있는 아이템의 종류를 확인하거나 쓰기 위한 프로퍼티(쓰기는 private)
    /// </summary>
    public ItemData ItemData
    {
        get => slotItemData;
        private set
        {
            if (slotItemData != value)          // 변경이 있을때만 처리
            {
                slotItemData = value;
                onSlotItemChange?.Invoke();     // 변경이 있었음을 알림
            }
        }
    }

    /// <summary>
    /// 이 슬롯이 비어있는지 확인하는 프로퍼티(true면 비어있다. false면 아이템이 있다)
    /// </summary>
    public bool IsEmpty => slotItemData == null;

    /// <summary>
    /// 아이템 갯수를 확인하고 변경하는 프로퍼티
    /// </summary>
    public uint ItemCount
    {
        get => itemCount;
        set
        {
            if (itemCount != value)
            {
                itemCount = value;
                onSlotItemChange?.Invoke();
            }
        }
    }

    /// <summary>
    /// 슬롯의 아이템이 변경되었음을 알리는 델리게이트
    /// </summary>
    public event Action onSlotItemChange;

    /// <summary>
    /// 슬롯 생성자
    /// </summary>
    /// <param name="index">슬롯의 인덱스(인벤토리에서의 위치)</param>
    public InvenSlot(uint index)
    {
        slotIndex = index;
        ItemData = null;
        ItemCount = 0;
    }

    /// <summary>
    /// 이 슬롯에 아이템을 설정하는 함수
    /// </summary>
    /// <param name="data">설정할 아이템의 종류</param>
    /// <param name="count">설정할 아이템의 개수</param>
    /// <param name="isEquipped">설정할 장비 상태</param>
    public void AssignSlotItem(ItemData data, uint count = 1)
    {
        if (data != null)
        {
            ItemData = data;
            ItemCount = count;
        }
        else
        {
            ClearSlotItem();    // data가 없으면 비우기
        }
    }

    /// <summary>
    /// 이 슬롯에 들어있는 아이템을 제거하는 함수
    /// </summary>
    public virtual void ClearSlotItem()
    {
        ItemData = null;
        ItemCount = 0;
    }

    /// <summary>
    /// 이 슬롯에 들어있는 아이템 개수를 증가시키는 함수
    /// </summary>
    /// <param name="overCount">return이 false가 되었을 때 남은 개수(true면 0)</param>
    /// <param name="increaseCount">증가시킬 개수</param>
    /// <returns>increaseCount만큼 증가에 성공했으면 true, 남은 것이 있으면 false</returns>
    public bool IncreaseSlotItem(out uint overCount, uint increaseCount = 1)
    {
        bool result = false;

        uint newCount = ItemCount + increaseCount;                  // 합계를 구하기
        int over = (int)newCount - (int)ItemData.maxStackCount;     // 최대치를 넘었는지 계산해보기

        if (over > 0)
        {
            // 넘쳤다
            ItemCount = ItemData.maxStackCount; // 넘쳤으면 최대치까지만 설정
            overCount = (uint)over;             // 남은 것 기록

            result = false;
        }
        else
        {
            // 안넘쳤다.
            ItemCount = newCount;
            overCount = 0;
            result = true;
        }

        return result;
    }

    /// <summary>
    /// 이 슬롯에 들어있는 아이템 개수를 감소시키는 함수
    /// </summary>
    /// <param name="decreaseCount">감소시킬 개수</param>
    public void DecreaseSlotItem(uint decreaseCount = 1)
    {
        int newCount = (int)ItemCount - (int)decreaseCount;
        if (newCount > 0)
        {
            // 아직 아이템이 남아있다.
            ItemCount = (uint)newCount;
        }
        else
        {
            // 완전히 비었다.
            ClearSlotItem();
        }
    }

    /// <summary>
    /// 이 슬롯의 아이템을 사용하는 함수
    /// </summary>
    /// <param name="target">아이템의 효과를 받을 대상</param>
    public void UseItem(GameObject target)
    {
        IUsable usable = ItemData as IUsable;
        if (usable != null)         // 사용 가능한 아이템이면
        {
            if (usable.Use(target)) // 아이템 사용 시도
            {
                DecreaseSlotItem(); // 아이템 사용에 성공하면 1개 감소
            }
        }
    }

    /// <summary>
    /// 델리게이트 연결 초기화 함수
    /// </summary>
    public void ClearDelegates()
    {
        onSlotItemChange = null;
    }
}
