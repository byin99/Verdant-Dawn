using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RFX4_PhysicsMotion : MonoBehaviour
{
    public bool UseCollisionDetect = true;
    public float MaxDistnace = -1;
    public float Mass = 1;
    public float Speed = 10;
    public float RandomSpeedOffset = 0f;
    public float AirDrag = 0.1f;
    public bool UseGravity = true;
    public ForceMode ForceMode = ForceMode.Impulse;
    public Vector3 AddRealtimeForce = Vector3.zero;
    public float MinSpeed = 0;
    public float ColliderRadius = 0.05f;
    public bool FreezeRotation;

    public bool UseTargetPositionAfterCollision;
    public GameObject EffectOnCollision;
    public bool CollisionEffectInWorldSpace = true;
    public bool LookAtNormal = true;
    public float CollisionEffectDestroyAfter = 5;

    public GameObject[] DeactivateObjectsAfterCollision;

    [HideInInspector] public float HUE = -1;

    public event EventHandler<RFX4_CollisionInfo> CollisionEnter;

    Rigidbody rigid;
    SphereCollider collid;
    bool isCollided;
    GameObject targetAnchor;
    bool isInitializedForce;
    float currentSpeedOffset;
    private RFX4_EffectSettings effectSettings;

    public event Action<Collider> onTriggerEnter;

    void OnEnable()
    {
        effectSettings = GetComponentInParent<RFX4_EffectSettings>();
        foreach (var obj in DeactivateObjectsAfterCollision)
        {
            if (obj != null)
            {
                if (obj.GetComponent<ParticleSystem>() != null) obj.SetActive(false);
                obj.SetActive(true);
            }
        }
        currentSpeedOffset = Random.Range(-RandomSpeedOffset * 10000f, RandomSpeedOffset * 10000f) / 10000f;
        InitializeCollider();
    }

    void InitializeCollider()
    {
        if (effectSettings.UseCollisionDetection)
        {
            collid = gameObject.AddComponent<SphereCollider>();
            collid.radius = ColliderRadius;
            collid.isTrigger = true; // 트리거로 설정
        }
        isInitializedForce = false;
    }

    void InitializeForce()
    {
        rigid = gameObject.AddComponent<Rigidbody>();
        rigid.mass = effectSettings.Mass;
        rigid.drag = effectSettings.AirDrag;
        rigid.useGravity = effectSettings.UseGravity;
        if (FreezeRotation) rigid.constraints = RigidbodyConstraints.FreezeRotation;
        rigid.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rigid.interpolation = RigidbodyInterpolation.Interpolate;
        rigid.AddForce(transform.forward * (effectSettings.Speed + currentSpeedOffset), ForceMode);
        isInitializedForce = true;
    }

    void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(other);

        if (isCollided && !effectSettings.UseCollisionDetection) return;
        isCollided = true;

        if (UseTargetPositionAfterCollision)
        {
            if (targetAnchor != null) Destroy(targetAnchor);

            targetAnchor = new GameObject();
            targetAnchor.hideFlags = HideFlags.HideAndDontSave;
            targetAnchor.transform.parent = other.transform;
            targetAnchor.transform.position = transform.position;
            targetAnchor.transform.rotation = transform.rotation;
        }

        CollisionEnter?.Invoke(this, new RFX4_CollisionInfo { HitPoint = transform.position, HitCollider = other, HitGameObject = other.gameObject });

        if (EffectOnCollision != null)
        {
            var instance = Instantiate(EffectOnCollision, transform.position, Quaternion.identity);
            if (HUE > -0.9f) RFX4_ColorHelper.ChangeObjectColorByHUE(instance, HUE);
            if (LookAtNormal) instance.transform.LookAt(transform.position + transform.forward);
            else instance.transform.rotation = transform.rotation;
            if (!CollisionEffectInWorldSpace) instance.transform.parent = other.transform;
            Destroy(instance, CollisionEffectDestroyAfter);
        }

        foreach (var obj in DeactivateObjectsAfterCollision)
        {
            if (obj != null)
            {
                var ps = obj.GetComponent<ParticleSystem>();
                if (ps != null) ps.Stop();
                else obj.SetActive(false);
            }
        }

        RemoveRigidbody();
    }

    private void FixedUpdate()
    {
        if (!isInitializedForce) InitializeForce();
        if (rigid != null && AddRealtimeForce.magnitude > 0.001f) rigid.AddForce(AddRealtimeForce);
        if (rigid != null && MinSpeed > 0.001f) rigid.AddForce(transform.forward * MinSpeed);
        if (rigid != null && effectSettings.MaxDistnace > 0 && transform.localPosition.magnitude > effectSettings.MaxDistnace) RemoveRigidbody();

        if (UseTargetPositionAfterCollision && isCollided && targetAnchor != null)
        {
            transform.position = targetAnchor.transform.position;
            transform.rotation = targetAnchor.transform.rotation;
        }
    }

    public class RFX4_CollisionInfo : EventArgs
    {
        public Vector3 HitPoint;
        public Collider HitCollider;
        public GameObject HitGameObject;
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        RemoveRigidbody();
    }

    void RemoveRigidbody()
    {
        isCollided = false;
        if (rigid != null) Destroy(rigid);
        if (collid != null) Destroy(collid);
    }

    void OnDrawGizmosSelected()
    {
        if (Application.isPlaying) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ColliderRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 100);
    }
}
