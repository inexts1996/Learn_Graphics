using System.Collections.Generic;
using UnityEngine;

public class TransformationGrid : MonoBehaviour
{
    [SerializeField] private Transform _prafab;

    [SerializeField] private int _gridResolution = 10;

    private Transform[] _grid;

    private List<Transformation> _transformations;
    private Matrix4x4 transformation;

    private void Awake()
    {
        _transformations = new List<Transformation>();
        _grid = new Transform[_gridResolution * _gridResolution * _gridResolution];

        for (int i = 0, z = 0; z < _gridResolution; z++)
        for (var y = 0; y < _gridResolution; y++)
        for (var x = 0; x < _gridResolution; x++, i++)
            _grid[i] = CreateGridPoint(x, y, z);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateTransformation();
        GetComponents(_transformations);
        for (int i = 0, z = 0; z < _gridResolution; z++)
        for (var y = 0; y < _gridResolution; y++)
        for (var x = 0; x < _gridResolution; x++, i++)
            _grid[i].localPosition = TransformPoint(x, y, z);
    }

    private void UpdateTransformation()
    {
        GetComponents(_transformations);
        var count = _transformations.Count;

        if (count > 0)
        {
            transformation = _transformations[0].Matrix;
            for (var i = 1; i < count; i++) transformation = _transformations[i].Matrix * transformation;
        }
    }

    private Vector3 TransformPoint(int x, int y, int z)
    {
        var coordinates = GetCoordinates(x, y, z);

        // for (var i = 0; i < _transformations.Count; i++) coordinates = _transformations[i].Apply(coordinates);

        // return coordinates;
        return transformation.MultiplyPoint(coordinates);
    }

    private Transform CreateGridPoint(int x, int y, int z)
    {
        var point = Instantiate(_prafab);
        point.localPosition = GetCoordinates(x, y, z);
        point.GetComponent<MeshRenderer>().material.color = new Color(
            (float)x / _gridResolution,
            (float)y / _gridResolution,
            (float)z / _gridResolution
        );

        return point;
    }

    private Vector3 GetCoordinates(int x, int y, int z)
    {
        return new Vector3(
            x - (_gridResolution - 1) * 0.5f,
            y - (_gridResolution - 1) * 0.5f,
            z - (_gridResolution - 1) * 0.5f
        );
    }
}