using Abstractions;
using Commands;
using UnityEngine;

namespace Core
{
    public sealed class Chomper : MonoBehaviour, ISelectable, IAttackable, IDamageDealer
    {
        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;

        public Transform PivotPoint => _pivotPoint;

        [SerializeField] private float _maxHealth = 1000;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Transform _pivotPoint;

        [SerializeField] private Animator _animator;
        [SerializeField] private StopComandExecutor _stopCommand;
        
        public int Damage => _damage;
        [SerializeField] private int _damage = 25;


        private float _health;
        private void Start()
        {
            _health = _maxHealth;
        }

        public void RecieveDamage(int amount)
        {
            if (_health <= 0)
            {
                return;
            }
            _health -= amount;
            if (_health <= 0)
            {
                _animator.SetTrigger("PlayDead");
                Invoke(nameof(destroy), 1f);
            }

        }

        private async void destroy()
        {
            await _stopCommand.ExecuteSpecificCommand(new StopCommand());
            Destroy(gameObject);
        }

    }
}