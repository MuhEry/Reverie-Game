using UnityEngine;

public class UseShovel : MonoBehaviour
{
    public GameObject useShovelText;
    public GameObject noShovelText;
    public GameObject deleteObject;
    public GameObject activeObject;
    public Animator blackoutAnimator;
    private bool inArea = false;
 

    private void Update()
    {
        if (inArea && Input.GetKeyDown(KeyCode.F))
        {
            if (Manager.manager.durumlar[2])
            {
                useShovelText.SetActive(false);
                blackoutAnimator.SetTrigger("blackout");
                Invoke("deleteObjectWithDelay", 1f);
            }
            else
            {
                noShovelText.SetActive(true);
                Invoke("HideNoShovelText", 2f);
            }
        }
    }
    private void deleteObjectWithDelay()
    {
        activeObject.SetActive(true);
        Destroy(deleteObject);
        Destroy(gameObject);
    }
    private void HideNoShovelText()
    {
        noShovelText.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
            useShovelText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = false;
            useShovelText.SetActive(false);
        }
    }
}
