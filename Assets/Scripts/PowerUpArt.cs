using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

/// <summary>
/// Container used to define the ball's art style options.
/// </summary>
[Serializable]
public class PowerUpArt
{
  public PowerUps.PowerUpTypes powerUpType;
  public GameObject gameObject;
}
