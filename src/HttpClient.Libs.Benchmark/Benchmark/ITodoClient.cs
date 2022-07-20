using HttpClient.Libs.Benchmark.Benchmark.Dto;
using Refit;

namespace HttpClient.Libs.Benchmark.Benchmark;

public interface ITodoClient
{
    [Post("/todos")]
    Task<Todo> CreateTodo([Body] Todo todo);
    
    [Put("/todos/{id}")]
    Task<Todo> UpdateTodo(int id, [Body] Todo todo);

    [Get("/todos")]
    Task<List<Todo>> Get();
}