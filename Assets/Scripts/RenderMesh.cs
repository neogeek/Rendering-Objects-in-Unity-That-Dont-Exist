using System.Collections.Generic;
using UnityEngine;

public class RenderMesh : MonoBehaviour
{

    [SerializeField]
    private Mesh _mesh;

    [SerializeField]
    private Material _material;

    [SerializeField]
    private float _rotationSpeed = 100;

    private float _rotation;

    private Vector3 _scale;

    private int _layer;

    private readonly List<Matrix4x4> _matrices = new();

    private void Awake()
    {
        _material.enableInstancing = true;

        _layer = LayerMask.NameToLayer("Default");
    }

    private void Start()
    {
        var rotation = Quaternion.Euler(_rotation, _rotation, _rotation);

        _scale = Vector3.one / 2;

        const int size = 100;

        for (var x = -size; x < size; x += 1)
        {
            for (var y = -size; y < size; y += 1)
            {
                _matrices.Add(Matrix4x4.TRS(new Vector3(x + 0.5f, y, 0), rotation, _scale));
            }
        }
    }

    private void Update()
    {
        _rotation += _rotationSpeed * Time.deltaTime;

        var rotation = Quaternion.Euler(_rotation, _rotation, _rotation);

        for (var i = 0; i < _matrices.Count; i++)
        {
            _matrices[i] = Matrix4x4.TRS(_matrices[i].GetColumn(3), rotation, _scale);
        }

        Graphics.DrawMeshInstanced(_mesh, _layer, _material, _matrices);
    }

}
