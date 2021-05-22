using Avansight.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Avansight.Domain.BLL
{
    public class GenerateTreatmentReadingService
    {
        public List<TreatmentReading> GetTreatmentReadings(int patientId)
        {
            List<TreatmentReading> treatmentReadings = new List<TreatmentReading>();
            var recodecount = GetRandomAge(1, 10);
            for (int i = 0; i < recodecount; i++)
            {
                treatmentReadings.Add(new TreatmentReading()
                {
                    PatientId = patientId,
                    VisitWeek = "V" + (i + 1),
                    Reading = GetReadings()
                });
            }
            return treatmentReadings;
        }
        public int GetRandomAge(int from, int to)
        {
            Random r = new Random();
            return r.Next(from, to);
        }
        public double GetReadings()
        {
            Random r = new Random();
            int rInt = r.Next(0, 9);
            var r1 = (r.NextDouble() / 1000) + (r.NextDouble() / 100) + (r.NextDouble() / 10) + (r.NextDouble()) + rInt;
            return r1;
        }
    }

}