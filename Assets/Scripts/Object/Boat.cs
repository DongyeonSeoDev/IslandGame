using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Outline, IInteractable
{
    [SerializeField] private Transform walkTransform; // 플레이어가 가는 목표

    private bool focus = false; // 포커스가 맞춰저 있는지 확인

    public void EnterFocus() // 포커스가 맞춰짐
    {
        if (!focus)
        {
            focus = true;

            Debug.Log(gameObject.name + "을 선택하고 있습니다.");

            OutlineWidth = 3; // 아웃라인 추가
            OutlineColor = Color.white;
        }
    }

    public void ExitFocus() // 포커스 해제
    {
        focus = false;

        Debug.Log(gameObject.name + "선택을 해제했습니다.");

        OutlineWidth = 0; // 아웃라인 삭제
    }

    public Vector3 GetWalkPosition() // 플레이어가 가는 목표를 받아옴
    {
        return walkTransform.transform.position;
    }

    public void Interact() // 보트 클릭
    {
        GameManager.Instance.currentInteractablePosition = GetWalkPosition();

        GameManager.Instance.PlayerMove(() =>
        {
            UIManager.Instance.ShowBoat();
        });
    }

    // 사용하지는 않지만 인터페이스 구현 때문에 만들어놓음
    public void LeftButtonClick()
    {
        return;
    }

    public void RightButtonClick()
    {
        return;
    }

    public void UpButtonClick()
    {
        return;
    }

    public void DownButtonClick()
    {
        return;
    }


    public bool GetComplete()
    {
        return true;
    }

    public bool GetSeed()
    {
        return true;
    }

    public bool GetStone()
    {
        return true;
    }

    public bool GetTree()
    {
        return true;
    }

    public bool GetUseSunPower()
    {
        return true;
    }

    public bool GetWater()
    {
        return true;
    }
}
