using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectTravel
{
    class Trip
    {
        private string _destination;
        public string Destination
        {
            get { return _destination; }
            set
            {
                Regex rx = new Regex(@"^[^;]{1,255}$");
                if (!rx.IsMatch(value))
                {
                    throw new InvalidDataException("Invalid value for Destination.");

                }
                _destination = value;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                Regex rx = new Regex(@"^[^;]{1,255}$");
                if (!rx.IsMatch(value))
                {
                    throw new InvalidDataException("Invalid value for Name.");

                }
                _name = value;
            }
        }

        private string _passport;
        public string Passport
        {
            get { return _passport; }
            set
            {
                Regex rx = new Regex(@"^[\s*A-Za-z0-9]{1,9}$");
                if (!rx.IsMatch(value))
                {
                    throw new InvalidDataException("Invalid value for Passport number.");
                }
                _passport = value;
            }
        }

        private string _departure;
        public string Departure
        {
            get { return _departure; }
            set
            {
                DateTime dt;
                if (!DateTime.TryParse(value, out dt))
                {
                    throw new InvalidDataException("Invalid value for date.");
                }
                _departure = value;
            }
        }

        private string _returnDate;
        public string ReturnDate
        {
            get { return _returnDate; }
            set
            {
                DateTime dt;
                if (!DateTime.TryParse(value, out dt))
                {
                    throw new InvalidDataException("Invalid value for date.");
                }
                else if (DateTime.Parse(Departure).Date > dt.Date)
                {
                    throw new InvalidDataException("Return date must not be earlier than departure date.");
                }

                _returnDate = value;
            }
        }

        public Trip(string destination, string name, string passport, string departure, string returnDate)
        {
            Destination = destination;
            Name = name;
            Passport = passport;
            Departure = departure;
            ReturnDate = returnDate;
        }

        public Trip(string dataline)
        {
            string[] data = dataline.Split(';');
            if (data.Length != 5)
            {
                throw new InvalidDataException("Line has invalid value for: \n" + dataline);
            }
            else
            {
                try
                {
                    Destination = data[0];
                }
                catch (InvalidDataException)
                {
                }

                try
                {
                    Name = data[1];
                }
                catch (InvalidDataException)
                {
                }

                try
                {
                    Passport = data[2];
                }
                catch (InvalidDataException)
                {
                }

                DateTime departDate;
                if (!DateTime.TryParse(data[3], out departDate))
                {
                    throw new InvalidDataException("Line has invalid departure date for: \n" + dataline);
                }
                else
                {
                    Departure = departDate.Date.ToString("d");
                }

                DateTime returnDate;
                if (!DateTime.TryParse(data[4], out returnDate))
                {
                    throw new InvalidDataException("Line has invalid return date for: \n" + dataline);
                }
                else if (departDate > returnDate)
                {
                    throw new InvalidDataException("Return date must not be earlier than departure date.");
                }
                else
                {
                    ReturnDate = returnDate.Date.ToString("d");
                }

            }
        }

        public Trip()
        {
        }

        public override string ToString()
        {
            return $"{Destination};{Name};{Passport};{Departure};{ReturnDate}";
        }

    }
}
