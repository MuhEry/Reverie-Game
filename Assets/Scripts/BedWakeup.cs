using UnityEngine;

public class BedWakeup : MonoBehaviour
{
    public Animator animator;
    public GameObject blackPanel;
    public MonoBehaviour playerControlScript;
    
    void Start()
    {
        // 1 saniye sonra closepanel çağır
        Invoke("ClosePanel", 1f);
    }

    void Update()
    {
        
    }

    void ClosePanel()
    {
        blackPanel.SetActive(false);
    }
}
