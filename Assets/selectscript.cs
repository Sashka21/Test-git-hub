using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class selectscript : MonoBehaviour
{
    private GameObject[] characters;
    private int index;

    private void Start()
    {
        index = PlayerPrefs.GetInt("SelectPlayer", 0);  // Значення за замовчуванням — 0
        characters = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characters[i] = transform.GetChild(i).gameObject;
        }

        // Вимикаємо всіх персонажів
        foreach (GameObject go in characters)
        {
            go.SetActive(false);
        }

        // Вмикаємо тільки вибраного
        if (characters.Length > 0 && index < characters.Length)
        {
            characters[index].SetActive(true);
        }
    }

    public void SelectLeft()
    {
        characters[index].SetActive(false);
        index--;

        if (index < 0)
        {
            index = characters.Length - 1;
        }

        characters[index].SetActive(true);
    }

    public void SelectRight()
    {
        characters[index].SetActive(false);
        index++;

        if (index >= characters.Length)
        {
            index = 0;
        }

        characters[index].SetActive(true);
    }

    public void StartScene()
    {
        PlayerPrefs.SetInt("SelectPlayer", index);
        SceneManager.LoadScene("Level1");
    }
}
