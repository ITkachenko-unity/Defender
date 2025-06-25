using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage = 1;

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        MoveForward();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }

    private void MoveForward()
    {
        transform.position += transform.up * _speed * Time.deltaTime;
    }
}