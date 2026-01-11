using UnityEngine;

public class DoActive : MonoBehaviour
{
public GameObject[] objects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in objects)
            {
                obj.SetActive(true);
            }
        }
    }
}
