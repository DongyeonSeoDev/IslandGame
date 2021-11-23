using UnityEngine;

public interface IInteractable // 상호작용이 가능한 오브젝트를 관리하는 인터페이스
{
    public void EnterFocus(); // 마우스가 포커스 안에 들어왔을 때 실행
    public void ExitFocus(); // 마우스가 포커스 밖으로 나갔을때 실행
    public void Interact(); // 오브젝트를 클릭했을때 실행
    public void UpButtonClick(); // UI에서 위쪽 버튼을 클릭했을때 실행
    public void DownButtonClick(); // UI에서 아레쪽 버튼을 클릭했을때 실행
    public void RightButtonClick(); // UI에서 오른쪽 버튼을 클릭했을때 실행
    public void LeftButtonClick(); // UI에서 왼쪽 버튼을 클릭했을때 실행
    public Vector3 GetWalkPosition(); // 플레이어가 이동해야 하는 위치를 가져옴
    public bool GetUseSunPower(); // 발전기를 사용했는지를 가져옴
    public bool GetSeed(); // 씨앗을 주었는지 가져옴
    public bool GetWater(); // 물을 주었는지 가져옴
    public bool GetComplete(); // 농사가 완료되었는지를 가져옴
    public bool GetStone(); // 돌을 얻을 수 있는지를 가져옴
    public bool GetTree(); // 나무를 얻을 수 있는지를 가져옴
}