<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>  <div class="searchWrap phyBio">
<asp:HiddenField id="hdnItemId" Value='<%# Eval("ItemId")%>'      runat="server" > </asp:HiddenField >

     <h2> <%# Eval("FirstName")%> <%# IfEmpty(Eval("MiddleName"),"",Eval("MiddleName")) %>       
                          <%# Eval("LastName") %>,&nbsp; <%# Eval("Suffix") %></h2>
            <p><i><%#Convert.ToString(Eval("Specialty")).Replace("|",", ") %></i></p>
       <div class="media clearfix">
<%# IfEmpty(Eval("Photo"), "<img width='97' height='107' class='media-object pull-left' src='"+GetWebPartproperty(this,"StaffDefaultImagePath")+"'>", 
"<img width='97' height='107' class='media-object pull-left' src='~/getmedia/" +Eval("Photo")  + "/file.aspx'>")
 %>
          <div class="media-body"><span>
                  <!--Repater name :- repeater_(RelationName) in CustomTableRelationMaster with Primary table Staff -->
                     <cms:cmsrepeater id="repeater_Staff_Location" 
                     transformationname="customtable.Emerge_{0}_SD_StaffFacility.StaffLocation"
                        runat="server">
                     </cms:cmsrepeater></span>
                  
          </div>
                </div>
 </div>
        <h2>Bio</h2>
        <p>
        <%# Eval("Description") %>
        </p><!--Repater name :- repeater_(RelationName) in CustomTableRelationMaster with Primary table Staff -->
        <p style='<%# Convert.ToString(Eval("Language"))==""?"display:none;":""%>'>Languages spoken- <%# Convert.ToString(Eval("Language")).Replace(" |",",") %></p>


        <h2>Specialties</h2>
        <ul>
            <li> <%# Convert.ToString(Eval("Specialty")).Replace("|","</li><li>") %></li>
        </ul>
        <h2>Hospital Locations</h2>
        <ul>
            <li> <%# Convert.ToString(Eval("Hospitals")).Replace("|","</li><li>") %></li>
        </ul>
        <h2>Education</h2>
        <ul><!--Repater name :- repeater_(RelationName) in CustomTableRelationMaster with Primary table Staff -->
         <cms:cmsrepeater id="repeater_Staff_Education" 
                     transformationname="customtable.Emerge_{0}_SD_StaffEducation.StaffEducation"
                        runat="server">
                     </cms:cmsrepeater>
                     
      
        </ul>
        <section class="btnWrapper">
          <input type="button" class="btn btn-default" value="Back to Search Results" onclick='<%# "window.location.href=\""+GetWebPartproperty(this,"BackToResultPage")+"\""  %>' />
            <input type="button" class="btn btn-default" value="New Search" onclick='<%# "window.location.href=\""+GetWebPartproperty(this,"NewSearchUrl")+"\""  %>' />
              
        
        </section>