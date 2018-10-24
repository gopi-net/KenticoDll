<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMS.DocumentEngine.Web.UI.CMSTransformation" %><%@ Register TagPrefix="cms" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %>
<%@ Register TagPrefix="cc1" Namespace="CMS.DocumentEngine.Web.UI" Assembly="CMS.DocumentEngine.Web.UI" %><address>
                        <span><i><%# Eval("Street") %>,</i></span>
                        <span><i><%# IfEmpty(Eval("City"),"",Eval("City")+", ")  %> <%# IfEmpty(Eval("State"),"",Eval("State"))  %>  <%# Eval("Zip") %> </i></span>
                        <span><i><%# IfEmpty(Eval("Phone"),"","Phone: "+Eval("Phone"))%></i></span>
                        <span><i><%# IfEmpty(Eval("Fax"),"","Fax: "+Eval("Fax"))%></i></span>
                        <a style='<%# Convert.ToString(Eval("Street"))==""?"display:none":""%>' target="_blank" href='<%# "https://maps.google.com/maps?saddr="+ Eval("Street")+" "+Eval("City")+" "+Eval("State")+" "+Eval("Zip")%>'><i>Directions</i></a>
  <script type="text/javascript">
    AddToMap('<%# Eval("Street") +", "+Eval("City")+" "+Eval("State")+" "+ Eval("Zip")%>');
    </script>
                    </address>


