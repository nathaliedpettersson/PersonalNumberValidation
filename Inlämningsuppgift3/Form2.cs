using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inlämningsuppgift3
{
    public partial class Form2 : Form
    {
        public class Person
        {
            public string firstName;
            public string lastName;
            public string personalNumber;
            public string gender;

            public string Firstname
            {
                get { return firstName; }
                set { firstName = value; }
            }

            public string Lastname
            {
                get { return lastName; }
                set { lastName = value; }
            }

            public string Personalnumber
            {
                get { return personalNumber; }
                set { personalNumber = value; }
            }

            public string Gender
            {
                get { return gender; }
                set { gender = value; }
            }

        }
        public Form2()
        {
            InitializeComponent();
        }
        private void AddNewPerson(string firstName, string lastName, string personalNumber)
        {
            // Skapa en ny instans av klassen Person
            Person newPerson = new Person
            {
                Firstname = firstName,
                Lastname = lastName,
                Personalnumber = personalNumber
            };

            // Validera personnummer och hämta kön
            bool isValid = PersonalNumberValidation(personalNumber);
            Console.WriteLine(personalNumber + isValid);
            bool isMale = GetGender(personalNumber);

            if (!isValid)
            {
                string message = "Vänligen skriv ett giltigt personnummer! Exempel: ÅRMMDDXXXX";
                MessageBox.Show(message);

            }
            else
            {
                // Uppdatera personens kön
                newPerson.Gender = isMale ? "Man" : "Kvinna";

                // Uppdatera i textrutan så att den senaste personens uppgifter syns för användaren
                // Textboxen har för nuvarande bara plats till 3 personer
                textBoxResult.AppendText($"Förnamn: {newPerson.Firstname}" + Environment.NewLine);
                textBoxResult.AppendText($"Efternamn: {newPerson.Lastname}" + Environment.NewLine);
                textBoxResult.AppendText($"Personnummer: {newPerson.Personalnumber}" + Environment.NewLine);
                textBoxResult.AppendText($"Kön: {newPerson.Gender}" + Environment.NewLine);

                textBoxResult.SelectionStart = textBoxResult.Text.Length;
                textBoxResult.ScrollToCaret();
            }
        }
        public bool PersonalNumberValidation(string personalNumber)
        {
            if (personalNumber.Length != 10)
                return false;

            // Luhn-algoritmen/Kontrollsiffran används för att validera personnummer
            int sum = 0;
            for (int i = 0; i < personalNumber.Length; i++)
            {
                int n = (personalNumber[i] - '0');
                if (i % 2 == 0)
                    n = n * 2 > 9 ? n * 2 - 9 : n * 2;
                sum += n;
            }
            return (sum % 10) == 0;  // Om personnumret är delbart med 10 så är det korrekt
        }

        // Kolla näst sista siffran i personnumret för att avgöra kön på personen
        // Uppgiften kräver inte att man hanterar 1900/2000-talet olika men det hade man kunnat lägga till om hela årtal hade använts
        // Ytterligare förbättring av programmet hade varit att kolla att inte ex "månad > 12 && dag > 31" 
        public bool GetGender(string personalNumber)
        {
            int n = int.Parse(personalNumber.Substring(8, 1));

            return ((n % 2) == 1); // Om siffran är udda (true) blir resultatet man, om jämn (false) - kvinna  
        }
        private void validatePerson_Click(object sender, EventArgs e)
        {
            // Hämta värden från textboxarna
            string firstName = textBoxFirstName.Text;
            string lastName = textBoxLastName.Text;
            string personalNumber = textBoxPersonalNumber.Text;

            // Lägg till ny person
            AddNewPerson(firstName, lastName, personalNumber);
        }

    }
    }

