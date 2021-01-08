using UnityEngine;
[System.Serializable]
public class SaveData
{
    public Vector2[] positions;
    public Vector2[] scales;
    public bool[] spawn;
    public int num;
    public SaveData(BubbleLayout layout)
    {
        num = layout.bubbleNum;
        positions = new Vector2[num];
        scales = new Vector2[num];
        spawn = new bool[num];
        for (int i = 0; i< num; i++)
        {
            positions[i] = layout.bubblesPositions[i];
            scales[i] = layout.bubblesScales[i];
            spawn[i] = layout.spawn[i];
        }
    }
}
