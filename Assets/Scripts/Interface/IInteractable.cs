using UnityEngine;

public interface IInteractable // ��ȣ�ۿ��� ������ ������Ʈ�� �����ϴ� �������̽�
{
    public void EnterFocus(); // ���콺�� ��Ŀ�� �ȿ� ������ �� ����
    public void ExitFocus(); // ���콺�� ��Ŀ�� ������ �������� ����
    public void Interact(); // ������Ʈ�� Ŭ�������� ����
    public void UpButtonClick(); // UI���� ���� ��ư�� Ŭ�������� ����
    public void DownButtonClick(); // UI���� �Ʒ��� ��ư�� Ŭ�������� ����
    public void RightButtonClick(); // UI���� ������ ��ư�� Ŭ�������� ����
    public void LeftButtonClick(); // UI���� ���� ��ư�� Ŭ�������� ����
    public Vector3 GetWalkPosition(); // �÷��̾ �̵��ؾ� �ϴ� ��ġ�� ������
    public bool GetUseSunPower(); // �����⸦ ����ߴ����� ������
    public bool GetSeed(); // ������ �־����� ������
    public bool GetWater(); // ���� �־����� ������
    public bool GetComplete(); // ��簡 �Ϸ�Ǿ������� ������
    public bool GetStone(); // ���� ���� �� �ִ����� ������
    public bool GetTree(); // ������ ���� �� �ִ����� ������
}