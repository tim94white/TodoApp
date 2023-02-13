using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TodoList.Pages;

//public class TodoContext : DbContext
//{
//    public DbSet<TodoItem> Todos { get; set; }
//    public string? Dbpath { get; }
//    public TodoContext()
//    {
//        var folder = Environment.SpecialFolder.LocalApplicationData;
//        var path = Environment.GetFolderPath(folder);
//        Dbpath = System.IO.Path.Join(path, "todo.db");
//    }

//    protected override void OnConfiguring(DbContextOptionsBuilder options)
//        => options.UseSqlite($"Data Source={Dbpath}");
//}
public class TodoItem
{
    [JsonProperty("id")]
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool IsDone { get; set; }
}