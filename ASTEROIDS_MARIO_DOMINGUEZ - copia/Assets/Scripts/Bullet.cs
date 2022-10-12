using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 500f;
    public float maxLifetime = 10f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        // MOVIMIENTO PARA LA BALA
        rigidbody.AddForce(direction * speed);

        // DESTRUYE LA BALA DESPUES DE UN TIEMPO LIMITE
        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // DESTRUYE LA BALA CUANDO CHOCA CON ALGO
        Destroy(gameObject);
    }

}
