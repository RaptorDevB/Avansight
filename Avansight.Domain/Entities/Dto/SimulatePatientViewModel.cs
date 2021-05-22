namespace Avansight.Domain.BLL
{
    public class SimulatePatientViewModel
    {
        public string Status { get; set; }
        public int SampleSize { get; set; }
        public int GenderMale { get; set; }
        public int GenderFemale { get; set; }
        public int Age20s { get; set; }
        public int Age30s { get; set; }
        public int Age40s { get; set; }
        public int Age50s { get; set; }
        public int Age60s { get; set; }
        public AgeDistribution AgeDistribution { get; set; }
        public GenderDistribution GenderDistribution { get; set; }
    }

    public class AgeDistribution
    {
        public int Age20s { get; set; }
        public int Age30s { get; set; }
        public int Age40s { get; set; }
        public int Age50s { get; set; }
        public int Age60s { get; set; }
    }

    public class GenderDistribution
    {
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
    }

}