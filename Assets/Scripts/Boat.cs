using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Outline, IInteractable
{
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

            Debug.Log(gameObject.name + "�� �����ϰ� �ֽ��ϴ�.");

            OutlineWidth = 3;
            OutlineColor = Color.white;
        }
    }

    public void ExitFocus()
    {
        focus = false;

        Debug.Log(gameObject.name + "������ �����߽��ϴ�.");

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

    public bool GetWater()
    {
        return true;
    }

    public void Interact()
    {
        UIManager.Instance.ShowBoat();
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
