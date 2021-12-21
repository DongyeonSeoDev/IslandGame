public class TopUICount // UI ������ ������ �����ϴ� Ŭ����
{
    private int[] topUICount = new int[3];

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

    public int[] GetAllTopUICount()
    {
        return topUICount;
    }
}
