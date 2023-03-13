<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AperturaDia.aspx.vb" Inherits="AperturaDia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BIFConvenios - Apertura día</title>
    <%--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type=text/css rel=stylesheet>--%>
    <LINK href="<%= ResolveUrl("~/css/global.css") %>" type="text/css" rel="stylesheet" />
</head>

<body topmargin="0" leftmargin="0" rightmargin="0">
    <form id="form1" runat="server">
    <div>
        <Table ID="Table1" cellSpacing="0" cellPadding="0" width="100%" height="100%" border="0">
        <tr>
				<td align="center">
					<img src="images/Caption.jpg" width="210" height="51">
				</td>
		</tr>
        <tr>
            <td  valign="top" align="middle">
                <DIV ms_positioning="FlowLayout"><FONT face="Arial" color="red" size="4"><STRONG>El Sistema aún no ha sido actualizado.</STRONG></FONT></DIV>
            </td>
        </tr>
        </Table>
    
    </div>
    </form>
</body>
</html>
