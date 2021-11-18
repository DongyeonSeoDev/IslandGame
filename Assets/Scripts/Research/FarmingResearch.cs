public class FarmingResearch : Research
{
    public override void Level1()
    {
        base.Level1();

        ResearchManager.Instance.FarmLevel1Upgrade();
    }

    public override void Level2()
    {
        base.Level2();

        ResearchManager.Instance.FarmLevel2Upgrade();
    }

    public override void Level3()
    {
        base.Level3();

        ResearchManager.Instance.FarmLevel3Upgrade();
    }
}
