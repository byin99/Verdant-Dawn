using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClassSlotUI : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// 클래스를 바꿀 때 실행되는 델리게이트
    /// </summary>
    public event Action<CharacterClass> onChangeClass;

    /// <summary>
    /// Slot별 클래스 타입
    /// </summary>
    public CharacterClass classSlot;

    // 컴포넌트들
    WeaponChangeUI weaponChange;

    private void Awake()
    {
        weaponChange = GetComponentInParent<WeaponChangeUI>();
    }

    private void Start()
    {
        onChangeClass += weaponChange.ChangeClass;  // 델리게이트에 ChangeClass 함수 연결하기
    }

    /// <summary>
    /// 슬롯을 누르면 실행되는 함수
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onChangeClass?.Invoke(classSlot);   // 델리게이트 실행
        }
    }
}
