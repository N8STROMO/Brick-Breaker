using UnityEngine;

/*
 * 
 * */

public class Paddle : MonoBehaviour
{
  /// <summary>
  /// Assign one child gameObject per size supported.  Power ups will increment/decrement the size.
  /// </summary>
  [SerializeField]
  GameObject[] paddleArtBySize;

  private Rigidbody2D rb2d;
  public float speed;

  private int _currentSize;

  /// <summary>
  /// Manages the size of the paddle, swapping art as appropriate.
  /// </summary>
  public int currentSize
  {
    get
    {
      return _currentSize;
    }
    set
    {
      if(currentSize > 0)
      {
        paddleArtBySize[currentSize - 1].SetActive(false);
      }
      _currentSize = value;
      if(currentSize > paddleArtBySize.Length)
      { // Max size supported
        currentSize = paddleArtBySize.Length;
      }

      paddleArtBySize[currentSize - 1].SetActive(true);
    }
  }

  /// <summary>
  /// True if we are not already at the max supported size.
  /// </summary>
  public bool canGrow
  {
    get
    {
      return currentSize < paddleArtBySize.Length;
    }
  }
     

  /// <summary>
  /// Call on first frame
  /// </summary>
  private void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    currentSize = 1;
  }

  /// <summary>
  /// Deals with the movement of the paddle
  /// </summary>
  void FixedUpdate()
  {
    //Created two boolean variables to determine if left or right arrow is pressed
    bool movementRight = Input.GetKey(KeyCode.RightArrow);
    bool movementLeft = Input.GetKey(KeyCode.LeftArrow);

    //Controls the movement based on which arrow key is selected
    //If movement right set new velocity to a positive speed vector, no movement on y
    if(movementRight)
    {
      rb2d.velocity = new Vector2(speed, 0);
    }

    //If movement left set new velocity to a negative speed vector, no movement on y
    else if(movementLeft)
    {
      rb2d.velocity = new Vector2(-speed, 0);
    }

    //Default condition needed in nase neither arrow key is being pressed
    else
    {
      rb2d.velocity = Vector2.zero;
    }
  }
}
