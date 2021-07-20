using UnityEngine;
using UnityEngine.Events;

namespace Magrathea
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal class Ball : MonoBehaviour
    {
        [SerializeField] private UnityEvent ontouchRightSide;
        [SerializeField] private UnityEvent ontouchLeftSide;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Left"))
            {
                ontouchLeftSide.Invoke();
            } 
            
            else if (other.gameObject.CompareTag("Right"))
            {
                ontouchRightSide.Invoke();
            }
        }
    }
}