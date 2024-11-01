using System;
using System.Collections.Generic;
using System.Linq;

namespace RegistroLibros
{
    // Sacamos las clases fuera de la clase Program
    public class Book
    {
        #region Properties
        public int BookId { get; set; }
        public string Name { get; set; }
        public int PublishAge { get; set; }
        #endregion

        #region Constructors
        public Book() { }

        public Book(int bookId, string name, int publishAge)
        {
            BookId = bookId;
            Name = name;
            PublishAge = publishAge;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Book: {Name} (Published: {PublishAge})";
        }
        #endregion
    }

    public class Author
    {
        #region Properties
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        #endregion

        #region Constructors
        public Author() { }

        public Author(int authorId, string name, string lastName)
        {
            AuthorId = authorId;
            Name = name;
            LastName = lastName;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{Name} {LastName}";
        }
        #endregion
    }

    public class Work
    {
        #region Properties
        public int WorkId { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        #endregion

        #region Constructors
        public Work() { }

        public Work(int workId, int authorId, int bookId)
        {
            WorkId = workId;
            AuthorId = authorId;
            BookId = bookId;
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Listas de datos
            List<Author> authors = new List<Author>
            {
                new Author(1, "Robert C.", "Martin"),
                new Author(2, "Martin", "Fowler"),
                new Author(3, "Kent", "Beck"),
                new Author(4, "Erich", "Gamma"),
                new Author(5, "Grady", "Booch"),
                new Author(6, "Andrew", "Hunt"),
                new Author(7, "Dave", "Thomas"),
                new Author(8, "Chris", "Pine")
            };

            List<Book> books = new List<Book>
            {
                new Book(1, "Clean Code", 2008),
                new Book(2, "Refactoring: Improving the Design of Existing Code", 1999),
                new Book(3, "Test Driven Development: By Example", 2003),
                new Book(4, "Design Patterns: Elements of Reusable Object-Oriented Software", 1994),
                new Book(5, "The Unified Modeling Language User Guide", 1999),
                new Book(6, "The Pragmatic Programmer", 1999),
                new Book(7, "Agile Software Development: Principles, Patterns, and Practices", 2002),
                new Book(8, "Patterns of Enterprise Application Architecture", 2002),
                new Book(9, "Extreme Programming Explained", 2000),
                new Book(10, "The Pragmatic Programmer (2nd Edition)", 2019)
            };

            List<Work> works = new List<Work>
            {
                new Work(1, 1, 1),
                new Work(2, 2, 2),
                new Work(3, 3, 3),
                new Work(4, 4, 4),
                new Work(5, 5, 5),
                new Work(6, 6, 6),
                new Work(7, 7, 6),
                new Work(8, 1, 7),
                new Work(9, 2, 8),
                new Work(10, 3, 9),
                new Work(11, 6, 10),
                new Work(12, 7, 10)
            };

            // Implementación de los ejercicios solicitados
            Console.WriteLine("Ejercicios de Consulta de Libros y Autores");

            // 1. Obtener todos los libros escritos por Robert C. Martin
            var martinBooks = works
                .Where(w => w.AuthorId == authors.First(a => a.Name == "Robert C." && a.LastName == "Martin").AuthorId)
                .Select(w => books.First(b => b.BookId == w.BookId))
                .ToList();

            Console.WriteLine("1. Libros de Robert C. Martin:");
            foreach (var book in martinBooks)
            {
                Console.WriteLine(book);
            }

            // 2. Obtener autores de "The Pragmatic Programmer"
            var pragmaticProgrammerAuthors = works
                .Where(w => w.BookId == books.First(b => b.Name == "The Pragmatic Programmer").BookId)
                .Select(w => authors.First(a => a.AuthorId == w.AuthorId))
                .ToList();

            Console.WriteLine("\n2. Autores de The Pragmatic Programmer:");
            foreach (var author in pragmaticProgrammerAuthors)
            {
                Console.WriteLine(author);
            }

            // 3. Libros publicados después del 2000
            var booksAfter2000 = books.Where(b => b.PublishAge > 2000).ToList();

            Console.WriteLine("\n3. Libros publicados después de 2000:");
            foreach (var book in booksAfter2000)
            {
                Console.WriteLine(book);
            }

            // 4. Número total de autores que han escrito al least un libro
            var uniqueAuthors = works.Select(w => w.AuthorId).Distinct().Count();
            Console.WriteLine($"\n4. Número total de autores que han escrito al menos un libro: {uniqueAuthors}");

            // 5. Libro más reciente
            var newestBook = books.OrderByDescending(b => b.PublishAge).First();
            Console.WriteLine($"\n5. Libro más reciente: {newestBook}");

            // 6. Autor que ha escrito más libros
            var mostProlificAuthor = works
                .GroupBy(w => w.AuthorId)
                .OrderByDescending(g => g.Count())
                .First();

            var prolificAuthor = authors.First(a => a.AuthorId == mostProlificAuthor.Key);
            Console.WriteLine($"\n6. Autor con más libros: {prolificAuthor} (Libros: {mostProlificAuthor.Count()})");

            // 7. Títulos de libros con múltiples autores
            var multiAuthorBooks = works
                .GroupBy(w => w.BookId)
                .Where(g => g.Count() > 1)
                .Select(g => books.First(b => b.BookId == g.Key))
                .ToList();

            Console.WriteLine("\n7. Libros con múltiples autores:");
            foreach (var book in multiAuthorBooks)
            {
                Console.WriteLine(book);
            }
        }
    }
}