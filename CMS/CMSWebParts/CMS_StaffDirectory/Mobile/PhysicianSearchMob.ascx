<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PhysicianSearchMob.ascx.cs" Inherits="CMSWebParts_CMS_StaffDirectory_Mobile_PhysicianSearchMob" %>
<cms:MessagesPlaceHolder ErrorBasicCssClass="ErrorMessage" ID="MessagesPlaceHolder1" BasicStyles="true" runat="server" />
<asp:Panel ID="SearchPanel" runat="server">
    
    <cms:LocalizedLabel ID="ErrorLabel" runat="server" EnableViewState="false" />
    <cms:LocalizedHidden ID="hdnIsMailToPatient" runat="server" />
    <section class="formWrapper">
        <label>
            <cms:LocalizedLabel ID="lblLastName" runat="server" EnableViewState="false"
                ResourceString="Emerge.SD.SearchPhysician.Label.LastName"
                DisplayColon="true" AssociatedControlID="LastName" />
            <cms:CMSTextBox runat="server" ID="LastName" MaxLength="50"></cms:CMSTextBox>

        </label>
        <label>
            <cms:LocalizedLabel ID="lblSpecialty" runat="server"
                EnableViewState="false" ResourceString="Emerge.SD.SearchPhysician.Label.Specialty"
                DisplayColon="true" AssociatedControlID="Specialty" />

            <cms:LocalizedDropDownList runat="server" ID="Specialty" DataTextField="SpecialtyName" DataValueField="ItemId"></cms:LocalizedDropDownList>
            <cms:CMSQueryDataSource ID="Specialty_DataSource" runat="server"
                QueryName="customtable.Emerge_{0}_SD_Specialty.GetSpecialty" />

        </label>
    </section>

    <section class="btnWrapper">

        <asp:Button ID="btnSearch" class="btn btn-primary" Text="List Physicians" runat="server" CausesValidation="false" />
        <asp:Button ID="btnClear" class="btn btn-default" Text="Clear" runat="server" />
    </section>
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
                <asp:LinkButton ID="btnW" runat="server" Text="W" CausesValidation="false" OnClick="AlphaPhysicianSearch"></asp:LinkButton></li>
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
