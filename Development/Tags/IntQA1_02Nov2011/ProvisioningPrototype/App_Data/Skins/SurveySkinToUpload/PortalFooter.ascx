<%@ Control %>
<%@ Register TagPrefix="inc" Namespace="Wilson.WebControls" Assembly="WilsonWebControls" %>
<%@ Import NameSpace="VisionCritical.PanelPlus"%>
<%
		string qs = Request.QueryString["t"];
		string testNode = "Portal";
		if(qs == "1")
		{
			testNode = "PortalStaging";
		}
	%>
	</td><!-- end contentBox cell -->
	</tr>
	<tr>
    <td id="footer">      
        <div id="VC-logo"><a href="http://www.visioncritical.com" target="_blank">vision critical</a></div> 
        	<ul>  
				<li><a href="<%=AppSettings.Context.BaseUrl%><%=testNode%>/privacy-policy.aspx" title="Privacy Policy" target="_blank">Privacy Policy</a></li>
				<li>|</li>
				<li><a href="<%=AppSettings.Context.BaseUrl%><%=testNode%>/web-terms.aspx" title="Web Terms" target="_blank">Web Terms</a></li>
				<li>|</li>
				<li><a href="<%=AppSettings.Context.BaseUrl%><%=testNode%>/tech-support.aspx" title="Technical Support" target="_blank">Technical Support</a></li>
            </ul> 
			<div class="clear"></div>
		</td><!-- end footer-->
		</tr>
    </table><!-- end container-->
       
</body></html>