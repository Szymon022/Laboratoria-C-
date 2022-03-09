using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab_12_PL_A
{
    public class Book
    {
        [XmlElement(ElementName = "GUID")]
        public Guid ID { get; set; }
        public string Title { get; set; }
        public CoverType CoverType { get; set; }
        public Person Author { get; set; }
        public Details Details { get; set; }

        public override string ToString()
        {
            return $"Book '{Title}' by {Author}";
        }

        public Book() { }
    }

    public enum CoverType
    {
        Hardcover, EBook
    }

    public class Details
    {
        public DateTime PublicationDate { get; set; }
        public string PublicationCity { get; set; }
        public string PublisherName { get; set; }

        public Details(DateTime pd, string pc, string pn)
        {
            PublicationDate = pd;
            PublicationCity = pc;
            PublisherName = pn;
        }

        public Details() { }
    }

    public class Person
    {
        public Person() { }

        public Person(Guid id, string name, string surname, DateTime birthDate)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Birthday = birthDate;
        }
        [XmlElement(ElementName = "GUID")]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }

    public class Library
    {
        //private List<Book> books;
        //private List<Person> authors;
        public List<Book> Books { get; set; }
        public List<Person> Authors { get; set; }

        public string RootDir { get; set; }

        public Library(string path)
        {
            RootDir = path;
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Books = new List<Book>();
            Authors = new List<Person>();
        }

        public static Library Create(string libraryPath, string sourceFile)
        {
            Library newLibrary = new Library(libraryPath);

            string[] sourceLines = File.ReadAllLines(sourceFile);

            for(int i = 1; i < sourceLines.Length; i++)
            {
                string[] elements = sourceLines[i].Split(";");

                Book newBook = new Book();
                newBook.ID = Guid.Parse(elements[0]);
                newBook.Title = elements[1];
                newBook.CoverType = elements[2] == "Hardcover" ? CoverType.Hardcover : CoverType.EBook;

                string[] date_entries = elements[6].Split(".");
                int day = int.Parse(date_entries[0]);
                int month = int.Parse(date_entries[1]);
                int year = int.Parse(date_entries[2]);
                DateTime birthDate = new DateTime(year, month, day);

                Person author = new Person(Guid.Parse(elements[3]), elements[4], elements[5], birthDate);
                newBook.Author = author;
                //newLibrary.authors.Add(author);
                newLibrary.Add(author);

                string[] publication_date_entries = elements[7].Split(".");
                int pub_day = int.Parse(date_entries[0]);
                int pub_month = int.Parse(date_entries[1]);
                int pub_year = int.Parse(date_entries[2]);
                DateTime pubDay = new DateTime(pub_year, pub_month, pub_day);

                newBook.Details = new Details(pubDay, elements[8], elements[9]);

                //newLibrary.books.Add(newBook);
                newLibrary.Add(newBook);
            }

            return newLibrary;
        }

        public void Add(object obj)
        {
            string filePath;
            if (obj.GetType() == typeof(Book))
            {
                filePath = Path.Combine(RootDir, ((Book)obj).ID.ToString() + ".xml");
                if (File.Exists(filePath))
                {
                    throw new PathAlreadyExistsException();
                }
                Books.Add((Book)obj);
            }

            else
            {
                filePath = Path.Combine(RootDir, ((Person)obj).ID.ToString() + ".xml");
                if (File.Exists(filePath))
                {
                    throw new PathAlreadyExistsException();
                }
                Authors.Add((Person)obj);
            }


            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public void Info()
        {
            for(int i = 0; i < Books.Count; i++)
            {
                Console.WriteLine($"\'{Books[i]}\'");
            }
        }

        public Book Get(string title)
        {
            Book? foundBook = null;
            string[] fileNames = Directory.GetFiles(RootDir);

            foreach(string fileName in fileNames)
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(Book));
                using(TextReader reader = new StreamReader(fileName))
                {
                    try
                    {
                        foundBook = (Book)deserializer.Deserialize(reader);
                    }
                    catch (Exception e)
                    {
                        foundBook = null;
                        continue;
                    }
                    if (foundBook.Title == title)
                        return foundBook;
                    
                }
            }

            return foundBook;
        }

        public bool Delete(Guid id)
        {
            string filePath = Path.Combine(RootDir, id.ToString() + ".xml");
            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException();
                
            }
            File.Delete(filePath);
            return true;
        }

    }

    public class PathAlreadyExistsException : Exception
    {
        public PathAlreadyExistsException() { }
    }
}
