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
            case UIType.tree:
                gameObject.SetActive(false);
                Debug.Log("tree ����");
                break;
            case UIType.stone:
                gameObject.SetActive(false);
                Debug.Log("stone ����");
                break;
            case UIType.none:
                Debug.Log("none Ŭ��");
                break;
            default:
                Debug.LogError("type�� �����ϴ�.");
                break;
        }
    }
}
