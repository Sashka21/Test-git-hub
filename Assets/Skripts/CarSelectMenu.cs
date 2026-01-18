using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelectMenu : MonoBehaviour
{
    public Sprite[] carSprites;

    public Image player1Image;
    public Image player2Image;

    int p1Index = 0;
    int p2Index = 0;

    void Start()
    {
        UpdateImages();
    }

    void UpdateImages()
    {
        player1Image.sprite = carSprites[p1Index];
        player2Image.sprite = carSprites[p2Index];
    }

    // ---- PLAYER 1 ----
    public void P1_Left()
    {
        p1Index--;
        if (p1Index < 0) p1Index = carSprites.Length - 1;
        UpdateImages();
    }

    public void P1_Right()
    {
        p1Index++;
        if (p1Index >= carSprites.Length) p1Index = 0;
        UpdateImages();
    }

    // ---- PLAYER 2 ----
    public void P2_Left()
    {
        p2Index--;
        if (p2Index < 0) p2Index = carSprites.Length - 1;
        UpdateImages();
    }

    public void P2_Right()
    {
        p2Index++;
        if (p2Index >= carSprites.Length) p2Index = 0;
        UpdateImages();
    }

    // ---- PLAY ----
    public void PlayGame()
    {
        CarSelectionData.Instance.player1Index = p1Index;
        CarSelectionData.Instance.player2Index = p2Index;

        SceneManager.LoadScene("SampleScene");
    }
}
