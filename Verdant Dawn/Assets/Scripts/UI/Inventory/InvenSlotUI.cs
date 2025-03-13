//#define PrintTestLog

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenSlotUI : SlotUI_Base, IDragHandler, IBeginDragHandler, IEndDragHandler,
    IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerClickHandler
{
    /// <summary>
    /// 드래그의 시작을 알리는 델리게이트(uint:드래그를 시작한 슬롯의 인덱스)
    /// </summary>
    public event Action<uint> onDragBegin;

    /// <summary>
    /// 드래그의 종료를 알리는 델리게이트(uint?:드래그가 끝난 슬롯의 인덱스(null이면 슬롯이 아닌 곳에서 종료))
    /// </summary>
    public event Action<uint?> onDragEnd;

    /// <summary>
    /// 마우스 클릭을 알리는 델리게이트(uint : 클릭된 슬롯의 인덱스)
    /// </summary>
    public event Action<uint> onClick;

    /// <summary>
    /// 마우스 커서가 슬롯 위로 올라왔음을 알리는 델리게이트(uint : 올라간 슬롯의 인덱스)
    /// </summary>
    public event Action<uint> onPointerEnter;

    /// <summary>
    /// 마우스 커서가 슬롯 밖으로 나갔음을 알리는 델리게이트
    /// </summary>
    public event Action onPointerExit;

    /// <summary>
    /// 마우스 커서가 슬롯 위에서 움직임을 알리는 델리게이트(Vector2 : 마우스 포인터의 스크린좌표)
    /// </summary>
    public event Action<Vector2> onPointerMove;

    /// <summary>
    /// 빛나는 틀
    /// </summary>
    Image glowFrame;

    /// <summary>
    /// glowFrame을 return하는 프로퍼티
    /// </summary>
    public Image GlowFrame => glowFrame;

    protected override void Awake()
    {
        base.Awake();
        glowFrame = transform.GetChild(4).GetComponent<Image>();
    }

    private void Start()
    {
        glowFrame.enabled = false;
    }

    /// <summary>
    /// 슬롯 포기화 함수
    /// </summary>
    /// <param name="slot">초기화 할 슬롯</param>
    public override void InitializeSlot(InvenSlot slot)
    {
        ClearDelegates();
        base.InitializeSlot(slot);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        onDragBegin?.Invoke(Index);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // OnBeginDrag와 OnEndDrag를 발동시키기 위해 필요
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;       // 드래그 끝난 위치에 있는 게임 오브젝트
        if (obj != null)
        {
            // 마우스 위치에 어떤 것이 있다.
            InvenSlotUI endSlot = obj.GetComponent<InvenSlotUI>();
            uint? endIndex = null;
            if (endSlot != null)
            {
                // 슬롯이다.
                endIndex = endSlot.Index;
            }
            onDragEnd?.Invoke(endIndex);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(Index);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke(Index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onPointerExit?.Invoke();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        onPointerMove?.Invoke(eventData.position);
    }

    /// <summary>
    /// 모든 델리게이트에 연결된 함수 없애기
    /// </summary>
    public void ClearDelegates()
    {
        onClick = null;
        onDragBegin = null;
        onDragEnd = null;
        onPointerEnter = null;
        onPointerExit = null;
        onPointerMove = null;
    }
}

