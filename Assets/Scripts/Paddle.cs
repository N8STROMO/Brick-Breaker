using UnityEngine;

public class Paddle : MonoBehaviour
{
  // Assign one child gameObject per size supported.  Power ups will increment/decrement the size.
  [SerializeField]
  GameObject[] paddleArtBySize;

  private Rigidbody2D rb2d;

  public float speed;

  int _currentSize;
  
  // Manages the size of the paddle, swapping art as appropriate.
  public int currentSize
  {
    get
    {
      return _currentSize;
    }
    set
    {
      // Make sure the value stays within the bounds of the paddleArtBySize array we dont' get index out of bounds exception
      int newValue = Mathf.Clamp(value, 0, paddleArtBySize.Length - 1);
      int previousValue = currentSize;
      
      // Hide the previous paddle object we were displaying
      paddleArtBySize[previousValue].SetActive(false);
      
      // Update the private current size field
      _currentSize = newValue;

      // Display the new paddle object we set
      paddleArtBySize[newValue].SetActive(true);
    }
  }
 
  // True if we are not already at the max supported size.
  public bool canGrow
  {
    get
    {
      return currentSize < paddleArtBySize.Length;
    }
  }
     
  private void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    currentSize = 1;
  }

  // Deals with the movement of the paddle
  void FixedUpdate()
  {
    //Created two boolean variables to determine if left or right arrow is pressed
    bool movementRight = Input.GetKey(KeyCode.RightArrow);
    bool movementLeft = Input.GetKey(KeyCode.LeftArrow);

    // Controls the movement based on which arrow key is selected
    // If movement right set new velocity to a positive speed vector, no movement on y
    if(movementRight)
    {
      rb2d.velocity = new Vector2(speed, 0);
    }

    // If movement left set new velocity to a negative speed vector, no movement on y
    else if(movementLeft)
    {
      rb2d.velocity = new Vector2(-speed, 0);
    }

    // Default condition needed in nase neither arrow key is being pressed
    else
    {
      rb2d.velocity = Vector2.zero;
    }
  }
}
