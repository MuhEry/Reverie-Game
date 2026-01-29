using UnityEngine;

public class TriggerHelper : MonoBehaviour
{
public GameObject helperText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowText();
            Invoke("DestroyMe", 3f);
        }
    }
    private void ShowText()
    {
        helperText.SetActive(true);
    }
    private void DestroyMe()
    {
        helperText.SetActive(false);
        Destroy(gameObject);
    }
}
