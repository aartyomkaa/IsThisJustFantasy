﻿using Agava.WebUtility;
using Agava.YandexGames;
using Assets.Scripts.GameLogic.Utilities;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.PlayerUnits;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PlayerInput
{
    internal class MobileInput : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Button _attack;
        [SerializeField] private Button _changeWeapon;
        [SerializeField] private SelectedUnitsHandler _selectedUnitsHandler;
        [SerializeField] private LayerMask _ground;
        [SerializeField] private CanvasGroup _canvasGroup;

        private PlayerMovement _playerMover;
        private PlayerAttacker _playerAttacker;
        private Vector2 _moveDirection;
        private SpriteChanger _spriteChanger;

        private WorldPointFinder _worldPointFinder;
        private PointerSelectableChecker _poinerChecker;

        private float _doubleTapThreshold = 1f;
        private float _lastTapTime;

        private void Start()
        {
            _spriteChanger = _changeWeapon.GetComponent<SpriteChanger>();
            SetVisibility(true);
        }

        private void FixedUpdate()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    if (Time.time - _lastTapTime <= _doubleTapThreshold)
                    {
                        _lastTapTime = 0;
                        OnMoveUnits(_worldPointFinder.GetPosition(touch.position));
                    }
                    else
                    {
                        _lastTapTime = Time.time;
                    }
                }
            }

            Vector3 direction = Vector3.forward * _joystick.Vertical + Vector3.right * _joystick.Horizontal;

            _moveDirection = new Vector2(direction.x, direction.z);

            OnMoveInput(_moveDirection);
        }

        private void OnDisable()
        {
            _attack.onClick.RemoveListener(OnAttackInput);
            _changeWeapon.onClick.RemoveListener(OnChangeWeaponInput);
        }

        public void Init(Player player)
        {
            _playerMover = player.GetComponent<PlayerMovement>();
            _playerAttacker = player.GetComponent<PlayerAttacker>();

            _worldPointFinder = new WorldPointFinder(_ground);
            _poinerChecker = new PointerSelectableChecker();

            _attack.onClick.AddListener(OnAttackInput);
            _changeWeapon.onClick.AddListener(OnChangeWeaponInput);
        }

        public void SetVisibility(bool isVisible)
        {
            _canvasGroup.alpha = isVisible ? 1 : 0;
            _canvasGroup.interactable = isVisible;
            _canvasGroup.blocksRaycasts = isVisible;
        }

        private void OnMoveInput(Vector2 direction)
        {
            _playerMover.Move(direction);
        }

        private void OnAttackInput()
        {
            _playerAttacker.Attack();
        }

        private void OnChangeWeaponInput()
        {
            _playerAttacker.ChangeWeapon();
            _spriteChanger.ChangeSprite();
        }

        private void OnMoveUnits(Vector3 position)
        {
            if (_poinerChecker.IsPointerOverSelectableObject(position) == false)
                _selectedUnitsHandler.MoveUnits(position);
        }
    }
}
