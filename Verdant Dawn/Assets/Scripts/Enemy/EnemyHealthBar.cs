using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    /// <summary>
    /// FillPivot의 트랜스폼
    /// </summary>
    Transform fillPivot;

    private void Awake()
    {
        // onHealthChange 델리게이트에 함수 연결하기
        IHealth health = transform.parent.GetComponent<IHealth>();
        health.onHealthChange += Refresh;

        // FillPivot위치 찾기
        fillPivot = transform.GetChild(1);
    }

    /// <summary>
    /// HP바 바꾸기
    /// </summary>
    /// <param name="ratio">바꾸는 비율</param>
    private void Refresh(float ratio)
    {
        fillPivot.localScale = new Vector3(ratio, 1, 1);
    }

    private void LateUpdate()
    {
        // 빌보드 효과 넣기
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
