using EquipIT.Front.Models;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace EquipIT.Front.Services;

public class EquipmentApiService
{

    private readonly HttpClient _httpClient;

    public EquipmentApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    // TODO: Pour l'exercice 2, remplacez ces données mockées par un appel HTTP réel vers votre API Backend.
    // En attendant, le front fonctionne hors-ligne avec cette liste simulée en mémoire.

    public async Task<List<Equipment>> GetEquipmentAsync()
    {
        var equipments = await _httpClient.GetFromJsonAsync<List<Equipment>>("api/equipment/All");

        return equipments ?? new List<Equipment>();
    }

    public async Task BorrowAsync(int id, string firstName, string lastName)
    {
        var response = await _httpClient.PutAsync(
            $"api/equipment/{id}/borrow?firstName={firstName}&lastName={lastName}",
            null);

        response.EnsureSuccessStatusCode();
    }

    public async Task ReturnAsync(int id)
    {
        using var response = await _httpClient.PutAsync($"api/equipment/{id}/return",  null);

        response.EnsureSuccessStatusCode();
    }
}


