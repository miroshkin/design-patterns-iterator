using System;
using System.Runtime.InteropServices;

namespace DesignPatterns_Iterator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Design Patterns - Iterator!");
            Library library = new Library();
            Reader reader = new Reader();
            reader.SeeBooks(library);
            Console.Read();
        }
    }

    class Reader
    {
        public void SeeBooks(Library library)
        {
            IIBookIterator iterator = library.CreateNumerator();
            while (iterator.HasNext())
            {
                Book book = iterator.Next();
                Console.WriteLine(book.Name);
            }
        }
    }

    class Book
    {
        public string Name { get; set; }
    }

    interface IIBookIterator
    {
        bool HasNext();
        Book Next();
    }

    interface IBookNumerable
    {
        IIBookIterator CreateNumerator();
        int Count { get; }
        Book this[int index] { get; }
    }

    class Library : IBookNumerable
    {
        private Book[] _books;

        public Library()
        {
            _books = new Book[]
            {
                new Book() {Name = "C# In Real Life"},
                new Book() {Name = "Design Patters for Dummies"},
                new Book() {Name = ".NET architecture with examples"}
            };
        }

        public Book this[int index]
        {
            get { return _books[index]; }
        }

        public int Count
        {
            get { return _books.Length; }
        }

        public IIBookIterator CreateNumerator()
        {
            return new LibraryNumerator(this);
        }
    }

    class LibraryNumerator : IIBookIterator
    {
        private IBookNumerable aggregate;
        private int index = 0;

        public LibraryNumerator(IBookNumerable a)
        {
            aggregate = a;
        }

        public bool HasNext()
        {
            return index < aggregate.Count;
        }

        public Book Next()
        {
            return aggregate[index++];
        }
    }
}
