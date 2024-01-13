using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    
    public new Rigidbody2D rigidbody {get; private set;}
    private Vector2 direction = Vector2.down;
    public float speed = 5f;
    public float minSpeed = 5f;
    public float maxSpeed = 10f;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer SpriteRendererDeath;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource itemPickupSoundEffect;

    private void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }
    private void Update(){
        if (Input.GetKey(inputUp)){
            SetDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(inputDown)){
            SetDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(inputLeft)){
            SetDirection(Vector2.left, spriteRendererLeft);
        }
        else if (Input.GetKey(inputRight)){
            SetDirection(Vector2.right, spriteRendererRight);
        }
        else {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
    }

    private void FixedUpdate(){
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;

        rigidbody.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer){
        direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer =spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion") ){
            FindObjectOfType<GameManager>().GameOver();
            DeathSequence();
        }
    }

    private void DeathSequence(){
        deathSoundEffect.Play();
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        SpriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);

    }

    private void OnDeathSequenceEnded(){
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Enemy")){
            DeathSequence();
        }
    }

    public void IncreaseSpeed(){
        itemPickupSoundEffect.Play();
        if(speed < 10){
            speed++;
            StartCoroutine(IncreaseSpeedOverTime());
        }
    }

    public IEnumerator IncreaseSpeedOverTime(){
        float duration = 30f;
        float elapsedTime = 0f;

        while(elapsedTime < duration){
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (speed > minSpeed){
            speed--; 
            yield return null;
        }
    }
}
