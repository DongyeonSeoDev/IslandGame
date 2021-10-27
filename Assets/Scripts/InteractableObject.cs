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

    public void Interact()
    {
        Debug.Log(gameObject.name + "를 클릭했습니다.");
        playerMove.TargetPosition = transform.position;

        UIManager.Instance.OnUI(uiType);
    }

    public void UpButtonClick()
    {
        switch (uiType)
        {
            case UIType.farmField:
                Debug.Log("씨앗");
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
                Debug.Log("tree 제거");
                break;
            case UIType.stone:
                gameObject.SetActive(false);
                GameManager.topUICount[TopUI.stone] += 3;
                Debug.Log("stone 제거");
                break;
            case UIType.waterTank:
                GameManager.topUICount[TopUI.water] = 100;
                UIManager.Instance.ChangeWarming(TopUI.water, false);
                Debug.Log("물 충전");
                break;
            case UIType.lightHouse:
                Debug.Log("등대 작동");
                break;
            case UIType.food:
                gameObject.SetActive(false);
                Debug.Log("음식 얻음");
                GameManager.topUICount[TopUI.food] += 2;
                if (GameManager.topUICount[TopUI.food] >= 4)
                {
                    UIManager.Instance.ChangeWarming(TopUI.food, false);
                }
                break;
            case UIType.chest:
                gameObject.SetActive(false);
                Debug.Log("상자를 열음");
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
                Debug.Log("채집");
                GameManager.topUICount[TopUI.food] += 5;
                if (GameManager.topUICount[TopUI.food] >= 4)
                {
                    UIManager.Instance.ChangeWarming(TopUI.food, false);
                }
                break;
            case UIType.iron:
                gameObject.SetActive(false);
                GameManager.topUICount[TopUI.iron] += 1;
                Debug.Log("iron 제거");
                break;
            case UIType.none:
                Debug.Log("none 클릭");
                break;
            default:
                Debug.LogError("type이 없습니다.");
                break;
        }
    }

    public void RightButtonClick()
    {
        switch (uiType)
        {
            case UIType.farmField:
                Debug.Log("물");
                break;
        }
    }

    public void LeftButtonClick()
    {
        Debug.Log("닫힘");
    }
}
