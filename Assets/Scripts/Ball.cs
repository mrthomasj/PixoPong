using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float _xSpeed = 3;
    [SerializeField]
    private float _ySpeed = 3;
    [SerializeField]
    private Vector3 moveAmount;

    public int _xDirection { get; private set; }
    private int _yDirection;

    private float maxAngle = 45f;


    private GameManager gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameRunning)
            MoveBall();
    }

    public void LaunchBall()
    {

        transform.localPosition = Vector3.zero;

        _xDirection = Random.Range(-1, 2);
        if (_xDirection == 0)
            _xDirection = -1;

        _yDirection = Random.Range(-1, 2);
        if (_yDirection == 0)
            _yDirection = -1;

        moveAmount = new Vector3(.35f * _xSpeed * _xDirection, .65f * _ySpeed * _yDirection, 0);
    }

    private void MoveBall()
    {
        //moveAmount = new Vector3(5f * _xSpeed * _xDirection, 3.5f * _ySpeed * _yDirection, 0);
        //Debug.Log(moveAmount);
        transform.localPosition += moveAmount * Time.deltaTime;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (WallCollision(collision))
    //        return;
       
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        Debug.Log(collision.name);
        if (WallCollision(collision))
            return;
        PaddleCollision(collision);
        GoalCollision(collision);
    }

    private void GoalCollision(Collider2D collision)
    {
        if(collision.tag == "P1Goal")
        {
            Debug.Log("Player Two Scores!");
            gameManager.playerTwoScore++;
            Debug.Log(gameManager.playerTwoScore);
            gameManager.SetScoreUI(2);
            gameManager._justScored = true;
        }
        if(collision.tag == "P2Goal")
        {
            Debug.Log("Player One Scores!");
            gameManager.playerOneScore++;
            Debug.Log(gameManager.playerOneScore);
            gameManager.SetScoreUI(1);
            gameManager._justScored = true;
        }

    }

    private bool WallCollision (Collider2D collision)
    {
        
        
        if (collision.gameObject.tag == "Wall")
        {
            if(_yDirection == 1)
            {
                _yDirection = -1;
                moveAmount = new Vector3(moveAmount.x, moveAmount.y * _yDirection, 0);
            }
            else
            {
                _yDirection = 1;
                moveAmount = new Vector3(moveAmount.x, moveAmount.y * -1, 0);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ChangeXDirection()
    {
        _xDirection = -_xDirection;
    }

    private void PaddleCollision(Collider2D collision)
    {

        Debug.Log($"Entering paddle check,  collider hit:{collision.name}");

        if(collision.tag == "Player" || collision.tag == "AI")
        {
            ChangeXDirection();
            Bounce(collision);
        }
    }

    private void Bounce(Collider2D collider)
    {
        float colYextent = collider.bounds.extents.y;
        float yOffset = transform.localPosition.y - collider.transform.localPosition.y;

        float yRatio = yOffset / colYextent;
        float bounceAngle = maxAngle * yRatio * Mathf.Deg2Rad;
        Vector3 bounceDirection = new Vector3(Mathf.Cos(bounceAngle) * _xSpeed * _xDirection, Mathf.Sin(bounceAngle) * _ySpeed);

        moveAmount = bounceDirection;
    }
}