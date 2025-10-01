// 代码生成时间: 2025-10-02 03:25:20
using Microsoft.AspNetCore.Components;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

// 该组件展示如何在Blazor应用中防止SQL注入
partial class AntiSqlInjectionBlazorApp {

    [Inject]
    private IDbContextFactory<MyDbContext> dbFactory { get; set; }

    // 用于存储用户输入的搜索查询
    private string searchQuery { get; set; }

    // 处理搜索输入变化的方法
    private async Task SearchAsync(string query) {
        if (string.IsNullOrWhiteSpace(query)) {
            // 如果查询为空，则不执行任何操作
            return;
        }

        try {
            searchQuery = query;
            // 使用参数化查询防止SQL注入
            var db = await dbFactory.CreateDbContextAsync();
            await db.Users.Where(u => u.UserName.Contains(searchQuery)).ToListAsync();

            // 执行数据库查询并显示用户列表
            // 此处省略了显示用户列表的代码
        } catch (Exception ex) {
            // 错误处理
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    // 该方法用于渲染搜索框
    private string searchInputClass => string.IsNullOrEmpty(searchQuery) ? "" : "bg-red-100";

    // 该方法用于渲染搜索按钮
    private async Task OnSearch() {
        await SearchAsync(searchQuery);
    }

    // 在组件的构建方法中初始化
    protected override void OnInitialized() {
        base.OnInitialized();
        // 这里可以执行一些初始化操作
    }
}

// 假设这是一个用于数据库操作的上下文类
public class MyDbContext : DbContext {
    public DbSet<User> Users { get; set; }
}

// 用户实体类
public class User {
    public int Id { get; set; }
    public string UserName { get; set; }
}
