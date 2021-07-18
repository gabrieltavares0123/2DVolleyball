using System.Collections;
using UnityEngine;

namespace Magrathea
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Respawnner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPosition;

        private new Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Respawn(bool right)
        {
            transform.position = spawnPosition.position;

            if (right)
            {
                float xForce = Random.Range(2, 5);
                rigidbody2D.velocity = new Vector2(xForce, 2);
            }
            else
            {
                float xForce = Random.Range(-2, -5);
                rigidbody2D.velocity = new Vector2(xForce, 2);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Left") || other.gameObject.CompareTag("Right"))
            {
                StartCoroutine("DelayedRespawn", 1f);
            }
        }

        private IEnumerator DelayedRespawn(float seconds)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(seconds);
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            // > 50 right is true.
            // < 50 right is false.
            bool side = Random.Range(0f, 1f) > 0.50f;
            Respawn(side);
        }
    }
}