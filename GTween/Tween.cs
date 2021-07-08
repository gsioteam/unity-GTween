using System.Collections.Generic;
using UnityEngine;

namespace GTween
{
    public abstract class Property<T, V>
    {
        public abstract V Mix(V from, V to, double p);
        public abstract void Set(T target, V value);
    }

    public class DelegateProperty<T, V> : Property<T, V>
    {
        public System.Func<V, V, double, V> OnMix;
        public System.Action<T, V> OnSet;

        public override V Mix(V from, V to, double p) => OnMix(from, to, p);
        public override void Set(T target, V value) => OnSet(target, value);
    }

    public delegate double EasingFunc(double amount);

    interface ITween
    {
        object Target { get; }

        void Update(double delta);
    }

    class TweenManager : MonoBehaviour
    {
        public HashSet<ITween> Tweens = new HashSet<ITween>();

        private void Update()
        {
            int count = Tweens.Count;
            if (count > 0)
            {
                ITween[] tweens = new ITween[count];
                Tweens.CopyTo(tweens);
                foreach (var tween in tweens)
                {
                    tween.Update(Time.deltaTime);
                }
            }
        }

        public static TweenManager GetManager()
        {
            TweenManager manager = FindObjectOfType<TweenManager>();
            if (manager)
            {
                return manager;
            } else
            {
                GameObject gameObject = new GameObject();
                gameObject.name = "TweenManager";
                return gameObject.AddComponent<TweenManager>();
            }
        }
    }

    public class Tween<T, V> : ITween
    {
        private Property<T, V> property;
        private V from;
        private V to;

        private T target;

        private bool loop = false;

        private double amount = 0;

        public EasingFunc EasingFunction = (amount) => amount;

        private double duration;

        public object Target => target;

        public event System.Action OnComplete;

        public Tween<T, V> Start()
        {
            var tweens = TweenManager.GetManager().Tweens;
            if (!tweens.Contains(this))
                tweens.Add(this);
            return this;
        }

        public Tween<T, V> Stop()
        {
            TweenManager.GetManager().Tweens.Remove(this);
            return this;
        }

        public Tween<T, V> SetEasing(EasingFunc easingFunc)
        {
            this.EasingFunction = easingFunc;
            return this;
        }

        public void Update(double delta)
        {
            amount = System.Math.Max(0.0, System.Math.Min(1.0, amount + delta / duration));
            property.Set(target, property.Mix(from, to, EasingFunction(amount)));
            if (amount >= 1) end();
        }

        private void end()
        {
            amount = 0;
            if (!loop)
            {
                TweenManager.GetManager().Tweens.Remove(this);
                OnComplete?.Invoke();
            }
        }

        public Tween(T target, V from, V to, double duration, Property<T, V> property)
        {
            this.target = target;
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.property = property;
        }
    }

    public static class Tween
    {
        public static Tween<T, V> AddTween<T, V>(this T target, V from, V to, double duration, Property<T, V> property)
            => new Tween<T, V>(target, from, to, duration, property);
        public static Tween<T, V> AddTween<T, V>(this T target, V from, V to, double duration, System.Func<V, V, double, V> OnMix, System.Action<T, V> OnSet)
            => AddTween(target, from, to, duration, new DelegateProperty<T, V>
            {
                OnMix = OnMix,
                OnSet = OnSet
            });

        public static void StopTweens<T>(this T target)
            => TweenManager.GetManager().Tweens.RemoveWhere((val) => val.Target.Equals(target));

        /**
         * Default extensions
         */
        public static Tween<T, float> AddTween<T>(this T target, float from, float to, double duration, System.Action<T, float> OnSet)
            => AddTween(target, from, to, duration, (f, t, p) => f * (float)(1 - p) + t * (float)p, OnSet);
        public static Tween<T, double> AddTween<T>(this T target, double from, double to, double duration, System.Action<T, double> OnSet)
            => AddTween(target, from, to, duration, (f, t, p) => f * (1 - p) + t * p, OnSet);
        public static Tween<T, int> AddTween<T>(this T target, int from, int to, double duration, System.Action<T, int> OnSet)
            => AddTween(target, from, to, duration, (f, t, p) => (int)(f * (1 - p) + t * p), OnSet);
        public static Tween<T, long> AddTween<T>(this T target, long from, long to, double duration, System.Action<T, long> OnSet)
            => AddTween(target, from, to, duration, (f, t, p) => (long) (f* (1 - p) + t* p), OnSet);
        public static Tween<T, Vector2> AddTween<T>(this T target, Vector2 from, Vector2 to, double duration, System.Action<T, Vector2> OnSet)
            => AddTween(target, from, to, duration, (f, t, p) => f * (float)(1 - p) + t * (float)p, OnSet);
        public static Tween<T, Vector3> AddTween<T>(this T target, Vector3 from, Vector3 to, double duration, System.Action<T, Vector3> OnSet)
            => AddTween(target, from, to, duration, (f, t, p) => f * (float)(1 - p) + t * (float)p, OnSet);
        public static Tween<T, Vector4> AddTween<T>(this T target, Vector4 from, Vector4 to, double duration, System.Action<T, Vector4> OnSet)
            => AddTween(target, from, to, duration, (f, t, p) => f * (float)(1 - p) + t * (float)p, OnSet);
        public static Tween<T, Color> AddTween<T>(this T target, Color from, Color to, double duration, System.Action<T, Color> OnSet)
            => AddTween(target, from, to, duration, (f, t, p) => f * (float)(1 - p) + t * (float)p, OnSet);
    }
}
