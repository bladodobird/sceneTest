using UnityEngine;
using UnityEngine.UI;

public class flipcard : MonoBehaviour
{
    public Image[] cardImages; // �|�i�P���Ϥ�
    public Sprite backSprite; // �µP���Ϯ�
    public Sprite[] cardSprites; // �P�����P�Ϯס]�®�B���ߡB�٧ΡB����^

    private bool[] cardStates = { false, false, false, false }; // �C�i�P��½�P���A

    void Start()
    {
        // �ˬd Playerprefs ���O�_���O�s���i�סA�p�G���AŪ����
        for (int i = 0; i < cardStates.Length; i++)
        {
            int state = PlayerPrefs.GetInt("CardState_" + i, 0);
            cardStates[i] = (state == 1);
        }

        UpdateCardsDisplay();
    }

    public void FlipCard()
    {
        for (int i = 0; i < cardStates.Length; i++)
        {
            // ���U�@�i��½���P
            if (!cardStates[i])
            {
                cardStates[i] = true;

                // �N½�P���A�O�s�b Playerprefs ��
                PlayerPrefs.SetInt("CardState_" + i, 1);

                // ��s�P�����
                UpdateCardsDisplay();
                return;
            }
        }
    }

    void UpdateCardsDisplay()
    {
        for (int i = 0; i < cardStates.Length; i++)
        {
            // �ھڵP��½�P���A�]�m�������Ϲ�
            cardImages[i].sprite = cardStates[i] ? cardSprites[i] : backSprite;
        }
    }

    public void ResetPlayerProgress()
    {
        // ���m�Ҧ����a�i�שM�O�s���ƾ�
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < cardStates.Length; i++)
        {
            cardStates[i] = false;
        }

        // ��s�P�����
        UpdateCardsDisplay();
    }
}
