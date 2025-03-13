using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconRotator : MonoBehaviour
{
    /// <summary>
    /// 아이템이 도는 속도
    /// </summary>
    public float rotateSpeed = 360.0f;

    /// <summary>
    /// 아이템이 상하로 움직이는 속도
    /// </summary>
    public float moveSpeed = 2.0f;

    /// <summary>
    /// 아이템의 최소 높이
    /// </summary>
    public float minHeight = 0.5f;

    /// <summary>
    /// 아이템의 최대 높이
    /// </summary>
    public float maxHeight = 1.5f;

    /// <summary>
    /// 보간용 시간 누적 변수
    /// </summary>
    float timeElapsed = 0.0f;

    private void Start()
    {
        transform.Rotate(0, Random.Range(0.0f, 360.0f), timeElapsed);               // 초기 랜덤 회전
        transform.position = transform.parent.position + Vector3.up * maxHeight;    // 시작 위치 설정
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime * moveSpeed;

        // 위치 설정
        Vector3 pos;
        pos.x = transform.parent.position.x;
        pos.y = minHeight + (Mathf.Cos(timeElapsed) + 1) * 0.5f * (maxHeight - minHeight);  // 범위 : 0.5 ~ 1.5
        pos.z = transform.parent.position.z;

        transform.position = pos;

        // 회전
        transform.Rotate(0, Time.deltaTime * rotateSpeed, 0);
    }
}
