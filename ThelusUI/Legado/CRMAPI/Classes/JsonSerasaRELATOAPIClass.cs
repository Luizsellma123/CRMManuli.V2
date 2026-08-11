using System.Collections.Generic;
using Newtonsoft.Json;

namespace CRMAPI.Classes
{
    public class JsonSerasaRELATOAPIClass
    {
        [JsonProperty("reports")]
        public List<Report> reports { get; set; }

        [JsonProperty("optionalFeatures")]
        public OptionalFeatures optionalFeatures { get; set; }

        public class Address
        {
            [JsonProperty("addressLine")]
            public string addressLine { get; set; }
            [JsonProperty("zipCode")]
            public string zipCode { get; set; }
            [JsonProperty("district")]
            public string district { get; set; }
            [JsonProperty("city")]
            public string city { get; set; }
            [JsonProperty("state")]
            public string state { get; set; }
        }

        public class Administrator
        {
            [JsonProperty("kindPerson")]
            public string kindPerson { get; set; }
            [JsonProperty("document")]
            public string document { get; set; }
            [JsonProperty("documentBranch")]
            public string documentBranch { get; set; }
            [JsonProperty("documentDigit")]
            public string documentDigit { get; set; }
            [JsonProperty("documentSequence")]
            public string documentSequence { get; set; }
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("office")]
            public string office { get; set; }
            [JsonProperty("maritalStatus")]
            public string maritalStatus { get; set; }
            [JsonProperty("startDateTerm")]
            public string startDateTerm { get; set; }
            [JsonProperty("endDateTerm")]
            public string endDateTerm { get; set; }
            [JsonProperty("entryDate")]
            public string entryDate { get; set; }
            [JsonProperty("nationality")]
            public string nationality { get; set; }
            [JsonProperty("restrictionIndicator")]
            public string restrictionIndicator { get; set; }
            [JsonProperty("relationship")]
            public string relationship { get; set; }
            [JsonProperty("birthDate")]
            public string birthDate { get; set; }
            [JsonProperty("updateDate")]
            public string updateDate { get; set; }
            [JsonProperty("address")]
            public Address address { get; set; }
            [JsonProperty("phone")]
            public Phone phone { get; set; }
            [JsonProperty("debts")]
            public List<Debt> debts { get; set; }
        }

        public class AdvancedCommercialPaymentHistory
        {
            [JsonProperty("paymentHistory")]
            public PaymentHistory paymentHistory { get; set; }
            [JsonProperty("mainSuppliers")]
            public MainSuppliers mainSuppliers { get; set; }
            [JsonProperty("relationshipSuppliersPeriods")]
            public RelationshipSuppliersPeriods relationshipSuppliersPeriods { get; set; }
            [JsonProperty("evolutionCommitmentsSuppliers")]
            public EvolutionCommitmentsSuppliers evolutionCommitmentsSuppliers { get; set; }
            [JsonProperty("businessReferences")]
            public BusinessReferences businessReferences { get; set; }
        }

        public class AverageDelayPeriod
        {
            [JsonProperty("periodList")]
            public List<periodList> periodList { get; set; }
            [JsonProperty("summary")]
            public Summary summary { get; set; }
        }

        public class Bankrupts
        {
            [JsonProperty("bankruptsResponse")]
            public List<BankruptsResponse> bankruptsResponse { get; set; }
            [JsonProperty("summary")]
            public Summary summary { get; set; }
        }

        public class BankruptsResponse
        {
            [JsonProperty("eventDate")]
            public string eventDate { get; set; }
            [JsonProperty("origin")]
            public string origin { get; set; }
            [JsonProperty("city")]
            public string city { get; set; }
            [JsonProperty("state")]
            public string state { get; set; }
            [JsonProperty("varaCourt")]
            public string varaCourt { get; set; }
            [JsonProperty("eventType")]
            public string eventType { get; set; }
        }

        public class BusinessReferences
        {
            [JsonProperty("lastUpdateDate")]
            public string lastUpdateDate { get; set; }
            [JsonProperty("businessReferencesList")]
            public List<BusinessReferencesList> businessReferencesList { get; set; }
        }

