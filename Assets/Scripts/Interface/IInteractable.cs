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
    //TODO
    public bool GetUseSunPower();
    public bool GetSeed();
    public bool GetWater();
    public bool GetComplete();
    public bool GetStone();
    public bool GetTree();
}