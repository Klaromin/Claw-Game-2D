using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CanvasPositioningExtensions {
    public static Vector3 WorldToCanvasPosition(this UnityEngine.Canvas canvas, Vector3 worldPosition, Camera camera) {
        var viewportPosition = camera.WorldToViewportPoint(worldPosition);
        return canvas.ViewportToCanvasPosition(viewportPosition);
    }

    public static Vector3 ScreenToCanvasPosition(this UnityEngine.Canvas canvas, Vector2 screenPosition) {
        var viewportPosition = new Vector2(screenPosition.x / Screen.width, screenPosition.y / Screen.height);
        return canvas.ViewportToCanvasPosition(viewportPosition);
    }

    public static Vector3 ViewportToCanvasPosition(this UnityEngine.Canvas canvas, Vector2 viewportPosition) {
        var canvasTransform = (RectTransform) canvas.transform;
        var scaled = Vector2.Scale(viewportPosition, canvasTransform.sizeDelta);
        return Vector2.Scale(scaled, canvasTransform.localScale);
    }
}
