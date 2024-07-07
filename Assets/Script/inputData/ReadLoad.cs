using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ReadLoad : MonoBehaviour
{
    private string filePath;
    List<float> mins = new List<float>();
    List<float> maxs = new List<float>();
    [SerializeField] private TextMeshProUGUI minText;
    [SerializeField] private TextMeshProUGUI maxText;
    private int index = 0;
    private void Awake()
    {
        filePath = Path.Combine(Application.dataPath, "变电站电力负荷数据.CSV");
        Read();
    }
    private void Start()
    {
        StartCoroutine(DelayedAction());
    }


    public void Read()
    {
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                bool isHeader = true;
                while ((line = reader.ReadLine()) != null)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }
                    string[] values = line.Split(',');

                    if (values.Length > 2)
                    {
                        if (float.TryParse(values[2], out float max))
                        {
                            maxs.Add(max);
                        }
                        if (float.TryParse(values[4], out float min))
                        {
                            mins.Add(min);
                        }
                    }
                }
            }
        }
        catch (IOException e)
        {
            Debug.Log("The file could not be read!");
        }
    }

    IEnumerator DelayedAction()
    {
        while (true)
        {
            maxText.text = maxs[index].ToString("F2");
            minText.text = mins[index].ToString("F2");
            yield return new WaitForSeconds(5f);
            index = (index + 1) % mins.Count;
        }
    }
}
