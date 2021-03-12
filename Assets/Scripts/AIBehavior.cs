using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{
    private Ball ball;
    private BoxCollider2D bc;
    [SerializeField]
    private float _ySpeed = 8f;

    private bool firstIncoming;

    private float randomYOffset;
    private Vector3 moveAmount;


    // Start is called before the first frame update
    void Awake()
    {
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    //TODO: Undumbify AI
    private void Move()
    {
        
        if (!FindObjectOfType<GameManager>().gameRunning)
            return;

        float direction = GetYPosition();

        direction = Mathf.Clamp(direction, -32f, 32f);

        moveAmount = new Vector3(transform.localPosition.x, direction, 0);

        transform.localPosition = moveAmount;


    }

    private bool BallIncoming()
    {
        return ball._xDirection > 0;
    }

    private float GetYPosition()
    {
        float result = transform.localPosition.y;
        if (BallIncoming())
        {
            if(firstIncoming)
            {
                firstIncoming = false;
                randomYOffset = GetRandomOffset();
            }
            result = Mathf.MoveTowards(transform.localPosition.y, ball.transform.localPosition.y + randomYOffset, _ySpeed * Time.deltaTime);
        }
        else
        {
            firstIncoming = true;
        }
            
        return result;
    }


    //clamps between -31,5 and 31.5

    private float GetRandomOffset()
    {
        float maxOffset = bc.bounds.extents.y;
        return Random.Range(-maxOffset-.5f, maxOffset+.5f);
    }
}
