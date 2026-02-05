using UnityEngine;

public class ObjectPlace : MonoBehaviour
{
    [Tooltip("0 - Kitap, 1 - Oyuncak, 2 - Labirent")]
    public int placeObjectNumber;
    [Tooltip("0 - Kitap, 1 - Oyuncak, 2 - Labirent")]
    public GameObject placeObjects;
    public GameObject placeText;
    public GameObject findObjectText;

    private bool inArea = false;
    void Update()
    {
        if (inArea && Input.GetKeyDown(KeyCode.F))
        {
            if (Manager.manager.durumlar[placeObjectNumber])
            {
                placeText.SetActive(false);
                placeObjects.SetActive(true);
                Manager.manager.placedObjectCount++;
                Destroy(gameObject);
                if (Manager.manager.placedObjectCount == 3)
                {
                    Manager.manager.WinLevel();
                }
            }
            else
            {
                placeText.SetActive(false);
                findObjectText.SetActive(true);
                Invoke("HideFindObjectText", 2f);
            }
        }
    }
    private void HideFindObjectText()
    {
        findObjectText.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            placeText.SetActive(true);
            inArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            placeText.SetActive(false);
            inArea = false;
        }
    }
}
