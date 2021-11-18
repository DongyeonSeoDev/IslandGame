using UnityEngine;

public class InteractableObject : Outline, IInteractable
{
    private PlayerMove playerMove = null;

    [SerializeField] private Transform walkTransform;

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

    protected override void Awake()
    {
        base.Awake();

        playerMove = FindObjectOfType<PlayerMove>();
    }

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

        GameManager.Instance.currentInteractablePosition = GetWalkPosition();

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

                        ResearchManager.Instance.ReserchPoint += 1;
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
                if (!ResearchManager.Instance.isUseAxe || InventoryManager.GetItemCount(InventoryItem.Axe) <= 0)
                {
                    return;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    InventoryManager.AddItem(InventoryItem.Tree, 5);
                    Debug.Log("tree 제거");

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.stone:

                if (!ResearchManager.Instance.isUsePickax || InventoryManager.GetItemCount(InventoryItem.Pickax) <= 0)
                {
                    return;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    InventoryManager.AddItem(InventoryItem.Stone, 2);
                    InventoryManager.AddItem(InventoryItem.Flint, 2);
                    InventoryManager.AddItem(InventoryItem.FlakesOfIron, 1);
                    Debug.Log("stone 제거");

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.waterTank:

                if (!InventoryManager.UseItem(InventoryItem.NoWaterBottle, 1))
                {
                    return;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    InventoryManager.AddItem(InventoryItem.UnrefinedWaterBottle, 1);
                    Debug.Log("물 충전");

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.lightHouse:

                if (InventoryManager.GetItemCount(InventoryItem.ElectricalParts) <= 0 || InventoryManager.GetItemCount(InventoryItem.ElectricWires) <= 0)
                {
                    return;
                }

                InventoryManager.UseItem(InventoryItem.ElectricalParts, 1);
                InventoryManager.UseItem(InventoryItem.ElectricWires, 1);

                GameManager.Instance.PlayerMove(() =>
                {
                    GameManager.Instance.isLight = true;
                    Debug.Log("등대 작동");

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.food:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    Debug.Log("음식 얻음");
                    GameManager.topUICount[TopUI.food] += 2;

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.chest:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    Debug.Log("상자를 열음");
                    InventoryManager.AddItem(InventoryItem.Tree, 10);
                    InventoryManager.AddItem(InventoryItem.Stone, 10);
                    InventoryManager.AddItem(InventoryItem.Iron, 5);
                    InventoryManager.AddItem(InventoryItem.PartsBox, 1);
                    GameManager.topUICount[TopUI.food] += 10;

                    ResearchManager.Instance.ReserchPoint += 1;
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

                    InventoryManager.AddItem(InventoryItem.Crops, (int)(2 * ResearchManager.Instance.farmValue));
                    seed.SetActive(false);
                    tomato.SetActive(false);

                    isFarmFieldComplete = false;
                    isFarmFieldSeed = false;
                    isFarmFieldWater = false;

                    MeshRenderer mesh = GetComponent<MeshRenderer>();
                    mesh.material = currentMaterial;
                    waterCount = 0;

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.iron:

                if (!ResearchManager.Instance.isUseStrongPickax || InventoryManager.GetItemCount(InventoryItem.StrongPickaxe) <= 0)
                {
                    return;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    InventoryManager.AddItem(InventoryItem.Iron, 1);
                    Debug.Log("iron 제거");

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.sunPower:

                if (InventoryManager.GetItemCount(InventoryItem.ElectricalParts) <= 0 || InventoryManager.GetItemCount(InventoryItem.ElectricWires) <= 0)
                {
                    return;
                }

                InventoryManager.UseItem(InventoryItem.ElectricalParts, 1);
                InventoryManager.UseItem(InventoryItem.ElectricWires, 1);

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

                        ResearchManager.Instance.ReserchPoint += 1;
                    }
                });
                break;
            case UIType.raft:

                RaftMove raftMove = GetComponent<RaftMove>();

                GameManager.Instance.PlayerMove(() =>
                {
                    raftMove.Move();
                    Debug.Log("이동");
                }, false, null, false);
                break;
            case UIType.water:

                if (!InventoryManager.UseItem(InventoryItem.UnrefinedWaterBottle, 1))
                {
                    return;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    InventoryManager.AddItem(InventoryItem.RefinedWaterBottle, 1);

                    ResearchManager.Instance.ReserchPoint += 1;
                }, false, null, false);
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

                    ResearchManager.Instance.ReserchPoint += 1;
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

                    InventoryManager.AddItem(InventoryItem.Branch, 2);
                    InventoryManager.AddItem(InventoryItem.Leaf, 1);
                    InventoryManager.AddItem(InventoryItem.Stem, 2);
                    Invoke("Wood", 30f);

                    ResearchManager.Instance.ReserchPoint += 1;
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

                    InventoryManager.AddItem(InventoryItem.FlakesOfStone, 1);
                    Invoke("Stone", 30f);

                    ResearchManager.Instance.ReserchPoint += 1;
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

    public Vector3 GetWalkPosition()
    {
        return walkTransform.position - (Quaternion.FromToRotation(transform.position, playerMove.transform.position) * Vector3.one);
    }
}
