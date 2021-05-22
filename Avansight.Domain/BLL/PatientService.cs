using Avansight.Domain.Entities;
using Avansight.Domain.Services;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Avansight.Domain.BLL
{
    public class PatientService
    {
        public void InsertRecords(List<Patient> patients)
        {
            List<int> rtn = new List<int>();
            List<int> rtn2 = new List<int>();
            new DataAccessService().ExecuteScopedTransaction((conn) =>
            {
                Insertpatient(conn, ref rtn, patients);
                new TreatmentReadingService().InsertTreatmentReading(conn, ref rtn2, rtn);
            });
        }

        public void Insertpatient(SqlConnection conn, ref List<int> rtn, List<Patient> patients)
        {
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Newtonsoft.Json.JsonConvert.SerializeObject(patients));
            var spName = "PatientSet";
            var dyn = new { PatientDetails = dt.AsTableValuedParameter("PatientTableType") };
            new Repository<int>().Query<int>(spName, dyn, conn, ref rtn);
        }

        public List<Patient> GeneratePatientList(SimulatePatientViewModel inputs)
        {
            var GeneratePatientService = new GeneratePatientService();
            return GeneratePatientService.GeratePatientList(inputs.SampleSize, inputs.GenderMale, inputs.GenderFemale, inputs.Age20s, inputs.Age30s, inputs.Age40s, inputs.Age50s, inputs.Age60s);
        }

        public GenderDistribution GetGenderDistribution(List<Patient> patients)
        {
            var genderDeistribution = new GenderDistribution();
            genderDeistribution.MaleCount = patients.Count(e => e.Gender == "Male");
            genderDeistribution.FemaleCount = patients.Count(e => e.Gender == "Female");
            return genderDeistribution;
        }

        public AgeDistribution GetAgeDistribution(List<Patient> patients)
        {
            var ageDeistribution = new AgeDistribution();
            ageDeistribution.Age20s = patients.Count(e => e.Age > 20 && e.Age <= 30);
            ageDeistribution.Age30s = patients.Count(e => e.Age > 30 && e.Age <= 40);
            ageDeistribution.Age40s = patients.Count(e => e.Age > 40 && e.Age <= 50);
            ageDeistribution.Age50s = patients.Count(e => e.Age > 50 && e.Age <= 60);
            ageDeistribution.Age60s = patients.Count(e => e.Age > 60 && e.Age <= 70);
            return ageDeistribution;
        }

        public List<PatientDto> GetUniquePatients(List<TreatmentReading> readings)
        {
            var patientDtos = new List<PatientDto>();
            var counter = 0;
            var byPatient = readings.GroupBy(e => e.PatientId);
            foreach (var record in byPatient)
            {
                counter++;
                var patientDto = new PatientDto();
                patientDto.PatientId = record.FirstOrDefault().PatientId;
                patientDto.DisplayName = "Patient " + counter;
                patientDtos.Add(patientDto);
            }
            return patientDtos;
        }
    }
}