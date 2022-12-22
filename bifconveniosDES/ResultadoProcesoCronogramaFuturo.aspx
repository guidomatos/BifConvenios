<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.ResultadoProcesoCronogramaFuturo" CodeFile="ResultadoProcesoCronogramaFuturo.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="controls/Banner.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>BIFConvenios - Reporte de Carga y proceso</title>
	<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
	<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
	<meta content="JavaScript" name="vs_defaultClientScript" />
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
	<link href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet" />
	<script language="javascript" type="text/javascript" src="<%Response.Write(Request.ApplicationPath)%>/js/global.js"></script>
              

    <link href="css/StylePopup.css" rel="stylesheet" type="text/css" />        
    <link href="css/style.css" rel="stylesheet" type="text/css" />
                    
    <style type="text/css">    
    
        .ui-dialog-title
        {
            font-size:14px;
        }
     
        .ui-button-text
        {
            font-size:14px;
        }

        .ui-dialog-content
        {
            font-size:12px;
        }
        
        .FondoAplicacion
        {
	        background-color: Gray;
	        filter: alpha(opacity=70);
	        opacity: 0.7;
        } 
        
        .filtro
        {
          /*font-weight: bold;
          font-size: 11px;*/
        
            color: white;
            font-family:  Arial, Helvetica;
            /*background-color: #003366;*/
            border-right : 1px solid #7F7F7F;
            border-top : 1px solid #7F7F7F;
            border-bottom : 1px solid #7F7F7F;
            border-left : 1px solid #7F7F7F;                   
        }
        
        #container
        {
            display:table;
            border:dashed 1px gray;
            margin: 30px 30px;
        }
        .row
        {
            display:table-row;
        }
        .cell
        {
            display:table-cell;
        }
        .containercell
        {
            padding:1px;
        }
        .search
        {
            display:table;
            width:100%;
            height:30px;
        }
        
        .searchborderbotones
        {
            border: 1px solid black;
            padding: 5px;
        }
        .searchborder
        {
            border: 1px solid black;
            padding: 5px;
        }
        .button
        {
            font-weight:bold;
            color:#FFFFFF;
            background-color:#555555;
            border-style:solid;
            border-color:#000000;
            border-width:1px;
            height:25px;
        }
        .textbox 
        {        
            padding:4px 4px 4px 22px;
            border:1px solid #CCCCCC;  
            height:25px;      
        }
        .label
        {
            padding:4px 4px 4px 22px;         
            height:25px;  
        }
        
    </style>

	<script type="text/javascript">				 
	    
	    function GenerarArchivo (id, nombre, anio, mes, fechaProcesoAS400){			    
		    var returnValue = OpenFormatPageDialog('selectFileFormat.aspx?id='+ id +"&nombre=" + nombre + "&anio=" + anio + "&mes=" + mes + "&fechaProcesoAS400=" + fechaProcesoAS400, 250,580 );
		    if ( fctTrim(returnValue) != '' ){ 
			    document.all('hdId').value = returnValue;
			    __doPostBack('lnkGenerarArchivo','');
		    }
	    }
	    
	    function GenerarReporte(id, nombre, anio, mes, fechaProcesoAS400){
		    var returnValue = OpenFormatPageDialog('selectFilterReport.aspx?id='+ id +"&nombre=" + nombre + "&anio=" + anio + "&mes=" + mes + "&fechaProcesoAS400=" + fechaProcesoAS400, 250,500);
		
		    if ( fctTrim(returnValue) != '' ){ 			
			    document.all('hdFilter').value = returnValue;
			    __doPostBack('lnkObtenerReporte','');								
		    }
	}				
	
	/* MOD NCA 08/07/2014 EA2013-273 OPT. PROCESOS CONVENIOS
		function EnviarReporte(id, formatoArchivo, tipoCliente){
			openPage( 'consultas/ContainerListadoConsultaEnvio.aspx?export=1&idp='+ id , 430,500 );
		}
	//END
    */			
    /* ADD NCA 08/07/2014 EA2013-273 OPT. PROCESOS CONVENIOS */
		function EnviarReporte(id, formatoArchivo, tipoCliente){
			document.all('hdId').value = id;
			__doPostBack('lnkEnviarMail','');
		}
	/* END */	
	
	function OpenFormatPageDialog(url, height , width ) {
				var returnValue = window.showModalDialog(url,'', 'dialogTop: 200px; dialogLeft: 200px;dialogWidth:' + width +  'px;dialogHeight:' + height+ 'px;status: no;help:no;'); 
				if (typeof (returnValue) == "undefined"){
					returnValue = '';
				}
				return returnValue;
		}						
    -->                

