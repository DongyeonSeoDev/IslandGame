public class TopUICount // UI ������ ������ �����ϴ� Ŭ����
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
