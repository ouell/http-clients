namespace HttpClient.Libs.Benchmark.Benchmark.Dto;

public record struct Todo(int Id, string Title, string Body, int UserId);
public record TodoClass(int Id, string Title, string Body, int UserId);