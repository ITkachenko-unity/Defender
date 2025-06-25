using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private float _baseSpeed = 1f;
    [SerializeField] private float _speedPerLevel = 0.2f;
    [SerializeField] private float _shootInterval = 1f;
    [SerializeField] private int _killReward = 2;

    public static List<Enemy> ActiveEnemies = new List<Enemy>();

    private void OnEnable() => ActiveEnemies.Add(this);
    private void OnDisable() => ActiveEnemies.Remove(this);

    private Animator _animator;
    private float _currentSpeed;
    private float _borderPosX;
    private Coroutine _attackRoutine;
    private Corn _target;
    private bool _isMoving = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (!_animator) enabled = false;
    }

    private void Start()
    {
        _currentSpeed = _baseSpeed + _speedPerLevel * LevelController.Level;
    }

    private void Update()
    {
        if (!_target) return;

        _isMoving = transform.position.x > _borderPosX;
        _animator.SetBool("isMoving", _isMoving);

        if (_isMoving)
            Move();
        else if
            (_attackRoutine == null) _attackRoutine = StartCoroutine(AttackRoutine());
    }

    public void SetTarget(Corn corn)
    {
        _target = corn;
        _borderPosX = corn.transform.position.x;
    }

    private void Move()
    {
        if (_attackRoutine != null)
        {
            StopCoroutine(_attackRoutine);
            _attackRoutine = null;
        }

        transform.position += -transform.right * (_currentSpeed * Time.deltaTime);
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            _target.TakeDamage();

            yield return new WaitForSeconds(_shootInterval);
        }
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Max(_health - damage, 0);
        if (_health == 0) Die();
    }

    private void Die()
    {
        _target.AddCrystals(_killReward);
        Destroy(gameObject);
    }
}