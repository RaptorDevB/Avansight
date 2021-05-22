using Avansight.Domain.Services;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Avansight.Domain.BLL
{
    public class PatientService
    {
        public void InsertRecords(SimulatePatientViewModel simulatePatientViewModel)
        {
            List<int> rtn = new List<int>();
            List<int> rtn2 = new List<int>();
            new DataAccessService().ExecuteScopedTransaction((conn) =>
            {
                Insertpatient(conn, ref rtn, simulatePatientViewModel);
                new TreatmentReadingService().InsertTreatmentReading(conn, ref rtn2, rtn);
            });
        }

        public void Insertpatient(SqlConnection conn, ref List<int> rtn, SimulatePatientViewModel inputs)
        {
            var GeneratePatientService = new GeneratePatientService();
            var patients = GeneratePatientService.GeratePatientList(inputs.SampleSize, inputs.GenderMale, inputs.GenderFemale, inputs.Age20s, inputs.Age30s, inputs.Age40s, inputs.Age50s, inputs.Age60s);
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Newtonsoft.Json.JsonConvert.SerializeObject(patients));

            var spName = "PatientSet";
            var dyn = new { PatientDetails = dt.AsTableValuedParameter("PatientTableType") };

            new Repository<int>().insert<int>(spName, dyn, conn, ref rtn);
        }
    }
}