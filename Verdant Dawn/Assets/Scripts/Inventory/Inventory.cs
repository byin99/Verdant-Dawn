
public class Inventory
{
    /// <summary>
    /// 인벤토리의 기본 슬롯 갯수(3칸)
    /// </summary>
    const int Default_Inventory_Size = 3;

    /// <summary>
    /// 인벤토리의 슬롯들(아이템 한종류가 들어간다)
    /// </summary>
    InvenSlot[] slots;

    /// <summary>
    /// 인벤토리의 슬롯의 갯수
    /// </summary>
    int SlotCount => slots.Length;

    /// <summary>
    /// 임시 슬롯(드래그나 아이템 분리작업에서 사용)
    /// </summary>
    InvenTempSlot tempSlot;

    /// <summary>
    /// 아이템 데이터 매니저(아이템 종류별 정보를 전부 가지고 있다.)
    /// </summary>
    ItemDataManager itemDataManager;

    /// <summary>
    /// 인벤토리의 수요자
    /// </summary>
    PlayerStatus owner;

    /// <summary>
    /// 소유자 확인용 프로퍼티
    /// </summary>
    public PlayerStatus Owner => owner;

    /// <summary>
    /// 인벤토리 슬롯에 접근하기 위한 인덱서
    /// </summary>
    /// <param name="index">슬롯의 인덱스</param>
    /// <returns>인덱스 번째의 슬롯</returns>
    public InvenSlot this[uint index] => slots[index];

    /// <summary>
    /// 임시 슬롯 확인용 프로퍼티
    /// </summary>
    public InvenTempSlot TempSlot => tempSlot;

    /// <summary>
    /// 인벤토리 생성자
    /// </summary>
    /// <param name="owner">인벤토리의 소유자</param>
    /// <param name="size">인벤토리의 크기(기본값은 6)</param>
    public Inventory(PlayerStatus owner, uint size = Default_Inventory_Size)
    {
        slots = new InvenSlot[size];
        for (uint i = 0; i < slots.Length; i++)
        {
            slots[i] = new InvenSlot(i);
        }
        tempSlot = new InvenTempSlot();
        itemDataManager = GameManager.Instance.ItemDataManager;    // 타이밍 조심 필요

        this.owner = owner;
    }

