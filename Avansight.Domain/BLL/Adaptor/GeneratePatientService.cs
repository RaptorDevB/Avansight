using Avansight.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Avansight.Domain.BLL
{
    public class GeneratePatientService
    {
        public List<Patient> GeratePatientList(double samplesize, int male, int female, int age1, int age2, int age3, int age4, int age5)
        {
            var patientList = new List<Patient>();
            double unit = samplesize / (male + female);
            patientList.AddRange(GetGenderViseRecords(unit * male, "Male", age1, age2, age3, age4, age5));
            patientList.AddRange(GetGenderViseRecords(unit * female, "Female", age1, age2, age3, age4, age5));
            return patientList;
        }
        private List<Patient> GetGenderViseRecords(double samplesize, string gender, int age1, int age2, int age3, int age4, int age5)
        {
            var patientList = new List<Patient>();
            double unit = samplesize / (age1 + age2 + age3 + age4 + age5);
            patientList.AddRange(GetAgeViseRecords(21, gender, Convert.ToInt32(unit * age1)));
            patientList.AddRange(GetAgeViseRecords(31, gender, Convert.ToInt32(unit * age2)));
            patientList.AddRange(GetAgeViseRecords(41, gender, Convert.ToInt32(unit * age3)));
            patientList.AddRange(GetAgeViseRecords(51, gender, Convert.ToInt32(unit * age4)));
            patientList.AddRange(GetAgeViseRecords(61, gender, Convert.ToInt32(unit * age5)));
            return patientList;
        }
        private List<Patient> GetAgeViseRecords(int age, string gender, int unit)
        {
            var patientList = new List<Patient>();
            for (int i = 0; i < unit; i++)
            {
                int agerandom = GetRandomAge(age, age + 9);
                patientList.Add(new Patient()
                {
                    Age = agerandom,
                    Gender = gender
                });
            }
            return patientList;
        }
        public int GetRandomAge(int from, int to)
        {
            Random r = new Random();
            return r.Next(from, to);
        }
    }

}