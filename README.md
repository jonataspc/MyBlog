# TODO!!! :



### Entity Framework Core Migration:
Examples:

``` Add-Migration AddMail -Verbose -Context MyBlogDbContext -Project MyBlog.Infra.Data -Startup MyBlog.Web.Mvc ``` or ```dotnet ef migrations add AddMail  -c MyBlogDbContext -p src/MyBlog.Infra.Data -s src/MyBlog.Web.Mvc -v```

``` Script-Migration -Verbose -Context MyBlogDbContext -Project MyBlog.Infra.Data -Startup MyBlog.Web.Mvc ``` 

or 

```Update-Database -Verbose -Context MyBlogDbContext -Project MyBlog.Infra.Data -Startup MyBlog.Web.Mvc ```

or 

```dotnet ef database update -c MyBlogDbContext -p src/MyBlog.Infra.Data -s src/MyBlog.Web.Mvc -v```


Resources:

https://stackoverflow.com/a/53757540/9399171 