        public class BusinessReferencesList
        {
            [JsonProperty("businessDescription")]
            public string businessDescription { get; set; }
            [JsonProperty("yearPotentialDate")]
            public string yearPotentialDate { get; set; }
            [JsonProperty("monthPotentialDate")]
            public string monthPotentialDate { get; set; }
            [JsonProperty("potentialValueRangeCode")]
            public string potentialValueRangeCode { get; set; }
            [JsonProperty("potentialValueRangeDescription")]
            public string potentialValueRangeDescription { get; set; }
            [JsonProperty("potentialValueFrom")]
            public string potentialValueFrom { get; set; }
            [JsonProperty("potentialValueTo")]
            public string potentialValueTo { get; set; }
            [JsonProperty("potentialMidrangeCode")]
            public string potentialMidrangeCode { get; set; }
            [JsonProperty("potentialMidrangeDescription")]
            public string potentialMidrangeDescription { get; set; }
            [JsonProperty("potentialMidrangeValueFrom")]
            public string potentialMidrangeValueFrom { get; set; }
            [JsonProperty("potentialMidrangeValueTo")]
            public string potentialMidrangeValueTo { get; set; }
        }

        public class Check
        {
            [JsonProperty("checkResponse")]
            public List<CheckResponse> checkResponse { get; set; }
            [JsonProperty("summary")]
            public Summary summary { get; set; }
        }

        public class CheckResponse
        {
            [JsonProperty("occurrenceDate")]
            public string occurrenceDate { get; set; }
            [JsonProperty("alinea")]
            public string alinea { get; set; }
            [JsonProperty("bankName")]
            public string bankName { get; set; }
            [JsonProperty("bankAgencyId")]
            public string bankAgencyId { get; set; }
            [JsonProperty("checkNumber")]
            public string checkNumber { get; set; }
            [JsonProperty("checkCount")]
            public string checkCount { get; set; }
            [JsonProperty("city")]
            public string city { get; set; }
            [JsonProperty("federalUnit")]
            public string federalUnit { get; set; }
        }

        public class CollectionRecords
        {
            [JsonProperty("collectionRecordsResponse")]
            public List<CollectionRecordsResponse> collectionRecordsResponse { get; set; }
            [JsonProperty("summary")]
            public Summary summary { get; set; }
        }

        public class CollectionRecordsResponse
        {
            [JsonProperty("occurrenceDate")]
            public string occurrenceDate { get; set; }
            [JsonProperty("legalNatureId")]
            public string legalNatureId { get; set; }
            [JsonProperty("legalNature")]
            public string legalNature { get; set; }
            [JsonProperty("contractId")]
            public string contractId { get; set; }
            [JsonProperty("creditorName")]
            public string creditorName { get; set; }
            [JsonProperty("amount")]
            public string amount { get; set; }
            [JsonProperty("Estado")]
            public string Estado { get; set; }
            [JsonProperty("principal")]
            public string principal { get; set; }
            [JsonProperty("federalUnit")]
            public string federalUnit { get; set; }
            [JsonProperty("city")]
            public string city { get; set; }
            [JsonProperty("dispute")]
            public Dispute dispute { get; set; }
        }

        public class Debt
        {
            [JsonProperty("debtType")]
            public string debtType { get; set; }
            [JsonProperty("summary")]
            public Summary summary { get; set; }
        }

        public class Dispute
        {
            [JsonProperty("disputeIndicativeFlag")]
            public string disputeIndicativeFlag { get; set; }
        }

        public class EvolutionCommitmentsSuppliers
        {
            [JsonProperty("lastUpdateDate")]
            public string lastUpdateDate { get; set; }
            [JsonProperty("evolutionCommitmentsSuppliersList")]
            public List<EvolutionCommitmentsSuppliersList> evolutionCommitmentsSuppliersList { get; set; }
            [JsonProperty("summary")]
            public Summary summary { get; set; }
        }

