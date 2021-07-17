using UnityEngine;
using UnityEngine.InputSystem;

namespace Magrathea
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal abstract class BaseControl : MonoBehaviour
    {
        [Header("The action configuration.")]
        [Tooltip("The input signal to react.")]
        [SerializeField] protected InputAction inputAction;

        [Tooltip("Base factor to calculate the action.")]
        [SerializeField] protected float actionFactor;

        protected new Rigidbody2D rigidbody2D;

        protected virtual void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        protected virtual void OnEnable()
        {
            inputAction.performed += OnAction;
            inputAction.canceled += OnAction;
            inputAction.Enable();
        }

        protected virtual void OnDisable()
        {
            inputAction.performed -= OnAction;
            inputAction.canceled -= OnAction;
            inputAction.Disable();
        }

        public abstract void OnAction(InputAction.CallbackContext context);
    }
}