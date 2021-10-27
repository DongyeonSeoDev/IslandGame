using UnityEngine;

public class InteractableObject : Outline, IInteractable
{
    private bool focus = false;

    private PlayerMove playerMove;

    public UIType uiType = UIType.none;

    private void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
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

    public void Interact()
    {
        Debug.Log(gameObject.name + "�� Ŭ���߽��ϴ�.");
        playerMove.TargetPosition = transform.position;

        UIManager.Instance.OnUI(uiType);
    }

    public void UpButtonClick()
    {
        switch (uiType)
        {
            case UIType.farmField:
                Debug.Log("����");
                break;
        }
    }

    public void DownButtonClick()
    {
        switch (uiType)
        {
            case UIType.tree:
                gameObject.SetActive(false);
                GameManager.topUICount[TopUI.wood] += 5;
                Debug.Log("tree ����");
                break;
            case UIType.stone:
                gameObject.SetActive(false);
                GameManager.topUICount[TopUI.stone] += 3;
                Debug.Log("stone ����");
                break;
            case UIType.waterTank:
                GameManager.topUICount[TopUI.water] = 100;
                UIManager.Instance.ChangeWarming(TopUI.water, false);
                Debug.Log("�� ����");
                break;
            case UIType.lightHouse:
                Debug.Log("��� �۵�");
                break;
            case UIType.food:
                gameObject.SetActive(false);
                Debug.Log("���� ����");
                GameManager.topUICount[TopUI.food] += 2;
                if (GameManager.topUICount[TopUI.food] >= 4)
                {
                    UIManager.Instance.ChangeWarming(TopUI.food, false);
                }
                break;
            case UIType.chest:
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
                break;
            case UIType.farmField:
                Debug.Log("ä��");
                GameManager.topUICount[TopUI.food] += 5;
                if (GameManager.topUICount[TopUI.food] >= 4)
                {
                    UIManager.Instance.ChangeWarming(TopUI.food, false);
                }
                break;
            case UIType.iron:
                gameObject.SetActive(false);
                GameManager.topUICount[TopUI.iron] += 1;
                Debug.Log("iron ����");
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
                Debug.Log("��");
                break;
        }
    }

    public void LeftButtonClick()
    {
        Debug.Log("����");
    }
}
