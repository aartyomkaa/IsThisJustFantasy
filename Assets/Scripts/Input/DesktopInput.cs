using UnityEngine;
using Agava.WebUtility;
using Assets.Scripts.PlayerUnits;
using Assets.Scripts.PlayerComponents;
using Assets.Scripts.GameLogic.Utilities;

namespace Assets.Scripts.PlayerInput
{
    internal class DesktopInput : MonoBehaviour
    {
        [SerializeField] private SelectedUnitsHandler _selectedUnitsHandler;
        [SerializeField] private LayerMask _ground;

        private InputActions _inputActions;
        private PlayerMovement _playerMover;
        private PlayerAttacker _playerAttacker;
        private WorldPointFinder _worldPointFinder;
        private PointerSelectableChecker _poinerChecker;

        private Vector2 _moveDirection;

        private void FixedUpdate()
        {
            _moveDirection = _inputActions.Player.Move.ReadValue<Vector2>();

            OnMoveInput(_moveDirection);
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void Init(Player player)
        {
            _inputActions = new InputActions();
            _inputActions.Enable();

            _playerMover = player.GetComponent<PlayerMovement>();
            _playerAttacker = player.GetComponent<PlayerAttacker>();

            Cursor.visible = true;

            _worldPointFinder = new WorldPointFinder(_ground);
            _poinerChecker = new PointerSelectableChecker();

            _inputActions.Player.Attack.performed += ctx => OnAttackInput();
            _inputActions.Player.ChangeWeapon.performed += ctx => OnChangeWeaponInput();
            _inputActions.Player.MoveUnits.performed += ctx => OnMoveUnits();
        }

        private void OnMoveInput(Vector2 direction)
        {
            _playerMover.Move(direction);
        }

        private void OnAttackInput()
        {
            if (_poinerChecker.IsPointerOverSelectableObject() == false)
            {
                _playerAttacker.Attack();
            }
        }
        
        private void OnChangeWeaponInput()
        {
            _playerAttacker.ChangeWeapon();
        }

        private void OnMoveUnits()
        {
            _selectedUnitsHandler.MoveUnits(_worldPointFinder.GetPosition(Input.mousePosition));
        }
    }
}