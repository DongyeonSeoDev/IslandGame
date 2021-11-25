using UnityEngine;

public class InteractableObject : Outline, IInteractable
{
    [SerializeField] private Transform walkTransform; // �÷��̾� �̵� ��ġ

    private PlayerMove playerMove = null; // �÷��̾� Ŭ����

    private bool focus = false; // ��Ŀ�� ����

    public UIType uiType = UIType.None; // ��ġ �ǹ� ����

    private bool isUseSunPower = false; // �����⸦ ����ߴ���
    public bool isStone = true; // ���� ���� �� �ִ���
    public bool isTree = true; // ������ ���� �� �ִ���

    // ��� ���� ����
    public GameObject seed; //���� ������Ʈ
    public GameObject tomato; // ���� ������Ʈ
    public Material material; // ���� ���� ���׸���
    public Material currentMaterial; // ���� ���׸���

    private int waterCount = 0; // ���� �� Ƚ��

    public bool isFarmFieldSeed = false; //���� ����
    public bool isFarmFieldWater = false; // �� ����
    public bool isFarmFieldComplete = false; // ��� �Ϸ�

    protected override void Awake()
    {
        //���� �ʱ�ȭ
        base.Awake();

        playerMove = FindObjectOfType<PlayerMove>();
    }

    public void EnterFocus() // ��Ŀ�� ���϶�
    {
        if (!focus)
        {
            focus = true;

            Debug.Log(gameObject.name + "�� �����ϰ� �ֽ��ϴ�.");

            OutlineWidth = 3; // �׵θ� ǥ��
            OutlineColor = Color.white;
        }
    }

    public void ExitFocus() // ��Ŀ���� ��������
    {
        focus = false;

        Debug.Log(gameObject.name + "������ �����߽��ϴ�.");

        OutlineWidth = 0; // �׵θ� ����
    }

    public void Interact() // ������Ʈ Ŭ��
    {
        Debug.Log(gameObject.name + "�� Ŭ���߽��ϴ�.");

        GameManager.Instance.currentInteractablePosition = GetWalkPosition(); // �̵���ǥ ����

        UIManager.Instance.OnUI(uiType); // UI ǥ��
    }

    public void UpButtonClick() // �� ��ư Ŭ��
    {
        //�����۰� �ǹ� ������ ���� ������ �޶���
        switch (uiType)
        {
            case UIType.FarmField: // �ǹ� ����
                if (isFarmFieldSeed) // ��� �������� Ȯ��
                {
                    return;
                }
                GameManager.Instance.PlayerMove(() => // �̵�
                {
                    if (!isFarmFieldSeed)
                    {
                        Debug.Log("����"); // ���
                        seed.SetActive(true);
                        isFarmFieldSeed = true;

                        ResearchManager.Instance.ReserchPoint += 1; // ���� ����Ʈ ����
                    }
                });
                break;
        }
    }

    public void DownButtonClick() // �Ʒ� ��ư Ŭ��
    {
        //�����۰� �ǹ� ������ ���� ������ �޶���
        switch (uiType)
        {
            case UIType.Tree:
                if (!ResearchManager.Instance.isUseAxe || InventoryManager.GetItemCount(InventoryItem.Axe) <= 0)
                {
                    return;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    InventoryManager.AddItem(InventoryItem.Tree, 5);
                    Debug.Log("tree ����");

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.Stone:

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
                    Debug.Log("stone ����");

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.WaterTank:

                if (!InventoryManager.UseItem(InventoryItem.NoWaterBottle, 1))
                {
                    return;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    InventoryManager.AddItem(InventoryItem.UnrefinedWaterBottle, 1);
                    Debug.Log("�� ����");

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.LightHouse:

                if (InventoryManager.GetItemCount(InventoryItem.ElectricalParts) <= 0 || InventoryManager.GetItemCount(InventoryItem.ElectricWires) <= 0)
                {
                    return;
                }

                InventoryManager.UseItem(InventoryItem.ElectricalParts, 1);
                InventoryManager.UseItem(InventoryItem.ElectricWires, 1);

                GameManager.Instance.PlayerMove(() =>
                {
                    GameManager.Instance.isLight = true;
                    Debug.Log("��� �۵�");

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.Food:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    Debug.Log("���� ����");
                    GameManager.topUICount[TopUI.Food] += 2;

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.Chest:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    Debug.Log("���ڸ� ����");
                    InventoryManager.AddItem(InventoryItem.Tree, 10);
                    InventoryManager.AddItem(InventoryItem.Stone, 10);
                    InventoryManager.AddItem(InventoryItem.Iron, 5);
                    InventoryManager.AddItem(InventoryItem.PartsBox, 1);
                    GameManager.topUICount[TopUI.Food] += 10;

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.FarmField:
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

                    Debug.Log("ä��");

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
            case UIType.Iron:

                if (!ResearchManager.Instance.isUseStrongPickax || InventoryManager.GetItemCount(InventoryItem.StrongPickaxe) <= 0)
                {
                    return;
                }

                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    InventoryManager.AddItem(InventoryItem.Iron, 1);
                    Debug.Log("iron ����");

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.SunPower:

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
                        GameManager.topUICount[TopUI.Electricity] += 20;
                        isUseSunPower = true;
                        Debug.Log("���⸦ ����");

                        ResearchManager.Instance.ReserchPoint += 1;
                    }
                });
                break;
            case UIType.Raft:

                RaftMove raftMove = GetComponent<RaftMove>();

                GameManager.Instance.PlayerMove(() =>
                {
                    raftMove.Move();
                    Debug.Log("�̵�");
                }, false, null, false);
                break;
            case UIType.Water:

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
            case UIType.None:
                Debug.Log("none Ŭ��");
                break;
            default:
                Debug.LogError("type�� �����ϴ�.");
                break;
        }
    }

    public void RightButtonClick() // ������ ��ư Ŭ��
    {
        switch (uiType)
        {
            case UIType.FarmField:
                if (isFarmFieldWater || !isFarmFieldSeed || GameManager.topUICount[TopUI.Water] < 21)
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
                    GameManager.topUICount[TopUI.Water] -= 3;

                    if (waterCount >= 2)
                    {
                        isFarmFieldComplete = true;
                        tomato.SetActive(true);
                    }
                    else
                    {
                        Invoke("ChangeWater", 30);
                    }

                    Debug.Log("��");

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.Tree:
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
            case UIType.Stone:
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

    public void LeftButtonClick() // ���� ��ư Ŭ��
    {
        Debug.Log("����"); // ���� ��ư�� â�� �ݴ� ��ư��
    }

    private void Wood() // ���� Ȯ��
    {
        isTree = true;
    }

    private void Stone() // �� Ȯ��
    {
        isStone = true;
    }

    private void ChangeWater() // �� ���׸��� ����
    {
        isFarmFieldWater = false;

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.material = currentMaterial;
    }

    public bool GetUseSunPower() // �����Ұ� ��� ���� ����
    {
        return isUseSunPower;
    }
    
    // ��� ���� ���� �� �� �ִ���
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

    // �� ���� ����
    public bool GetStone()
    {
        return isStone;
    }

    // ���� ���� ����
    public bool GetTree()
    {
        return isTree;
    }

    // �÷��̾� �̵� ��ġ ����
    public Vector3 GetWalkPosition()
    {
        return walkTransform.position - (Quaternion.FromToRotation(transform.position, playerMove.transform.position) * Vector3.one);
    }
}
