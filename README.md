# unity-GTween
A tween engine for unity.

### Using

```c#
// Stop tweens
transform.StopTweens();

// Start a tween
transform.AddTween(
    Vector3.zero,
    Vector3.one,
    0.6,
    (t, v) => t.position = v)
    .SetEasing(Easing.BounceOut)
    .Start();
```