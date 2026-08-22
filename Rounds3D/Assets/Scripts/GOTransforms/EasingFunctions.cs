using UnityEngine;


// thank you chat
public enum EasingType {
    Linear,
    EaseInQuad,
    EaseOutQuad,
    EaseInOutQuad,
    EaseInCubic,
    EaseOutCubic,
    EaseInOutCubic,
    EaseInQuart,
    EaseOutQuart,
    EaseInOutQuart,
    EaseInSine,
    EaseOutSine,
    EaseInOutSine,
    EaseInExpo,
    EaseOutExpo,
    EaseInOutExpo,
    EaseInBack,
    EaseOutBack,
    EaseInOutBack,
    EaseInElastic,
    EaseOutElastic,
    EaseInOutElastic,
    EaseInBounce,
    EaseOutBounce,
    EaseInOutBounce
}

public static class EasingFunctions {
    public static float Ease(float t, EasingType type) {
        t = Mathf.Clamp01(t);

        switch (type) {
            case EasingType.Linear: return Linear(t);
            case EasingType.EaseInQuad: return EaseInQuad(t);
            case EasingType.EaseOutQuad: return EaseOutQuad(t);
            case EasingType.EaseInOutQuad: return EaseInOutQuad(t);
            case EasingType.EaseInCubic: return EaseInCubic(t);
            case EasingType.EaseOutCubic: return EaseOutCubic(t);
            case EasingType.EaseInOutCubic: return EaseInOutCubic(t);
            case EasingType.EaseInQuart: return EaseInQuart(t);
            case EasingType.EaseOutQuart: return EaseOutQuart(t);
            case EasingType.EaseInOutQuart: return EaseInOutQuart(t);
            case EasingType.EaseInSine: return EaseInSine(t);
            case EasingType.EaseOutSine: return EaseOutSine(t);
            case EasingType.EaseInOutSine: return EaseInOutSine(t);
            case EasingType.EaseInExpo: return EaseInExpo(t);
            case EasingType.EaseOutExpo: return EaseOutExpo(t);
            case EasingType.EaseInOutExpo: return EaseInOutExpo(t);
            case EasingType.EaseInBack: return EaseInBack(t);
            case EasingType.EaseOutBack: return EaseOutBack(t);
            case EasingType.EaseInOutBack: return EaseInOutBack(t);
            case EasingType.EaseInElastic: return EaseInElastic(t);
            case EasingType.EaseOutElastic: return EaseOutElastic(t);
            case EasingType.EaseInOutElastic: return EaseInOutElastic(t);
            case EasingType.EaseInBounce: return EaseInBounce(t);
            case EasingType.EaseOutBounce: return EaseOutBounce(t);
            case EasingType.EaseInOutBounce: return EaseInOutBounce(t);
            default: return t;
        }
    }

    // Linear
    private static float Linear(float t) {
        return t;
    }

    // Quad
    private static float EaseInQuad(float t) {
        return t * t;
    }

    private static float EaseOutQuad(float t) {
        return 1f - (1f - t) * (1f - t);
    }

    private static float EaseInOutQuad(float t) {
        if (t >= 0.5f) {
            return 1f - Mathf.Pow(-2f * t + 2f, 2f) / 2f;
        }
        return 2f * t * t;
    }

    // Cubic
    private static float EaseInCubic(float t) {
        return t * t * t;
    }

    private static float EaseOutCubic(float t) {
        return 1f - Mathf.Pow(1f - t, 3f);
    }

    private static float EaseInOutCubic(float t) {
        if (t >= 0.5f) {
            return 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f;
        }
        return 4f * t * t * t;
    }

    // Quart
    private static float EaseInQuart(float t) {
        return t * t * t * t;
    }

    private static float EaseOutQuart(float t) {
        return 1f - Mathf.Pow(1f - t, 4f);
    }

    private static float EaseInOutQuart(float t) {
        if (t >= 0.5f) {
            return 1f - Mathf.Pow(-2f * t + 2f, 4f) / 2f;
        }
        return 8f * t * t * t * t;
    }

    // Sine
    private static float EaseInSine(float t) {
        return 1f - Mathf.Cos((t * Mathf.PI) / 2f);
    }

    private static float EaseOutSine(float t) {
        return Mathf.Sin((t * Mathf.PI) / 2f);
    }

    private static float EaseInOutSine(float t) {
        return -(Mathf.Cos(Mathf.PI * t) - 1f) / 2f;
    }

    // Expo
    private static float EaseInExpo(float t) {
        return Mathf.Pow(2f, 10f * t - 10f);
    }

    private static float EaseOutExpo(float t) {
        return 1f - Mathf.Pow(2f, -10f * t);
    }

    private static float EaseInOutExpo(float t) {
        if (t >= 0.5f) {
            return (2f - Mathf.Pow(2f, -20f * t + 10f)) / 2f;
        }
        return Mathf.Pow(2f, 20f * t - 10f) / 2f;
    }

    // Back
    private static float EaseInBack(float t) {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        return c3 * t * t * t - c1 * t * t;
    }

    private static float EaseOutBack(float t) {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
    }

    private static float EaseInOutBack(float t) {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;

        if (t >= 0.5f) {
            return (Mathf.Pow(2f * t - 2f, 2f) * ((c2 + 1f) * (t * 2f - 2f) + c2) + 2f) / 2f;
        }
        return (Mathf.Pow(2f * t, 2f) * ((c2 + 1f) * 2f * t - c2)) / 2f;
    }

    // Elastic
    private static float EaseInElastic(float t) {
        const float c4 = (2f * Mathf.PI) / 3f;
        return -Mathf.Pow(2f, 10f * t - 10f) * Mathf.Sin((t * 10f - 10.75f) * c4);
    }

    private static float EaseOutElastic(float t) {
        const float c4 = (2f * Mathf.PI) / 3f;
        return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t * 10f - 0.75f) * c4) + 1f;
    }

    private static float EaseInOutElastic(float t) {
        const float c5 = (2f * Mathf.PI) / 4.5f;
        if (t >= 0.5f) {
            return (Mathf.Pow(2f, -20f * t + 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) / 2f + 1f;
        }
        return -(Mathf.Pow(2f, 20f * t - 10f) * Mathf.Sin((20f * t - 11.125f) * c5)) / 2f;
    }

    // Bounce
    private static float EaseInBounce(float t) {
        return 1f - EaseOutBounce(1f - t);
    }

    private static float EaseOutBounce(float t) {
        const float n1 = 7.5625f;
        const float d1 = 2.75f;

        if (t < 1f / d1) {
            return n1 * t * t;
        }
        if (t < 2f / d1) {
            t -= 1.5f / d1;
            return n1 * t * t + 0.75f;
        }
        if (t < 2.5f / d1) {
            t -= 2.25f / d1;
            return n1 * t * t + 0.9375f;
        }

        t -= 2.625f / d1;
        return n1 * t * t + 0.984375f;
    }

    private static float EaseInOutBounce(float t) {
        if (t >= 0.5f) {
            return (1f + EaseOutBounce(2f * t - 1f)) / 2f;
        }
        return (1f - EaseOutBounce(1f - 2f * t)) / 2f;
    }
}