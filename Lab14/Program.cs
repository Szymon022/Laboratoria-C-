using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    // https://grouplens.org/datasets/movielens/1m/
    public class Program
    {
        public static void Main(string[] args)
        {
			// Dostarczona jest baza danych z trzema tabelami: database.Movies, database.Users, database.Ratings
			// Dokładne informacje o dostępnych kolumnach można znaleźć przeglądając klasy Movie, User, RatingEntry
			// W tabeli Users pole Age jest raczej identyfikatorem pewnego zakresu wieku, niż wiekiem samym w sobie, np. 1 oznacza przedział 1-18
			// Każdy film ma unikatowy numer id MovieID, każdy użytkownik ma unikatowy numer id w UserID
			// Obie te tabele (Movies, Users) są połączone przez Ratings: pojedynczy głos na film jest oddawany przez jednego użytkownika na jeden film
            DatabaseMovies database = DatabaseMovies.GetInstance("movies.csv", "users.csv", "ratings.csv");

            // pytanie 1

            // Policz ile jest użytkowników w podanych grupach wiekowych, posortuj (rosnąco) po wieku


            // Miejsce na Twoje rozwiązanie
            var seq = from user in database.Users
                      group user by new { age = user.Age, count = (from u in database.Users where u.Age == user.Age select u).Count() }
                      into rUsers
                      orderby rUsers.Key.age ascending
                      select rUsers;

            foreach(var user in seq)
            {
                Console.WriteLine($"{user.Key.age} : {user.Key.count}");
            }


            Console.WriteLine($"--------------");

            // pytanie 2

            // Jaki jest minimalny rok oglądanego filmu w poszczególnych grupach wyznaczanych przez wiek i płeć - wyniki posortuj rosnąco po minimalnym roku

            // Miejsce na Twoje rozwiązanie
            var seq2 = from user in database.Users
                       group user by new
                       {
                           age = user.Age,
                           gender = user.Gender,
                           minYear = (from rating in database.Ratings
                                      where rating.UserID == user.UserID
                                      group rating by new { mYear = (from movie in database.Movies where movie.MovieID == rating.UserID select movie).Min<>()} )
                       }
                       into rUser
                       orderby rUser.Key.minYear ascending
                       select rUser;

            foreach(var x in seq2)
            {
                Console.WriteLine($"{x.Key.age} {x.Key.gender} {x.Key.minYear}");
            }

            Console.WriteLine($"--------------");

            // pytanie 3

            // Policz liczbę wystąpień każdego gatunku filmowego 
            // (każdy gatunek liczymy pojedynczo, więc dla "Horror|Comedy" mamy +1 dla gatunku Horror i +1 dla gatunku Comedy)
            // posortuj rosnąco po gatunku
            //var seq3 = from movie in database.Movies 
            //           group movie by new 
            //           { 
            //               genere = (from m in database.Movies ) 
            //           }

            // Miejsce na Twoje rozwiązanie

            Console.WriteLine($"--------------");

            // pytanie 4

			// Definiujemy remaki pojedynczego filmu jako zbiór filmów których tytuł jest taki sam (ale lata produkcji są różne)
			// Twój wynik powinien zawierać wszystkie filmy, które są remakami (wszystkie filmy, dla których możemy znaleźć co najmniej jeden inny film z tym samym tytułem)
            // Wynik powinien być posortowany rosnąco po roku produkcji.
			// Wynik powinien zawierać pierwsze 14 filmów

            // Miejsce na Twoje rozwiązanie

            Console.WriteLine($"--------------");


            // pytanie 5

            // Posortuj (malejąco) filmy po średniej ocenie głosów
			// Weź pod uwagę jedynie filmy, na które oddano 100 głosów lub więcej
			// Wynik powinien zawierać pierwsze 20 filmów

            // Miejsce na Twoje rozwiązanie

            Console.WriteLine($"--------------");

        }
    }
}