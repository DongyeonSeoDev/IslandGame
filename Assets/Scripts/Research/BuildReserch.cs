public class BuildReserch : Research
{
    public override void Level1()
    {
        base.Level1();

        ResearchManager.Instance.BuildLevel1Upgrade();
    }

    public override void Level2()
    {
        base.Level2();

        ResearchManager.Instance.BuildLevel2Upgrade();
    }

    public override void Level3()
    {
        base.Level3();

        ResearchManager.Instance.BuildLevel3Upgrade();
    }
}
