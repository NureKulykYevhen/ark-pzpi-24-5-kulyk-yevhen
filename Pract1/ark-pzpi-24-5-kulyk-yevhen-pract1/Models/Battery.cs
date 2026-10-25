namespace PowerMonitor.Models;

/// <summary>
/// Акумуляторна батарея резервної системи живлення.
/// </summary>
public class Battery
{
    private const double MaxPercent = 100.0;

    /// <summary>
    /// Унікальний ідентифікатор батареї.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Номінальна ємність батареї, кВт·год.
    /// </summary>
    public required double CapacityKwh { get; init; }

    /// <summary>
    /// Поточний рівень заряду, %.
    /// </summary>
    public double ChargePercent { get; set; }

    /// <summary>
    /// Ступінь зношення батареї, %.
    /// </summary>
    public double WearPercent { get; set; }

    /// <summary>
    /// Обчислює енергію, яку батарея може віддати з урахуванням
    /// поточного заряду та зношення.
    /// </summary>
    /// <returns>Доступна енергія, кВт·год.</returns>
    public double GetAvailableEnergyKwh() =>
        CapacityKwh
        * (ChargePercent / MaxPercent)
        * (1 - (WearPercent / MaxPercent));
}