        public class EvolutionCommitmentsSuppliersList
        {
            [JsonProperty("yearCommitment")]
            public string yearCommitment { get; set; }
            [JsonProperty("monthCommitment")]
            public string monthCommitment { get; set; }
            [JsonProperty("descriptionMonthCommitment")]
            public string descriptionMonthCommitment { get; set; }
            [JsonProperty("trackCodeToExpire")]
            public string trackCodeToExpire { get; set; }
            [JsonProperty("trackDescriptionToExpire")]
            public string trackDescriptionToExpire { get; set; }
            [JsonProperty("valueCommitmentsDueFrom")]
            public string valueCommitmentsDueFrom { get; set; }
            [JsonProperty("valueCommitmentsDueTo")]
            public string valueCommitmentsDueTo { get; set; }
            [JsonProperty("totalMonthRangeCode")]
            public string totalMonthRangeCode { get; set; }
            [JsonProperty("totalMonthRangeDescription")]
            public string totalMonthRangeDescription { get; set; }
            [JsonProperty("totalMonthlyRangeValueFrom")]
            public string totalMonthlyRangeValueFrom { get; set; }
            [JsonProperty("totalMonthlyRangeValueTo")]
            public string totalMonthlyRangeValueTo { get; set; }
            [JsonProperty("segmentInformation")]
            public string segmentInformation { get; set; }
        }

        public class Facts
        {
            [JsonProperty("judgementFilings")]
            public JudgementFilings judgementFilings { get; set; }
            [JsonProperty("bankrupts")]
            public Bankrupts bankrupts { get; set; }
            [JsonProperty("inquiryCompanyResponse")]
            public InquiryCompanyResponse inquiryCompanyResponse { get; set; }
        }

        public class Historical
        {
            [JsonProperty("inquiryDate")]
            public string inquiryDate { get; set; }
            [JsonProperty("occurrences")]
            public string occurrences { get; set; }
        }

        public class IdentificationReport
        {
            [JsonProperty("updateDate")]
            public string updateDate { get; set; }
            [JsonProperty("statusCode")]
            public string statusCode { get; set; }
            [JsonProperty("documentNumber")]
            public string documentNumber { get; set; }
            [JsonProperty("companyName")]
            public string companyName { get; set; }
            [JsonProperty("companyAlias")]
            public string companyAlias { get; set; }
            [JsonProperty("address")]
            public Address address { get; set; }
            [JsonProperty("phone")]
            public Phone phone { get; set; }
            [JsonProperty("companyUrl")]
            public string companyUrl { get; set; }
            [JsonProperty("partnership")]
            public string partnership { get; set; }
            [JsonProperty("companyRegister")]
            public string companyRegister { get; set; }
            [JsonProperty("companyRegisterDate")]
            public string companyRegisterDate { get; set; }
            [JsonProperty("companyFoundation")]
            public string companyFoundation { get; set; }
            [JsonProperty("numberEmployees")]
            public string numberEmployees { get; set; }
            [JsonProperty("taxOption")]
            public string taxOption { get; set; }
            [JsonProperty("economicActivity")]
            public string economicActivity { get; set; }
            [JsonProperty("importPurchases")]
            public string importPurchases { get; set; }
            [JsonProperty("exportSales")]
            public string exportSales { get; set; }
            [JsonProperty("cnae")]
            public string cnae { get; set; }
            [JsonProperty("branchOffices")]
            public string branchOffices { get; set; }
            [JsonProperty("serasaActiveCode")]
            public string serasaActiveCode { get; set; }
            [JsonProperty("nireNumber")]
            public string nireNumber { get; set; }
            [JsonProperty("predecessorList")]
            public List<PredecessorList> predecessorList { get; set; }
            [JsonProperty("legalNatureCode")]
            public string legalNatureCode { get; set; }
            [JsonProperty("stateRegistration")]
            public string stateRegistration { get; set; }
            [JsonProperty("statusRegistration")]
            public string statusRegistration { get; set; }
        }

        public class InquiryCompanyResponse
        {
            [JsonProperty("results")]
            public List<Result> results { get; set; }
            [JsonProperty("quantity")]
            public Quantity quantity { get; set; }
        }

        public class JudgementFilings
        {
            [JsonProperty("judgementFilingsResponse")]
            public List<JudgementFilingsResponse> judgementFilingsResponse { get; set; }
            [JsonProperty("summary")]
            public Summary summary { get; set; }
        }

