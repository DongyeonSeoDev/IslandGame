using UnityEngine;
using UnityEngine.UI;

public abstract class Research : MonoBehaviour
{
    public Button[] buttons;

    private int level = 0;

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public int[] usePoint = null;

    public abstract void Level1();
    public abstract bool Level1Condition();
    public abstract void Level2();
    public abstract bool Level2Condition();
    public abstract void Level3();
    public abstract bool Level3Condition();
}
