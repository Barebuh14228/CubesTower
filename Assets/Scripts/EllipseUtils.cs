using System.Linq;
using UnityEngine;

//класс написан нейросетью DeepSeek
public static class EllipseUtils
{
    //Метод, который я добавил сам. Увеличивает эллипс до тех пор, пока он не будет включать в себя все точки
    public static EllipseParams CalculateBoundingEllipse(Vector3[] points, float scalingFactor)
    {
        var pointsList = points.ToList();
        var approximateParameters = CalculateApproximateParameters(points);
        
        var realParameters = approximateParameters;
        var scale = 1f;
        
        while (pointsList.Any())
        {
            scale += scalingFactor;
            realParameters.MajorAxis = approximateParameters.MajorAxis * scale;
            realParameters.MinorAxis = approximateParameters.MinorAxis * scale;
                
            pointsList.RemoveAll(p => realParameters.ContainsPoint(p));
        }

        return realParameters;
    }

    private static EllipseParams CalculateApproximateParameters(Vector3[] points)
    {
        if (points == null || points.Length == 0)
            throw new System.ArgumentException("Points array cannot be null or empty");

        // Игнорируем Z-координату и конвертируем в Vector2
        Vector2[] points2D = points.Select(p => new Vector2(p.x, p.y)).ToArray();

        // 1. Находим центр масс (центр эллипса)
        Vector2 center = Vector2.zero;
        foreach (Vector2 p in points2D) center += p;
        center /= points2D.Length;

        // 2. Вычисляем ковариационную матрицу для определения ориентации
        float a = 0, b = 0, c = 0;
        foreach (Vector2 p in points2D)
        {
            Vector2 delta = p - center;
            a += delta.x * delta.x;
            b += delta.x * delta.y;
            c += delta.y * delta.y;
        }
        a /= points2D.Length;
        b /= points2D.Length;
        c /= points2D.Length;

        // 3. Находим собственные значения и векторы (оси эллипса)
        float discriminant = Mathf.Sqrt((a - c) * (a - c) + 4 * b * b);
        float lambda1 = (a + c + discriminant) / 2;
        float lambda2 = (a + c - discriminant) / 2;

        // 4. Вычисляем полуоси
        float majorAxis = Mathf.Sqrt(lambda1) * 2; // Главная ось
        float minorAxis = Mathf.Sqrt(lambda2) * 2; // Вторая ось

        // 5. Находим угол поворота (в градусах)
        float angle = Mathf.Atan2(2 * b, a - c) * Mathf.Rad2Deg / 2;
        
        return new EllipseParams
        {
            Center = center,
            MajorAxis = majorAxis,
            MinorAxis = minorAxis,
            RotationAngle = angle
        };
    }
    
    public static bool ContainsPoint(this EllipseParams ellipse, Vector3 point)
    {
        // Переводим точку в локальные координаты эллипса (учитываем центр и поворот)
        Vector2 centeredPoint = new Vector2(point.x, point.y) - ellipse.Center;
        
        // Если эллипс не повернут, просто проверяем стандартное уравнение
        if (Mathf.Approximately(ellipse.RotationAngle, 0f))
        {
            return IsPointInAxisAlignedEllipse(centeredPoint, ellipse.MajorAxis, ellipse.MinorAxis);
        }
        
        // Поворачиваем точку в обратную сторону, чтобы учесть наклон эллипса
        float angleRad = -ellipse.RotationAngle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angleRad);
        float sin = Mathf.Sin(angleRad);
        
        Vector2 rotatedPoint = new Vector2(
            centeredPoint.x * cos - centeredPoint.y * sin,
            centeredPoint.x * sin + centeredPoint.y * cos
        );
        
        return IsPointInAxisAlignedEllipse(rotatedPoint, ellipse.MajorAxis, ellipse.MinorAxis);
    }
    
    private static bool IsPointInAxisAlignedEllipse(Vector2 point, float a, float b)
    {
        // Нормализуем координаты точки относительно полуосей
        float nx = point.x / (a / 2);
        float ny = point.y / (b / 2);
        
        // Проверяем уравнение эллипса: (x/a)^2 + (y/b)^2 <= 1
        return (nx * nx + ny * ny) <= 1f + 1e-6f; // Добавляем небольшой эпсилон для учета погрешностей float
    }
    
    public static Vector3[] GetBoundaryPoints(this EllipseParams ellipse, float z = 0f)
    {
        Vector3[] points = new Vector3[8];
        float angleRad = ellipse.RotationAngle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angleRad);
        float sin = Mathf.Sin(angleRad);
        
        // Полуоси
        float a = ellipse.MajorAxis / 2f;
        float b = ellipse.MinorAxis / 2f;
        
        for (int i = 0; i < 8; i++)
        {
            // Угол в радианах (0, 45, 90, ... градусов)
            float theta = i * Mathf.PI / 4f;
            
            // Точка на не повёрнутом эллипсе
            float x = a * Mathf.Cos(theta);
            float y = b * Mathf.Sin(theta);
            
            // Поворачиваем точку на угол эллипса
            float rotatedX = x * cos - y * sin;
            float rotatedY = x * sin + y * cos;
            
            // Сдвигаем в центр эллипса и добавляем Z-координату
            points[i] = new Vector3(
                rotatedX + ellipse.Center.x,
                rotatedY + ellipse.Center.y,
                z
            );
        }
        
        return points;
    }

    public struct EllipseParams
    {
        public Vector2 Center;
        public float MajorAxis; // Длина главной оси
        public float MinorAxis; // Длина второй оси
        public float RotationAngle; // Угол поворота в градусах
    }
}