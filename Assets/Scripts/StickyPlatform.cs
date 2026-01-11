using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 lastPlatformPosition;
    private Quaternion lastPlatformRotation;

    [Header("Dönme Ayarları")]
    [Tooltip("Karakterin UFO etrafında dönme hızı")]
    public float orbitMultiplier = 2.0f; 

    [Tooltip("Karakterin kendi etrafında dönme hızı")]
    public float rotationMultiplier = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            lastPlatformPosition = transform.position;
            lastPlatformRotation = transform.rotation;
        }
    }

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            // 1. UFO'nun GERÇEK dönüş açısını bul
            Quaternion realRotationDelta = transform.rotation * Quaternion.Inverse(lastPlatformRotation);
            
            // Yörünge için açıyı orbitMultiplier katına çıkar
            Quaternion orbitRotation = ScaleRotation(realRotationDelta, orbitMultiplier);

            // Kendi ekseni için açıyı rotationMultiplier katına çıkar
            Quaternion selfRotation = ScaleRotation(realRotationDelta, rotationMultiplier);

            // --- POZİSYON HESABI (Yörünge - Orbit) ---
            Vector3 offset = playerTransform.position - lastPlatformPosition;
            
            // Burada manipüle edilmiş "orbitRotation" kullanıyoruz
            offset = orbitRotation * offset; 
            
            playerTransform.position = transform.position + offset;

            // --- YÖN HESABI (Rotation - Self) ---
            // Burada manipüle edilmiş "selfRotation" (0.5x) kullanıyoruz
            playerTransform.rotation = selfRotation * playerTransform.rotation;

            // --- KAYIT ---
            lastPlatformPosition = transform.position;
            lastPlatformRotation = transform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = null;
        }
    }
    private Quaternion ScaleRotation(Quaternion rotation, float multiplier)
    {
        float angle;
        Vector3 axis;
        rotation.ToAngleAxis(out angle, out axis);

        // Açıyı 180'den büyükse düzelt (Unity bazen 359 derece verebilir, onu -1 yapmak daha sağlıklıdır)
        if (angle > 180) angle -= 360;

        // Açıyı istediğimiz sayıyla çarp
        angle *= multiplier;

        return Quaternion.AngleAxis(angle, axis);
    }
}