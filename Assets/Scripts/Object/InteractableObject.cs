using UnityEngine;

public class InteractableObject : Outline, IInteractable
{
    [SerializeField] private Transform walkTransform; // 플레이어 이동 위치

    private PlayerMove playerMove = null; // 플레이어 클래스

    private bool focus = false; // 포커스 상태

    public UIType uiType = UIType.None; // 설치 건물 종류

    private bool isUseSunPower = false; // 발전기를 사용했는지
    public bool isStone = true; // 돌을 얻을 수 있는지
    public bool isTree = true; // 나무를 얻을 수 있는지

    // 농사 전용 변수
    public GameObject seed; //씨앗 오브젝트
    public GameObject tomato; // 열매 오브젝트
    public Material material; // 물에 젖은 메테리얼
    public Material currentMaterial; // 현재 메테리얼

    private int waterCount = 0; // 물을 준 횟수

    public bool isFarmFieldSeed = false; //씨앗 상태
    public bool isFarmFieldWater = false; // 물 상태
    public bool isFarmFieldComplete = false; // 농사 완료

    protected override void Awake()
    {
        //변수 초기화
        base.Awake();

        playerMove = FindObjectOfType<PlayerMove>();
    }

    public void EnterFocus() // 포커스 안일때
    {
        if (!focus)
        {
            focus = true;

            Debug.Log(gameObject.name + "을 선택하고 있습니다.");

            OutlineWidth = 3; // 테두리 표시
            OutlineColor = Color.white;
        }
    }

    public void ExitFocus() // 포커스를 나갔을때
    {
        focus = false;

        Debug.Log(gameObject.name + "선택을 해제했습니다.");

        OutlineWidth = 0; // 테두리 제거
    }

    public void Interact() // 오브젝트 클릭
    {
        Debug.Log(gameObject.name + "를 클릭했습니다.");

        GameManager.Instance.currentInteractablePosition = GetWalkPosition(); // 이동좌표 설정

        UIManager.Instance.OnUI(uiType); // UI 표시
    }

    public void UpButtonClick() // 위 버튼 클릭
    {
        //아이템과 건물 종류에 따라 조건이 달라짐
        switch (uiType)
        {
            case UIType.FarmField: // 건물 종류
                if (isFarmFieldSeed) // 사용 가능한지 확인
                {
                    return;
                }
                GameManager.Instance.PlayerMove(() => // 이동
                {
                    if (!isFarmFieldSeed)
                    {
                        Debug.Log("씨앗"); // 사용
                        seed.SetActive(true);
                        isFarmFieldSeed = true;

                        ResearchManager.Instance.ReserchPoint += 1; // 연구 포인트 오름
                    }
                });
                break;
        }
    }

    public void DownButtonClick() // 아레 버튼 클릭
    {
        //아이템과 건물 종류에 따라 조건이 달라짐
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
                    Debug.Log("tree 제거");

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
                    Debug.Log("stone 제거");

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
                    Debug.Log("물 충전");

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
                    Debug.Log("등대 작동");

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.Food:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    Debug.Log("음식 얻음");
                    GameManager.topUICount[TopUI.Food] += 2;

                    ResearchManager.Instance.ReserchPoint += 1;
                });
                break;
            case UIType.Chest:
                GameManager.Instance.PlayerMove(() =>
                {
                    gameObject.SetActive(false);
                    Debug.Log("상자를 열음");
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
            case UIType.Iron:

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
                        Debug.Log("전기를 얻어옴");

                        ResearchManager.Instance.ReserchPoint += 1;
                    }
                });
                break;
            case UIType.Raft:

                RaftMove raftMove = GetComponent<RaftMove>();

                GameManager.Instance.PlayerMove(() =>
                {
                    raftMove.Move();
                    Debug.Log("이동");
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
                Debug.Log("none 클릭");
                break;
            default:
                Debug.LogError("type이 없습니다.");
                break;
        }
    }

    public void RightButtonClick() // 오른쪽 버튼 클릭
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

                    Debug.Log("물");

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

    public void LeftButtonClick() // 왼쪽 버튼 클릭
    {
        Debug.Log("닫힘"); // 왼쪽 버튼은 창을 닫는 버튼임
    }

    private void Wood() // 나무 확인
    {
        isTree = true;
    }

    private void Stone() // 돌 확인
    {
        isStone = true;
    }

    private void ChangeWater() // 물 메테리얼 변경
    {
        isFarmFieldWater = false;

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.material = currentMaterial;
    }

    public bool GetUseSunPower() // 발전소가 사용 가능 한지
    {
        return isUseSunPower;
    }
    
    // 농사 관련 일을 할 수 있는지
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

    // 돌 상태 리턴
    public bool GetStone()
    {
        return isStone;
    }

    // 나무 상태 리턴
    public bool GetTree()
    {
        return isTree;
    }

    // 플레이어 이동 위치 리턴
    public Vector3 GetWalkPosition()
    {
        return walkTransform.position - (Quaternion.FromToRotation(transform.position, playerMove.transform.position) * Vector3.one);
    }
}
