using System;

namespace ConsoleAppDayThree
{
    public class Person
    {
        // property
        //shortcut: prop + tab2, to create properties
        //public string Name { get; set; }
        public DateTime BirthDate { get; private set; }

        // constructor
        public Person(DateTime birthdate)
        {
            BirthDate = birthdate;
        }
      
        //method
        public int Age
        {
            get
            {
                var timespan = DateTime.Today - BirthDate;
                var years = timespan.Days / 365;
                return years;
            }
        }
    }
}
