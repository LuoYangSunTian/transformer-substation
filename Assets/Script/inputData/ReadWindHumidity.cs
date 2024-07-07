using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ReadWindHumidity : MonoBehaviour
{
    private string filePath;
    List<float> winds = new List<float>();
    List<float> humiditys = new List<float>();
    [SerializeField] private TextMeshProUGUI windText;
    [SerializeField] private TextMeshProUGUI humidityText;
    private int index = 0;
    private void Awake()
    {
        filePath = Path.Combine(Application.dataPath, "风速湿度.csv");
        Read();
    }
    private void Start()
    {
        Debug.Log(winds.Count);
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
                        if (float.TryParse(values[2], out float humidity))
                        {
                            humiditys.Add(humidity);
                        }
                        if (float.TryParse(values[3], out float wind))
                        {
                            winds.Add(wind);
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
            humidityText.text = humiditys[index].ToString("F2") + "%";
            windText.text = winds[index].ToString("F2") + "m/s";
            yield return new WaitForSeconds(5f);
            index = (index + 1) % winds.Count;
        }
    }
}
