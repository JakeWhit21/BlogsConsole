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

var db = new BloggingContext();

try
{

    if (input == "1")
    {
        // Display all Blogs from the database

        var query = db.Blogs.OrderBy(b => b.Name);

        Console.WriteLine("All blogs in the database:");
        foreach (var item in query)
        {
            Console.WriteLine(item.Name);
        }
    }

    if (input == "2")
    {
        // Create and save a new Blog
        Console.Write("Enter a name for a new Blog: ");
        var name = Console.ReadLine();

        var blog = new Blog { Name = name };

        db.AddBlog(blog);
        logger.Info("Blog added - {name}", name);
    }

    if (input == "3")
    {
        //Create post in selected blog
        Console.WriteLine("Select blog to post to: ");
        string selectedBlog = Console.ReadLine();

        Post post = new Post();
        post.BlogId = db.Blogs.Where(b => b.Name.Equals(selectedBlog)).Select(b => b.BlogId).First();

        Console.WriteLine("Enter title for post");
        post.Title = Console.ReadLine();

        Console.WriteLine("Enter content for post");
        post.Content = Console.ReadLine();

        db.AddPost(post);
        logger.Info("Post added - {title}", post.Title);



        //use post.(paramater) = ...

        //use db.AddPost(post);


        // foreach (var blog in db.Blogs)
        // {
        //     if (blog.BlogId == foundBlogId)
        //     {
        //         Console.WriteLine("Enter title for post: ");
        //         string title = Console.ReadLine();

        //         Console.WriteLine("Enter content for the post: ");
        //         string content = Console.ReadLine();

        //         Post post = new Post
        //         {
        //             Title = title,
        //             Content = content,
        //             BlogId = foundBlogId,
        //             Blog = blog
        //         };

        //         blog.Posts.Add(post);
        //         logger.Info("Post added - {title}", title);
        //     }
        // }




    }

}
catch (Exception ex)
{
    logger.Error(ex.Message);
}

logger.Info("Program ended");