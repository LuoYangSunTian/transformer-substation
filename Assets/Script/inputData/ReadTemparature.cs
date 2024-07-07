using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;



public class ReadTemparature : MonoBehaviour
{
    private string filePath;
    List<float> temparatures = new List<float>();
    [SerializeField] private TextMeshProUGUI text;
    private int index = 0;
    private void Awake()
    {
        filePath = Path.Combine(Application.dataPath, "温度.csv");
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
                        if (float.TryParse(values[2], out float temparature))
                        {
                            temparatures.Add(temparature);
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
            text.text = temparatures[index].ToString("F2") + "℃";
            yield return new WaitForSeconds(5f);
            index = (index + 1) % temparatures.Count;
        }
    }
}
