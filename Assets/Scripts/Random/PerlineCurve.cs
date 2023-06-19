using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a Perlin noise curve to generate values over time.
/// </summary>
public class PerlineCurve
{
    private float _amplitude;
    private float _frequency;
    private float _phase;
    private float _verticalShift;

    private float _currentX;

    /// <summary>
    /// Initializes a new instance of the PerlineCurve class.
    /// </summary>
    /// <param name="amplitude">The amplitude of the Perlin curve. Controls the range of generated values.Higher value would result in bigger range of numbers</param>
    /// <param name="frequency">The frequency of the Perlin noise curve. Controls how quickly the curve fluctuates.</param>
    /// <param name="phase">The phase of the Perlin noise curve. Shifts the pattern horizontally.</param>
    /// <param name="verticalShift">The vertical shift of the Perlin noise curve. Shifts the pattern vertically.</param>
    public PerlineCurve(float amplitude, float frequency, float phase, float verticalShift)
    {
        _amplitude = amplitude;
        _frequency = frequency;
        _phase = phase;
        _verticalShift = verticalShift;
    }

    /// <summary>
    /// Get the next value of the Perlin curve.
    /// </summary>
    /// <returns>Next value.</returns>
    public float GetNextValue()
    {
        _currentX += _frequency;
        return Mathf.PerlinNoise(_currentX + _phase, 0) * _amplitude + _verticalShift;
    }
}