    /// <summary>
    /// 인벤트리의 특정 슬롯에 아이템을 하나 추가하는 함수
    /// </summary>
    /// <param name="code">추가할 아이템의 종류</param>
    /// <param name="slotIndex">아이템을 추가할 슬롯의 인덱스</param>
    /// <returns>true면 추가 성공, false면 추가 실패</returns>
    public bool AddItem(ItemCode code, uint slotIndex)
    {
        bool result = false;

        if (IsValidIndex(slotIndex, out InvenSlot slot))    // 인덱스가 정상범위인지 확인하고 슬롯 받아오기
        {
            // 인덱스가 정상이다.
            ItemData itemData = itemDataManager[code];      // 아이템 데이터 찾아 놓기
            if (slot.IsEmpty)                               // 슬롯이 비어있는지 확인
            {
                slot.AssignSlotItem(itemData);              // 슬롯이 비어있으면 아이템 설정
                result = true;
            }
            else
            {
                // 슬롯이 비어있지 않다.
                if (slot.ItemData == itemData)              // 같은 종류의 아이템인지 확인         
                {
                    // 같은 종류의 아이템이 들어있다.
                    result = slot.IncreaseSlotItem(out _);  // 아이템 증가 시도(증가되면 true, 안되면 false)
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 인벤토리에 아이템을 하나 추가하는 함수
    /// </summary>
    /// <param name="code">추가할 아이템의 종류</param>
    /// <returns>true면 추가 성공, false면 추가 실패</returns>
    public bool AddItem(ItemCode code)
    {
        bool result = false;
        for (uint i = 0; i < SlotCount; i++)
        {
            if (AddItem(code, i))
            {
                result = true;
                break;
            }
        }
        return result;
    }

    /// <summary>
    /// 인벤토리의 from슬롯에 있는 아이템을 to 위치로 옮기는 함수
    /// </summary>
    /// <param name="from">위치 변경 시작 인덱스</param>
    /// <param name="to">위치 변경 도착 인덱스</param>
    public void MoveItem(uint from, uint to)
    {
        if (from != to                                      // from과 to가 다른 슬롯이고 각각 적절한 슬롯일 때
            && IsValidIndex(from, out InvenSlot fromSlot)
            && IsValidIndex(to, out InvenSlot toSlot))
        {
            if (!fromSlot.IsEmpty) // from에는 반드시 아이템이 들어있어야 한다.
            {
                if (toSlot is InvenTempSlot)
                {
                    TempSlot.FromIndex = from;  // toSlot이 임시 슬롯이면 FromIndex에 돌어갈 위치 설정
                }

                if (fromSlot.ItemData == toSlot.ItemData)
                {
                    // from과 to에 같은 아이템이 들어있는 경우
                    toSlot.IncreaseSlotItem(out uint overCount, fromSlot.ItemCount);    // to에 from이 가지고 있는 개수 만큼 증가 시도
                    fromSlot.DecreaseSlotItem(fromSlot.ItemCount - overCount);          // to로 넘어간 양 만큼만 감소
                }
                else
                {
                    // from과 to에 다른 아이템이 들어있는 경우

                    if (fromSlot is InvenTempSlot)
                    {
                        // from이 임시 슬롯이다(= to는 일반 슬롯)
                        // => temp슬롯에서 to슬롯으로 아이템을 옮기는 경우 => 드래그가 끝나는 상황
                        // (일반적인 아이템 이동 마무리 상황)
                        // (임시 슬롯에 있던 것이 to에 들어가고 to에 있던 것이 드래그 시작한 슬롯으로 돌아가야 하는 상황)

                        if (TempSlot.FromIndex != null)
                        {
                            InvenSlot dragStartSlot = slots[TempSlot.FromIndex.Value];     // 드래그 시작한 슬롯 찾기

                            if (dragStartSlot.IsEmpty)
                            {
                                // dragStartSlot이 비어있다.(드래그로 아이템 옮기기)
                                dragStartSlot.AssignSlotItem(toSlot.ItemData, toSlot.ItemCount); // dragStartSlot에는 to 슬롯에 있는 아이템 옮기기
                                toSlot.AssignSlotItem(TempSlot.ItemData, TempSlot.ItemCount);  // to 슬롯에는 임시 슬롯에 있는 아이템 옮기기
                                TempSlot.ClearSlotItem();
                            }
                            else
                            {
                                // dragStartSlot에 아이템이 있다.(dragStartSlot 슬롯에서 아이템을 덜어낸 상황)
                                if (dragStartSlot.ItemData == toSlot.ItemData)
                                {
                                    dragStartSlot.IncreaseSlotItem(out uint overCount, toSlot.ItemCount);
                                    toSlot.DecreaseSlotItem(toSlot.ItemCount - overCount);
                                }
                                SwapSlot(TempSlot, toSlot);
                            }
                        }
                        else
                        {
                            SwapSlot(fromSlot, toSlot); // fromIndex가 비어있는 상황이면 그냥 스왑
                        }
                    }
                    else
                    {
                        // fromSlot이 임시 슬롯이 아닌 경우(toSlot이 임시 슬롯)
                        SwapSlot(fromSlot, toSlot);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 슬롯간에 스왑을 하는 함수
    /// </summary>
    /// <param name="slotA">대상1</param>
    /// <param name="slotB">대상2</param>
    void SwapSlot(InvenSlot slotA, InvenSlot slotB)
    {
        ItemData tempData = slotA.ItemData;
        uint tempCount = slotA.ItemCount;
        slotA.AssignSlotItem(slotB.ItemData, slotB.ItemCount);
        slotB.AssignSlotItem(tempData, tempCount);

        // 임시 슬롯은 이제 FromIndex를 비운다
        InvenTempSlot tempA = slotA as InvenTempSlot;
        if (tempA != null)
        {
            tempA.FromIndex = null;
        }
        InvenTempSlot tempB = slotB as InvenTempSlot;
        if (tempB != null)
        {
            tempB.FromIndex = null;
        }
    }

    /// <summary>
    /// 슬롯 인덱스가 적절한 인덱스인지 확인하는 함수
    /// </summary>
    /// <param name="index">확인할 인덱스 번호</param>
    /// <param name="targetSlot">index가 가리키는 슬롯</param>
    /// <returns>존재하는 인덱스면 true, 아니면 false</returns>
    bool IsValidIndex(uint index, out InvenSlot targetSlot)
    {
        // index가 (0 ~ SlotCount-1)이거나 임시 슬롯의 인덱스이면 true
        targetSlot = null;

        if (index < SlotCount)
        {
            targetSlot = slots[index];
        }
        else if (index == InvenTempSlot.TempSlotIndex)
        {
            targetSlot = TempSlot;
        }

        return targetSlot != null;
    }
}
