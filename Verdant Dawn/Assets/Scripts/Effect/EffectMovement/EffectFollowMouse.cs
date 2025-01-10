using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class EffectFollowMouse : MonoBehaviour
{
    /// <summary>
    /// 마우스 위치
    /// </summary>
    Vector3 targetPosition;

    private void OnEnable()
    {
        // 커서 안보이게 하기
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        // 커서 다시 보이게 하기
        Cursor.visible = true;
    }

    private void Update()
    {
        // 마우스 따라다니기
        FollowMouse();
    }

    /// <summary>
    /// 마우스를 따라다니는 함수
    /// </summary>
    void FollowMouse()
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(screen);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000))
        {
            transform.position = hitInfo.point;
        }
    }
}
