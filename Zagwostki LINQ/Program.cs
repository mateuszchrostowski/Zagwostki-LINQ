using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {


            CultureInfo culture = new CultureInfo("en-US");

            var lines = System.IO.File.ReadAllLines(@"..\..\..\owid-covid-data.csv");



            var date_start = new DateTime(2021, 1, 1);
            var date_end = new DateTime(2021, 12, 31);

            Console.WriteLine(DateTime.Now.ToString(CultureInfo.CreateSpecificCulture("en-US")));




            Console.WriteLine();


            var covids = new List<Covid>();

            for (int i = 1; i < lines.Length; i++)
            {
                Covid cv = new Covid(lines[i]);

                covids.Add(cv);
            }



            Console.WriteLine("miesięczne sumaryczne zachorowania w Polsce: \n");

            /*.Where(x => x != null && x.Country == "Poland" && !String.IsNullOrEmpty(x.Deaths) && (x.Date >= date_start && x.Date <= date_end)).ToList();*/

            var zachorowania = covids.Where(x => x != null && x.Country == "Poland").ToList();

            zachorowania.ForEach(x => Console.WriteLine(x));







            Console.ReadKey();


        }
        public class Covid
        {

            private double newCases;

            public string Continent { get; set; }
            public string Country { get; set; }
            public DateTime Date { get; set; }
            public double NewCases { get => newCases; set => newCases = value; }
            public string Deaths { get; set; }

            internal Covid(string row)
            {
                string[] columns = row.Split(',');


                Continent = columns[1];
                Country = columns[2];
                Date = DateTime.Parse(columns[3]);



                if (!double.TryParse(columns[5], out newCases)) NewCases = 0;






                //NewCases = columns[5];



                Deaths = columns[7];


            }


            public override string ToString()
            {
                string str = $"{Continent} {Country} {Date} {NewCases} {Deaths}";
                return str;
            }
        }
    }
}


