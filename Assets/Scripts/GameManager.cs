using System.Collections;
using UnityEngine;
using TMPro;

namespace Magrathea
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject ball;
        [SerializeField] private Transform respawn;
        private Rigidbody2D ballRigidbody2D;
        private SpriteRenderer ballSpriteRenderer;
        private Collider2D ballCollider2D;
        private Transform ballTransform;

        [SerializeField] private TextMeshProUGUI leftText;
        [SerializeField] private TextMeshProUGUI rightText;
        private int leftScore = 0;
        private int rightScore = 0;

        private void Awake()
        {
            ball.SetActive(true);
        }

        private void Start()
        {
            ballRigidbody2D = ball.GetComponent<Rigidbody2D>();
            ballSpriteRenderer = ball.GetComponent<SpriteRenderer>();
            ballCollider2D = ball.GetComponent<CircleCollider2D>();
            ballTransform = ball.GetComponent<Transform>();

            StartMatch();
        }

        public void StartMatch() 
        {
            rightScore = 0;
            rightText.text = rightScore.ToString();
            leftScore = 0;
            leftText.text = leftScore.ToString();
            bool sideStart = Random.Range(0f, 1f) > 0.5f;
            RespawnBall(sideStart);
        }

        #region Score Count
        public void ScoreLeft()
        {
            leftScore += 1;
            leftText.text = leftScore.ToString();
            TryEndMatch(true);
        }

        public void ScoreRight()
        {
            rightScore += 1;
            rightText.text = rightScore.ToString();
            TryEndMatch(false);
        }

        public void TryEndMatch(bool spawnSide)
        {
            if (leftScore >= 3 || rightScore >= 3) 
            {
                ball.SetActive(false);
                StopAllCoroutines();
                StartCoroutine(RestartMatchDelayed(2f));
            } 
            else
            {
                RespawnBall(spawnSide);
            }
        }

        private IEnumerator RestartMatchDelayed(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            StartMatch();
        }
        #endregion

        #region Ball Respawn
        public void RespawnBall(bool rightSide)
        {
            StopAllCoroutines();
            StartCoroutine(SpawnDelayed(1f, rightSide));
        }

        private void ThrowBallRight()
        {
            float xForce = Random.Range(2, 5);
            ballRigidbody2D.velocity = new Vector2(xForce, 2);
        }

        private void ThrowBallLeft()
        {
            float xForce = Random.Range(-2, -5);
            ballRigidbody2D.velocity = new Vector2(xForce, 2);
        }

        private IEnumerator SpawnDelayed(float seconds, bool rightSide)
        {
            ballCollider2D.enabled = false;
            ballSpriteRenderer.enabled = false;
            
            yield return new WaitForSeconds(seconds);

            if (!ball.activeInHierarchy) ball.SetActive(true);

            ballCollider2D.enabled = true;
            ballSpriteRenderer.enabled = true;

            ballTransform.position = respawn.position;

            if (!rightSide) ThrowBallRight();
            else ThrowBallLeft();
        }
        #endregion
    }
}