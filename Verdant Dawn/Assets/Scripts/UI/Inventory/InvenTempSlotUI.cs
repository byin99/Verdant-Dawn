using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvenTempSlotUI : SlotUI_Base
{
    /// <summary>
    /// UI 오브젝트의 RectTransform
    /// </summary>
    private RectTransform rectTransform;

    /// <summary>
    /// 이 UI 오브젝트가 속해있는 Canvas
    /// </summary>
    Canvas canvas;

    /// <summary>
    /// 플레이어
    /// </summary>
    PlayerInventory owner;

    // 컴포넌트들
    AudioManager audioManager;

    protected override void Awake()
    {
        base.Awake();

        rectTransform = GetComponent<RectTransform>();
        owner = GameManager.Instance.PlayerInventory;
        canvas = GetComponentInParent<Canvas>();
        audioManager = GameManager.Instance.AudioManager;
    }

    private void Update()
    {
        // 마우스 위치 가져오기
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        // 마우스 위치를 월드 좌표로 변환
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.GetComponent<RectTransform>(),
            mousePosition,
            canvas.worldCamera,
            out Vector3 worldPosition
        );

        // UI 요소 위치 변경
        rectTransform.position = worldPosition;
    }

    /// <summary>
    /// screen좌표가 가리키는 월드 포지션에 임시 슬롯에 들어있는 아이템을 드랍하는 함수
    /// </summary>
    /// <param name="screen">화면의 벡터 값</param>
    public void ItemDrop(Vector2 screen)
    {
        if (!InvenSlot.IsEmpty)
        {
            Ray ray = Camera.main.ScreenPointToRay(screen);     // 레이 구하기
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000.0f, LayerMask.GetMask("Ground"))) // Ground레이어를 가진 물체와 충돌 확인
            {
                audioManager.PlaySound2D(AudioCode.ItemDrop);

                Vector3 dropPosition = hitInfo.point;           // 충돌한 위치를 드랍위치로 설정
                dropPosition.y = 0;
    
                Vector3 dropDir = dropPosition - owner.transform.position;
                if (dropDir.sqrMagnitude > owner.ItemPickUpRange * owner.ItemPickUpRange)   // 드랍 위치가 너무 멀면
                {
                    // 오너의 위치에서 dropDir방향으로 owner.ItemPickUpRange만큼 이동한 위치
                    dropPosition = dropDir.normalized * owner.ItemPickUpRange + owner.transform.position;   // 일정 반경안으로 조정
                }
    
                Factory.Instance.MakeItems(     // 아이템 생성
                    InvenSlot.ItemData.code,
                    InvenSlot.ItemCount,
                    dropPosition,
                    InvenSlot.ItemCount > 1);   // 아이템이 1개면 노이즈 없음, 1개 초과면 노이즈 있음
                InvenSlot.ClearSlotItem();      // 슬롯 비우기
            }
        }
    }
}
