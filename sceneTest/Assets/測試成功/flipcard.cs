using UnityEngine;
using UnityEngine.UI;

public class flipcard : MonoBehaviour
{
    public Image[] cardImages; // 四張牌的圖片
    public Sprite backSprite; // 黑牌的圖案
    public Sprite[] cardSprites; // 牌的不同圖案（黑桃、紅心、菱形、梅花）

    private bool[] cardStates = { false, false, false, false }; // 每張牌的翻牌狀態

    void Start()
    {
        // 檢查 Playerprefs 中是否有保存的進度，如果有，讀取它
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
            // 找到下一張未翻的牌
            if (!cardStates[i])
            {
                cardStates[i] = true;

                // 將翻牌狀態保存在 Playerprefs 中
                PlayerPrefs.SetInt("CardState_" + i, 1);

                // 更新牌的顯示
                UpdateCardsDisplay();
                return;
            }
        }
    }

    void UpdateCardsDisplay()
    {
        for (int i = 0; i < cardStates.Length; i++)
        {
            // 根據牌的翻牌狀態設置相應的圖像
            cardImages[i].sprite = cardStates[i] ? cardSprites[i] : backSprite;
        }
    }

    public void ResetPlayerProgress()
    {
        // 重置所有玩家進度和保存的數據
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < cardStates.Length; i++)
        {
            cardStates[i] = false;
        }

        // 更新牌的顯示
        UpdateCardsDisplay();
    }
}
