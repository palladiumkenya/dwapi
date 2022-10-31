namespace Dwapi.ExtractsManagement.Infrastructure.Repository.Hts.TempExtracts
{
    public class QueryUtil
    {
        public static string Linkage => QueryBuilder("TempHtsClientsLinkageExtracts");
        public static string Tests => QueryBuilder("TempHtsClientTestsExtracts");
        public static string Tracing => QueryBuilder("TempHtsClientTracingExtracts");
        public static string Pns => QueryBuilder("TempHtsPartnerNotificationServicesExtracts");
        public static string PartnerTracing => QueryBuilder("TempHtsPartnerTracingExtracts");
        public static string Kits => QueryBuilder("TempHtsTestKitsExtracts");
        public static string Eligibility => QueryBuilder("TempHtsEligibilityExtracts");
        public static string RiskScores => QueryBuilder("TempHtsRiskScoresExtracts");


        public static string LinkageCount => QueryBuilder("TempHtsClientsLinkageExtracts", true);
        public static string TestsCount => QueryBuilder("TempHtsClientTestsExtracts", true);
        public static string TracingCount => QueryBuilder("TempHtsClientTracingExtracts", true);
        public static string PnsCount => QueryBuilder("TempHtsPartnerNotificationServicesExtracts", true);
        public static string PartnerTracingCount => QueryBuilder("TempHtsPartnerTracingExtracts", true);
        public static string KitsCount => QueryBuilder("TempHtsTestKitsExtracts", true);
        public static string EligibilityCount => QueryBuilder("TempHtsEligibilityExtracts", true);
        public static string RiskScoresCount => QueryBuilder("TempHtsRiskScoresExtracts", true);



        public static string QueryBuilder(string extract, bool count = false)
        {
            var select = count ? "SELECT Count(s.Id) " : "SELECT s.* ";

            var from = $@"
                FROM {extract} s
                         INNER JOIN HtsClientsExtracts p ON s.PatientPK = p.PatientPK AND s.SiteCode = p.SiteCode
                WHERE ErrorType = 0";

            var query = $"{select} {from}";

            return query;
        }
    }
}
