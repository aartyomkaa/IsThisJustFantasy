using Assets.Scripts.PlayerComponents.Weapons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerComponents
{
    [RequireComponent(typeof(PlayerAnimator))]
    internal class PlayerAttacker : PlayerComponent
    {
        [SerializeField] private List<Weapon> _weapons;

        private PlayerSFX _playerSFX;
        private PlayerAnimator _animator;
        private Weapon _currentWeapon;

        private void Start()
        {
            _animator = GetComponent<PlayerAnimator>();

            _currentWeapon = _weapons[0];
            _currentWeapon.gameObject.SetActive(true);
        }

        public void Attack()
        {
            if (_currentWeapon.CanAttack)
            {
                _animator.SetAnimatorAttackTrigger(_currentWeapon);

                _currentWeapon.Attack();
            }
        }

        public void ChangeWeapon()
        {
            _currentWeapon.gameObject.SetActive(false);

            int currentIndex = _weapons.IndexOf(_currentWeapon);
            int nextIndex = (currentIndex + 1) % _weapons.Count;

            _currentWeapon = _weapons[nextIndex];
            _currentWeapon.gameObject.SetActive(true);

            _animator.SetAnimatorChangeWeaponTrigger(_currentWeapon);
            _playerSFX.PlayChangeWeaponSound();
        }

        public override void Init(PlayerData level, PlayerSFX sfx)
        {
            _playerSFX = sfx;

            foreach (var weapon in _weapons)
            {
                weapon.LevelUp(level.WeaponDamageLevelUpAmount, level.WeaponAttackSpeedLevelUpAmount);
            }
        }
    }
}
