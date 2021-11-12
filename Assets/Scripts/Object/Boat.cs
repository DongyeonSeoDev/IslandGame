using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Outline, IInteractable
{
    [SerializeField] private Transform walkTransform;

    private bool focus = false;

    public void DownButtonClick()
    {
        return;
    }

    public void EnterFocus()
    {
        if (!focus)
        {
            focus = true;

            Debug.Log(gameObject.name + "을 선택하고 있습니다.");

            OutlineWidth = 3;
            OutlineColor = Color.white;
        }
    }

    public void ExitFocus()
    {
        focus = false;

        Debug.Log(gameObject.name + "선택을 해제했습니다.");

        OutlineWidth = 0;
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

    public Vector3 GetWalkPosition()
    {
        return walkTransform.transform.position;
    }

    public bool GetWater()
    {
        return true;
    }

    public void Interact()
    {
        GameManager.Instance.currentInteractablePosition = GetWalkPosition();

        GameManager.Instance.PlayerMove(() =>
        {
            UIManager.Instance.ShowBoat();
        });
    }

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
}
