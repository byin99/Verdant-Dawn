using UnityEngine;
using System.Collections;

public class RFX4_ReplaceModelOnCollision : MonoBehaviour
{
    public GameObject[] PhysicsObjects;

    private bool isCollided = false;
    Transform t;

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollided)
        {
            isCollided = true;
            foreach (var physicsObj in PhysicsObjects)
            {
                physicsObj.SetActive(true);
            }
            var mesh = GetComponent<MeshRenderer>();
            if (mesh != null)
                mesh.enabled = false;
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
    }

    void OnEnable()
    {
        isCollided = false;
        foreach (var physicsObj in PhysicsObjects)
        {
            if (physicsObj != null) physicsObj.SetActive(false);
        }
        var mesh = GetComponent<MeshRenderer>();
        if (mesh != null)
            mesh.enabled = true;
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;  // ✅ 트리거 감지를 위해 기본적으로 Kinematic 설정
        rb.detectCollisions = true;
    }
}