</script>

    <script type="text/javascript">

        function validar()
        {
        
            //pagare = document.Form1.paga.value                        
            
            
                if ( fctTrim (document.all("txtPagare").value ) == '' ) {
				    alert ('Ingrese el número de pagaré');
				    document.all("txtPagare").focus();
				    args.IsValid = false;
				    return;
			    }
				
	            if ( fctTrim (document.all("txtNombre").value ) == '' ) {
				    alert ('Ingrese el nombre');
				    document.all("txtNombre").focus();
				    args.IsValid = false;
				    return;
			    }							
				
			    if ( fctTrim (document.all("txtApellidoP").value ) == '' ) {
				    alert ('Ingrese el Apellido Paterno');
				    document.all("txtApellidoP").focus();
				    args.IsValid = false;
				    return;
			    }							
				
			    if ( fctTrim (document.all("txtApelllidoM").value ) == '' ) {
				    alert ('Ingrese el Apellido Materno');
				    document.all("txtApelllidoM").focus();
				    args.IsValid = false;
				    return;
			    }							
				
			    if ( fctTrim (document.all("txtDocumento").value ) == '' ) {
				    alert ('Ingrese el Documento de Identidad');
				    document.all("txtDocumento").focus();
				    args.IsValid = false;
				    return;
			    }							
				
			    if ( fctTrim (document.all("txtDeuda").value ) == '' ) {
				    alert ('Ingrese el valor de la Deuda');
				    document.all("txtDeuda").focus();
				    args.IsValid = false;
				    return;
			    }							
				
			    if ( fctTrim (document.all("txtImporteNew").value ) == '' ) {
				    alert ('Ingrese el valor del Importe');
				    document.all("txtImporteNew").focus();
				    args.IsValid = false;
				    return;
			    }							
				
			    if ( fctTrim (document.all("txtMontoOriginal").value ) == '' ) {
				    alert ('Ingrese el valor del Monto Original');
				    document.all("txtMontoOriginal").focus();
				    args.IsValid = false;
				    return;
			    }							
				
			    if ( fctTrim (document.all("txtCuotaInformada").value ) == '' ) {
				    alert ('Ingrese el valor de Cuota Informada');
				    document.all("txtCuotaInformada").focus();
				    args.IsValid = false;
				    return;
			    }							
				
			    if ( fctTrim (document.all("txtCuota").value ) == '' ) {
				    alert ('Ingrese el valor de Cuota');
				    document.all("txtCuota").focus();
				    args.IsValid = false;
				    return;
			    }							
				
			    if ( fctTrim (document.all("txtCuotaPactada").value ) == '' ) {
				    alert ('Ingrese el valor de Cuota Pactada');
				    document.all("txtCuotaPactada").focus();
				    args.IsValid = false;
				    return;
			    }							
				
			    if ( fctTrim (document.all("txtCuotaPagada").value ) == '' ) {
				    alert ('Ingrese el valor de Cuota Pagada');
				    document.all("txtCuotaPagada").focus();
				    args.IsValid = false;
				    return;
			    }									
         
        }    
	
        function OnKeyPressTextNumeros(evt)
        {                    
            var charCode = (evt.wich)? evt.wich : event.keyCode
            if(charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
	       
           return true;
        }	
			
			
        function OnKeyPressTextDecimales(evt)
        {                    
            var charCode = (evt.which)? evt.which : event.keyCode
            
            if(charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
	       
           return true;
        }	
    </script>
</head>	
<body style="margin-left:0; margin-top:0;">	
	<form id="Form1" method="post" runat="server">
	    <table style="border:0; width:100%;" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" runat="server" Title="Resultado de Carga y Proceso"></uc1:Banner>
                </td>
            </tr>
        </table>    
        <asp:ScriptManager ID="scriptmn1" runat="server"></asp:ScriptManager>
	    <div id="container" style="width:1300px;">
            <div class="row">
                <div class="cell containercell">
                    <div class="searchborder">
                        <div class="search" style="width:100%; background-color:#e6f5ff;">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <div class="row">
                                            <div class="cell">
                                                <table>
                                                    <tr>
                                                        <td style="width:100px; height: 27px;" align="right">
                                                            <asp:Label ID="lblTitleEmpresa" runat="server" Width="100" Text="Empresa:" Font-Bold="true" />
                                                        </td>
                                                        <td style="width:500px; height: 27px;">
                                                            <asp:literal id="ltrlCliente" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <table>
                                                    <tr>
                                                        <td style="width:100px; height: 27px;" align="right">
                                                            <asp:Label ID="lblTitleDocumento" runat="server" Width="100" Text="Documento:" Font-Bold="true"/>
                                                        </td>
                                                        <td style="width:300px; height: 27px;">
                                                            <asp:literal id="ltrlDocumento" runat="server" />
                                                        </td>
                                                        <td style="width:80px; height: 27px;" align="right">
                                                            <asp:Label ID="lblTitlePeriodo" runat="server" Width="80" Text="Periodo:" Font-Bold="true" />
                                                        </td>
                                                        <td style="width:120px; height: 27px;">
                                                            <asp:literal id="ltrlPeriodo" runat="server" />
                                                        </td>                                                        
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <table>
                                                    <tr>
                                                        <td style="width:100px; height: 27px;" align="right">
                                                            <asp:Label ID="lblTitleEstado" runat="server" Width="100" Text="Estado:" Font-Bold="true" />
                                                        </td>                                                        
                                                        <td style="width:300px; height: 27px;">
                                                            <asp:literal id="ltrlEstado" runat="server" />
                                                        </td>
                                                        <td style="width:80px; height: 27px;" align="right">
                                                            <asp:Label ID="lblTitleFechaCarga" runat="server" Width="80" Text="Fec. Carga:" Font-Bold="true" />
                                                        </td>
                                                        <td style="width:120px; height: 27px;">
                                                            <asp:literal id="ltrlProcesoAS400" runat="server" Text="" />
                                                        </td>                                                        
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>                                    
                                    </td>
                                    <td>
                                        <div class="row">
                                            <div class="cell">
                                                <table>
                                                    <tr>
                                                        <td style="width:100px; height: 27px;" align="right">
                                                            <asp:Label ID="lblTitleTrabajador" runat="server" Text="Trabajador:" Font-Bold="true" />
                                                        </td>
                                                        <td style="width:190px; height: 27px;">
                                                            <asp:TextBox ID="txtFiltroTrabajador" runat="server" Width="190px" />
                                                        </td>                                                
                                                        <td style="width:100px; height: 27px;" align="right">
                                                            <asp:Label ID="lblTitleDocTrabajador" runat="server" Text="Num. Doc.:" Width="100" Font-Bold="true" />
                                                        </td>
                                                        <td style="width:120px; height: 27px;">
                                                            <asp:TextBox ID="txtFiltroDocumento" runat="server" Width="120px" />
                                                        </td>                                                        
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <table>
                                                    <tr>
                                                        <td style="width:100px; height: 27px;" align="right">
                                                            <asp:Label ID="lblTitleEstadoTrabajador" runat="server" Text="Estado:" Font-Bold="true"/>
                                                        </td>
                                                        <td style="width:190px; height: 27px;">
                                                            <asp:DropDownList Runat="server" id="ddlEstadoTrabajador" DataTextField="nombreCortoFlag" DataValueField="dlst" Width="190" />
                                                        </td>
                                                        <td style="width:100px; height: 27px;" align="right">
                                                            <asp:Label ID="lblTitlePagare" runat="server" Text="Pagaré:" Width="100" Font-Bold="true" />
                                                        </td>
                                                        <td style="width:120px; height: 27px;">
                                                            <asp:TextBox ID="txtFiltroPagare" runat="server" Width="120px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="cell">
                                                <table>
                                                    <tr>
                                                        <td style="width:100px; height: 27px;" align="right">
                                                            <asp:Label ID="lblTitleZonaUse" runat="server" Text="Zona Use:" Font-Bold="true"/>
                                                        </td>
                                                        <td style="width:190px; height: 27px;">
                                                            <asp:DropDownList Runat="server" id="ddlZonaUse" Width="190" />
                                                        </td>
                                                        <td style="width:100px; height: 27px;" align="right">                                                            
                                                        </td>
                                                        <td style="width:120px;" align="left">
                                                            <asp:Button ID="btnBuscar" runat="server" Text="Filtrar" CssClass="button"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </td>
                                </tr>                                
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="cell container">
                    <div class="searchborder">
                        <div class="search" style="width:100%; background-color:#e6f5ff;">
                            <table class="box">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="2" cellspacing="0" class="box">
                                            <thead>	
                                                <tr>
                                                    <th>                        
                                                        <img src="<%=Request.ApplicationPath%>/images/bar_begin.gif" height="17" alt="" />
                                                    </th>
                                                    <th>
	                                                    <a href='javascript:GenerarArchivo("<%=Request.Params("id")%>","<asp:literal id="ltrlNombre" runat="server"/>","<asp:literal id="ltrlAnhio" runat="server"/>","<asp:literal id="ltrlMes" runat="server"/>","<asp:literal id="ltrlFechaIBS" runat="server"/>");'>
		                                                    Generar archivo
		                                                </a>
	                                                </th>
                                                    <th>
                                                        <img src="<%=Request.ApplicationPath%>/images/bar_div.gif" width="17" height="18" alt="" />
                                                    </th>
                                                    <th>
                                                        <a href='javascript:GenerarReporte("<%=Request.Params("id")%>","<asp:literal id="ltrlNombre1" runat="server"/>","<asp:literal id="ltrlAnhio1" runat="server"/>","<asp:literal id="ltrlMes1" runat="server"/>","<asp:literal id="ltrlFechaIBS1" runat="server"/>");'>
                                                            Descargar reporte
                                                        </a>
                                                        <asp:LinkButton Runat="server" ID="lnkObtenerReporte" />
                                                        <input id="hdFilter" type="hidden" name="hdFilter" runat="server"/>
                                                    </th>
                                                    <th>
                                                        <img src="<%=Request.ApplicationPath%>/images/bar_div.gif" width="17" height="18" alt="" />
                                                    </th>																								
                                                    <th>
                                                        <asp:LinkButton Runat="server" ID="LnkNuevo" Text="Nuevo Cliente" />
                                                        <input id="HdnGrabar" type="hidden" name="HdnGrabar" runat="server"/>
                                                        <asp:ModalPopupExtender ID="MdlPupTrabajador" runat="server" BackgroundCssClass="FondoAplicacion" Enabled="true" PopupControlID="pnlDialog" TargetControlID="HdnGrabar">
                                                        </asp:ModalPopupExtender>
                                                    </th>
                                                    <th>
		                                                <img src="<%=Request.ApplicationPath%>/images/bar_begin.gif"  height="17" alt="" />
	                                                </th>
                                                </tr>																		                            
                                            </thead>
                                        </table>
                                    </td>                                        
                                    <th>								                                    
                                        <asp:Label ID="lblMensajeImp" runat="server" ForeColor="Red" />
                                        <asp:TextBox ID="importeAnterior" runat="server" Visible="False" />
                                    </th>
                                    <td class="SubHead" style="height: 19px">Total &nbsp; &nbsp; de Registros&nbsp;:
                                        <asp:label CssClass="Text" id="lblTotalReg" Runat="server"></asp:label>
                                    </td>
                                    <td style="height: 19px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td class="SubHead" style="height: 19px">Monto &nbsp; &nbsp; &nbsp;Total Soles&nbsp;: &nbsp;&nbsp;
                                    </td>
                                    <td style="height: 19px">
                                        <asp:Label Runat="server" ID="lblMontoTotalSoles" CssClass="Text"></asp:Label>
                                    </td>
                                    <td style="height: 19px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    <td class="SubHead" style="height: 19px">Monto &nbsp; &nbsp; &nbsp;Total Dólares&nbsp;: &nbsp;&nbsp;</td>
                                    <td style="height: 19px"><asp:Label Runat="server" ID="lblMontoTotalDolares" CssClass="Text"></asp:Label></td>					
                                    <td style="width:30px;"></td>
                                    <td colspan="2">
                                        <asp:linkbutton id="lnkBack" Runat="server"><img src='/BIFConvenios/images/regresar.jpg' name='Image1' border="0" alt='Regresar' /></asp:linkbutton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <asp:Panel ID="pnlQueryResult" runat="server" Visible="true">
                <div class="row">
                    <div class="cell container">
                        <asp:UpdatePanel ID="upQuery" runat="server">
                            <ContentTemplate>
                                <asp:datagrid id="dgProcesoResult" runat="server" Visible="False" CellPadding="3" CellSpacing="3" 
                                        BackColor="white" ForeColor="black"
						                BorderWidth="1px" AutoGenerateColumns="False" Width="100%" DataKeyField="DLCC" PageSize="500"
						                OnClick="return Eliminar();">
				
					                <PagerStyle BackColor="#999999" ForeColor="black" HorizontalAlign="center" />
                                    <SelectedItemStyle BackColor="#000099" Font-Bold="true" ForeColor="white" />
                                    <HeaderStyle BackColor="black" Font-Bold="true" ForeColor="white" />
                                    <AlternatingItemStyle BackColor="#CCCCCC" />
					
					                <Columns>
					                    <asp:TemplateColumn HeaderText="Codigo Empresa" Visible="false">
				                            <ItemTemplate>
				                                <asp:Label ID="lblCodigoCliente" runat="server" Text='<%# Bind("DLCC") %>'></asp:Label>
				                            </ItemTemplate>
				                            <EditItemTemplate>
				                                <asp:Label ID="lblDLCC" runat="server" Text='<%# Bind("DLCC") %>'></asp:Label>
				                            </EditItemTemplate>
				                        </asp:TemplateColumn>
					
					                    <asp:TemplateColumn HeaderText="Convenio" ItemStyle-Width="85px">
					                        <ItemTemplate>
								                <%#(DataBinder.Eval(Container.DataItem, "Convenio").ToString())%>
							                </ItemTemplate>
					                    </asp:TemplateColumn>
					    
					                    <asp:TemplateColumn HeaderText="Número Pagar&#233">
				                            <ItemTemplate>
				                                <asp:Label ID="lblPagare" runat="server" Text='<%# Bind("DLNP") %>'></asp:Label>
				                            </ItemTemplate>
				                            <EditItemTemplate>
				                                <asp:Label ID="lblDLNP" runat="server" Text='<%# Bind("DLNP") %>'></asp:Label>
				                            </EditItemTemplate>
				                        </asp:TemplateColumn>
				       
				                        <asp:TemplateColumn HeaderText="Fecha Desembolso" ItemStyle-Width="80px">
					                        <ItemTemplate>															
								                <asp:Label ID="lblFecDesembolso" runat="server" Text='<%# Bind("FechaDesembolso","{0:d}") %>'></asp:Label>
							                </ItemTemplate>
					                    </asp:TemplateColumn>
                                        				       
				                        <asp:TemplateColumn HeaderText="Tipo Crédito" ItemStyle-Width="80px"  ItemStyle-HorizontalAlign="Center">
					                        <ItemTemplate>
								                <%#(DataBinder.Eval(Container.DataItem, "TipoCuenta").ToString())%>
							                </ItemTemplate>
					                    </asp:TemplateColumn>					
					    
					                    <asp:TemplateColumn HeaderText="Nro. Documento" ItemStyle-Width="80px">
					                        <ItemTemplate>
								                <%#(DataBinder.Eval(Container.DataItem, "NroDocumento").ToString())%>
							                </ItemTemplate>
					                    </asp:TemplateColumn>
					    
					                    <asp:TemplateColumn HeaderText="C&#243;digo Trabajador" ItemStyle-Width="80px">
					                        <ItemTemplate>
								                <%#(DataBinder.Eval(Container.DataItem, "DLCM").ToString())%>
							                </ItemTemplate>
					                    </asp:TemplateColumn>
					    
					                    <asp:TemplateColumn HeaderText="Nombre del Trabajador">
				                            <ItemTemplate>
				                                <asp:Label ID="lblTrabajador" runat="server" Text='<%# Bind("DLNE") %>'></asp:Label>
				                            </ItemTemplate>
				                            <EditItemTemplate>
				                                <asp:Label ID="lblDLNE" runat="server" Text='<%# Bind("DLNE") %>'></asp:Label>											                
				                            </EditItemTemplate>										            									
				                        </asp:TemplateColumn>											       											       											       
				       
					                    <asp:TemplateColumn HeaderText="C&#243;digo Referencia" ItemStyle-Width="80px" Visible="false">
					                        <ItemTemplate>
								                <%#(DataBinder.Eval(Container.DataItem, "DLCR").ToString())%>
							                </ItemTemplate>
					                    </asp:TemplateColumn>
						
						                <asp:TemplateColumn HeaderText="Periodo" ItemStyle-Width="70px" Visible="false">
							                <ItemTemplate>
								                <%# ( BIFConvenios.Periodo.GetMonthByNumber( DataBinder.Eval(Container, "DataItem.DLMP")) + "&nbsp;"+ DataBinder.Eval(Container.DataItem, "DLAP").ToString() )%>
							                </ItemTemplate>
						                </asp:TemplateColumn>
						
						                <asp:TemplateColumn HeaderText="Moneda" ItemStyle-Width="80px">
					                        <ItemTemplate>
								                <%#(DataBinder.Eval(Container.DataItem, "DLMO").ToString())%>
							                </ItemTemplate>
					                    </asp:TemplateColumn>
					    
					                    <asp:TemplateColumn HeaderText="Monto Original">											        
				                            <ItemTemplate>
				                                <asp:Label ID="lblMontoOriginal" runat="server" Text='<%# Bind("MontoOriginal") %>'></asp:Label>
				                            </ItemTemplate>
				                        </asp:TemplateColumn>
				        
				                        <asp:TemplateColumn HeaderText="Cuotas Pactadas" ItemStyle-Width="80px"  ItemStyle-HorizontalAlign="Center">
					                        <ItemTemplate>
					                            <asp:Label ID="lblCuotaPactada" runat="server" Text='<%# Bind("CuotaPactadas") %>'></asp:Label>															
							                </ItemTemplate>
							                <EditItemTemplate>
				                                <asp:TextBox ID="txtCuotaPactada" runat="server" Width="100px" MaxLength="5" Text='<%# Bind("CuotaPactadas") %>' onkeypress="return OnKeyPressTextNumeros(event)"></asp:TextBox>
				                            </EditItemTemplate>
					                    </asp:TemplateColumn>
					    											
						                <asp:TemplateColumn HeaderText="Cuotas Pagadas" ItemStyle-Width="80px"  ItemStyle-HorizontalAlign="Center">
					                        <ItemTemplate>
					                            <asp:Label ID="lblCuotaPagada" runat="server" Text='<%# Bind("CuotaPagadas") %>'></asp:Label>																														
							                </ItemTemplate>
							                <EditItemTemplate>
				                                <asp:TextBox ID="txtCuotaPagada" runat="server" Width="100px" MaxLength="5" Text='<%# Bind("CuotaPagadas") %>' onkeypress="return OnKeyPressTextNumeros(event)"></asp:TextBox>
				                            </EditItemTemplate>										            									
					                    </asp:TemplateColumn>
						
						                <asp:TemplateColumn HeaderText="Cuotas Pendientes" ItemStyle-Width="80px"  ItemStyle-HorizontalAlign="Center">
					                        <ItemTemplate>
						                        <asp:Label ID="lblCuotaPendiente" runat="server" Text='<%# Bind("CuotaPendientes") %>'></asp:Label>
							                </ItemTemplate>
					                    </asp:TemplateColumn>
					    
				                        <asp:TemplateColumn HeaderText="Cuotas Informadas" ItemStyle-Width="80px"  ItemStyle-HorizontalAlign="Center" Visible="false">
					                        <ItemTemplate>
								                <%#(DataBinder.Eval(Container.DataItem, "CuotaInformada").ToString())%>
							                </ItemTemplate>
					                    </asp:TemplateColumn>
					    
					                    <asp:TemplateColumn HeaderText="Fecha Cargo" ItemStyle-Width="80px">
					                        <ItemTemplate>
								                <asp:Label ID="lblFecCargo" runat="server" Text='<%# Bind("FechaCargo","{0:d}") %>'></asp:Label>
							                </ItemTemplate>
					                    </asp:TemplateColumn>
					    
					                    <asp:TemplateColumn HeaderText="Importe Cuota" ItemStyle-Width="80px">
					                        <ItemTemplate>
								                <asp:Label ID="lblImporte" runat="server" Text='<%# Bind("DLIC") %>'></asp:Label>
							                </ItemTemplate>
							                <EditItemTemplate>
				                                <asp:TextBox ID="txtImporte" runat="server" Width="100px" MaxLength="8" Text='<%# Bind("DLIC") %>' onkeypress="return OnKeyPressTextDecimales(event)"></asp:TextBox>
				                            </EditItemTemplate>
					                    </asp:TemplateColumn>
						
						                <asp:TemplateColumn HeaderText="Estado" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
					                        <ItemTemplate>
								                <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("EstadoDeuda") %>'></asp:Label>
							                </ItemTemplate>
							                <EditItemTemplate>
				                                <asp:Label ID="lblEditEstado" runat="server" Text='<%# Bind("EstadoDeuda") %>'></asp:Label>
				                            </EditItemTemplate>
					                    </asp:TemplateColumn>
				        
		                                <asp:EditCommandColumn CancelText="Cancelar" EditText="Editar" UpdateText="Actualizar"></asp:EditCommandColumn>
				                        <asp:ButtonColumn CommandName="Eliminar" Text="Eliminar"></asp:ButtonColumn>
						
					                </Columns>
					                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Left" Position="TopAndBottom" CssClass="CommandButton" Mode="NumericPages"></PagerStyle>
				                </asp:datagrid>            
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <input id="hdId" type="hidden" name="hdId" runat="server"/>
		                <asp:LinkButton id="lnkGenerarArchivo" runat="server"></asp:LinkButton>                
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel Runat="server" ID="pnlMensaje" Visible="False">		
			    <table cellspacing="0" cellpadding="10" border="0">
				    <tr>
					    <td>
						    <asp:Label id="lblMensajeSistema" Runat="server" Font-Bold=true ForeColor="red" Font-Size="14"></asp:Label>
					    </td>
				    </tr>
			    </table>
		    </asp:Panel>
            <asp:Panel Runat="server" ID="pnlGenArchivos" Visible="False">		
			    <table cellspacing="0" cellpadding="10" border="0">
				    <tr>
					    <td>
						    <asp:Label id="lblMensaje" Runat="server" CssClass="SubHead"></asp:Label>
					    </td>
				    </tr>
			    </table>
			
			    <script type="text/javascript">						
				    //openPage('/BIFConvenios/generacionCf/enviomail.aspx?idp=<%=idP%>&formatoArchivo=<%=formatoArchivo%>&situacionTrabajador=<%=situacionTrabajador%>&modalidad=<%=modalidad%>', 430,500 );
					openPage('<%Response.Write(Request.ApplicationPath)%>/generacionCf/EsperaFinalGeneracionCf.aspx?id=<%=idGenFile%>&cantidad=<asp:literal id="lblTotalReg1" runat="server"/>&montosoles=<asp:literal id="lblMontoTotalSoles1" runat="server"/>&montodolares=<asp:literal id="lblMontoTotalDolares1" runat="server"/>', 300, 390);
			    </script>
		    </asp:Panel>
		    <asp:Panel Runat="server" ID="pnlGenReporte" Visible="False">
			    <table cellspacing="0" cellpadding="10" border="0">
				    <tr>
					    <td>
						    <asp:Label id="lblMensaje1" Runat="server" CssClass="SubHead"></asp:Label>
						</td>
				    </tr>
			    </table>
                <%RedirectReporte(idP, modalidad, situacionTrabajador)%>
		    </asp:Panel>    
        </div>
	</form>
</body>
</html>
