public class ElectricityReserch : Research
{
    public override void Level1()
    {
        base.Level1();

        ResearchManager.Instance.ElectricityLevel1Upgrade();
    }

    public override void Level2()
    {
        base.Level2();

        ResearchManager.Instance.ElectricityLevel2Upgrade();
    }

    public override void Level3()
    {
        base.Level3();

        ResearchManager.Instance.ElectricityLevel3Upgrade();
    }
}
