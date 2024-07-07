using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ReadWindDirection : MonoBehaviour
{
    private string filePath;
    List<string> winds = new List<string>();
    [SerializeField] private TextMeshProUGUI text;
    private int index = 0;
    private void Awake()
    {
        filePath = Path.Combine(Application.dataPath, "风向.csv");
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
                        winds.Add(values[2]);
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
            text.text = winds[index];
            yield return new WaitForSeconds(120f);
            index = (index + 1) % winds.Count;
        }
    }
}
