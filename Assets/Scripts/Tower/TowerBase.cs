using UnityEngine;

public class TowerBase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetColor(Color color)
    {
        var allTemp = GetComponentsInChildren<MeshRenderer>();

        foreach (var temp in allTemp)
        {
            temp.material.color = color;
        }
    }
}
