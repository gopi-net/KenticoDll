using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bluespire.Emerge.Components.PreRegistration
{
    public static class PreRegistrationConstants
    {
        #region CustomTableNames

        public const string CUSTOMTABLE_PREREGISTRATIONINFORMATION = "customtable.Emerge_{0}_PR_PreRegistrationInformation";

        #endregion

        #region CustomTableField

        public const string FIELD_PREREGINFO_ISONLYFORFAMILYBIRTHCENTER = "IsOnlyForFamilyBirthCenter";
        public const string FIELD_PREREGINFO_SERVICEORCENTER = "ServiceOrCenter";
        public const string FIELD_PREREGINFO_FIRSTNAME = "FirstName";
        public const string FIELD_PREREGINFO_MIDDLEINITIAL = "MiddleInitial";
        public const string FIELD_PREREGINFO_LASTNAME = "LastName";
        public const string FIELD_PREREGINFO_MAIDENNAME = "MaidenName";
        public const string FIELD_PREREGINFO_DATEOFBIRTH = "DateOfBirth";
        public const string FIELD_PREREGINFO_BIRTHSTATEORCOUNTRY = "BirthStateOrCountry";
        public const string FIELD_PREREGINFO_SSN = "SSN";
        public const string FIELD_PREREGINFO_EMAIL = "Email";
        public const string FIELD_PREREGINFO_MAILINGADDRESS = "MailingAddress";
        public const string FIELD_PREREGINFO_CITY = "City";
        public const string FIELD_PREREGINFO_STATE = "State";
        public const string FIELD_PREREGINFO_ZIP = "Zip";
        public const string FIELD_PREREGINFO_RESIDENCYSAMEASABOVE = "ResidencySameAsAbove";
        public const string FIELD_PREREGINFO_RESIDENCYADDRESS = "ResidencyAddress";
        public const string FIELD_PREREGINFO_RESIDENCYCITY = "ResidencyCity";
        public const string FIELD_PREREGINFO_RESIDENCYCOUNTY = "ResidencyCounty";
        public const string FIELD_PREREGINFO_RESIDENCYSTATE = "ResidencyState";
        public const string FIELD_PREREGINFO_RESIDENCYZIP = "ResidencyZip";
        public const string FIELD_PREREGINFO_HOMEPHONE = "HomePhone";
        public const string FIELD_PREREGINFO_RACE = "Race";
        public const string FIELD_PREREGINFO_OFHISPANIC = "OfHispanic";
        public const string FIELD_PREREGINFO_IFOFHISPANIC = "IfOfHispanic";
        public const string FIELD_PREREGINFO_RELIGION = "Religion";
        public const string FIELD_PREREGINFO_MARITALSTATUS = "MaritalStatus";
        public const string FIELD_PREREGINFO_HIGHESTGRADE = "HighestGrade";
        public const string FIELD_PREREGINFO_EMPLOYMENTSTATUS = "EmploymentStatus";
        public const string FIELD_PREREGINFO_OCCUPATION = "Occupation";
        public const string FIELD_PREREGINFO_TYPEOFBUSINESS = "TypeOfBusiness";
        public const string FIELD_PREREGINFO_EMPLOYER = "Employer";
        public const string FIELD_PREREGINFO_WORKPHONE = "WorkPhone";
        public const string FIELD_PREREGINFO_MOTHERPARTICIPATEDIN = "MotherParticipatedIn";
        public const string FIELD_PREREGINFO_DIDMOTHERSMOKE = "DidMotherSmoke";
        public const string FIELD_PREREGINFO_ADVANCEDIRECTIVES = "AdvanceDirectives";
        public const string FIELD_PREREGINFO_GIFIRSTNAME = "GIFirstName";
        public const string FIELD_PREREGINFO_GIMIDDLEINITIAL = "GIMiddleInitial";
        public const string FIELD_PREREGINFO_GILASTNAME = "GILastName";
        public const string FIELD_PREREGINFO_GIRELATIONSHIP = "GIRelationship";
        public const string FIELD_PREREGINFO_GIDATEOFBIRTH = "GIDateOfBirth";
        public const string FIELD_PREREGINFO_GISSN = "GISSN";
        public const string FIELD_PREREGINFO_GIADDRESS = "GIAddress";

        public const string FIELD_PREREGINFO_GICITY = "GICity";
        public const string FIELD_PREREGINFO_GISTATE = "GIState";
        public const string FIELD_PREREGINFO_GIZIP = "GIZip";
        public const string FIELD_PREREGINFO_GIGENDER = "GIGender";
        public const string FIELD_PREREGINFO_GIOTHERLEGALNAMES = "GIOtherLegalNames";
        public const string FIELD_PREREGINFO_GIRACE = "GIRace";
        public const string FIELD_PREREGINFO_GIRELIGION = "GIReligion";
        public const string FIELD_PREREGINFO_GIMARITALSTATUS = "GIMaritalStatus";
        public const string FIELD_PREREGINFO_GIEMPLOYMENTSTATUS = "GIEmploymentStatus";
        public const string FIELD_PREREGINFO_GIOCCUPATION = "GIOccupation";
        public const string FIELD_PREREGINFO_GIEMPLOYER = "GIEmployer";
        public const string FIELD_PREREGINFO_GIWORKPHONE = "GIWorkPhone";
        public const string FIELD_PREREGINFO_ECOMPANY = "ECompany";
        public const string FIELD_PREREGINFO_EADDRESS = "EAddress";
        public const string FIELD_PREREGINFO_ECITY = "ECity";
        public const string FIELD_PREREGINFO_ESATE = "EState";
        public const string FIELD_PREREGINFO_EZIP = "EZip";
        public const string FIELD_PREREGINFO_EPHONE = "EPhone";
        public const string FIELD_PREREGINFO_NRNAME = "NRName";
        public const string FIELD_PREREGINFO_NRRELATIONTOPATIENT = "NRRelationToPatient";

        public const string FIELD_PREREGINFO_NRADDRESS = "NRAddress";
        public const string FIELD_PREREGINFO_NRCITY = "NRCity";
        public const string FIELD_PREREGINFO_NRSTATE = "NRState";
        public const string FIELD_PREREGINFO_NRZIP = "NRZip";
        public const string FIELD_PREREGINFO_NRPHONE = "NRPhone";
        public const string FIELD_PREREGINFO_NRALTERNATEPHONE = "NRAlternatePhone";
        public const string FIELD_PREREGINFO_ECNAME = "ECName";
        public const string FIELD_PREREGINFO_ECRELATIONTOPATIENT = "ECRelationToPatient";
        public const string FIELD_PREREGINFO_ECADDRESS = "ECAddress";
        public const string FIELD_PREREGINFO_ECCITY = "ECCity";
        public const string FIELD_PREREGINFO_ECSTATE = "ECState";
        public const string FIELD_PREREGINFO_ECZIP = "ECZip";
        public const string FIELD_PREREGINFO_ECPHONE = "ECPhone";
        public const string FIELD_PREREGINFO_ECALTERNATEPHONE = "ECAlternatePhone";
        public const string FIELD_PREREGINFO_PICOMPANYNAME = "PICompanyName";
        public const string FIELD_PREREGINFO_PIPOLICYNUMBER = "PIPolicyNumber";
        public const string FIELD_PREREGINFO_PIGROUPNUMBER = "PIGroupNumber";
        public const string FIELD_PREREGINFO_PISUBSCRIBERNAME = "PISubscriberName";
        public const string FIELD_PREREGINFO_PISOCIALSECURITYNUMBER = "PISocialSecurityNumber";
        public const string FIELD_PREREGINFO_PIADDRESS = "PIAddress";
        public const string FIELD_PREREGINFO_PICITY = "PICity";
        public const string FIELD_PREREGINFO_PISTATE = "PIState";
        public const string FIELD_PREREGINFO_PIZIP = "PIZip";
        public const string FIELD_PREREGINFO_PIDATEOFBIRTH = "PIDateOfBirth";

        public const string FIELD_PREREGINFO_PIRELATIONTOPOLICYHOLDER = "PIRelationToPolicyHolder";
        public const string FIELD_PREREGINFO_PISUBSCRIBERPHONE = "PISubscriberPhone";
        public const string FIELD_PREREGINFO_PIEMPLOYER = "PIEmployer";
        public const string FIELD_PREREGINFO_DOYOUHAVESECINSPOLICY = "DoYouHaveSecInsPolicy";
        public const string FIELD_PREREGINFO_SICOMPANYNAME = "SICompanyName";
        public const string FIELD_PREREGINFO_SIPOLICYNUMBER = "SIPolicyNumber";
        public const string FIELD_PREREGINFO_SIGROUPNUMBER = "SIGroupNumber";
        public const string FIELD_PREREGINFO_SISUBSCRIBERNAME = "SISubscriberName";
        public const string FIELD_PREREGINFO_SISOCIALSECURITYNUMBER = "SISocialSecurityNumber";
        public const string FIELD_PREREGINFO_SIADDRESS = "SIAddress";
        public const string FIELD_PREREGINFO_SICITY = "SICity";
        public const string FIELD_PREREGINFO_SISTATE = "SIState";
        public const string FIELD_PREREGINFO_SIZIP = "SIZip";
        public const string FIELD_PREREGINFO_SIDATEOFBIRTH = "SIDateOfBirth";
        public const string FIELD_PREREGINFO_SIRELATIONTOPOLICYHOLDER = "SIRelationToPolicyHolder";
        public const string FIELD_PREREGINFO_SISUBSCRIBERPHONE = "SISubscriberPhone";
        public const string FIELD_PREREGINFO_SIEMPLOYER = "SIEmployer";
        public const string FIELD_PREREGINFO_SCHEDULEDDATEOFDELIVERY = "ScheduledDateOfDelivery";
        public const string FIELD_PREREGINFO_PROVIDERSNAME = "ProvidersName";
        public const string FIELD_PREREGINFO_PCPHYSICIANSNAME = "PCPhysiciansName";
        public const string FIELD_PREREGINFO_PEDIATRICIANSNAME = "PediatriciansName";

        public const string FIELD_PREREGINFO_VISITEDBEFORE = "VisitedBefore";
        public const string FIELD_PREREGINFO_NAMEIFCHANGED = "NameIfChanged";
        public const string FIELD_PREREGINFO_HOWYOUFINDUS = "HowYouFindUs";
        public const string FIELD_PREREGINFO_BPIFIRSTNAME = "BPIFirstName";
        public const string FIELD_PREREGINFO_BPIMIDDLEINITIAL = "BPIMiddleInitial";
        public const string FIELD_PREREGINFO_BPILASTNAME = "BPILastName";
        public const string FIELD_PREREGINFO_BPIDATEOFBIRTH = "BPIDateOfBirth";
        public const string FIELD_PREREGINFO_BPIBIRTHSTATEORCOUNTRY = "BPIBirthStateOrCountry";
        public const string FIELD_PREREGINFO_BPISSN = "BPISSN";
        public const string FIELD_PREREGINFO_BPIOFHISPANIC = "BPIOfHispanic";
        public const string FIELD_PREREGINFO_BPIIFOFHISPANIC = "BPIIfOfHispanic";
        public const string FIELD_PREREGINFO_BPIRACE = "BPIRace";
        public const string FIELD_PREREGINFO_BPIOCCUPATION = "BPIOccupation";
        public const string FIELD_PREREGINFO_BPITYPEOFBUSINESS = "BPITypeOfBusiness";
        public const string FIELD_PREREGINFO_BPIWORKPHONE = "BPIWorkPhone";
        public const string FIELD_PREREGINFO_BPIHIGHESTGRADE = "BPIHighestGrade";
        public const string FIELD_PREREGINFO_PICEOFHISPANIC = "PICEOfHispanic";
        public const string FIELD_PREREGINFO_PICEIFOFHISPANIC = "PICEIfOfHispanic";
        public const string FIELD_PREREGINFO_PICERACE = "PICERace";
        public const string FIELD_PREREGINFO_STATUS = "Status";
        public const string FIELD_PREREGINFO_ISACTIVE = "IsActive";

        #endregion

        #region Page URLs

        public const string PAGEURL_DATA_LIST = "~/CMSModules/CMS_PreRegistration/Tools/PreRegistration_Data_List.aspx";
        public const string PAGEURL_NEW_ITEM = "~/CMSModules/CMS_PreRegistration/Tools/PreRegistration_Data_EditItem.aspx";
        public const string PAGEURL_LIST = "~/CMSModules/CMS_PreRegistration/Tools/PreRegistration_List.aspx";
        public const string PAGEURL_DATA_SELECTFIELDS = "~/CMSModules/CMS_PreRegistration/Tools/PreRegistration_Data_SelectFields.aspx";
        public const string PAGEURL_DATA_VIEWITEM = "~/CMSModules/CMS_PreRegistration/Tools/PreRegistration_Data_ViewItem.aspx";
        public const string PATH_DATALIST_XML = "~/CMSModules/CMS_PreRegistration/Tools/PreRegistration_Data_List.xml";
        public const string PATH_PREREGDATALIST_XML = "~/CMSModules/CMS_PreRegistration/Pages/PreRegistration_Data_PreRegistrationList.xml";
        public const string PAGEURL_PREREG_DASHBOARD = "~/CMSModules/CMS_PreRegistration/Dashboard/Dashboard.aspx";
        public const string PAGEURL_DATA_VIEW_PREREG_ITEM = "~/CMSModules/CMS_PreRegistration/Pages/PreRegistration_Data_View_PreRegistration_Item.aspx";

        #endregion

        #region String Codes
        public const string STRINGCODE_PREREGISTRATIONHOME = "Emerge.PR.Dashboard";
        public const string STRINGCODE_PREREGISTRATIONSAVEEXCEPTIONMESSAGE = "Emerge.PR.PreRegistrationSaveExceptionMessage";
        public const string STRINGCODE_PREREGISTRATIONSENDEMAILEXCEPTION = "Emerge.PR.PreRegistrationSendEmailException";
        #endregion

        #region Queries
        public const string QUERY_GETSTATELIST = "customtable.Emerge_{0}_PR_States.GetStates";
        public const string QUERY_GETRELATIONTOPHLIST = "customtable.Emerge_{0}_PR_RelationToPolicyHolder.GetRelationToPolicyHolder";
        public const string QUERY_GETSTATENAMEBYID = "customtable.Emerge_{0}_PR_States.GetStateNameByID";
        public const string QUERY_GETMARITALSTATUSBYID = "customtable.Emerge_{0}_PR_MaritalStatus.GetMaritalStatusByID";
        public const string QUERY_GETRACEBYID = "customtable.Emerge_{0}_PR_Race.GetRaceByID";
        public const string QUERY_GETHOWFINDOUTABOUTUS_BYID = "customtable.Emerge_{0}_PR_HowFindOutAboutUs.GetHowFindOutAboutUsByID";
        public const string QUERY_GETRELATIONTO_POLITYHOLDER_BYID = "customtable.Emerge_{0}_PR_RelationToPolicyHolder.GetRelationToPolicyHolderById";
        public const string QUERY_GETRELATIONTO_PATIENTNR_BYID = "customtable.Emerge_{0}_PR_RelationToPatientOfNR.GetRelationToPatientNRById";
        public const string QUERY_GETRELATIONTO_PATIENTEC_BYID = "customtable.Emerge_{0}_PR_RelationToPatientOfEC.GetRelationToPatientECById";
        public const string QUERY_GETMOTHERPARTICIPATEDIN_BYID = "customtable.Emerge_{0}_PR_MotherParticipatedIn.GetMotherParticipatedInByID";
        public const string QUERY_GETPREREGISTRATIONINFO = "customtable.Emerge_{0}_PR_PreRegistrationInformation.GetPreRegistrationInfo";
        public const string QUERY_FIELD_PARAMETER = "ItemID";        
        #endregion

        #region Transformation
        public const string TRANSFORMATION_PREVIEW = "customtable.Emerge_{0}_PR_PreRegistrationInformation.Preview";
        #endregion
        #region Other
        public const string SESSIONKEY_PREREGISTRATIONINFO = "PreRegistrationInfo";
        public const string TEMPLATE_CONFIRMATIONEMAIL = "Emerge_{0}_PreRegistrationConfirmation";
        public const string STATE_BRITISH_COLUMBIA = "British Columbia";
        public const string VALIDATION_EXPRESSION_FOR_STATE_BRITISH_COLUMBIA = "^([a-zA-Z0-9]{3})$";
        public const string VALIDATION_EXPRESSION_FOR_OTHER_STATE = "^([a-zA-Z0-9]{5,7})$";
        public const string PREREGINFO_MESSAGE_FAILURE = "Error during saving data. Please try again.";
        public const string PREREGINFO_STATUS = "Pending";
        public const string PREREGINFO_DATEFORMAT = "MM/dd/yyyy";
        public const string PREREGINFO_REVIEW_IFNOVALUE = "--";
        #endregion
    }
}
