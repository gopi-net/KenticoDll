<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %> <div class="media clearfix">

    <asp:hiddenfield id="hdnItemId" runat="server" value='<%# Eval("ItemID")%>' />
               
        <%# IfEmpty(Eval("Photo"), "<img width='97' height='107' class='media-object pull-left' src='"+GetWebPartproperty(this,"StaffDefaultImagePath")+"'>", 
"<img width='97' height='107' class='media-object pull-left' src='~/getmedia/" +Eval("Photo")  + "/file.aspx'>")
        %>
      <div class="media-body">
            <a class="" href='<%# GetWebPartproperty(this,"PhysicianRedirectPage")+"/"+Eval("FirstName") +"-"+ IfEmpty(Eval("MiddleName"),"", Eval("MiddleName")+"-")+Eval("LastName")%>'>
              <h4 class="media-heading">
                <%# Eval("FirstName")%>
                <%# IfEmpty(Eval("MiddleName"),"",Eval("MiddleName")) %>
                <%# Eval("LastName") %>,&nbsp;
                <%# Eval("Suffix") %>
              </h4>
              </a>
        <span><i>
            <%# Convert.ToString(Eval("Specialty")).Replace("|",", ") %></i></span>
       
    </div>
</div>
<hr>