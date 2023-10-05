// Вставьте сюда финальное содержимое файла BilliardsTask.cs
public static class BilliardsTask
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directionRadians">Угол направления движения шара</param>
    /// <param name="wallInclinationRadians">Угол</param>
    /// <returns></returns>
    public static double BounceWall(double directionRadians, double wallInclinationRadians)
    {
       return -directionRadians + 2*wallInclinationRadians; //5-9
    }
}
