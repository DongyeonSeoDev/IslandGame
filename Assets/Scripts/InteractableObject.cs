using UnityEngine;

public class InteractableObject : Outline, IInteractable
{
    private bool focus = false;

    private PlayerMove playerMove;

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

        UIManager.Instance.OnUI();
    }
}
