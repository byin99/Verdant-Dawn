using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryGround : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 임시 바닥을 만들기 위한 함수
    /// </summary>
    public void MakeTemporaryGround()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 임시 바닥을 없애는 함수
    /// </summary>
    public void RemoveTemporaryGround()
    {
        gameObject.SetActive(false);
    }
}
