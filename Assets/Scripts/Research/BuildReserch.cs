public class BuildReserch : Research
{
    public override void Level1() //레벨1
    {
        base.Level1();

        ResearchManager.Instance.BuildLevel1Upgrade(); //레벨1 업그레이드
    }

    public override void Level2() //레벨2
    {
        base.Level2();

        ResearchManager.Instance.BuildLevel2Upgrade(); //레벨2 업그레이드
    }

    public override void Level3() //레벨3
    {
        base.Level3();

        ResearchManager.Instance.BuildLevel3Upgrade(); //레벨3 업그레이드
    }
}
