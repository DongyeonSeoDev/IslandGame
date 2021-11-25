public class FarmingResearch : Research
{
    public override void Level1()  // ���� 1
    {
        base.Level1();

        ResearchManager.Instance.FarmLevel1Upgrade();  // ���� 1 ���׷��̵�
    }

    public override void Level2()  // ���� 2
    {
        base.Level2();

        ResearchManager.Instance.FarmLevel2Upgrade();  // ���� 2 ���׷��̵�
    }

    public override void Level3()  // ���� 3
    {
        base.Level3();

        ResearchManager.Instance.FarmLevel3Upgrade();  // ���� 3 ���׷��̵�
    }
}
