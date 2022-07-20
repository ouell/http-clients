namespace HttpClient.Libs.Benchmark.Benchmark.Dto;

public static class Dtos
{
    public static int IdTodo = 10;
    
    public static Todo CreateTodo => new Todo(IdTodo, "Teste Title", "Teste body original", 10);
    public static Todo UpdateTodo => new Todo(IdTodo, "Teste Title Update", "Teste Body Update", 11);
    
    public static TodoClass CreateTodoClass => new TodoClass(IdTodo, "Teste Title", "Teste body original", 10);
    public static TodoClass UpdateTodoClass => new TodoClass(IdTodo, "Teste Title Update", "Teste Body Update", 11);
}