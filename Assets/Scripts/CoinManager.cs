using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private int _coins = 0;
    public int GetCoins => _coins;
    public static CoinManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCoinsUI();
    }

    public void AddCoin(int value)
    {
        _coins += value;
        UpdateCoinsUI();

        //захардкодил, сколько нам нужно собрать монет, для победы :)
        if (_coins == 8)
        {
            WindowManager.Instance.Win();
        }
    }

    private void UpdateCoinsUI()
    {
        coinText.text = "Монет: " + _coins;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
