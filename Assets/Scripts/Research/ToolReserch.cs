public class ToolReserch : Research
{
    public override void Level1() // ���� 1
    {
        base.Level1();

        ResearchManager.Instance.ToolLevel1Upgrade(); // ���� 1 ���׷��̵�
    }

    public override void Level2() // ���� 2
    {
        base.Level2();

        ResearchManager.Instance.ToolLevel2Upgrade(); // ���� 2 ���׷��̵�
    }

    public override void Level3() // ���� 3
    {
        base.Level3();

        ResearchManager.Instance.ToolLevel3Upgrade(); // ���� 3 ���׷��̵�
    }
}
