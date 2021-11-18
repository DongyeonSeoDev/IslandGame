public class ToolReserch : Research
{
    public override void Level1()
    {
        base.Level1();

        ResearchManager.Instance.ToolLevel1Upgrade();
    }

    public override void Level2()
    {
        base.Level2();

        ResearchManager.Instance.ToolLevel2Upgrade();
    }

    public override void Level3()
    {
        base.Level3();

        ResearchManager.Instance.ToolLevel3Upgrade();
    }
}
