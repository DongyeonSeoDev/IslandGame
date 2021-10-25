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
            case UIType.tree:
                gameObject.SetActive(false);
                Debug.Log("tree 제거");
                break;
            case UIType.stone:
                gameObject.SetActive(false);
                Debug.Log("stone 제거");
                break;
            case UIType.none:
                Debug.Log("none 클릭");
                break;
            default:
                Debug.LogError("type이 없습니다.");
                break;
        }
    }
}
