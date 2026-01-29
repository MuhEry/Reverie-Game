using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager manager; 

    // 0 - Kitap, 1 - Oyuncak, 2 - Labirent, 3 - Kürek
    public bool[] durumlar = new bool[3];

    void Awake()
    {
        if (manager == null) 
        {
            manager = this;
        }
    }
}
