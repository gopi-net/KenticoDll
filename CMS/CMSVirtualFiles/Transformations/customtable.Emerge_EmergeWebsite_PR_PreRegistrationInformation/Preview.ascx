<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div>
        <div class="mainTitle">
            <h3>Patient Information:</h3>
            <hr />
        </div>
        <div class="formSectn">
            <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">
                <tr>
                    <td width="50%">
                        First Name:
                    </td>
                    <td>
                        <%# Eval("FirstName") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Middle Initial: 
                    </td>
                    <td>
                        <%# Eval("MiddleInitial") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Last Name:
                    </td>
                    <td>
                        <%# Eval("LastName") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Maiden Name:
                    </td>
                    <td>
                        <%# Eval("MaidenName") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date of Birth (mm/dd/yyyy):
                    </td>
                    <td>
                        <%# Eval("DateOfBirth") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Email Address:
                    </td>
                    <td>
                        <%# Eval("Email") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Birth State or Country:
                    </td>
                    <td>
                        <%# Eval("BirthStateOrCountry") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       Social Security Number: 
                    </td>
                    <td>
                        <%# Eval("SSN") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Mailing Address: 	
                    </td>
                    <td>
                        <%# Eval("MailingAddress") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       City: 
                    </td>
                    <td>
                        <%# Eval("City") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        State: 	
                    </td>
                    <td>
						<%# Eval("State") == "--" ? Eval("State") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("State")),"customtable.Emerge_{0}_PR_States.GetStateNameByID")%>	
                    </td>
                </tr>
                <tr>
                    <td>
                        Zip: 	
                    </td>
                    <td>
                        <%# Eval("Zip") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Check this box if your Residency is the same as above: 	
                    </td>
                    <td>
                        <%# Convert.ToBoolean(Eval("ResidencySameAsAbove"))==true?"Yes":"No"%>
                    </td>
                </tr>
                <tr class="ResidencySameAsAbove" style="display:<%# Convert.ToBoolean(Eval("ResidencySameAsAbove"))==true?"none":"table-row"%>">
                    <td>
                        Residence (If P.O. Box, please give street address, too): 	
                    </td>
                    <td>
                        <%# Eval("ResidencyAddress") %>
                    </td>
                </tr>
                <tr class="ResidencySameAsAbove" style="display:<%# Convert.ToBoolean(Eval("ResidencySameAsAbove"))==true?"none":"table-row"%>">
                    <td>
                        City: 	
                    </td>
                    <td>
                        <%# Eval("ResidencyCity") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        County: 	
                    </td>
                    <td>
                        <%# Eval("ResidencyCounty") %>
                    </td>
                </tr>
                <tr class="ResidencySameAsAbove" style="display:<%# Convert.ToBoolean(Eval("ResidencySameAsAbove"))==true?"none":"table-row"%>">
                    <td>
                        State: 	
                    </td>
                    <td>
					 <%# Eval("State") == "--" ? Eval("State") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("State")),"customtable.Emerge_{0}_PR_States.GetStateNameByID")%>	
                    </td>
                </tr>
                <tr class="ResidencySameAsAbove" style="display:<%# Convert.ToBoolean(Eval("ResidencySameAsAbove"))==true?"none":"table-row"%>">
                    <td>
                        Zip: 	
                    </td>
                    <td>
                        <%# Eval("ResidencyZip") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Home/Cell Phone:
                    </td>
                    <td>
                        <%# Eval("HomePhone") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Race/Ethnicity:
                    </td>
                    <td>                     
							<%# Eval("Race") == "--" ? Eval("Race") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("Race")),"customtable.Emerge_{0}_PR_Race.GetRaceByID")%>	
                    </td>
                </tr>
                <tr>
                    <td>
                        Of Hispanic origin or descent?:
                    </td>
                    <td>
                        <%# Eval("OfHispanic") %>
                    </td>
                </tr>
                <tr id="trIfOfHispanic" class="OfHispanic" style="display:<%# Convert.ToString(Eval("OfHispanic")).ToLower()=="yes"?"table-row":"none"%>">
                    <td>
                        If of Hispanic origin or descent, specify Cuban, Mexican, Puerto Rican, etc.:
                    </td>
                    <td>
                        <%# Eval("IfOfHispanic") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Religion:
                    </td>
                    <td>
                        <%# Eval("Religion") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Marital Status:
                    </td>
                    <td>                        
						<%# Eval("MaritalStatus") == "--" ? Eval("MaritalStatus") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("MaritalStatus")),"customtable.Emerge_{0}_PR_MaritalStatus.GetMaritalStatusByID")%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Highest Grade of School Completed:
                    </td>
                    <td>
                        <%# Eval("HighestGrade") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Employment Status:
                    </td>
                    <td>
                        <%# Eval("EmploymentStatus") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Occupation (Within Last Year):	
                    </td>
                    <td>
                        <%# Eval("Occupation") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Type of Business or Industry: 	
                    </td>
                    <td>
                        <%# Eval("TypeOfBusiness") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Employer (Within Last Year):
                    </td>
                    <td>
                        <%# Eval("Employer") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Work/Cell Phone:
                    </td>
                    <td>
                        <%# Eval("WorkPhone") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        During Pregnancy Mother Participated in:
                    </td>
                    <td>
                        <%# Eval("MotherParticipatedIn") == "--" ? Eval("MotherParticipatedIn") : CMSTransformation.GetCommaSeperatedValues(Convert.ToString(Eval("MotherParticipatedIn")),"customtable.Emerge_{0}_PR_MotherParticipatedIn.GetMotherParticipatedInByID")%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Did mother smoke at any time during pregnancy?:
                    </td>
                    <td>
                        <%# Eval("DidMotherSmoke") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Advance Directives (Living Will or Durable Power of Attorney for Health Care):
                    </td>
                    <td>
                        <%# Eval("AdvanceDirectives") %>
                    </td>
                </tr>
            </table>
        </div>
        <div class="mainTitle">
            <h3>Responsible Party/ Guarantor Information:</h3>
            <hr />
        </div>
        <div class="formSectn">
            <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">
                <tr>
                    <td width="50%">
                        First Name:
                    </td>
                    <td>
                        <%# Eval("GIFirstName") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Middle Initial: 	
                    </td>
                    <td>
                        <%# Eval("GIMiddleInitial") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       Last Name: 	
                    </td>
                    <td>
                        <%# Eval("GILastName") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       Relationship: 	
                    </td>
                    <td>
                        <%# Eval("GIRelationship") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Date of Birth: 	
                    </td>
                    <td>
                        <%# Eval("GIDateOfBirth") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       Social Security Number: 
                    </td>
                    <td>
                        <%# Eval("GISSN") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Address (If other than above): 	
                    </td>
                    <td>
                        <%# Eval("GIAddress") %>
                    </td>
                </tr>
                <tr>
                    <td>
                      City: 	
                    </td>
                    <td>
                        <%# Eval("GICity") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        State:
                    </td>
                    <td>					 	
                  <%# Eval("GIState") == "--" ? Eval("GIState") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("GIState")),"customtable.Emerge_{0}_PR_States.GetStateNameByID")%>	
                    </td>
                </tr>
                <tr>
                    <td>                        
						Zip: 	
                    </td>
                    <td>
                        <%# Eval("GIZip") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       Gender:
                    </td>
                    <td>
                        <%# Eval("GIGender") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Other Legal Names: 
                    </td>
                    <td>
                        <%# Eval("GIOtherLegalNames") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Race/Ethnicity:
                    </td>
                    <td>
						<%# Eval("GIRace") == "--" ? Eval("GIRace") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("GIRace")),"customtable.Emerge_{0}_PR_Race.GetRaceByID")%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Religion:
                    </td>
                    <td>
                        <%# Eval("GIReligion") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Marital Status: 
                    </td>
                    <td>
					<%# Eval("GIMaritalStatus") == "--" ? Eval("GIMaritalStatus") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("GIMaritalStatus")),"customtable.Emerge_{0}_PR_MaritalStatus.GetMaritalStatusByID")%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Employment Status: 
                    </td>
                    <td>
                        <%# Eval("GIEmploymentStatus") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Occupation (Within Last Year):
                    </td>
                    <td>
                        <%# Eval("GIOccupation") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       Employer:
                    </td>
                    <td>
                        <%# Eval("GIEmployer") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Work/Cell Phone:
                    </td>
                    <td>
                        <%# Eval("GIWorkPhone") %>
                    </td>
                </tr>
            </table>
        </div>
        <div class="mainTitle">
            <h3>Employer:</h3>
            <hr />
        </div>
        <div class="formSectn">
            <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">
                <tr>
                    <td width="50%">
                        Company: 
                    </td>
                    <td>
                        <%# Eval("ECompany") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       Address:
                    </td>
                    <td>
                        <%# Eval("EAddress") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       City:
                    </td>
                    <td>
                        <%# Eval("ECity") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       State:
                    </td>
                    <td>                         
						<%# Eval("EState") == "--" ? Eval("EState") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("EState")),"customtable.Emerge_{0}_PR_States.GetStateNameByID")%>	
						</td>
                </tr>
                <tr>
                    <td>
                       Zip:
                    </td>
                    <td>
                        <%# Eval("EZip") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Phone:
                    </td>
                    <td>
                        <%# Eval("EPhone") %>
                    </td>
                </tr>
            </table>
        </div>
        <div class="mainTitle">
            <h3>Next of Kin/Emergency Contact Information:</h3>
            <hr />
        </div>
        <div class="formSectn">
            <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">
                <tr>
                    <td width="50%">
                        Name of Nearest Relative (Other than Spouse):
                    </td>
                    <td>
                        <%# Eval("NRName") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Relation to Patient:
                    </td>
                    <td> 
						<%# Eval("NRRelationToPatient") == "--" ? Eval("NRRelationToPatient") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("NRRelationToPatient")),"customtable.Emerge_{0}_PR_RelationToPatientOfNR.GetRelationToPatientNRById")%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Address: 	
                    </td>
                    <td>
                        <%# Eval("NRAddress") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        City: 
                    </td>
                    <td>
                        <%# Eval("NRCity") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        State: 	
                    </td>
                    <td>
						<%# Eval("NRState") == "--" ? Eval("NRState") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("NRState")),"customtable.Emerge_{0}_PR_States.GetStateNameByID")%>	
                    </td>
                </tr>
                <tr>
                    <td>
                       Zip: 	
                    </td>
                    <td>
                        <%# Eval("NRZip") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Phone:	
                    </td>
                    <td>
                        <%# Eval("NRPhone") %>
                    </td>
                </tr>
                <tr>
					<td>                        
						Alternate Phone: 	
                    </td>
                    <td>
                        <%# Eval("NRAlternatePhone") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Name of Emergency Contact:
                    </td>
                    <td>
                        <%# Eval("ECName") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Relation to Patient:
                    </td>
                    <td>
					<%# Eval("ECRelationToPatient") == "--" ? Eval("ECRelationToPatient") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("ECRelationToPatient")),"customtable.Emerge_{0}_PR_RelationToPatientOfEC.GetRelationToPatientECById")%>                       
                    </td>
                </tr>
                <tr>
                    <td>
                        Address: 	
                    </td>
                    <td>
                        <%# Eval("ECAddress") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       City: 
                    </td>
                    <td>
                        <%# Eval("ECCity") %>
                    </td>
                </tr>
                <tr>
                    <td>    	
						State: 	
                    </td>
                    <td>                                          
					   <%# Eval("ECState") == "--" ? Eval("ECState") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("ECState")),"customtable.Emerge_{0}_PR_States.GetStateNameByID")%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Zip: 	
                    </td>
                    <td>
                        <%# Eval("ECZip") %>
                    </td>
                </tr>
                <tr>
                    <td>                       
						Phone:
                    </td>
                    <td>
                        <%# Eval("ECPhone") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Alternate Phone:
                    </td>
                    <td>
                        <%# Eval("ECAlternatePhone") %>
                    </td>
                </tr>
            </table>
        </div>
        <div class="mainTitle">
            <h3>Primary Insurance Information:</h3>
            <hr />
        </div>
        <div class="formSectn">
            <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">
                <tr>
                    <td width="50%">
                        Primary Insurance Company Name:
                    </td>
                    <td>
                        <%# Eval("PICompanyName") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Policy Number: 	
                    </td>
                    <td>
                        <%# Eval("PIPolicyNumber") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       Group Number:	
                    </td>
                    <td>
                        <%# Eval("PIGroupNumber") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Subscriber Name:	
                    </td>
                    <td>
                        <%# Eval("PISubscriberName") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Social Security Number: 	
                    </td>
                    <td>
                        <%# Eval("PISocialSecurityNumber") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Address: 	
                    </td>
                    <td>
                        <%# Eval("PIAddress") %>
                    </td>
                </tr>
                <tr>
                    <td>
                       City: 	
                    </td>
                    <td>
                        <%# Eval("PICity") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        State: 
                    </td>
                    <td>                                        
						<%# Eval("PIState") == "--" ? Eval("PIState") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("PIState")),"customtable.Emerge_{0}_PR_States.GetStateNameByID")%>
                    </td>
                </tr>
                <tr>
                    <td>
                        Zip: 	
                    </td>
                    <td>
                        <%# Eval("PIZip") %>
                    </td>
                </tr>
                <tr>
                    <td>
						Date of Birth:	
                    </td>
                    <td>
                        <%# Eval("PIDateOfBirth") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Relation to Policy Holder:
                    </td>
                    <td>
					<%# Eval("PIRelationToPolicyHolder") == "--" ? Eval("PIRelationToPolicyHolder") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("PIRelationToPolicyHolder")),"customtable.Emerge_{0}_PR_RelationToPolicyHolder.GetRelationToPolicyHolderById")%>                        
                    </td>
                </tr>
                <tr>
                    <td>
                        Subscriber Phone:
                    </td>
                    <td>
                        <%# Eval("PISubscriberPhone") %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Employer:
                    </td>
                    <td>
                        <%# Eval("PIEmployer") %>
                    </td>
                </tr>
            </table>
        </div>
        <div class="mainTitle">
            <h3>Secondary Insurance Information:</h3>
            <hr />
        </div>
        <div class="formSectn">
            <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">
                <tr>
                    <td width="50%">
                        Do you have a secondary insurance policy?:
                    </td>
                    <td>
                        <%# Eval("DoYouHaveSecInsPolicy") %>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formSectn SecondaryInsDiv" id="SecondaryInsDiv2" style="display:<%# Convert.ToString(Eval("DoYouHaveSecInsPolicy")).ToLower()=="yes"?"block":"none"%>">            
                <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">
                    <tr id="trSICompanyName">
                        <td width="50%" runat="server">
                           Secondary Insurance Company Name:
                        </td>
                        <td>
                            <%# Eval("SICompanyName") %>
                        </td>
                    </tr>
                    <tr id="trSIPolicyNumber">
                        <td>
                            Policy Number:
                        </td>
                        <td>
                            <%# Eval("SIPolicyNumber") %>
                        </td>
                    </tr>
                    <tr id="trSIGroupNumber">
                        <td>
                            Group Number:
                        </td>
                        <td>
                            <%# Eval("SIGroupNumber") %>
                        </td>
                    </tr>
                    <tr id="trSISubscriberName">
                        <td>
                            Subscriber Name:
                        </td>
                        <td>
                            <%# Eval("SISubscriberName") %>
                        </td>
                    </tr>
                    <tr id="trSISocialSecurityNumber">
                        <td>
                            Social Security Number:
                        </td>
                        <td>
                            <%# Eval("SISocialSecurityNumber") %>
                        </td>
                    </tr>
                    <tr id="trSIAddress">
                        <td>
                           Address: 	
                        </td>
                        <td>
                            <%# Eval("SIAddress") %>
                        </td>
                    </tr>
                    <tr id="trSICity">
                        <td>
                           City: 
                        </td>
                        <td>
                            <%# Eval("SICity") %>
                        </td>
                    </tr>
                    <tr id="trSIState1">
                        <td>
                           State: 	
                        </td>
                        <td>                      
							 <%# Eval("SIState") == "--" ? Eval("SIState") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("SIState")),"customtable.Emerge_{0}_PR_States.GetStateNameByID")%>
                        </td>
                    </tr>
                    <tr id="trSIZip">
                        <td>                            
							Zip: 	
                        </td>
                        <td>
                            <%# Eval("SIZip") %>
                        </td>
                    </tr>
                    <tr id="trSIDateOfBirth">
                        <td>
                            Date of Birth:
                        </td>
                        <td>
                            <%# Eval("SIDateOfBirth") %>
                        </td>
                    </tr>
                    <tr id="trSIRelationToPolicyHolder">
                        <td>
                            Relation to Policy Holder:
                        </td>
                        <td>
						<%# Eval("SIRelationToPolicyHolder") == "--" ? Eval("SIRelationToPolicyHolder") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("SIRelationToPolicyHolder")),"customtable.Emerge_{0}_PR_RelationToPolicyHolder.GetRelationToPolicyHolderById")%>
                        </td>
                    </tr>
                    <tr id="trSISubscriberPhone">
                        <td>
                            Subscriber Phone:
                        </td>
                        <td>
                            <%# Eval("SISubscriberPhone") %>
                        </td>
                    </tr>
                    <tr id="trSIEmployer">
                        <td>
                            Employer:
                        </td>
                        <td>
                            <%# Eval("SIEmployer") %>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mainTitle">
                <h3>Visit Information:</h3>
                <hr />
            </div>
            <div class="formSectn">
                <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">
                    <tr>
                        <td width="50%">
                            Expected/Scheduled Date of Delivery:
                        </td>
                        <td>
                            <%# Eval("ScheduledDateOfDelivery") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Healthcare Provider’s Name for this Pregnancy:
                        </td>
                        <td>
                            <%# Eval("ProvidersName") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Primary Care Physician’s Name:
                        </td>
                        <td>
                            <%# Eval("PCPhysiciansName") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Pediatrician’s Name:
                        </td>
                        <td>
                            <%# Eval("PediatriciansName") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Have you visited before? (For any reason):
                        </td>
                        <td>
                            <%# Eval("VisitedBefore") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Your name then if changed:
                        </td>
                        <td>
                            <%# Eval("NameIfChanged") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            How did you find out about us?:
                        </td>
                        <td>
							<%# Eval("HowYouFindUs") == "--" ? Eval("HowYouFindUs") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("HowYouFindUs")),"customtable.Emerge_{0}_PR_HowFindOutAboutUs.GetHowFindOutAboutUsByID")%>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mainTitle">
                <h3>Baby's Parent Information: (Father: Spouse or Partner)</h3>
                <hr />
            </div>
            <div class="formSectn">
                <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">
                    <tr>
                        <td width="50%">
                            First Name:
                        </td>
                        <td>
                            <%# Eval("BPIFirstName") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Middle Initial:
                        </td>
                        <td>
                            <%# Eval("BPIMiddleInitial") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Last Name:
                        </td>
                        <td>
                            <%# Eval("BPILastName") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Date of Birth (mm/dd/yyyy):
                        </td>
                        <td>
                            <%# Eval("BPIDateOfBirth") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Birth State or Country:
                        </td>
                        <td>
                            <%# Eval("BPIBirthStateOrCountry") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           Social Security Number:
                        </td>
                        <td>
                            <%# Eval("BPISSN") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           Of Hispanic origin or descent?:
                        </td>
                        <td>
                            <%# Eval("BPIOfHispanic") %>
                        </td>
                    </tr>
                    <tr id="trBPIOfHispanic" class="BPIOfHispanic" style="display:<%# Convert.ToString(Eval("BPIOfHispanic")).ToLower()=="yes"?"table-row":"none"%>">
                        <td>
                            If of Hispanic origin or descent, specify Cuban, Mexican, Puerto Rican, etc.:
                        </td>
                        <td>
                            <%# Eval("BPIIfOfHispanic") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Race:
                        </td>
                        <td>
