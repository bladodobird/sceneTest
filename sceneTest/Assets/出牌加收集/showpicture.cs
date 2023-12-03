using UnityEngine;
using UnityEngine.UI;

public class showpicture : MonoBehaviour
{
    [SerializeField, Header("所有日記")]
    private Sprite[] picture; //sprite是因為圖片類型

    [SerializeField, Header("是否拿到日記")]
    private bool[] picturestate; //狀態

    private Image imgpicture; //Canvas顯示sprite可使用image

    public Sprite backSprite; // 黑牌的圖案

    [SerializeField]
    private Image[] imageSlots; // 放置圖片的格子（Image）

    private void Awake()
    {

        //PlayerPrefs.DeleteAll();
        //picture 陣列相對應的 bool 陣列，
        //new是因為前面的宣告是沒有分配到記憶體空間，單純是宣告(?)
        picturestate = new bool[picture.Length];

        //設定初始值(從0開始)、判斷式(0~9)、行為(+1)
        //用來檢查是否有打勾的紀錄的迴圈，避免重複取得
        //("圖片" + i)是字串鍵，在(後面的設定)後設定成已拿到的在這裡如果偵測到已拿到
        //就會是true，否則false
        for (int i = 0; i < picturestate.Length; i++)
        {
            picturestate[i] = PlayerPrefs.GetString("圖片" + i) == "已拿到";
        }

        imgpicture = GameObject.Find("圖片").GetComponent<Image>();

        Getpicture();

    }

    private void Getpicture()
    {
        int indexRandom = -1; // 初始化索引

        // 用while迴圈確認這個拿到的變數是不是已經拿過
        while (indexRandom == -1)
        {
            int randomIndex = Random.Range(0, picture.Length);

            if (!picturestate[randomIndex]) // 如果這張圖片還沒拿到
            {
                indexRandom = randomIndex; // 設定選中的索引
                picturestate[indexRandom] = true; // 標記圖片為已拿到
            }
        }

        imgpicture.sprite = picture[indexRandom];
        imgpicture.color = Color.white;

        PlayerPrefs.SetString("圖片" + indexRandom, "已拿到"); // 設定圖片已拿

        UpdateCardsDisplay();

    }

    private void UpdateCardsDisplay()
    {
        for (int u = 0; u < picturestate.Length; u++)
        {
            imageSlots[u].sprite = picturestate[u] ? picture[u] : backSprite;
        }
    }
}