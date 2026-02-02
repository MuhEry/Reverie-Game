using UnityEngine;

public class ObjectPlace : MonoBehaviour
{
    void Update()
    {
        /*if (inArea && Input.GetKeyDown(KeyCode.F))
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
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}