        public class JudgementFilingsResponse
        {
            [JsonProperty("occurrenceDate")]
            public string occurrenceDate { get; set; }
            [JsonProperty("legalNatureId")]
            public string legalNatureId { get; set; }
            [JsonProperty("legalNature")]
            public string legalNature { get; set; }
            [JsonProperty("amount")]
            public string amount { get; set; }
            [JsonProperty("distributor")]
            public string distributor { get; set; }
            [JsonProperty("civilCourt")]
            public string civilCourt { get; set; }
            [JsonProperty("city")]
            public string city { get; set; }
            [JsonProperty("state")]
            public string state { get; set; }
            [JsonProperty("principal")]
            public string principal { get; set; }
        }

        public class MainSuppliers
        {
            [JsonProperty("lastUpdateDate")]
            public string lastUpdateDate { get; set; }
            [JsonProperty("mainSuppliersList")]
            public List<MainSuppliersList> mainSuppliersList { get; set; }
        }

        public class MainSuppliersList
        {
            [JsonProperty("supplierName")]
            public string supplierName { get; set; }
            [JsonProperty("supplierDocument")]
            public string supplierDocument { get; set; }
        }

        public class Month
        {
            [JsonProperty("month")]
            public string month { get; set; }
            [JsonProperty("periodList")]
            public List<periodList> periodList { get; set; }
        }

        public class MonthDetail
        {
            [JsonProperty("months")]
            public List<Month> months { get; set; }
            [JsonProperty("summary")]
            public Summary summary { get; set; }
        }

        public class NegativeData
        {
            [JsonProperty("pefin")]
            public Pefin pefin { get; set; }
            [JsonProperty("refin")]
            public Refin refin { get; set; }
            [JsonProperty("collectionRecords")]
            public CollectionRecords collectionRecords { get; set; }
            [JsonProperty("check")]
            public Check check { get; set; }
            [JsonProperty("notary")]
            public Notary notary { get; set; }
            [JsonProperty("facts")]
            public Facts facts { get; set; }
            [JsonProperty("bankrupts")]
            public Bankrupts bankrupts { get; set; }
        }

        public class NegativeSummary
        {
        }

        public class Notary
        {
            [JsonProperty("notaryResponse")]
            public List<NotaryResponse> notaryResponse { get; set; }
            [JsonProperty("summary")]
            public Summary summary { get; set; }
        }

        public class NotaryResponse
        {
            [JsonProperty("occurrenceDate")]
            public string occurrenceDate { get; set; }
            [JsonProperty("amount")]
            public string amount { get; set; }
            [JsonProperty("officeNumber")]
            public string officeNumber { get; set; }
            [JsonProperty("city")]
            public string city { get; set; }
            [JsonProperty("federalUnit")]
            public string federalUnit { get; set; }
            [JsonProperty("dispute")]
            public Dispute dispute { get; set; }
        }

        public class OptionalFeatures
        {
            [JsonProperty("qsaCompleteReport")]
            public QsaCompleteReport qsaCompleteReport { get; set; }
            [JsonProperty("advancedCommercialPaymentHistory")]
            public AdvancedCommercialPaymentHistory advancedCommercialPaymentHistory { get; set; }
        }

