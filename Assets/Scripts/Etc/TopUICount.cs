public class TopUICount // UI 아이템 갯수를 관리하는 클래스
{
    private int[] topUICount = new int[6];

    public int this[TopUI index]
    {
        get
        {
            return topUICount[(int)index];
        }

        set
        {
            topUICount[(int)index] = value;

            UIManager.Instance.SetTopUIText(index, topUICount[(int)index]);
        }
    }
}
