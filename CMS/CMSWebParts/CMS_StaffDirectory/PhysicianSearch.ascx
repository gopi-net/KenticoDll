<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PhysicianSearch.ascx.cs" Inherits="CMSWebParts_CMS_StaffDirectory_PhysicianSearch" %>

  <cms:MessagesPlaceHolder ErrorBasicCssClass="FormErrorMessage" ID="MessagesPlaceHolder1" BasicStyles="true" runat="server" />
<asp:Panel ID="SearchPanel" runat="server" DefaultButton="btnSearch">
  
    <cms:LocalizedLabel ID="ErrorLabel" runat="server" EnableViewState="false" />
    <cms:LocalizedHidden ID="hdnIsMailToPatient" runat="server" />
    <div class="physician-form">
        <section class="formWrapper clearfix">
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="lblLastName" runat="server" EnableViewState="false"
                         ResourceString="Emerge.SD.SearchPhysician.Label.LastName"
                         DisplayColon="true" AssociatedControlID="LastName" />
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12"><cms:CMSTextBox runat="server" ID="LastName" MaxLength="50"></cms:CMSTextBox></div>
                </div>
                <div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <cms:LocalizedLabel ID="lblSpecialty" runat="server"
                            EnableViewState="false" ResourceString="Emerge.SD.SearchPhysician.Label.Specialty"
                            DisplayColon="true" AssociatedControlID="Specialty" />
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <cms:LocalizedDropDownList runat="server" ID="Specialty" DataTextField="SpecialtyName" DataValueField="ItemId"></cms:LocalizedDropDownList>
                        <cms:CMSQueryDataSource ID="Specialty_DataSource" runat="server"
                        QueryName="customtable.Emerge_{0}_SD_Specialty.GetSpecialty" />
                    </div>
                </div>

                 <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                           <cms:LocalizedLabel ID="lblDepartment" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.Department"
                                DisplayColon="true" AssociatedControlID="Department" />

                        </div>
                         <div class="col-md-6 col-sm-6 col-xs-12">
                            <cms:LocalizedDropDownList runat="server" ID="Department" DataTextField="DepartmentName" DataValueField="ItemId"></cms:LocalizedDropDownList>
                            <cms:CMSQueryDataSource ID="Department_DataSource" runat="server"
                                QueryName="customtable.Emerge_{0}_SD_Department.GetDepartment" />

                        </div>
                    </div>
                 <div class="row">
                       <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLabel ID="lblKeywords" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.Keywords"
                                DisplayColon="true" AssociatedControlID="Keywords" />
                       </div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                            <cms:CMSTextBox runat="server" ID="Keywords" MaxLength="50"></cms:CMSTextBox>


                        </div>
                    </div>

                 <div class="row">
                        <!-- other custom table need to implement logic on that-->
                         <div class="col-md-3 col-sm-6 col-xs-12">
                             <cms:LocalizedLabel ID="lblCity" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.City"
                                DisplayColon="true" AssociatedControlID="City" />

                        </div>
                         <div class="col-md-6 col-sm-6 col-xs-12">
                               <cms:LocalizedDropDownList runat="server" ID="City" DataTextField="CityName" DataValueField="ItemId"></cms:LocalizedDropDownList>
                            <cms:CMSQueryDataSource ID="City_DataSource" runat="server"
                                QueryName="customtable.Emerge_{0}_SD_City.GetCity" />


                        </div>
                    </div>
                 <div class="row">
                        <!-- other custom table need to implement logic on that-->
                        <div class="col-md-3 col-sm-6 col-xs-12">
                              <cms:LocalizedLabel ID="lblLanguages" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.Language"
                                DisplayColon="true" AssociatedControlID="Language" />

                           
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-12">
                           <cms:LocalizedDropDownList runat="server" ID="Language" DataTextField="language" DataValueField="ItemId"></cms:LocalizedDropDownList>
                            <cms:CMSQueryDataSource ID="Language_DataSource" runat="server"
                                QueryName="customtable.Emerge_{0}_SD_Languages.GetLanguages" />



                        </div>
                    </div>

                 <div class="row">
                        <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLabel ID="lblHospitals" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.Hospitals"
                                DisplayColon="true" AssociatedControlID="Hospitals" />

                        </div>
                         <div class="col-md-6 col-sm-6 col-xs-12">
                            <cms:LocalizedDropDownList runat="server" ID="Hospitals" DataTextField="HospitalName" DataValueField="ItemId"></cms:LocalizedDropDownList>
                            <cms:CMSQueryDataSource ID="Hospitals_DataSource" runat="server"
                                QueryName="customtable.Emerge_{0}_SD_Hospitals.GetHospitals" />

                        </div>
                    </div>

               <div class="row" id="trMiles" runat="server">

                         <div class="col-md-3 col-sm-6 col-xs-12">
                            <cms:LocalizedLabel ID="lblMiles" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.Miles"
                                DisplayColon="true" AssociatedControlID="Miles" />

                        </div>
                         <div class="col-md-6 col-sm-6 col-xs-12">
                            <cms:LocalizedDropDownList runat="server" ID="Miles" DataTextField="OfficeWithDistanceDisplayText" DataValueField="OfficeWithinDistanceValue"></cms:LocalizedDropDownList>
                            <cms:CMSQueryDataSource ID="Miles_DataSource" runat="server"
                                QueryName="customtable.Emerge_{0}_SD_OfficeWithinDistance.GetMiles" />

                        </div>
                    </div>

             <div class="row" id="trZip" runat="server">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <cms:LocalizedLabel ID="lblZip" runat="server" EnableViewState="false"
                        ResourceString="Emerge.SD.SearchPhysician.Label.Zip"
                        DisplayColon="true" AssociatedControlID="Zip" />

                </div>
                <div class="col-md-6 col-sm-6 col-xs-12">

                    <cms:CMSTextBox runat="server" ID="Zip" MaxLength="5"></cms:CMSTextBox>
                </div>
            </div>
                    
            <table>
                <tbody>
                    <%--<tr>
                        <td>
                            <cms:LocalizedLabel ID="lblLastName" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.LastName"
                                DisplayColon="true" AssociatedControlID="LastName" />
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="LastName" MaxLength="50"></cms:CMSTextBox>

                        </td>
                    </tr>--%>
                   <%-- <tr>
                        <td>
                            <cms:LocalizedLabel ID="lblSpecialty" runat="server"
                                EnableViewState="false" ResourceString="Emerge.SD.SearchPhysician.Label.Specialty"
                                DisplayColon="true" AssociatedControlID="Specialty" />
                        </td>
                        <td>

                            <cms:LocalizedDropDownList runat="server" ID="Specialty" DataTextField="SpecialtyName" DataValueField="ItemId"></cms:LocalizedDropDownList>
                            <cms:CMSQueryDataSource ID="Specialty_DataSource" runat="server"
                                QueryName="customtable.Emerge_{0}_SD_Specialty.GetSpecialty" />

                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td>
                            <!-- other custom table need to implement logic on that-->
                           <cms:LocalizedLabel ID="lblDepartment" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.Department"
                                DisplayColon="true" AssociatedControlID="Department" />

                        </td>
                        <td>
                            <cms:LocalizedDropDownList runat="server" ID="Department" DataTextField="DepartmentName" DataValueField="ItemId"></cms:LocalizedDropDownList>
                            <cms:CMSQueryDataSource ID="Department_DataSource" runat="server"
                                QueryName="customtable.Emerge_{0}_SD_Department.GetDepartment" />

                        </td>
                    </tr>--%>
                    <%--<tr>
                        <td>
                            <cms:LocalizedLabel ID="lblKeywords" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.Keywords"
                                DisplayColon="true" AssociatedControlID="Keywords" /></td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="Keywords" MaxLength="50"></cms:CMSTextBox>


                        </td>
                    </tr>--%>
                   <%-- <tr>
                        <!-- other custom table need to implement logic on that-->
                        <td>
                             <cms:LocalizedLabel ID="lblCity" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.City"
                                DisplayColon="true" AssociatedControlID="City" />

                        </td>
                        <td>
                               <cms:LocalizedDropDownList runat="server" ID="City" DataTextField="CityName" DataValueField="ItemId"></cms:LocalizedDropDownList>
                            <cms:CMSQueryDataSource ID="City_DataSource" runat="server"
                                QueryName="customtable.Emerge_{0}_SD_City.GetCity" />


                        </td>
                    </tr>--%>
                    <%--<tr>
                        <!-- other custom table need to implement logic on that-->
                        <td>
                              <cms:LocalizedLabel ID="lblLanguages" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.Language"
                                DisplayColon="true" AssociatedControlID="Language" />

                           
                        </td>
                        <td>
                           <cms:LocalizedDropDownList runat="server" ID="Language" DataTextField="language" DataValueField="ItemId"></cms:LocalizedDropDownList>
                            <cms:CMSQueryDataSource ID="Language_DataSource" runat="server"
                                QueryName="customtable.Emerge_{0}_SD_Languages.GetLanguages" />



                        </td>
                    </tr>--%>
                    <%--<tr>

                        <td>
                            <cms:LocalizedLabel ID="lblHospitals" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.Hospitals"
                                DisplayColon="true" AssociatedControlID="Hospitals" />

                        </td>
                        <td>
                            <cms:LocalizedDropDownList runat="server" ID="Hospitals" DataTextField="HospitalName" DataValueField="ItemId"></cms:LocalizedDropDownList>
                            <cms:CMSQueryDataSource ID="Hospitals_DataSource" runat="server"
                                QueryName="customtable.Emerge_{0}_SD_Hospitals.GetHospitals" />

                        </td>
                    </tr>--%>
                    <%--<tr id="trMiles" runat="server">

                        <td>
                            <cms:LocalizedLabel ID="lblMiles" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.Miles"
                                DisplayColon="true" AssociatedControlID="Miles" />

                        </td>
                        <td>
                            <cms:LocalizedDropDownList runat="server" ID="Miles" DataTextField="OfficeWithDistanceDisplayText" DataValueField="OfficeWithinDistanceValue"></cms:LocalizedDropDownList>
                            <cms:CMSQueryDataSource ID="Miles_DataSource" runat="server"
                                QueryName="customtable.Emerge_{0}_SD_OfficeWithinDistance.GetMiles" />

                        </td>
                    </tr>--%>
                    <tr id="trErrorForMiles" runat="server" visible="false">
                        <td></td>
                        <td>
                            <cms:LocalizedLabel ID="lblErrorForMiles" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.ErrorForMiles"
                                DisplayColon="false" ForeColor="Red" />

                        </td>
                    </tr>
            <%--        <tr id="trzip" runat="server">
                        <td>
                            <cms:localizedlabel id="lblzip" runat="server" enableviewstate="false"
                                resourcestring="emerge.sd.searchphysician.label.zip"
                                displaycolon="true" associatedcontrolid="zip" />

                        </td>
                        <td>

                            <cms:cmstextbox runat="server" id="zip" maxlength="100"></cms:cmstextbox>
                        </td>
                    </tr>--%>
                    <tr id="trErrorForZip" runat="server" visible="false">
                        <td></td>
                        <td>
                            <cms:LocalizedLabel ID="lblErrorForZip" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.ErrorForZip"
                                DisplayColon="false" ForeColor="Red" />

                        </td>
                    </tr>
                 <tr>
                        <td>

                            <cms:LocalizedLabel ID="lblGender" runat="server" EnableViewState="false"
                                ResourceString="Emerge.SD.SearchPhysician.Label.Gender"
                                DisplayColon="true" AssociatedControlID="Hospitals" />
                        </td>
                        <td>
                            <cms:LocalizedRadioButtonList Width="100%" runat="server" ID="Gender" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                            </cms:LocalizedRadioButtonList>

                           <%-- <cms:CMSQueryDataSource ID="Gender_DataSource" runat="server"
                                QueryName="customtable.Emerge_{0}_Common_Gender.GetGender" />--%>
                        </td>
                    </tr>

                </tbody>
            </table>


            <section class="btnWrapper">
                <asp:Button ID="btnSearch" Text="List Physicians" runat="server" CausesValidation="false" />
                <asp:Button ID="btnClear" Text="Clear" runat="server" />
            </section>
        </section>
    </div>
    <hr>
    <div id="divAlphaSearch" runat="server" class="alphabetSearch">
        <ul class="clearfix">
            <li class="active">
                <asp:LinkButton ID="btnA" runat="server" Text="A" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="btnB" runat="server" Text="B" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnC" runat="server" Text="C" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnD" runat="server" Text="D" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnE" runat="server" Text="E" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnF" runat="server" Text="F" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnG" runat="server" Text="G" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnH" runat="server" Text="H" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnI" runat="server" Text="I" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnJ" runat="server" Text="J" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnK" runat="server" Text="K" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnL" runat="server" Text="L" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnM" runat="server" Text="M" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnN" runat="server" Text="N" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnO" runat="server" Text="O" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnP" runat="server" Text="P" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnQ" runat="server" Text="Q" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnR" runat="server" Text="R" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnS" runat="server" Text="S" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnT" runat="server" Text="T" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnU" runat="server" Text="U" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnV" runat="server" Text="V" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnW" runat="server" style="padding:3px;" Text="W" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnX" runat="server" Text="X" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnY" runat="server" Text="Y" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="btnZ" runat="server" Text="Z" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
            <%--<li>
                <asp:LinkButton ID="btnAll" runat="server" Text="ALL" Width="24" CausesValidation="false"
                    OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>--%>
        </ul>
    </div>





</asp:Panel>
<%--    </section>
</section>--%>


