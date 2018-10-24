<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><asp:HiddenField ID="hdnItemId" Value='<%# Eval("ItemId")%>' runat="server"></asp:HiddenField>
<section class="findPhysician physician-bio">
    <div class="docProfile clearfix">
        <div class="docImage clearfix">
            <%# IfEmpty(Eval("Photo"), "<img width='97' height='107' src='"+GetWebPartproperty(this,"StaffDefaultImagePath")+"'>", 
"<img width='97' height='107' src='~/getmedia/" +Eval("Photo")  + "/file.aspx'>")
            %>
        </div>

        <div class="docInfo clearfix">
            <h1><%# Eval("FirstName")%> <%# IfEmpty(Eval("MiddleName"),"",Eval("MiddleName")) %>
                <%# Eval("LastName") %>,&nbsp; <%# Eval("Suffix") %></h1>
            <p><span><i><%#Convert.ToString(Eval("Specialty")).Replace("|",", ") %></i></span></p>
            <!--Repater name :- repeater_(RelationName) in CustomTableRelationMaster with Primary table Staff -->
            <cms:CMSRepeater ID="repeater_Staff_Location"
                TransformationName="customtable.Emerge_{0}_SD_StaffFacility.StaffLocation"
                runat="server">
            </cms:CMSRepeater>
        </div>
        <div class="docMap clearfix">
            <div id="map" style="width: 200px; height: 140px;"></div>
            <!-- <iframe scrolling="no" frameborder="0" src="http://www.mapquest.com/embed?q=320 Albert Rains Blvd. Gadsden, AL 35901"  width="210" height="140"
                  class="mapframe" scrolling="no" marginheight="0" marginwidth="0"></iframe>-->
        </div>

    </div>
</section>

<h2><%# IfEmpty(Eval("Description"), "", "Bio" )%></h2>
<p>
    <%# Eval("Description") %>
</p>
<!--Repater name :- repeater_(RelationName) in CustomTableRelationMaster with Primary table Staff -->
<p style='<%# Convert.ToString(Eval("Language"))==""?"display:none;": ""%>'>
    Languages spoken- 
          <%# Convert.ToString(Eval("Language")).Replace(" |",",") %>
</p>

<h2><%# IfEmpty(Eval("Specialty"), "", "Specialties" )%></h2>
<ul style='<%# Convert.ToString(Eval("Specialty"))==""?"display:none;": ""%>'>
    <li><%# Convert.ToString(Eval("Specialty")).Replace("|","</li><li>") %></li>
</ul>
<h2><%# IfEmpty(Eval("Hospitals"), "", "Hospital Location(s)" )%></h2>
<ul style='<%# Convert.ToString(Eval("Hospitals"))==""?"display:none;": ""%>'>
    <li><%# Convert.ToString(Eval("Hospitals")).Replace("|","</li><li>") %></li>
</ul>

    <!--Repater name :- repeater_(RelationName) in CustomTableRelationMaster with Primary table Staff -->
    <cms:CMSRepeater ID="repeater_Staff_Education"
        TransformationName="customtable.Emerge_{0}_SD_StaffEducation.StaffEducation"
        runat="server">
          <HeaderTemplate><h2>Education</h2>
<ul></HeaderTemplate>
            <FooterTemplate></ul></FooterTemplate>
    </cms:CMSRepeater>



<section class="btnWrapper">
    <input type="button" value="Back to Search Results" onclick='<%# "window.location.href=\""+GetWebPartproperty(this,"BackToResultPage")+"\""  %>' />
    <input type="button" value="New Search" onclick='<%# "window.location.href=\""+GetWebPartproperty(this,"NewSearchUrl")+"\""  %>' />


</section>

