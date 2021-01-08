using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLayout : MonoBehaviour
{
    public List<Vector2> bubblesPositions = new List<Vector2>();
    public List<Vector2> bubblesScales = new List<Vector2>();
    public List<bool> spawn = new List<bool>();
    public int bubbleNum;
}
