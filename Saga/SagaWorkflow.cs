using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SagaWorkflow
{
    private readonly List<Func<Task>> _steps = new List<Func<Task>>();
    private readonly Dictionary<string, Func<Task>> _compensations = new Dictionary<string, Func<Task>>();
    private int _currentStep = 0;

    public void AddStep(Func<Task> step)
    {
        _steps.Add(step);
    }

    public void OnCompensation(Func<string, Task> compensation)
    {
        _compensations["Compensation"] = () => compensation("Compensation");
    }

    public async Task ExecuteAsync()
    {
        try
        {
            while (_currentStep < _steps.Count)
            {
                await _steps[_currentStep]();
                _currentStep++;
            }
        }
        catch (Exception)
        {
            if (_compensations.ContainsKey("Compensation"))
                await _compensations["Compensation"]();
        }
    }
}
