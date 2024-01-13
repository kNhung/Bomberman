using System.Collections;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public new Rigidbody2D rigidbody {get; private set;}
    private Vector2 direction = Vector2.left;
    private Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    public float speed = 3f;

    public LayerMask movementLayerMask;

    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer SpriteRendererDeath;
    public AnimatedSpriteRenderer SpriteRendererAttack;
    private AnimatedSpriteRenderer activeSpriteRenderer;

    private void Awake(){
        rigidbody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    private void Update(){
        Vector2 position = rigidbody.position;
        Vector2 nextPosition = position + direction * speed * Time.deltaTime;

        Collider2D hitCollider = Physics2D.OverlapCircle(nextPosition, 0.53f, movementLayerMask);

        if (hitCollider != null)
        {   
            Vector2 randomDirection = GetRandomDirection();
            SetDirection(randomDirection, GetSpriteRenderer(randomDirection));
        }
        else {
            SetDirection(direction, GetSpriteRenderer(direction));
        }
    }


    private Vector2 GetRandomDirection()
    {
        return directions[Random.Range(0, directions.Length)];
    }

    private AnimatedSpriteRenderer GetSpriteRenderer(Vector2 direction)
    {
        if (direction == Vector2.up)
        {
            return spriteRendererUp;
        }
        else if (direction == Vector2.down)
        {
            return spriteRendererDown;
        }
        else if (direction == Vector2.left)
        {
            return spriteRendererLeft;
        }
        else if (direction == Vector2.right)
        {
            return spriteRendererRight;
        }

        return activeSpriteRenderer;
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion")){
            ScoreScript.instance.AddScore();
            DeathSequence();
        }
    }

    private void DeathSequence(){
        enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        SpriteRendererAttack.enabled = false;
        SpriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded(){
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            Attack();
        }
    }

    private void Attack(){
        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        SpriteRendererDeath.enabled = false;
        SpriteRendererAttack.enabled = true;

        StartCoroutine(WaitForAttackAnimationToEnd());
    }

    private IEnumerator WaitForAttackAnimationToEnd(){
        yield return new WaitForSeconds(0.35f);
        SpriteRendererAttack.enabled = false;
    }

}


