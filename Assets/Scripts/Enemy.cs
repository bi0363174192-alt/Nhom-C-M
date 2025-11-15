using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distance = 5f;
    private Vector3 startPos;
    private bool movingRight = true; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float leftBound = startPos.x - distance;
        float rightBound = startPos.x + distance;
        if(movingRight)
        {
            transform.Translate(Vector2.right*speed*Time.deltaTime);
            if(transform.position.x >= rightBound)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
                if(transform.position.x >= rightBound)
                {
                movingRight = false;
                    Flip();
                }
            }
        }
    }
    void Flip()
    {
        Vector3  scaler=transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
