<%@ Control %>
<%@ Import NameSpace="VisionCritical.PanelPlus"%>
<%@ Register TagPrefix="inc" Namespace="Wilson.WebControls" Assembly="WilsonWebControls" %>
<%@ Register TagPrefix="vc" Namespace="VisionCritical.WebControls" Assembly="VisionCritical.WebControls" %>
<inc:ContentRegion id="Content" runat="server"></inc:ContentRegion>
<inc:ContentRegion id="PortalHeader" runat="server"></inc:ContentRegion>
<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
<form id="Form1" method="post" runat="server">
  <table cellSpacing="0" width="100%" height="100%" border="1" cellpadding="0">
    <tr>
      <td width="100%" colspan="3">
        <inc:ContentRegion id="i_LoginModule" runat="server"></inc:ContentRegion>
      </td>
    </tr>
    <tr>
      <td width="100%" colspan="3">
        <inc:ContentRegion id="i_HomeModule" runat="server"></inc:ContentRegion>
      </td>
    </tr>
    <tr>
      <td valign=top height="270px" width="350px">
        <inc:ContentRegion id="i_QuickPollModule" runat="server"></inc:ContentRegion>        
      </td>
       <td valign=top height="270px" width="320px">
        <inc:ContentRegion id="i_NewsLettersModule" runat="server"></inc:ContentRegion>
      </td>
      <td valign=top height="270px" width="320px">
        <inc:ContentRegion id="i_StudyLinksModule" runat="server"></inc:ContentRegion>
      </td>
    </tr>
  </table>
</form>
<inc:ContentRegion id="PortalFooter" runat="server"></inc:ContentRegion>