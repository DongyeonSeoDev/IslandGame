using UnityEngine;

public class InteractableObject : Outline, IInteractable
{
    private bool focus = false;

    public UIType uiType = UIType.none;

    private bool isUseSunPower = false;

    public GameObject seed;
    public GameObject tomato;
    public Material material;
    public Material currentMaterial;

    private int waterCount = 0;

    public bool isFarmFieldSeed = false;
    public bool isFarmFieldWater = false;
    public bool isFarmFieldComplete = false;

    public bool isStone = true;
    public bool isTree = true;

    public void EnterFocus()
    {
        if (!focus)
        {
            focus = true;

            Debug.Log(gameObject.name + "을 선택하고 있습니다.");

            OutlineWidth = 3;
            OutlineColor = Color.white;
        }
    }

    public void ExitFocus()
    {
        focus = false;

        Debug.Log(gameObject.name + "선택을 해제했습니다.");

        OutlineWidth = 0;
    }

    public void Interact()
    {
        Debug.Log(gameObject.name + "를 클릭했습니다.");

        GameManager.Instance.currentInteractablePosition = transform.position;

        UIManager.Instance.OnUI(uiType);
    }

    public void UpButtonClick()
    {
        switch (uiType)
        {
            case UIType.farmField:
                if (isFarmFieldSeed)
                {
                    return;
                }
                GameManager.Instance.PlayerMove(() =>
                {
                    if (!isFarmFieldSeed)
                    {
                        Debug.Log("씨앗");
                        seed.SetActive(true);
                        isFarmFieldSeed = true;
                    }
                });
                break;
        }
    }

    public void DownButtonClick()
    {
        switch (uiType)
        {
            case UIType.tree:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    GameManager.topUICount[TopUI.wood] += 5;
                    Debug.Log("tree 제거");
                });
                break;
            case UIType.stone:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    GameManager.topUICount[TopUI.stone] += 3;
                    Debug.Log("stone 제거");
                });
                break;
            case UIType.waterTank:
                GameManager.Instance.PlayerMove(() =>
                {
                    GameManager.topUICount[TopUI.water] = 100;
                    UIManager.Instance.ChangeWarming(TopUI.water, false);
                    Debug.Log("물 충전");
                });
                break;
            case UIType.lightHouse:
                GameManager.Instance.PlayerMove(() =>
                {
                    GameManager.Instance.isLight = true;
                    Debug.Log("등대 작동");
                });
                break;
            case UIType.food:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    Debug.Log("음식 얻음");
                    GameManager.topUICount[TopUI.food] += 2;
                    if (GameManager.topUICount[TopUI.food] >= 4)
                    {
                        UIManager.Instance.ChangeWarming(TopUI.food, false);
                    }
                });
                break;
            case UIType.chest:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    Debug.Log("상자를 열음");
                    GameManager.topUICount[TopUI.wood] += 10;
                    GameManager.topUICount[TopUI.stone] += 10;
                    GameManager.topUICount[TopUI.food] += 10;
                    GameManager.topUICount[TopUI.iron] += 5;
                    if (GameManager.topUICount[TopUI.food] >= 4)
                    {
                        UIManager.Instance.ChangeWarming(TopUI.food, false);
                    }
                });
                break;
            case UIType.farmField:
                if (!isFarmFieldComplete)
                {
                    return;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    if (!isFarmFieldComplete)
                    {
                        return;
                    }

                    Debug.Log("채집");

                    GameManager.topUICount[TopUI.food] += 5;
                    seed.SetActive(false);
                    tomato.SetActive(false);

                    isFarmFieldComplete = false;
                    isFarmFieldSeed = false;
                    isFarmFieldWater = false;

                    MeshRenderer mesh = GetComponent<MeshRenderer>();
                    mesh.material = currentMaterial;
                    waterCount = 0;

                    if (GameManager.topUICount[TopUI.food] >= 4)
                    {
                        UIManager.Instance.ChangeWarming(TopUI.food, false);
                    }
                });
                break;
            case UIType.iron:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    GameManager.topUICount[TopUI.iron] += 1;
                    Debug.Log("iron 제거");
                });
                break;
            case UIType.sunPower:

                if (isUseSunPower)
                {
                    break;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    if (!isUseSunPower)
                    {
                        GameManager.topUICount[TopUI.electricity] += 20;
                        isUseSunPower = true;
                        Debug.Log("전기를 얻어옴");
                    }
                });
                break;
            case UIType.raft:

                RaftMove raftMove = GetComponent<RaftMove>();

                GameManager.Instance.PlayerMove(() =>
                {
                    raftMove.Move();
                    Debug.Log("이동");
                });
                break;
            case UIType.none:
                Debug.Log("none 클릭");
                break;
            default:
                Debug.LogError("type이 없습니다.");
                break;
        }
    }

    public void RightButtonClick()
    {
        switch (uiType)
        {
            case UIType.farmField:
                if (isFarmFieldWater || !isFarmFieldSeed || GameManager.topUICount[TopUI.water] < 21)
                {
                    return;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    if (isFarmFieldWater)
                    {
                        return;
                    }

                    MeshRenderer mesh = GetComponent<MeshRenderer>();
                    currentMaterial = mesh.material;
                    mesh.material = material;
                    isFarmFieldWater = true;

                    waterCount++;
                    GameManager.topUICount[TopUI.water] -= 3;

                    if (waterCount >= 2)
                    {
                        isFarmFieldComplete = true;
                        tomato.SetActive(true);
                    }
                    else
                    {
                        Invoke("ChangeWater", 30);
                    }

                    Debug.Log("물");
                });
                break;
            case UIType.tree:
                if (!isTree)
                {
                    return;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    if (!isTree)
                    {
                        return;
                    }

                    isTree = false;

                    GameManager.topUICount[TopUI.wood] += 1;
                    Invoke("Wood", 30f);
                });
                break;
            case UIType.stone:
                if (!isStone)
                {
                    return;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    if (!isStone)
                    {
                        return;
                    }

                    isStone = false;

                    GameManager.topUICount[TopUI.stone] += 1;
                    Invoke("Stone", 30f);
                });
                break;
        }
    }

    private void Wood()
    {
        isTree = true;
    }

    private void Stone()
    {
        isStone = true;
    }

    private void ChangeWater()
    {
        isFarmFieldWater = false;

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.material = currentMaterial;
    }

    public void LeftButtonClick()
    {
        Debug.Log("닫힘");
    }

    public bool GetUseSunPower()
    {
        return isUseSunPower;
    }

    public bool GetSeed()
    {
        return isFarmFieldSeed;
    }

    public bool GetWater()
    {
        return isFarmFieldWater;
    }

    public bool GetComplete()
    {
        return isFarmFieldComplete;
    }

    public bool GetStone()
    {
        return isStone;
    }

    public bool GetTree()
    {
        return isTree;
    }
}
