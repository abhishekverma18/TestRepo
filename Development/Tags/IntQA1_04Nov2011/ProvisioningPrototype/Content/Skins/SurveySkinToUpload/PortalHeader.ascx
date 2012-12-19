<%@ Control %>
<%@ Import NameSpace="VisionCritical.PanelPlus"%>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">

<html lang="<%=AppSettings.Context.Culture%>">
<head>
	<title><%= AppSettings.Context.Name%></title>
	
	<LINK href="<%=AppSettings.Context.SkinPath%>css/surveyLayout.css" type="text/css" rel="stylesheet">
	
	<script type="text/javascript" src="<%=AppSettings.Context.SkinPath%>js/jquery.js"></script>
	<!--[if lte IE 6]>
		<script type="text/javascript" src="<%=AppSettings.Context.SkinPath%>js/supersleight.plugin.js"></script>
		<script tyle="text/javascript">
			jQuery(document).ready(function() {
				jQuery('body').supersleight();
			});
		</script>
	<![endif]-->
	

	<script language=javascript>
	function ShowElement(divName)
	{
	   // hide them all 
	   document.getElementById('changeEmail').style.display='none';
	   document.getElementById('changePassword').style.display='none';
	   document.getElementById('changeUserName').style.display='none';
	   document.getElementById('resultMessageDiv').style.display='none';
	   
	   // show the one given
	   var divStyle = eval("document.getElementById('"+divName+"').style");
	   divStyle.display = 'block';
	}
	</script>
</head>

<body>

<table cellpadding="0" cellspacing="0" id="container" align="center">
    <tr>
		<td id="header-repeater">
		    <div id="header">
		        <span id="logo"><%=AppSettings.Context.Culture%> : <%=AppSettings.Context.Environment%></span>
		    </div>
	    </td>
    </tr>
    <tr>
		<td class="contentBox">