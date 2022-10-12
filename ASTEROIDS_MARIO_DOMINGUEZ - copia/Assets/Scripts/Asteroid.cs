using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] sprites;

    public float size = 1f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    public float movementSpeed = 50f;
    public float maxLifetime = 30f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // ASIGNA PROPIEDADES RANDOM A CADA METEORITO
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);

        // ESTABLECE LA ESCALA Y LA MASA DEL METEORITO DEPENDIENDO SU TAMAÑO
        transform.localScale = Vector3.one * size;
        rigidbody.mass = size;

        // DESTRUYE EL METEORITO DESPUES DE UN TIEMPO LIMITE
        Destroy(gameObject, maxLifetime);
    }

    public void SetTrajectory(Vector2 direction)
    {
        rigidbody.AddForce(direction * movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // VERIFICA SI EL ASTEROIDE ES APTO PARA PARTIRCE 
            if ((size * 0.5f) >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            FindObjectOfType<GameManager>().AsteroidDestroyed(this);

            // DESTRUYE EL ASTEROIDE ACTUAL PARA CONVERTIRLO EN DOS MA PEQUEÑOS
            Destroy(gameObject);
        }
    }

    private Asteroid CreateSplit()
    {
        // PONE LA POSICION DEL ASTEROIDE NUEVO EN LA MISMA DEL ASTEROIDE VIEJO
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        // CREA UN ASTEROIDE A LA MITAD DEL ASTEROIDE REFERENCIADO
        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = size * 0.5f;

        // ESTABLECE UNA TRAYECTORIA RANDOM
        half.SetTrajectory(Random.insideUnitCircle.normalized);

        return half;
    }

}
