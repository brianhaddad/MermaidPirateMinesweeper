using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private CustomGrid<int> _grid;

    // Start is called before the first frame update
    void Start()
    {
        _grid = new CustomGrid<int>(21, 10, 0.9f, new Vector3(-10.0f, -4.5f), (CustomGrid<int> g, int x, int y) => 0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(_grid.GetLocationString(GetMouseWorldPosition()));
        }
    }

    private Vector3 GetMouseWorldPosition()
        => Camera.main.ScreenToWorldPoint(Input.mousePosition);
}
