using UnityEngine;

public class InteractableObject : Outline, IInteractable
{
    private bool focus = false;

    public UIType uiType = UIType.none;

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

    public void Interact()
    {
        Debug.Log(gameObject.name + "�� Ŭ���߽��ϴ�.");

        GameManager.Instance.currentInteractablePosition = transform.position;

        UIManager.Instance.OnUI(uiType);
    }

    public void UpButtonClick()
    {
        switch (uiType)
        {
            case UIType.farmField:
                GameManager.Instance.PlayerMove(() =>
                {
                    Debug.Log("����");
                });
                break;
        }
    }

    public void DownButtonClick()
    {
        switch (uiType)
        {
            case UIType.tree:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    GameManager.topUICount[TopUI.wood] += 5;
                    Debug.Log("tree ����");
                });
                break;
            case UIType.stone:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    GameManager.topUICount[TopUI.stone] += 3;
                    Debug.Log("stone ����");
                });
                break;
            case UIType.waterTank:
                GameManager.Instance.PlayerMove(() =>
                {
                    GameManager.topUICount[TopUI.water] = 100;
                    UIManager.Instance.ChangeWarming(TopUI.water, false);
                    Debug.Log("�� ����");
                });
                break;
            case UIType.lightHouse:
                GameManager.Instance.PlayerMove(() =>
                {
                    Debug.Log("��� �۵�");
                });
                break;
            case UIType.food:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    Debug.Log("���� ����");
                    GameManager.topUICount[TopUI.food] += 2;
                    if (GameManager.topUICount[TopUI.food] >= 4)
                    {
                        UIManager.Instance.ChangeWarming(TopUI.food, false);
                    }
                });
                break;
            case UIType.chest:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    Debug.Log("���ڸ� ����");
                    GameManager.topUICount[TopUI.wood] += 10;
                    GameManager.topUICount[TopUI.stone] += 10;
                    GameManager.topUICount[TopUI.food] += 10;
                    GameManager.topUICount[TopUI.iron] += 5;
                    if (GameManager.topUICount[TopUI.food] >= 4)
                    {
                        UIManager.Instance.ChangeWarming(TopUI.food, false);
                    }
                });
                break;
            case UIType.farmField:
                GameManager.Instance.PlayerMove(() =>
                {
                    Debug.Log("ä��");
                    GameManager.topUICount[TopUI.food] += 5;
                    if (GameManager.topUICount[TopUI.food] >= 4)
                    {
                        UIManager.Instance.ChangeWarming(TopUI.food, false);
                    }
                });
                break;
            case UIType.iron:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    GameManager.topUICount[TopUI.iron] += 1;
                    Debug.Log("iron ����");
                });
                break;
            case UIType.none:
                Debug.Log("none Ŭ��");
                break;
            default:
                Debug.LogError("type�� �����ϴ�.");
                break;
        }
    }

    public void RightButtonClick()
    {
        switch (uiType)
        {
            case UIType.farmField:
                GameManager.Instance.PlayerMove(() =>
                {
                    Debug.Log("��");
                });
                break;
        }
    }

    public void LeftButtonClick()
    {
        Debug.Log("����");
    }
}
