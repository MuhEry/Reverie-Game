using UnityEngine;

public class Tespit : MonoBehaviour
{
    public GameObject[] objects;
    public float moveDistanceY = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in objects)
            {
                obj.transform.position += new Vector3(0, moveDistanceY, 0);
            }
            this.gameObject.SetActive(false);
        }
    }
}