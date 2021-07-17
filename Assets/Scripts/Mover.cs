using UnityEngine;
using UnityEngine.InputSystem;

namespace Magrathea
{
    internal class Mover : BaseControl
    {
        public override void OnAction(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<float>();
            float xMovementValue = inputValue * actionFactor;
            Vector2 newVelocity = rigidbody2D.velocity;
            newVelocity.x = xMovementValue;
            rigidbody2D.velocity = newVelocity;
        }
    }
}