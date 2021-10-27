using UnityEngine;

public interface IInteractable
{
    public void EnterFocus();
    public void ExitFocus();
    public void Interact();
    public void UpButtonClick();
    public void DownButtonClick();
    public void RightButtonClick();
    public void LeftButtonClick();
}