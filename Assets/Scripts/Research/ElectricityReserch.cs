public class ElectricityReserch : Research
{
    public override void Level1() // ���� 1
    {
        base.Level1();

        ResearchManager.Instance.ElectricityLevel1Upgrade(); // ���� 1 ���׷��̵�
    }

    public override void Level2() // ���� 2
    {
        base.Level2();

        ResearchManager.Instance.ElectricityLevel2Upgrade(); // ���� 2 ���׷��̵�
    }

    public override void Level3() // ���� 3
    {
        base.Level3();

        ResearchManager.Instance.ElectricityLevel3Upgrade(); // ���� 3 ���׷��̵�
    }
}
