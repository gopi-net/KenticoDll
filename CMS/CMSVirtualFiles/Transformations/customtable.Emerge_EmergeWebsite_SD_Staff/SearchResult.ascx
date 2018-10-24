<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><div class="docProfile clearfix">
    <asp:hiddenfield id="hdnItemId" runat="server" value='<%# Eval("ItemID")%>' />
    <div class="docImage clearfix">
        <%# IfEmpty(Eval("Photo"), "<img width='97' height='107' src='"+GetWebPartproperty(this,"StaffDefaultImagePath")+"'>", 
"<img width='97' height='107' src='~/getmedia/" +Eval("Photo")  + "/file.aspx'>")
        %>
    </div>
    <div class="docInfo clearfix">
        
            <a class="" href='<%# GetWebPartproperty(this,"PhysicianRedirectPage")+"/"+ Convert.ToString(Eval("FirstName",true)).Replace(",", "").Replace("'", "").Replace(".", "").Replace("`", "") +"_"+ IfEmpty(Eval("MiddleName",true),"", Convert.ToString(Eval("MiddleName",true)).Replace(",", "").Replace("'", "").Replace(".", "").Replace("`", "")+"_")+Convert.ToString(Eval("LastName",true)).Replace(",", "").Replace("'", "").Replace(".", "").Replace("`", "")%>'>
              <h4>
                <%# Eval("FirstName")%>
                <%# IfEmpty(Eval("MiddleName"),"",Eval("MiddleName")) %>
                <%# Eval("LastName") %>,&nbsp;
                <%# Eval("Suffix") %>
              </h4>
              </a>
        <span>
            <%# Convert.ToString(Eval("Specialty")).Replace("|",", ") %></span>
     <%# IfEmpty(Eval("Facility"),"","<span>Location:</span>") %>   
      
         <cms:cmsrepeater id="repeater_Staff_Location" transformationname="customtable.Emerge_{0}_SD_StaffFacility.StaffLocation"
                    runat="server">
          </cms:cmsrepeater>
       
    </div>
</div>
<hr>