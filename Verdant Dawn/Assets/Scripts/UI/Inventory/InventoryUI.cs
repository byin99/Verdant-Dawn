using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    /// <summary>
    /// 마우스가 UI위에 있는지 체크하는 변수
    /// </summary>
    [HideInInspector]
    public bool onPointer = false;

    /// <summary>
    /// 이 UI가 보여줄 인벤토리
    /// </summary>
    Inventory inven;

    /// <summary>
    /// 인벤토리에 들어있는 Slot들의 UI 모음
    /// </summary>
    InvenSlotUI[] slotsUIs;

    /// <summary>
    /// 상세 정보창
    /// </summary>
    DetailInfoUI detailInfoUI;

    /// <summary>
    /// 임시 슬롯의 UI   
    /// </summary>
    InvenTempSlotUI tempSlotUI;

    /// <summary>
    /// 인벤토리 소유주 확인용 프로퍼티
    /// </summary>
    public PlayerStatus Owner => inven.Owner;

    // 컴포넌트들
    PlayerInputActions inputActions;
    AudioManager audioManager;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        slotsUIs = new InvenSlotUI[3];

        Transform child = transform.GetChild(0);
        slotsUIs[0] = child.GetComponent<InvenSlotUI>();

        child = transform.GetChild(1);
        slotsUIs[1] = child.GetComponent<InvenSlotUI>();

        child = transform.GetChild(2);
        slotsUIs[2] = child.GetComponent<InvenSlotUI>();

        child = transform.GetChild(3);
        detailInfoUI = child.GetComponent<DetailInfoUI>();

        child = transform.GetChild(4);
        tempSlotUI = child.GetComponent<InvenTempSlotUI>();

        audioManager = GameManager.Instance.AudioManager;
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Inventory1.performed += OnInventory1;
        inputActions.UI.Inventory1.canceled += OffInventory1;  
        inputActions.UI.Inventory2.performed += OnInventory2;
        inputActions.UI.Inventory2.canceled += OffInventory2;
        inputActions.UI.Inventory3.performed += OnInventory3;
        inputActions.UI.Inventory3.canceled += OffInventory3;
        inputActions.UI.Click.canceled += OnItemDrop;
    }

    private void OnDisable()
    {
        inputActions.UI.Click.canceled -= OnItemDrop;
        inputActions.UI.Inventory3.canceled -= OffInventory3;
        inputActions.UI.Inventory3.performed -= OnInventory3;
        inputActions.UI.Inventory2.canceled -= OffInventory2;
        inputActions.UI.Inventory2.performed -= OnInventory2;
        inputActions.UI.Inventory1.canceled -= OffInventory1;
        inputActions.UI.Inventory1.performed -= OnInventory1;
        inputActions.UI.Disable();
    }

    public void InitializeInventory(Inventory inventory)
    {
        inven = inventory;                              // 인벤토리 저장
        for (uint i = 0; i < slotsUIs.Length; i++)
        {
            slotsUIs[i].InitializeSlot(inven[i]);       // SlotUI 초기화
            slotsUIs[i].onDragBegin += OnItemMoveBegin;
            slotsUIs[i].onDragEnd += OnItemMoveEnd;
            slotsUIs[i].onClick += OnSlotClick;
            slotsUIs[i].onPointerEnter += OnItemDetailInfoOpen;
            slotsUIs[i].onPointerExit += OnItemDetialInfoClose;
            slotsUIs[i].onPointerMove += OnSlotPointerMove;
        }

        tempSlotUI.InitializeSlot(inven.TempSlot);      // TempSlotUI 초기화
    }


    /// <summary>
    /// 슬롯에서 드래그가 시작되었을 때 실행되는 함수
    /// </summary>
    /// <param name="index">드래그가 시작된 슬롯의 인덱스</param>
    private void OnItemMoveBegin(uint index)
    {
        audioManager.PlaySound2D(AudioCode.ItemPick);
        detailInfoUI.isPause = true;        // 상세정보창 일시 정지
        inven.MoveItem(index, tempSlotUI.Index);
    }

    /// <summary>
    /// 슬롯에서 드래그가 끝났을 때 실행되는 함수
    /// </summary>
    /// <param name="index">드래그가 끝난 슬롯의 index(null이면 드래그가 비정상적으로 끝난 경우)</param>
    private void OnItemMoveEnd(uint? index)
    {
        if (index.HasValue)
        {
            audioManager.PlaySound2D(AudioCode.ItemDrop);
            inven.MoveItem(tempSlotUI.Index, index.Value);

            if (tempSlotUI.InvenSlot.IsEmpty)       // 임시슬롯이 비어있을 때만 상세정보창 열기
            {
                detailInfoUI.IsPaused = false;      // 상세정보창 일시 정지 해제
                detailInfoUI.Open(inven[index.Value].ItemData);
            }
        }
    }

    /// <summary>
    /// 슬롯을 클릭했을 때 실행되는 함수
    /// </summary>
    /// <param name="index">클릭한 슬롯의 인덱스</param>
    private void OnSlotClick(uint index)
    {
        if (tempSlotUI.InvenSlot.IsEmpty)
        {
            inven[index].UseItem(Owner.gameObject);     // 아이템 사용 시도
        }
        else
        {
            // 임시 슬롯에 아이템이 있을 때는 아이템을 드랍
            audioManager.PlaySound2D(AudioCode.ItemDrop);
            OnItemMoveEnd(index);
        }
    }

    /// <summary>
    /// 마우스 커서가 슬롯위에 들어갔을 때 상세 정보창을 여는 함수
    /// </summary>
    /// <param name="index">슬롯의 인덱스</param>
    private void OnItemDetailInfoOpen(uint index)
    {
        onPointer = true;
        detailInfoUI.Open(slotsUIs[index].InvenSlot.ItemData);  // 열기
    }

    /// <summary>
    /// 마우스 커서가 슬롯을 나갔을 때 상세 정보창을 닫는 함수
    /// </summary>
    private void OnItemDetialInfoClose()
    {
        onPointer = false;
        detailInfoUI.Close();
    }

    /// <summary>
    /// 마우스 커서가 슬롯 안에서 움직일 때 실행되는 함수
    /// </summary>
    /// <param name="screen">마우스 커서의 스크린 좌표</param>
    private void OnSlotPointerMove(Vector2 screen)
    {
        detailInfoUI.MovePosition(screen);
    }

    /// <summary>
    /// 1번 아이템 사용
    /// </summary>
    private void OnInventory1(InputAction.CallbackContext _)
    {
        if (!inven[0].IsEmpty)
        {
            inven[0].UseItem(Owner.gameObject);
            slotsUIs[0].GlowFrame.enabled = true;
        }
    }

    /// <summary>
    /// 1번 아이템 GlowFrame 끄기
    /// </summary>
    private void OffInventory1(InputAction.CallbackContext _)
    {
        slotsUIs[0].GlowFrame.enabled = false;
    }

    /// <summary>
    /// 2번 아이템 사용
    /// </summary>
    private void OnInventory2(InputAction.CallbackContext _)
    {
        if (!inven[1].IsEmpty)
        {
            inven[1].UseItem(Owner.gameObject);
            slotsUIs[1].GlowFrame.enabled = true;
        }
    }

    /// <summary>
    /// 2번 아이템 GlowFrame 끄기
    /// </summary>
    private void OffInventory2(InputAction.CallbackContext _)
    {
        slotsUIs[1].GlowFrame.enabled = false;
    }

    /// <summary>
    /// 3번 아이템 사용
    /// </summary>
    private void OnInventory3(InputAction.CallbackContext _)
    {
        if (!inven[2].IsEmpty)
        {
            inven[2].UseItem(Owner.gameObject);
            slotsUIs[2].GlowFrame.enabled = true;
        }
    }

    /// <summary>
    /// 3번 아이템 GlowFrame 끄기
    /// </summary>
    private void OffInventory3(InputAction.CallbackContext _)
    {
        slotsUIs[2].GlowFrame.enabled = false;
    }

    /// <summary>
    /// 마우스 버튼이 떨어지면 실행
    /// </summary>
    private void OnItemDrop(InputAction.CallbackContext _)
    {
        Canvas canvas = GetComponentInParent<Canvas>();         // 상위 Canvas 가져오기
        Camera uiCamera = canvas.worldCamera;                   // UI 카메라 가져오기

        Vector2 screen = Mouse.current.position.ReadValue();    // 마우스 스크린 좌표
        Vector3 worldPoint;

        RectTransform rectTransform = (RectTransform)transform;

        // 마우스 화면 좌표를 월드 좌표로 변환
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, screen, uiCamera, out worldPoint))
        {
            // 월드 좌표를 로컬 좌표로 변환
            Vector2 localPoint = rectTransform.InverseTransformPoint(worldPoint);

            // 인벤토리 영역 밖이면
            if (!rectTransform.rect.Contains(localPoint))
            {
                // 슬롯 영역 밖에 있으면 아이템 드랍 실행
                tempSlotUI.ItemDrop(worldPoint);
            }
        }
    }
}
