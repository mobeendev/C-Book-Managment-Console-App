using System;
using System.Linq;
using var db = new BookDBContext();
var allBooks = db.Books.ToList();

// Remove all entities
// db.Books.RemoveRange(allBooks);
// Save changes to apply deletion
// db.SaveChanges();
Console.WriteLine("You have clean database!");

string addAnotherBook;
string deleteBook;
string title;
int price;

addBooks();
printBooks();
removeBook();
printBooks();


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

void addBooks()
{
    using var db = new BookDBContext();
    // Create
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
}

void removeBook()
{
    do
    {
        Console.WriteLine("Do you want to delete any book? (Press 'q' to quit | Press 'y' to continue): ");
        deleteBook = Console.ReadLine();

        if (deleteBook.ToLower() != "q")
        {
            // Create a new entity with user input
            Console.WriteLine("Enter Book Title to remove:");
            string searchTerm = Console.ReadLine();

            var booksToRemove = db.Books.Where(e => e.Title.Contains(searchTerm)).ToList();

            if (booksToRemove != null)
            {
                foreach (var book in booksToRemove)
                {
                    Console.WriteLine("Book " + book.Title + " removed successfully!");
                    db.Books.Remove(book);
                    db.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine("No results found.");

            }

        }

    } while (addAnotherBook.ToLower() != "q");
}

static void printBooks()
{
    // Read
    using var db = new BookDBContext();
    Console.WriteLine("Listing all of your books");
    Console.WriteLine("ID \t Title \t \t Price");

    var bookList = db.Books;

    foreach (var book in bookList)
    {
        Console.WriteLine(book.BookId + " \t " + book.Title + " \t " + book.Price);
    }
}