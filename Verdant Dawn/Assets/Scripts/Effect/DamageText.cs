using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : RecycleObject
{ 
    /// <summary>
    /// 데미지 텍스트 움직임 정도(커브)
    /// </summary>
    public AnimationCurve movement;

    /// <summary>
    /// 데미지 텍스트 사라지는 정도(커브)
    /// </summary>
    public AnimationCurve fade;

    /// <summary>
    /// 데미지 텍스트 색깔
    /// </summary>
    public Color color;

    /// <summary>
    /// 데미지 텍스트가 없어지기 까지의 시간
    /// </summary>
    public float duration = 1.0f;

    /// <summary>
    /// 데미지 텍스트의 최고 높이
    /// </summary>
    public float maxHeight = 1.5f;

    /// <summary>
    /// 현재 높이
    /// </summary>
    float baseHeight = 0.0f;

    /// <summary>
    /// 시간 누적용
    /// </summary>
    float elapsedTime = 0.0f;

    // 컴포넌트
    TextMeshPro damageText;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
    }

    /// <summary>
    /// 초기화용 함수
    /// </summary>
    protected override void OnReset()
    {
        elapsedTime = 0.0f;
        damageText.color = color;
        transform.localScale = Vector3.one;
        baseHeight = transform.position.y;

        DisableTimer(duration);
    }

    private void Update()
    {
        // 시간 비율 계산
        elapsedTime += Time.deltaTime;
        float timeRatio = elapsedTime / duration;

        // 시간 값으로 높이 조정
        float curveMove = movement.Evaluate(timeRatio);
        float currentHeight = baseHeight + curveMove * maxHeight;
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // 사라지는 정도 조정
        float curveAlpha = fade.Evaluate(timeRatio);
        damageText.color = new Color(color.r, color.g, color.b, curveAlpha);
        transform.localScale = Vector3.one * curveAlpha;
    }

    private void LateUpdate()
    {
        // 빌보드 효과 만들기
        transform.rotation = Camera.main.transform.rotation;
    }

    /// <summary>
    /// 데미지에 따라 숫자 바꾸기
    /// </summary>
    /// <param name="damage">최종으로 받은 데미지</param>
    public void SetDamage(int damage)
    {
        damageText.text = damage.ToString();
    }
}
