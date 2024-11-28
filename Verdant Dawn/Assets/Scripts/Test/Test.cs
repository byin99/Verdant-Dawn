using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : TestBase
{
    Player player;

    protected override void Awake()
    {
        base.Awake();
        player = GameManager.Instance.Player;
    }

    protected override void OnTest1(InputAction.CallbackContext context)
    {
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Quaternion rotation = player.transform.rotation;
        Vector3 eulerAngle = rotation.eulerAngles;
        Factory.Instance.GetGreatSwordEffect(player.transform.position + Vector3.up + player.transform.forward * 2, eulerAngle);
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        Quaternion rotation = player.transform.rotation;
        Vector3 eulerAngle = rotation.eulerAngles;
        Factory.Instance.GetRipleEffect(player.transform.position + Vector3.up + player.transform.forward * 2, eulerAngle);
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        Quaternion rotation = player.transform.rotation;
        Vector3 eulerAngle = rotation.eulerAngles;
        Factory.Instance.GetStaffEffect(player.transform.position + Vector3.up + player.transform.forward * 2, eulerAngle);
    }

    protected override void OnTest5(InputAction.CallbackContext context)
    {
        Quaternion rotation = player.transform.rotation;
        Vector3 eulerAngle = rotation.eulerAngles;
        Factory.Instance.GetDaggerEffect(player.transform.position + Vector3.up + player.transform.forward * 2, eulerAngle);
    }
}
