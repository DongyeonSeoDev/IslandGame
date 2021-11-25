public class ToolReserch : Research
{
    public override void Level1() // 레벨 1
    {
        base.Level1();

        ResearchManager.Instance.ToolLevel1Upgrade(); // 레벨 1 업그레이드
    }

    public override void Level2() // 레벨 2
    {
        base.Level2();

        ResearchManager.Instance.ToolLevel2Upgrade(); // 레벨 2 업그레이드
    }

    public override void Level3() // 레벨 3
    {
        base.Level3();

        ResearchManager.Instance.ToolLevel3Upgrade(); // 레벨 3 업그레이드
    }
}
