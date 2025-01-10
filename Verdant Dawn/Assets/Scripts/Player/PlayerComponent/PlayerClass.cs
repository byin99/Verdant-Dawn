using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour, IEquipTarget
{
    /// <summary>
    /// 클래스를 바꾸면 실행되는 델리게이트
    /// </summary>
    public event Action<CharacterClass> onChangeClass;

    /// <summary>
    /// 무기들 오브젝트
    /// </summary>
    [Header("무기들")]
    public Weapon[] weapons;

    /// <summary>
    /// 무기 장착 부위
    /// </summary>
    [Header("무기 장착 트랜스폼")]
    public Transform[] equipTransform;

    /// <summary>
    /// 무기를 바꾸고 있는지 알려주는 변수(true면 바꾸고 있는 중)
    /// </summary>
    [HideInInspector]
    public bool isChange = false;

    /// <summary>
    /// Class상태별 enum부여하기
    /// </summary>
    Dictionary<IState<PlayerClass>, CharacterClass> stateClass;

    /// <summary>
    /// Class 전환에 필요한 StateMachine
    /// </summary>
    public StateMachine<PlayerClass> stateMachine;

    // Class들
    public IState<PlayerClass> fighter;
    public IState<PlayerClass> berserker;
    public IState<PlayerClass> hunter;
    public IState<PlayerClass> magician;
    public IState<PlayerClass> assassin;

    /// <summary>
    /// 현재 클래스
    /// </summary>
    CharacterClass currentClass = CharacterClass.Fighter;

    /// <summary>
    /// 현재클래스를 반환하고, 클래스가 바뀔때마다 자동으로 State를 변경해주는 프로퍼티
    /// </summary>
    public CharacterClass CurrentClass
    {
        get => currentClass;
        set
        {
            // WeaponChangeUI에서 Class를 바꾸면 TransitionTo 실행
            if (CurrentClass != value)
            {
                currentClass = value;
                onChangeClass?.Invoke(currentClass);
                switch (currentClass)
                {
                    case CharacterClass.Fighter:
                        stateMachine.TransitionTo(fighter);
                        break;
                    case CharacterClass.Berserker:
                        stateMachine.TransitionTo(berserker);
                        break;
                    case CharacterClass.Hunter:
                        stateMachine.TransitionTo(hunter);
                        break;
                    case CharacterClass.Magician:
                        stateMachine.TransitionTo(magician);
                        break;
                    case CharacterClass.Assassin:
                        stateMachine.TransitionTo(assassin);
                        break;
                }
            }
        }
    }

    private void Awake()
    {
        // Dictionary 생성
        stateClass = new Dictionary<IState<PlayerClass>, CharacterClass>(5);

        // 각 State들 만들기
        fighter = new FighterClass();
        hunter = new HunterClass();
        berserker = new BerserkerClass();
        magician = new MagicianClass();
        assassin = new AssassinClass();

        // StateMachine 만들기
        stateMachine = new StateMachine<PlayerClass>(this, fighter);
    }

    private void Start()
    {
        // IState와 CharaterClass 연결하기
        stateClass[fighter] = CharacterClass.Fighter;
        stateClass[berserker] = CharacterClass.Berserker;
        stateClass[hunter] = CharacterClass.Hunter;
        stateClass[magician] = CharacterClass.Magician;
        stateClass[assassin] = CharacterClass.Assassin;

        // 능력치 부여
        onChangeClass?.Invoke(CharacterClass.Fighter);
    }

    /// <summary>
    /// 무기를 장착하는 함수
    /// </summary>
    /// <param name="part">무기를 장착할 위치</param>
    /// <param name="weapon">장착할 무기</param>
    /// <returns>장착한 무기오브젝트</returns>
    public GameObject EquipItem(EquipType part, Weapon weapon)
    {
        GameObject weaponInstance = Instantiate(weapon.weapon, weapon.weaponGenerationTransform.position, weapon.weaponGenerationTransform.rotation);   // 무기 오브젝트 생성

        weaponInstance.transform.SetParent(GetEquipParentTransform(part), false);   // 무기의 장착 부위의 자식으로 설정하기

        return weaponInstance;  // 무기 오브젝트 반환
    }

    /// <summary>
    /// 무기를 해제하는 함수
    /// </summary>
    /// <param name="part">무기 해제하기</param>
    public void UnEquipItem(EquipType part)
    {
        Destroy(GetEquipParentTransform(part).GetChild(0).gameObject);  // 무기를 하나만 끼고 있기 때문에 무기 파괴하기
    }

    /// <summary>
    /// 장착 부위별 Transform을 구하는 함수
    /// </summary>
    /// <param name="part">무기를 장착 또는 해제할 부위</param>
    /// <returns>장착 부위의 Transform</returns>
    public Transform GetEquipParentTransform(EquipType part)
    {
        Transform equipPart = null;

        // 장착 부위별로 저장되어 있는 트랜스폼 반환
        switch (part)
        {
            case EquipType.LeftHand:
                equipPart = equipTransform[0];
                break;
            case EquipType.RightHand:
                equipPart = equipTransform[1];
                break;
        }

        return equipPart;
    }
}
