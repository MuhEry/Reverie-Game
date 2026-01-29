using UnityEngine;

public class ObjectCollect : MonoBehaviour
{
    [Tooltip("0 - Kitap, 1 - Oyuncak, 2 - Labirent, 3 - Kürek")]
    public int hangiNesne;
    public GameObject collectText;
    private bool inArea = false;
    
    private void Update()
    {
        if (inArea && Input.GetKeyDown(KeyCode.F))
        {
            Manager.manager.durumlar[hangiNesne] = true;
            collectText.SetActive(false);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
            collectText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = false;
            collectText.SetActive(false);
        }
    }
}