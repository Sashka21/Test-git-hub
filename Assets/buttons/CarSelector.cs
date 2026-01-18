using UnityEngine;
using UnityEngine.UI;

public class CarSelectorUI : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("UI Image where the car sprite will be shown. If left empty, script попробует найти Image в дочерних объектах.")]
    public Image carImage;

    [Header("Car Sprites")]
    [Tooltip("Добавь сюда все спрайты машин (type Sprite).")]
    public Sprite[] carSprites;

    [Header("Settings")]
    [Tooltip("Ключ в PlayerPrefs. Если пустой — сохранение не будет использоваться.")]
    public string playerPrefsKey = "SelectedCarIndex";

    private int currentIndex = 0;

    void Awake()
    {
        // если не назначено — попробуем найти Image в дочерних объектах
        if (carImage == null)
        {
            carImage = GetComponentInChildren<Image>();
            if (carImage == null)
                Debug.LogWarning("[CarSelectorUI] Image не назначен и не найден в дочерних объектах.");
        }
    }

    void Start()
    {
        if (carSprites == null || carSprites.Length == 0)
        {
            Debug.LogError("[CarSelectorUI] Массив carSprites пуст. Добавь спрайты в инспекторе.");
            return;
        }

        // Попробуем загрузить сохранённый индекс, если указан ключ
        if (!string.IsNullOrEmpty(playerPrefsKey) && PlayerPrefs.HasKey(playerPrefsKey))
        {
            int saved = PlayerPrefs.GetInt(playerPrefsKey);
            if (saved >= 0 && saved < carSprites.Length)
                currentIndex = saved;
        }

        UpdateCar();
    }

    public void NextCar()
    {
        if (!CheckReady()) return;
        currentIndex = (currentIndex + 1) % carSprites.Length;
        UpdateCar();
    }

    public void PreviousCar()
    {
        if (!CheckReady()) return;
        currentIndex--;
        if (currentIndex < 0) currentIndex = carSprites.Length - 1;
        UpdateCar();
    }

    // выставить конкретный индекс (может пригодиться для переключателей/точек)
    public void SetIndex(int index)
    {
        if (!CheckReady()) return;
        if (index < 0 || index >= carSprites.Length)
        {
            Debug.LogWarning("[CarSelectorUI] SetIndex: индекс вне диапазона.");
            return;
        }
        currentIndex = index;
        UpdateCar();
    }

    // Сохранение выбора
    public void SaveSelection()
    {
        if (string.IsNullOrEmpty(playerPrefsKey)) return;
        PlayerPrefs.SetInt(playerPrefsKey, currentIndex);
        PlayerPrefs.Save();
    }

    // Обновление UI
    private void UpdateCar()
    {
        if (!CheckReady()) return;

        carImage.sprite = carSprites[currentIndex];
        // при необходимости установить native size (вкл. если спрайты разного соотношения)
        carImage.SetNativeSize();
    }

    // Проверки и логирование
    private bool CheckReady()
    {
        if (carSprites == null || carSprites.Length == 0)
        {
            Debug.LogError("[CarSelectorUI] carSprites пуст. Невозможно переключать машины.");
            return false;
        }
        if (carImage == null)
        {
            Debug.LogError("[CarSelectorUI] carImage не назначен и не найден.");
            return false;
        }
        return true;
    }
}