<%# Eval("BPIRace") == "--" ? Eval("BPIRace") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("BPIRace")),"customtable.Emerge_{0}_PR_Race.GetRaceByID")%>                      
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Occupation (Within Last Year): 	
                        </td>
                        <td>
                            <%# Eval("BPIOccupation") %>
                        </td>
                    </tr>
                    <tr>
                        <td>                            
							Type of Business or Industry: 	
                        </td>
                        <td>
                            <%# Eval("BPITypeOfBusiness") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Work Phone: 	
                        </td>
                        <td>
                            <%# Eval("BPIWorkPhone") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Highest Grade of School Completed:
                        </td>
                        <td>
                            <%# Eval("BPIHighestGrade") %>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="mainTitle">
                <h3>Parental Identification of Child's Ethnicity and Race</h3>
                <hr />
            </div>
            <div class="formSectn">
                <table width="100%" cellpadding="5" cellspacing="0" class="tbl-pre-reg">
                    <tr>
                        <td width="50%">
                            Of Hispanic origin or descent?:
                        </td>
                        <td>
                            <%# Eval("PICEOfHispanic") %>
                        </td>
                    </tr>
                    <tr id="trPICEIfOfHispanic" class="PICEOfHispanic" style="display:<%# Convert.ToString(Eval("PICEOfHispanic")).ToLower()=="yes"?"table-row":"none"%>">
                        <td>
                            If of Hispanic origin or descent, specify Cuban, Mexican, Puerto Rican, etc.:
                        </td>
                        <td>
                            <%# Eval("PICEIfOfHispanic") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Race:
                        </td>
                        <td>
						<%# Eval("PICERace") == "--" ? Eval("PICERace") : CMSTransformation.GetFieldValuesById(Convert.ToInt16(Eval("PICERace")),"customtable.Emerge_{0}_PR_Race.GetRaceByID")%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>            