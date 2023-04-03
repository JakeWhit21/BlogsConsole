using NLog;
using System.Linq;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");

Console.WriteLine("1. Display all blogs");
Console.WriteLine("2. Add blog");
Console.WriteLine("3. Create post");
Console.WriteLine("4. Display posts");

string input = Console.ReadLine();

try
{
    if (input == "2")
    {
        // Create and save a new Blog
        Console.Write("Enter a name for a new Blog: ");
        var name = Console.ReadLine();

        var blog = new Blog { Name = name };

        var db = new BloggingContext();
        db.AddBlog(blog);
        logger.Info("Blog added - {name}", name);
    }

    if (input == "1")
    {
        // Display all Blogs from the database
        var db = new BloggingContext();

        var query = db.Blogs.OrderBy(b => b.Name);

        Console.WriteLine("All blogs in the database:");
        foreach (var item in query)
        {
            Console.WriteLine(item.Name);
        }
    }

}
catch (Exception ex)
{
    logger.Error(ex.Message);
}

logger.Info("Program ended");