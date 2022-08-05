using System.Net.Http.Headers;
using System.Text.Json;
using SkolplattformenElevApi.Models;
using SkolplattformenElevApi.Models.Internal.Absence;

namespace SkolplattformenElevApi;

public partial class Api
{
    public Task<List<Meal>> GetMealsAsync(int year, int week)
    {
        // Not done yet

        return Task.FromResult(new List<Meal>());
    }
}

