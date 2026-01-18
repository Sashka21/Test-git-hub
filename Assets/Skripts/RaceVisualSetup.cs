using UnityEngine;

public class RaceVisualSetup : MonoBehaviour
{
    [Header("Players in scene")]
    public GameObject player1;
    public GameObject player2;

    [Header("Car sprites (same order as in menu)")]
    public Sprite[] carSprites;

    void Start()
    {
        // если сцену запустили без меню
        if (CarSelectionData.Instance == null)
        {
            Debug.LogWarning("CarSelectionData not found Ч using default sprites");
            return;
        }

        ApplySprite(player1, CarSelectionData.Instance.player1Index);
        ApplySprite(player2, CarSelectionData.Instance.player2Index);
    }

    void ApplySprite(GameObject player, int spriteIndex)
    {
        if (player == null) return;

        SpriteRenderer sr = player.GetComponentInChildren<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("SpriteRenderer not found on " + player.name);
            return;
        }

        if (spriteIndex >= 0 && spriteIndex < carSprites.Length)
        {
            sr.sprite = carSprites[spriteIndex];
        }
        else
        {
            Debug.LogWarning("Sprite index out of range for " + player.name);
        }
    }
}
