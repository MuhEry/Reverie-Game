using UnityEngine;

public class ChurchTrigger1 : MonoBehaviour
{
    // Animator component reference
public Animator lightAnimator;

// karakter trigger alanına girdiğinde ışık animasyonunu başlat
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lightAnimator.SetTrigger("PlayerCome");
        }
        Destroy(this.gameObject);
    }

}