        public class Partner
        {
            [JsonProperty("kindPerson")]
            public string kindPerson { get; set; }
            [JsonProperty("document")]
            public string document { get; set; }
            [JsonProperty("documentBranch")]
            public string documentBranch { get; set; }
            [JsonProperty("documentDigit")]
            public string documentDigit { get; set; }
            [JsonProperty("documentSequence")]
            public string documentSequence { get; set; }
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("entryDate")]
            public string entryDate { get; set; }
            [JsonProperty("nationality")]
            public string nationality { get; set; }
            [JsonProperty("percentageVotingCapital")]
            public string percentageVotingCapital { get; set; }
            [JsonProperty("percentageCapital")]
            public string percentageCapital { get; set; }
            [JsonProperty("restrictionIndicator")]
            public string restrictionIndicator { get; set; }
            [JsonProperty("relationship")]
            public string relationship { get; set; }
            [JsonProperty("foundationDate")]
            public string foundationDate { get; set; }
            [JsonProperty("idNumber")]
            public string idNumber { get; set; }
            [JsonProperty("birthDate")]
            public string birthDate { get; set; }
            [JsonProperty("address")]
            public Address address { get; set; }
            [JsonProperty("phone")]
            public Phone phone { get; set; }
            [JsonProperty("debts")]
            public List<Debt> debts { get; set; }
        }

        public class PaymentHistory
        {
            [JsonProperty("titlesQuantity")]
            public List<TitlesQuantity> titlesQuantity { get; set; }
            [JsonProperty("monthDetail")]
            public MonthDetail monthDetail { get; set; }
            [JsonProperty("averageDelayPeriod")]
            public AverageDelayPeriod averageDelayPeriod { get; set; }
        }

        public class Pefin
        {
            [JsonProperty("pefinResponse")]
            public List<PefinResponse> pefinResponse { get; set; }
            [JsonProperty("summary")]
            public Summary summary { get; set; }
        }

        public class PefinResponse
        {
            [JsonProperty("occurrenceDate")]
            public string occurrenceDate { get; set; }
            [JsonProperty("legalNatureId")]
            public string legalNatureId { get; set; }
            [JsonProperty("legalNature")]
            public string legalNature { get; set; }
            [JsonProperty("contractId")]
            public string contractId { get; set; }
            [JsonProperty("creditorName")]
            public string creditorName { get; set; }
            [JsonProperty("amount")]
            public string amount { get; set; }
            [JsonProperty("federalUnit")]
            public string federalUnit { get; set; }
            [JsonProperty("principal")]
            public string principal { get; set; }
            [JsonProperty("city")]
            public string city { get; set; }
            [JsonProperty("dispute")]
            public Dispute dispute { get; set; }
        }

        public class Period
        {
            [JsonProperty("periodDescription")]
            public string periodDescription { get; set; }
            [JsonProperty("totalValueRangeCode")]
            public string totalValueRangeCode { get; set; }
            [JsonProperty("totalValueRangeDescription")]
            public string totalValueRangeDescription { get; set; }
            [JsonProperty("totalValueFrom")]
            public string totalValueFrom { get; set; }
            [JsonProperty("totalValueTo")]
            public string totalValueTo { get; set; }
            [JsonProperty("averageValueRangeCode")]
            public string averageValueRangeCode { get; set; }
            [JsonProperty("averageValueRangeDescription")]
            public string averageValueRangeDescription { get; set; }
            [JsonProperty("percentageValueFrom")]
            public string percentageValueFrom { get; set; }
            [JsonProperty("percentageValueTo")]
            public string percentageValueTo { get; set; }
            [JsonProperty("averagePaymentDelayPeriodRangeValueFrom")]
            public string averagePaymentDelayPeriodRangeValueFrom { get; set; }
            [JsonProperty("averagePaymentDelayPeriodRangeValueTo")]
            public string averagePaymentDelayPeriodRangeValueTo { get; set; }
            [JsonProperty("historicalAverageRangeFrom")]
            public string historicalAverageRangeFrom { get; set; }
            [JsonProperty("historicalAverageRangeTo")]
            public string historicalAverageRangeTo { get; set; }
            [JsonProperty("originCode")]
            public string originCode { get; set; }
        }

        public class periodList
        {
            [JsonProperty("period")]
            public string period { get; set; }
            [JsonProperty("averageDelayDaysFrom")]
            public string averageDelayDaysFrom { get; set; }
            [JsonProperty("averageDelayDaysTo")]
            public string averageDelayDaysTo { get; set; }

            [JsonProperty("rangeCode")]
            public string rangeCode { get; set; }
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("range")]
            public string range { get; set; }
            [JsonProperty("rangeValueFrom")]
            public string rangeValueFrom { get; set; }
            [JsonProperty("rangeValueTo")]
            public string rangeValueTo { get; set; }
            [JsonProperty("percentage")]
            public string percentage { get; set; }
            [JsonProperty("percentageFrom")]
            public string percentageFrom { get; set; }
            [JsonProperty("percentageTo")]
            public string percentageTo { get; set; }
        }

        public class Phone
        {
            [JsonProperty("areaCode")]
            public string areaCode { get; set; }
            [JsonProperty("phoneNumber")]
            public string phoneNumber { get; set; }
        }

        public class PredecessorList
        {
            [JsonProperty("predecessorName")]
            public string predecessorName { get; set; }
            [JsonProperty("predecessorDate")]
            public string predecessorDate { get; set; }
        }
     
        public class QsaCompleteReport
        {
            [JsonProperty("shareCapital")]
            public ShareCapital shareCapital { get; set; }
            [JsonProperty("partners")]
            public List<Partner> partners { get; set; }
            [JsonProperty("administrators")]
            public List<Administrator> administrators { get; set; }
        }

        public class Quantity
        {
            [JsonProperty("actual")]
            public string actual { get; set; }
            [JsonProperty("historical")]
            public List<Historical> historical { get; set; }
        }

        public class Refin
        {
            [JsonProperty("refinResponse")]
            public List<RefinResponse> refinResponse { get; set; }
            [JsonProperty("summary")]
            public Summary summary { get; set; }
        }

        public class RefinResponse
        {
            [JsonProperty("occurrenceDate")]
            public string occurrenceDate { get; set; }
            [JsonProperty("legalNatureId")]
            public string legalNatureId { get; set; }
            [JsonProperty("legalNature")]
            public string legalNature { get; set; }
            [JsonProperty("contractId")]
            public string contractId { get; set; }
            [JsonProperty("creditorName")]
            public string creditorName { get; set; }
            [JsonProperty("federalUnit")]
            public string federalUnit { get; set; }
            [JsonProperty("amount")]
            public string amount { get; set; }
            [JsonProperty("principal")]
            public string principal { get; set; }
            [JsonProperty("city")]
            public string city { get; set; }
            [JsonProperty("dispute")]
            public Dispute dispute { get; set; }
        }

        public class RelationshipSuppliersPeriodList
        {
            [JsonProperty("relationshipPeriodDescription")]
            public string relationshipPeriodDescription { get; set; }
            [JsonProperty("relationshipSourceQuantity")]
            public string relationshipSourceQuantity { get; set; }
        }

        public class RelationshipSuppliersPeriods
        {
            [JsonProperty("lastUpdateDate")]
            public string lastUpdateDate { get; set; }
            [JsonProperty("relationshipSuppliersPeriodList")]
            public List<RelationshipSuppliersPeriodList> relationshipSuppliersPeriodList { get; set; }
            [JsonProperty("summary")]
            public Summary summary { get; set; }
        }

        public class Report
        {
            [JsonProperty("reportName")]
            public string reportName { get; set; }
            [JsonProperty("identificationReport")]
            public IdentificationReport identificationReport { get; set; }
            [JsonProperty("negativeData")]
            public NegativeData negativeData { get; set; }
            [JsonProperty("facts")]
            public Facts facts { get; set; }
            [JsonProperty("negativeSummary")]
            public NegativeSummary negativeSummary { get; set; }
        }

        public class Result
        {
            [JsonProperty("occurrenceDate")]
            public string occurrenceDate { get; set; }
            [JsonProperty("companyName")]
            public string companyName { get; set; }
            [JsonProperty("companyDocumentId")]
            public string companyDocumentId { get; set; }
            [JsonProperty("daysQuantity")]
            public string daysQuantity { get; set; }
        }

        public class ShareCapital
        {
            [JsonProperty("capitalValue")]
            public string capitalValue { get; set; }
            [JsonProperty("realizedCapitalValue")]
            public string realizedCapitalValue { get; set; }
            [JsonProperty("origin")]
            public string origin { get; set; }
            [JsonProperty("nature")]
            public string nature { get; set; }
            [JsonProperty("updateDate")]
            public string updateDate { get; set; }
            [JsonProperty("control")]
            public string control { get; set; }
        }
        
        public class Summary
        {
            [JsonProperty("firstOccurrence")]
            public string firstOccurrence { get; set; }
            [JsonProperty("lastOccurrence")]
            public string lastOccurrence { get; set; }
            [JsonProperty("count")]
            public string count { get; set; }
            [JsonProperty("balance")]
            public string balance { get; set; }
            [JsonProperty("punctual")]
            public Period punctual { get; set; }
            [JsonProperty("period8To15")]
            public Period period8To15 { get; set; }
            [JsonProperty("period16To30")]
            public Period period16To30 { get; set; }
            [JsonProperty("period31To60")]
            public Period period31To60 { get; set; }
            [JsonProperty("periodGT60")]
            public Period periodGT60 { get; set; }
            [JsonProperty("spotPayment")]
            public Period spotPayment { get; set; }
            [JsonProperty("total")]
            public Total total { get; set; }
            [JsonProperty("averageDelayDaysFrom")]
            public string averageDelayDaysFrom { get; set; }
            [JsonProperty("averageDelayDaysTo")]
            public string averageDelayDaysTo { get; set; }
            [JsonProperty("sourcesTotal")]
            public string sourcesTotal { get; set; }
            [JsonProperty("paymentHistorySources")]
            public string paymentHistorySources { get; set; }
            [JsonProperty("paymentHistoryValuesSources")]
            public string paymentHistoryValuesSources { get; set; }
            [JsonProperty("evolutionCommitmentsSources")]
            public string evolutionCommitmentsSources { get; set; }
            [JsonProperty("businessReferencesSources")]
            public string businessReferencesSources { get; set; }
            [JsonProperty("spotPaymentBusinessReferencesSources")]
            public string spotPaymentBusinessReferencesSources { get; set; }
        }

        public class TitlesQuantity
        {
            [JsonProperty("rangeCode")]
            public string rangeCode { get; set; }
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("range")]
            public string range { get; set; }
            [JsonProperty("rangeValueFrom")]
            public string rangeValueFrom { get; set; }
            [JsonProperty("rangeValueTo")]
            public string rangeValueTo { get; set; }
            [JsonProperty("percentage")]
            public string percentage { get; set; }
            [JsonProperty("percentageFrom")]
            public string percentageFrom { get; set; }
            [JsonProperty("percentageTo")]
            public string percentageTo { get; set; }
        }

        public class Total
        {
            [JsonProperty("periodDescription")]
            public string periodDescription { get; set; }
            [JsonProperty("totalValueRangeCode")]
            public string totalValueRangeCode { get; set; }
            [JsonProperty("totalValueRangeDescription")]
            public string totalValueRangeDescription { get; set; }
            [JsonProperty("totalValueFrom")]
            public string totalValueFrom { get; set; }
            [JsonProperty("totalValueTo")]
            public string totalValueTo { get; set; }
            [JsonProperty("averageValueRangeCode")]
            public string averageValueRangeCode { get; set; }
            [JsonProperty("averageValueRangeDescription")]
            public string averageValueRangeDescription { get; set; }
            [JsonProperty("percentageValueFrom")]
            public string percentageValueFrom { get; set; }
            [JsonProperty("percentageValueTo")]
            public string percentageValueTo { get; set; }
            [JsonProperty("averagePaymentDelayPeriodRangeValueFrom")]
            public string averagePaymentDelayPeriodRangeValueFrom { get; set; }
            [JsonProperty("averagePaymentDelayPeriodRangeValueTo")]
            public string averagePaymentDelayPeriodRangeValueTo { get; set; }
            [JsonProperty("historicalAverageRangeFrom")]
            public string historicalAverageRangeFrom { get; set; }
            [JsonProperty("historicalAverageRangeTo")]
            public string historicalAverageRangeTo { get; set; }
            [JsonProperty("originCode")]
            public string originCode { get; set; }
            [JsonProperty("overdueTotalRangeCode")]
            public string overdueTotalRangeCode { get; set; }
            [JsonProperty("overdueTotalRangeDescription")]
            public string overdueTotalRangeDescription { get; set; }
            [JsonProperty("overdueTotalFrom")]
            public string overdueTotalFrom { get; set; }
            [JsonProperty("overdueTotalTo")]
            public string overdueTotalTo { get; set; }
            [JsonProperty("upcomingValueRangeCode")]
            public string upcomingValueRangeCode { get; set; }
            [JsonProperty("upcomingValueRangeDescription")]
            public string upcomingValueRangeDescription { get; set; }
            [JsonProperty("upcomingValueFrom")]
            public string upcomingValueFrom { get; set; }
            [JsonProperty("upcomingValueTo")]
            public string upcomingValueTo { get; set; }
        }
    }
}