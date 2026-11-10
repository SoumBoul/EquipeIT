using EquipIT.Front.Models;
using System.Net.Http.Json;

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
        var equipments = await _httpClient.GetFromJsonAsync<List<Equipment>>( "api/equipment/All");

        return equipments ?? new List<Equipment>();
    }

    public async Task BorrowAsync(
        int id, string firstName, string lastName)
    {
        var request = new
        {
            FirstName = firstName,
            LastName = lastName
        };

        using var response = await _httpClient.PutAsJsonAsync( $"api/equipment/{id}/borrow",request);

        response.EnsureSuccessStatusCode();
    }

    public async Task ReturnAsync(int id)
    {
        using var response = await _httpClient.PutAsync(
            $"api/equipment/{id}/return",
            null);

        response.EnsureSuccessStatusCode();
    }
}
    
   
    

