using UnityEngine;
using UnityEngine.InputSystem;

namespace Magrathea
{
    internal class Jumper : BaseControl
    {
        public override void OnAction(InputAction.CallbackContext context)
        {
            var inputValue = context.ReadValue<float>();
            if (CanJump()) 
            {
                float yJumpValue = inputValue * actionFactor;
                Vector2 newVelocity = rigidbody2D.velocity;
                newVelocity.y = yJumpValue;
                rigidbody2D.velocity = newVelocity;
            } 
        }

        private bool CanJump()
        {
            return transform.position.y <= -2.12f;
        }
    }
}