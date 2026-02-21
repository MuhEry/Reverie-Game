using UnityEngine;

public class MoveBird : MonoBehaviour
{
    public float speed = 5f;
    public Transform[] patrolPoints;
    private int currentPointIndex = 0;

    void Start()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;
    }

    void Update()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        Transform target = patrolPoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        SmoothTurn(target);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    void SmoothTurn(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero) // Yön sıfır değilse dön (Hata önleyici)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        }
    }
}