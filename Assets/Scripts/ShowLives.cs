using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowLives : MonoBehaviour
{
    int m_Lives;

    // Start is called before the first frame update
    private void Awake()
    {
        Observer.OnPlayerCaught += ShowLivesText;
    }

    void Start()
    {
        m_Lives = GameManager.GetManager().lives;
        ShowLivesText();
        
    }

    private void OnDestroy()
    {
        Observer.OnPlayerCaught -= ShowLivesText;   
    }

    void ShowLivesText()
    {
        GetComponent<TextMeshProUGUI>().text = "Lives: " + m_Lives;
        m_Lives--;
    }
}
