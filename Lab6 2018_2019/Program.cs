using System;
using System.Collections.Generic;

namespace p3z
{
    class Program
    {
		static void Main(string[] args)
		{
			Console.WriteLine("======= CZĘŚĆ 1 =======");
			Console.WriteLine("-- Konstruktor i ToString() --");
			CharSet c1 = new CharSet(('a', 1), ('b', 2), ('c', 3), ('d', 4), ('e', 5));
			Console.WriteLine($"c1: {c1}");
			CharSet c2 = new CharSet(('d', 1), ('e', 5), ('f', 6), ('g', 7), ('h', 8), ('i', 9), ('j', 10));
            Console.WriteLine($"c2: {c2}");
            CharSet c3 = new CharSet(('a', 3), ('c', 1), ('f', 9), ('i', 6));
            Console.WriteLine($"c3: {c3}");
            CharSet c4 = new CharSet(('c', 9));
            Console.WriteLine($"c4: {c4}");
            CharSet c5 = new CharSet();
            Console.WriteLine($"c5: {c5}");

            Console.WriteLine();
			Console.WriteLine("======= CZĘŚĆ 2 =======");
            Console.WriteLine("-- Operator + (char) --");
            CharSet c6 = c1 + 'a';
            Console.WriteLine($"c6: {c6}");
            c2 += 'c';
            Console.WriteLine($"c2': {c2}");
            c3 += 'i';
            c3 += 'e';
            Console.WriteLine($"c3': {c3}");
            CharSet c7 = c4 + 'c' + 'c' + 'c';
            Console.WriteLine($"c7: {c7}");

            Console.WriteLine();
            Console.WriteLine("-- Operator + (CharSet) --");
            Console.WriteLine($"c1+c2: {c1 + c2}");
            Console.WriteLine($"c1+c3: {c1 + c3}");
            Console.WriteLine($"c4+c5: {c4 + c5}");

            Console.WriteLine();
            Console.WriteLine("-- Operator * --");
            Console.WriteLine($"c1*c2: {c1 * c2}");
            Console.WriteLine($"c2*c3: {c2 * c3}");
            Console.WriteLine($"c4*c5: {c4 * c5}");

            Console.WriteLine();
            Console.WriteLine("-- Operator ++ --");
            Console.WriteLine($"++c1: {++c1}");
            Console.WriteLine($"++c3: {++c3}");
            Console.WriteLine($"++c5: {++c5}");

            Console.WriteLine();
			Console.WriteLine("======= CZĘŚĆ 3 =======");
			Console.WriteLine("-- Operatory porównywania --");
            CharSet c8 = new CharSet(('c', 8));
            Console.WriteLine($"c8: {c8}");
            CharSet c9 = new CharSet(('c', 9));
            Console.WriteLine($"c9: {c9}");
            CharSet c10 = new CharSet(('e', 6), ('b', 3), ('c', 4), ('a', 2), ('d', 5));
            Console.WriteLine($"c10: {c10}");

            Console.WriteLine();
            Console.WriteLine($"c10 == c1: {c10 == c1}");
            Console.WriteLine($"c4 == c9: {c4 == c9}");
            Console.WriteLine($"++c38 == c9: {++c8 == c9}");
            Console.WriteLine($"c1 != c6: {c1 != c6}");
            Console.WriteLine($"c4 != c9: {c4 != c9}");

            Console.WriteLine($"c1 < c5: {c5 < c1}");
            Console.WriteLine($"c9 + 'c' > c8: {c9 + 'c' > c8}");

            Console.WriteLine();
            Console.WriteLine($"c1 == c6: {c1 == c6}");
            Console.WriteLine($"c4 == c8: {c4 != c8}");
            Console.WriteLine($"c10 != c1: {c10 != c1}");
            Console.WriteLine($"c4 != c9: {c4 != c9}");

            Console.WriteLine($"c1 > c10: {c1 > c10}");
            Console.WriteLine($"c1 < c10: {c1 < c10}");
            Console.WriteLine($"c1 > c5: {c5 > c1}");
            Console.WriteLine($"c8 > c9: {c8 > c9}");
            Console.WriteLine($"c2 > c3: {c2 > c3}");
            Console.WriteLine($"c2 < c3: {c2 < c3}");

            Console.WriteLine();
			Console.WriteLine("======= CZĘŚĆ 4 =======");
			Console.WriteLine("-- Indeksator --");
            Console.WriteLine($"c1['c']: {c1['c']}");
            Console.WriteLine($"c1['j']: {c1['j']}");
            Console.WriteLine($"c5['x']: {c5['x']}");
            c1['c'] = 1;
            Console.WriteLine($"c1['c']: {c1['c']}");
            c1['j'] = 5;
            Console.WriteLine($"c1['j']: {c1['j']}");
            c5['x'] = 10;
            Console.WriteLine($"c5['x']: {c5['x']}");

            Console.WriteLine();
            Console.WriteLine("-- Rzutowanie --");
            string s1 = "aabbxyz%";
            CharSet c11 = s1;
            Console.WriteLine($"c11: {c11}");

            string s2 = "101010100010010110001101110010010111111";
            CharSet c12 = (CharSet)s2;
            Console.WriteLine($"c12: {c12}");

            CharSet c13 = (CharSet)'?';
            Console.WriteLine($"c13: {c13}");

            CharSet c14 = 'x'; //ta linijka powinna powodować błąd kompilacji
        }
    }
}
