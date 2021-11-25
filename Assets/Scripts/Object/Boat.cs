using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Outline, IInteractable
{
    [SerializeField] private Transform walkTransform; // �÷��̾ ���� ��ǥ

    private bool focus = false; // ��Ŀ���� ������ �ִ��� Ȯ��

    public void EnterFocus() // ��Ŀ���� ������
    {
        if (!focus)
        {
            focus = true;

            Debug.Log(gameObject.name + "�� �����ϰ� �ֽ��ϴ�.");

            OutlineWidth = 3; // �ƿ����� �߰�
            OutlineColor = Color.white;
        }
    }

    public void ExitFocus() // ��Ŀ�� ����
    {
        focus = false;

        Debug.Log(gameObject.name + "������ �����߽��ϴ�.");

        OutlineWidth = 0; // �ƿ����� ����
    }

    public Vector3 GetWalkPosition() // �÷��̾ ���� ��ǥ�� �޾ƿ�
    {
        return walkTransform.transform.position;
    }

    public void Interact() // ��Ʈ Ŭ��
    {
        GameManager.Instance.currentInteractablePosition = GetWalkPosition();

        GameManager.Instance.PlayerMove(() =>
        {
            UIManager.Instance.ShowBoat();
        });
    }

    // ��������� ������ �������̽� ���� ������ ��������
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
