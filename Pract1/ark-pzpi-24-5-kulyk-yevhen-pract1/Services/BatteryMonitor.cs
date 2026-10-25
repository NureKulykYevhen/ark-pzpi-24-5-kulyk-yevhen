using System;
using System.Collections.Generic;
using System.Linq;
using PowerMonitor.Models;

namespace PowerMonitor.Services;

/// <summary>
/// Відстежує стан батарей та оцінює час автономної роботи.
/// </summary>
public class BatteryMonitor
{
    private const double LowAutonomyHours = 2.0;

    private readonly List<Battery> _batteries;

    public BatteryMonitor(IEnumerable<Battery> batteries)
    {
        ArgumentNullException.ThrowIfNull(batteries);
        _batteries = [.. batteries];
    }

    /// <summary>
    /// Оцінює автономність батареї за поточного навантаження.
    /// </summary>
    /// <param name="batteryId">Ідентифікатор батареї.</param>
    /// <param name="loadKw">Поточне навантаження, кВт.</param>
    /// <returns>Час автономної роботи та ознака низького запасу.</returns>
    public AutonomyStatus CheckAutonomy(int batteryId, double loadKw)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(loadKw);

        Battery? battery =
            _batteries.FirstOrDefault(b => b.Id == batteryId);
        if (battery is null)
        {
            throw new KeyNotFoundException(
                $"Battery {batteryId} was not found.");
        }

        double autonomyHours = battery.GetAvailableEnergyKwh() / loadKw;
        bool isLow = autonomyHours < LowAutonomyHours;

        return new AutonomyStatus(autonomyHours, isLow);
    }
}

/// <summary>
/// Результат оцінки автономності батареї.
/// </summary>
public record AutonomyStatus(double Hours, bool IsLow);
