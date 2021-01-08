using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class BubbleLoader : MonoBehaviour
{
    public GameObject bubblePrefab;
    public BubbleLayout layout;
    private void Start()
    {
        layout = GetComponent<BubbleLayout>();
    }
    public void LoadBubbles()
    {
        if (PlayerPrefs.GetInt("replay") == 1)
        {
            SaveData data = SerializationManager.Load("bubbleLayout");
            layout.bubbleNum = data.num;
            for (int i = 0; i < data.num; i++)
            {
                Vector3 pos = new Vector3(data.positions[i].x, data.positions[i].y, 0);
                layout.bubblesPositions.Add(pos);

                Vector3 scale = new Vector3(data.scales[i].x, data.scales[i].y, 1);
                layout.bubblesScales.Add(scale);
                layout.spawn.Add(data.spawn[i]);
                if(data.spawn[i] == true)
                {
                    GameObject bubbleObj = Instantiate(bubblePrefab, pos, Quaternion.identity);
                    bubbleObj.GetComponent<Bubble>().bubbleId = i;
                    bubbleObj.transform.localScale = scale;
                }
            }
        }
    }
    public void AddBubble(Vector2 pos, Vector2 scale)
    {
        layout.bubblesPositions.Add(pos);
        layout.bubblesScales.Add(scale);
        layout.spawn.Add(true);
        layout.bubbleNum += 1;
    }
    public void RemoveBubble(int id)
    {
        layout.spawn[id] = false;
    }
    public void SaveBubbles()
    {
        SerializationManager.Save("bubbleLayout", layout);
    }
}
