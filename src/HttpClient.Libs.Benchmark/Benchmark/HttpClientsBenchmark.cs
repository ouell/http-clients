using System.Net.Http.Json;
using BenchmarkDotNet.Attributes;
using Flurl;
using Flurl.Http;
using HttpClient.Libs.Benchmark.Benchmark.Dto;
using Refit;
using RestSharp;

namespace HttpClient.Libs.Benchmark.Benchmark;

[MemoryDiagnoser]
public class HttpClientsBenchmark
{
    private const string Url = "https://jsonplaceholder.typicode.com/";

    private static readonly RestClient RestClient = new(Url);
    private static readonly System.Net.Http.HttpClient HttpClient = new();
    private readonly ITodoClient _refitClient = RestService.For<ITodoClient>(Url);

    public HttpClientsBenchmark()
    {
        HttpClient.BaseAddress = new Uri(Url);
    }

    [Benchmark]
    public async Task<Todo> FlUrl_Create()
    {
        var response = await Url.AppendPathSegment("todos")
                                .PostJsonAsync(Dtos.CreateTodo)
                                .ReceiveJson<Todo>();

        return response;
    }

    [Benchmark]
    public async Task<Todo> HttpClient_Create()
    {
        var response = await HttpClient.PostAsJsonAsync("todos", Dtos.CreateTodo);
        var todo = await response.Content.ReadFromJsonAsync<Todo>();

        return todo;
    }

    [Benchmark]
    public async Task<Todo> RestSharp_Create()
    {
        var request = new RestRequest("todos").AddJsonBody(Dtos.CreateTodoClass);
        var createdTodo = await RestClient.PostAsync<Todo>(request);

        return createdTodo;
    }

    [Benchmark]
    public async Task<Todo> Refit_Create()
    {
        var createdTodo = await _refitClient.CreateTodo(Dtos.CreateTodo);

        return createdTodo;
    }

    [Benchmark]
    public async Task<Todo> FlUrl_Update()
    {
        var updateData = Dtos.UpdateTodo;
        var response = await Url.AppendPathSegments("todos", updateData.Id)
                                .PutJsonAsync(updateData)
                                .ReceiveJson<Todo>();

        return response;
    }

    [Benchmark]
    public async Task<Todo> HttpClient_Update()
    {
        var updateData = Dtos.UpdateTodo;
        var response = await HttpClient.PutAsJsonAsync($"todos/{updateData.Id}", updateData);
        var todo = await response.Content.ReadFromJsonAsync<Todo>();

        return todo;
    }

    [Benchmark]
    public async Task<Todo> RestSharp_Update()
    {
        var updateData = Dtos.UpdateTodo;
        var request = new RestRequest($"todos/{updateData.Id}").AddJsonBody(Dtos.UpdateTodoClass);
        var createdTodo = await RestClient.PutAsync<Todo>(request);

        return createdTodo;
    }

    [Benchmark]
    public async Task<Todo> Refit_Update()
    {
        var updateData = Dtos.UpdateTodo;
        var createdTodo = await _refitClient.UpdateTodo(updateData.Id, updateData);

        return createdTodo;
    }

    [Benchmark]
    public async Task<List<Todo>> FlUrl_Get()
    {
        var response = await Url.AppendPathSegment("todos")
                                .GetAsync()
                                .ReceiveJson<List<Todo>>();

        return response;
    }

    [Benchmark]
    public async Task<List<Todo>?> HttpClient_Get()
    {
        var response = await HttpClient.GetAsync("todos");
        var todos = await response.Content.ReadFromJsonAsync<List<Todo>>();

        return todos;
    }

    [Benchmark]
    public async Task<List<Todo>?> RestSharp_Get()
    {
        var request = new RestRequest("todos");
        var response = await RestClient.GetAsync<List<Todo>>(request);

        return response;
    }

    [Benchmark]
    public async Task<List<Todo>> Refit_Get()
    {
        var response = await _refitClient.Get();

        return response;
    }
}