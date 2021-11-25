using UnityEngine;
using UnityEngine.UI;

public class ProductionUI : ChangeUI // ���� ���õ� UI�� ��Ƶ� Ŭ����
{
    [SerializeField] private Button productionButton; // ���� ��ư

    [SerializeField] private Button[] productionButtons; // ������ �������� �����ϴ� ��ư
    [SerializeField] private Panel[] needItemPanel; // �ʿ��� �������� ������

    [SerializeField] private NeedItem[] needItems; // �ʿ��� ������
    [SerializeField] private MakeItem[] makeItems; // ���� �� �ִ� ������

    private int currentProductionNumber = -1; // ���� �����ϰ� �ִ� ���� ���̵�

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < productionButtons.Length; i++) // ��� ��ư�� �Լ��� ����
        {
            Panel panel = needItemPanel[needItems[i].needItems.Length - 1];
            int num = i;

            productionButtons[num].onClick.AddListener(() => // ������ �������� �����ϸ�
            {
                if (currentProductionNumber >= 0) // ������ ������ Ȯ��
                {
                    needItemPanel[needItems[currentProductionNumber].needItems.Length - 1].gameObject.SetActive(false); //���� �����ִ��� ����
                }

                for (int j = 0; j < needItems[num].needItems.Length; j++) // ������ UI�� ������ �������
                {
                    panel.images[j].sprite = needItems[num].needItemSprites[j];
                    panel.texts[j].text = needItems[num].count[j].ToString();
                }

                panel.gameObject.SetActive(true); // ���� UI�� ������
                currentProductionNumber = num;
            });
        }

        productionButton.onClick.AddListener(() => //���� ��ư�� ������
        {
            if (currentProductionNumber < 0)
            {
                return;
            }

            NeedItem needItem = needItems[currentProductionNumber]; //�ʿ��� �������� �ҷ���
            MakeItem makeItem = makeItems[currentProductionNumber]; //���� �� �ִ� �������� �ҷ���

            for (int i = 0; i < needItem.needItems.Length; i++) //�ʿ��� �������� ��� �ִ��� Ȯ��
            {
                if (InventoryManager.GetItemCount(needItem.needItems[i]) < needItem.count[i])
                {
                    return;
                }
            }

            for (int i = 0; i < needItem.needItems.Length; i++) //�ʿ��� �������� ��� ���
            {
                InventoryManager.UseItem(needItem.needItems[i], needItem.count[i]);
            }

            ResearchManager.Instance.ReserchPoint += 1; //��������Ʈ ���׷��̵�

            switch (makeItem.makeItemType) //���� ������ ������ ���� �ٸ� ������� �������� ������
            {
                case MakeItemType.Inventory:
                    InventoryManager.AddItem(makeItem.makeItem, makeItem.count);
                    break;
                case MakeItemType.TopUI:
                    GameManager.topUICount[makeItem.makeTopUIItem] += makeItem.count;
                    break;
                case MakeItemType.Electrical:
                    ProductionManager.Instance.GetElectricalItem();
                    break;
            }
        });
    }
}
