using Avansight.Domain.Entities;
using Avansight.Domain.Services;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Avansight.Domain.BLL
{
    public class TreatmentReadingService
    {
        public void InsertTreatmentReading(SqlConnection conn, ref List<int> rtn, List<int> patientIdList)
        {
            List<TreatmentReading> treatmentReadings = new List<TreatmentReading>();
            foreach (var id in patientIdList)
            {
                treatmentReadings.AddRange(new GenerateTreatmentReadingService().GetTreatmentReadings(id));
            }
            DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Newtonsoft.Json.JsonConvert.SerializeObject(treatmentReadings));
            var spName = "TreatmentReadingSet";
            var dyn = new { TreatmentReadingDetails = dt.AsTableValuedParameter("TreatmentReadingTableType") };
            new Repository<int>().Query<int>(spName, dyn, conn, ref rtn);
        }

        public List<TreatmentReading> GetTreatmentRecords()
        {
            var spName = "TreatmentReadingsGet";
            return new DataAccessService().Query<TreatmentReading>(spName, null, CommandType.StoredProcedure, null).ToList();
        }

    }
}