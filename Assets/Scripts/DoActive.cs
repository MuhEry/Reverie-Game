using UnityEngine;

public class DoActive : MonoBehaviour
{
public GameObject[] setActiveObjects;
public GameObject[] setInactiveObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in setActiveObjects)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in setInactiveObjects)
            {
                obj.SetActive(false);
            }
            Destroy(gameObject);
        }
    }
}
