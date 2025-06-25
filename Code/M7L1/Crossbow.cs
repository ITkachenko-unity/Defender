using UnityEngine;
using System.Collections;

public class Crossbow : MonoBehaviour
{
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private float _shootInterval = 0.75f;

    private bool isShootingCooldown = false;

    private void Awake()
    {
        if (_arrowPrefab == null)
        {
            Debug.LogError("Arrow prefab not assigned", this);
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        RotateTowardsMouse();
        HandleShooting();
    }

    private void OnDisable()
    {
        isShootingCooldown = false;
    }

    private void HandleShooting()
    {
        if (!Input.GetMouseButton(0) || isShootingCooldown) return;

        Shoot();
        StartCoroutine(ShootCooldown());
    }

    private void RotateTowardsMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePosition - (Vector2)transform.position;
        transform.up = lookDirection;
    }

    private void Shoot()
    {
        Instantiate(_arrowPrefab, transform.position, transform.rotation);
    }

    private IEnumerator ShootCooldown()
    {
        isShootingCooldown = true;
        yield return new WaitForSeconds(_shootInterval);
        isShootingCooldown = false;
    }
}