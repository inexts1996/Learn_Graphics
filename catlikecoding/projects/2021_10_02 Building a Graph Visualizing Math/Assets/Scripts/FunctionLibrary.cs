using UnityEngine;
using static UnityEngine.Mathf;

/// <summary>
///     Feature: 函数库
///     Author： zhouzigui
///     Date： 2021_10_04
/// </summary>
public static class FunctionLibrary
{
    public delegate Vector3 Function(float u, float v, float t);

    private static Function[] _functions = { Wave, MultiWave, Ripple, TripleWave, Sphere, Torus, };

    public enum FunctionName
    {
        Wave,
        multiWave,
        Ripple,
        Triple,
        Sphere,
        Torus,
    }

    public static Function GetFunctionBy(FunctionName name)
    {
        return _functions[(int)name];
    }

    /// <summary>
    ///     正弦波
    /// </summary>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Vector3 Wave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;
        p.y = Sin(PI * (u + v + t));
        p.z = v;

        return p;
    }

    /// <summary>
    ///     多个正弦波
    /// </summary>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public static Vector3 MultiWave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;

        var y = Sin(PI * (u + 0.5f * t));
        y += 0.5f * Sin(2f * PI * (v + t));
        p.y = y * (2f / 3f);

        p.z = v;

        return p;
    }

    public static Vector3 Ripple(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;

        var d = Sqrt(u * u + v * v);
        var y = Sin(PI * (4f * d - t));
        p.y = y / (1f + 10f * d);

        p.z = v;

        return p;
    }

    public static Vector3 TripleWave(float u, float v, float t)
    {
        Vector3 p;
        p.x = u;

        float y = Sin(PI * (u + 0.5f * t));
        y += 0.5f * Sin(2f * PI * (v + t));
        y += Sin(PI * (u + v + 0.25f * t));
        p.y = y * (1 / 2.5f);

        p.z = v;
        return p;
    }

    public static Vector3 Sphere(float u, float v, float t)
    {
        float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v + t));
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);

        return p;
    }

    public static Vector3 Torus(float u, float v, float t)
    {
        float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f + t)); 
        float s = r1 + r2 * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);

        return p;
    }
}