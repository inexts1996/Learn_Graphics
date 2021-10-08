using UnityEngine;
using static FunctionLibrary;

public class Graph : MonoBehaviour
{
    [SerializeField] private Transform _pointPrefab;
    [SerializeField] [Range(10, 100)] private int _resolution = 10;
    [SerializeField] private FunctionName _function;
    private Transform[] _points;

    private void Awake()
    {
        _points = new Transform[_resolution * _resolution];
        // var position = Vector3.zero;
        var step = 2f / _resolution;
        var scale = Vector3.one * step;

        int pointCount = _points.Length;
        for (int i = 0; i < pointCount; i++)
        {
            // if (x == _resolution)
            // {
                // x = 0;
                // z += 1;
            // }

            var point = Instantiate(_pointPrefab, transform);
            // position.x = (x + 0.5f) * step - 1f;
            // position.z = (z + 0.5f) * step - 1f;
            // position.y = Mathf.Pow(position.x, 3);
            // point.localPosition = position;
            point.localScale = scale;

            _points[i] = point;
        }
    }

    private void Update()
    {
        var time = Time.time;
        var f = GetFunctionBy(_function);
        float step = 2f / _resolution;
        int pointCount = _points.Length;
        for (int i = 0, x = 0, z = 0; i < pointCount; i++, x++)
        {
            if (x == _resolution)
            {
                x = 0;
                z += 1;
            }

            float u = (x + 0.5f) * step - 1f;
            float v = (z + 0.5f) * step - 1f;

            _points[i].localPosition = f(u, v, time);
        }


        // for (var i = 0; i < _points.Length; i++)
        // {
        //     var point = _points[i];
        //     var position = point.localPosition;
        //
        //     point.localPosition = f(position.x, position.z, time);
        // }
    }
}