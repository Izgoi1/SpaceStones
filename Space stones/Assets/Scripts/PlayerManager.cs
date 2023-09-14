using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private int playerHp = 3;
    private static int level = 1;
    private Vector2 respawnPoint;

    public static int score;

    public AudioSource gameOverSound;
    public AudioSource levelCompleteSound;
    public AudioSource deathSound;
    public ParticleSystem explosion;
    public GameObject gameOverScreen;
    public GameObject nextLevelScreen;
    public GameObject currentLevelObject;
    public GameObject hp;
    List<GameObject> shipHp = new List<GameObject>();
    public Vector2 hpPosition;
    public Text scoreText;
    public Text bestScoreText;
    public Text currentLevelText;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();

        respawnPoint = transform.position;

        while (playerHp != 0)
        {
            GameObject newHp = Instantiate(hp, hpPosition, transform.rotation * Quaternion.Euler(0f, 0f, 180f));
            shipHp.Add(newHp);

            playerHp--;
            hpPosition.x += 0.45f;
        }

        if (level > 1)
        {
            levelCompleteSound.Play();
        }

        playerHp = 3;

        StartCoroutine(offColider(3f));
        
        BestScoreUpdate();

        StartCoroutine(CurrentLevel(1.5f));
    }

    private void Update()
    {
        scoreText.text = $"SCORE: {score}";

        if (score > PlayerPrefs.GetInt("bestScore", 0))
        {
            PlayerPrefs.SetInt("bestScore", score);
        }

        if (GameObject.FindGameObjectsWithTag("Asteroid").Length == 0)
        {
            nextLevelScreen.SetActive(true);
        }
    }

    private IEnumerator Respawn(float duration)
    {

        rb.simulated = false;
        transform.localScale = Vector3.zero;
        gameObject.GetComponent<PlayerController>().enabled = false;
        ParticleEffect();
        yield return new WaitForSeconds(duration);
        transform.position = respawnPoint;
        gameObject.GetComponent<PlayerController>().enabled = true;
        transform.localScale = new Vector3(0.47f, 0.45f, 0);
        rb.simulated = true;

    }

    private IEnumerator CurrentLevel(float duration)
    {
        currentLevelObject.gameObject.SetActive(true);
        currentLevelText.text = $"Level: {level}";
        yield return new WaitForSeconds(duration);
        currentLevelObject.gameObject.SetActive(false);
    }

    private void ParticleEffect()
    {
        deathSound.Play();
        this.explosion.transform.position = this.transform.position;
        this.explosion.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {

            if (playerHp >= 1)
            {
                playerHp--;
                Destroy(shipHp[playerHp]);

                StartCoroutine(Respawn(0.5f));
                StartCoroutine(offColider(3f));
            }
            else
            {
                gameOverSound.Play();
                Invoke("GameOver", 1f);
                score = 0;
            }

        }
    }

    private void GameOver()
    {
        gameObject.SetActive(false);
        ParticleEffect();
        gameOverScreen.SetActive(true);
    }

    private IEnumerator offColider(float sec)
    {
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(sec);
        boxCollider2D.enabled = true;
    }

    public void BestScoreUpdate()
    {
        bestScoreText.text = $"BEST SCORE: {PlayerPrefs.GetInt("bestScore", 0)}";
    }

    public void DeleteBestScore()
    {
        PlayerPrefs.DeleteKey("bestScore");
    }

    public void RestartLevel()
    {
        level = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
