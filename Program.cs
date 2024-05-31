using System;
using System.Linq;
using var db = new BookDBContext();
var allBooks = db.Books.ToList();

// Remove all entities
db.Books.RemoveRange(allBooks);
// Save changes to apply deletion
db.SaveChanges();
Console.WriteLine("You have clean database!");

// Create
string addAnotherBook;
string title;
int price;
string priceVal;
do
{
    Console.WriteLine("Would you like to add new book to your listing. (Press 'q' to quit | Press 'y' to continue): ");
    addAnotherBook = Console.ReadLine();

    if (addAnotherBook.ToLower() != "q")
    {
        // Create a new entity with user input
        Console.WriteLine("Add New Book");
        Console.WriteLine("Book Title:");
        title = Console.ReadLine();
        Console.WriteLine("Book Price:");
        price = GetValidIntegerInput();

        Book book = new Book();
        book.Title = title;
        book.Price = price;
        db.Add(book);

        db.SaveChanges();
        Console.WriteLine("Book inserted successfully!");
    }

} while (addAnotherBook.ToLower() != "q");

// Read
Console.WriteLine("Listing all of your books");
Console.WriteLine("ID \t Title \t \t Price");

var blogList = db.Books;

foreach (var blogItem in blogList)
{
    Console.WriteLine(blogItem.BookId + " \t " + blogItem.Title + " \t " + blogItem.Price);
}


static int GetValidIntegerInput()
{
    int number;
    bool isValidInput = false;

    do
    {
        string inputString = Console.ReadLine();

        isValidInput = int.TryParse(inputString, out number);

        if (!isValidInput)
        {
            Console.WriteLine("Error: Please enter a valid integer value for price.");
        }

    } while (!isValidInput);

    return number;
}