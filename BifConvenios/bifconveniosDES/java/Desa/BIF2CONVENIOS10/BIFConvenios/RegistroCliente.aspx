<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RegistroCliente.aspx.vb" Inherits="BIFConvenios.RegistroCliente" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register TagPrefix="uc1" TagName="Banner" Src="controls/Banner.ascx" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

        <title>Registro Cliente</title>    
        <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
	
	       
		<link href="css/global.css" rel="stylesheet" type="text/css" />
        <link href="css/tcal.css" rel="stylesheet" type="text/css" />
        <link href="css/style.css" rel="stylesheet" type="text/css" />
        <script src="js/tcal.js" type="text/javascript"></script>		
        <script src="js/global.js" type="text/javascript"></script>		

    
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
            #search
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
                width:150px;   
            }
            .label
            {
                padding:4px 4px 4px 22px;         
                height:25px;  
            }
            
            
            
        </style>      
      
    <script type="text/javascript">	
    

        function IsNumeric(valor) 

        { 

        var log=valor.length; var sw="S"; 

        for (x=0; x<log; x++) 

        { v1=valor.substr(x,1); 

        v2 = parseInt(v1); 

        //Compruebo si es un valor numérico 

        if (isNaN(v2)) { sw= "N";} 

        } 

        if (sw=="S") {return true;} else {return false; } 

        } 

        var primerslap=false; 

        var segundoslap=false; 

        function formateafecha(fecha) 

        { 

        var long = fecha.length; 

        var dia; 

        var mes; 

        var ano; 

        if ((long>=2) && (primerslap==false)) { dia=fecha.substr(0,2); 

        if ((IsNumeric(dia)==true) && (dia<=31) && (dia!="00")) { fecha=fecha.substr(0,2)+"/"+fecha.substr(3,7); primerslap=true; } 

        else { fecha=""; primerslap=false;} 

        } 

        else 

        { dia=fecha.substr(0,1); 

        if (IsNumeric(dia)==false) 

        {fecha="";} 

        if ((long<=2) && (primerslap=true)) {fecha=fecha.substr(0,1); primerslap=false; } 

        } 

        if ((long>=5) && (segundoslap==false)) 

        { mes=fecha.substr(3,2); 

        if ((IsNumeric(mes)==true) &&(mes<=12) && (mes!="00")) { fecha=fecha.substr(0,5)+"/"+fecha.substr(6,4); segundoslap=true; } 

        else { fecha=fecha.substr(0,3);; segundoslap=false;} 

        } 

        else { if ((long<=5) && (segundoslap=true)) { fecha=fecha.substr(0,4); segundoslap=false; } } 

        if (long>=7) 

        { ano=fecha.substr(6,4); 

        if (IsNumeric(ano)==false) { fecha=fecha.substr(0,6); } 

        else { if (long==10){ if ((ano==0) || (ano<1900) || (ano>2100)) { fecha=fecha.substr(0,6); } } } 

        } 

        if (long>=10) 

        { 

        fecha=fecha.substr(0,10); 

        dia=fecha.substr(0,2); 

        mes=fecha.substr(3,2); 

        ano=fecha.substr(6,4); 

        // Año no viciesto y es febrero y el dia es mayor a 28 

        if ( (ano%4 != 0) && (mes ==02) && (dia > 28) ) { fecha=fecha.substr(0,2)+"/"; } 

        } 

        return (fecha); 

        }
    
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
								
				if ( fctTrim (document.all("txtImporteNew").value ) == '' ) {
					alert ('Ingrese el valor del Importe Cuota');
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
				
				if ( fctTrim (document.all("txtFecha").value ) == '' ) {
					alert ('Ingrese la Fecha de Desembolso');
					document.all("txtFecha").focus();
					args.IsValid = false;
					return;
				}		
				
				if ( fctTrim (document.all("txtFecCargo").value ) == '' ) {
					alert ('Ingrese la Fecha de Cargo');
					document.all("txtFecCargo").focus();
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
        
        function ConvertirMayuscula(date)
        {
            date.value = date.value.toUpperCase();
            
            return date;
        }
        
        
    </script>					
        
</head>
<body style="margin-left:0; margin-top:0;" onload="MM_preloadImages('/BIFConvenios/images/regresar_on.jpg')">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <form method="post" runat="server"  action="#">
    
        
       <table style="border:0; width:100%;" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" runat="server" Title="Registro Nuevo Cliente"></uc1:Banner>
                </td>
            </tr>            
       </table>                
       
       
       <div id="container" style="width:800px;">
       
            <div class="row">
            
                <div class="cell containercell">
                     <div class="searchborder">                                
                        <div id="search1" style="background-color:#e6f5ff;">
                        
                            <div class="row">
                                <div class="cell">                                    
                                     <table>
                                        <tr>
                                            <td style="width:150px;" align="right">
                                                <asp:Label ID="lblTitleEmpresa" runat="server" Width="150" Text="Empresa:" />                                                            
                                            </td>
                                            <td style="width:650px;">
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
                                            <td style="width:150px;" align="right">
                                                <asp:Label ID="lblTitleDocumento" runat="server" Width="150" Text="Documento:" />                                                            
                                            </td>
                                            <td style="width:250px;">
                                                <asp:literal id="ltrlDocumento" runat="server" />
                                            </td>
                                            <td style="width:150px;" align="right">
                                                <asp:Label ID="lblTitleEstado" runat="server" Width="150" Text="Estado:" />                                                            
                                            </td>
                                            <td style="width:250px;">
                                                <asp:literal id="ltrlEstado" runat="server" />
                                            </td>
                                        </tr>
                                    </table>                                
                                </div>
                            </div>
                            
                            <div class="row">
                                 <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:150px;" align="right">
                                                <asp:Label ID="lblTitlePeriodo" runat="server" Width="150" Text="Periodo:" />
                                            </td>
                                            <td style="width:250px;">
                                                <asp:literal id="ltrlPeriodo" runat="server" />
                                            </td>
                                            <td style="width:150px;" align="right">
                                                <asp:Label ID="lblTitleFechaCarga" runat="server" Width="150" Text="Fecha de Carga:" />                                                            
                                            </td>
                                            <td style="width:250px;">
                                                <asp:literal id="ltrlProcesoAS400" runat="server" />
                                            </td>
                                        </tr>
                                    </table>                                         
                                 </div>
                            </div>
                            
                        </div>
                     </div>
                     
                     <br />                     
                     <br /> 
                     
                    <div id="dialog">                	        
                    
                         <table width="550px">    				
                    
				             <tr style="height:20px;">				                                          
				                <td>				                   
				                    <asp:Label id="lblPagare" Runat="server" Text="Número Pagaré(*):"></asp:Label>				             
				                </td>
				                <td>				                    
                                    <input id="txtPagare" type="text" runat="server"  style="border-color:Silver; border-bottom-width:1pt;" maxlength="12" class="textbox" onkeypress="return OnKeyPressTextNumeros(event)"/>			                                                                                    
				                </td>
				             </tr>
				             <tr>
				                <td>
				                    <asp:Label id="lblModular" Runat="server" Text="Código Trabajador:"></asp:Label>				             
				                </td>
				                <td>				                    
				                   <input id="TxtModular" type="text" runat="server"  style="border-color:Silver; border-bottom-width:1pt;" maxlength="20" class="textbox"/>			                                                                                    				                    
				                </td>
				             </tr>			
				             <tr>
				                <td>
				                    <asp:Label id="lblNombre" Runat="server" Text="Nombre (*):"></asp:Label>				             
				                </td>
				                <td>				                 
				                    <input id="txtNombre" type="text" runat="server" style="border-color:Silver; border-bottom-width:1pt;" maxlength="20" class="textbox" onblur="return ConvertirMayuscula(this)" />			                                                                                    				                    
				                </td>				        			        
				             </tr>
				             <tr>
				                <td>
				                     <asp:Label id="lblApellidoP" Runat="server" Text="Apellido Paterno (*):"></asp:Label>				             
				                </td>
				                <td>				                    
				                    <input id="txtApellidoP" type="text" runat="server"  style="border-color:Silver; border-bottom-width:1pt;" maxlength="50"  class="textbox" onblur="return ConvertirMayuscula(this)"/>			                                                                                    
				                </td>				     
				             </tr>
				             <tr>
				                <td>
			                        <asp:Label id="lblApellidoM" Runat="server" Text="Apellido Materno (*):"></asp:Label>				             
				                </td>
				                <td>				                    
				                    <input id="txtApelllidoM" type="text" runat="server"  style="border-color:Silver; border-bottom-width:1pt;" maxlength="50" class="textbox" onblur="return ConvertirMayuscula(this)"/>			                                                                                    
				                </td>
				             </tr>
				             <tr>
				                <td>
				                    <asp:Label id="lblDocumento" Runat="server" Text="Nro Documento (*):"></asp:Label>	
				                </td>
				                <td>				                    
				                    <input id="txtDocumento" type="text" runat="server"  style="border-color:Silver; border-bottom-width:1pt;" maxlength="15" class="textbox" />			                                                                                    
				                </td>
				             </tr>				            				             
				             <tr>
				                <td>
				                   <asp:Label id="lblFecha" Runat="server" Text="Fecha Desembolso (*):"></asp:Label>	
				                </td>
				                <td>				                    
				                    <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" onKeyUp = "this.value=formateafecha(this.value);" MaxLength="10"></asp:TextBox>				                    
                                    <cc1:CalendarExtender ID="CalFecDesembolso" runat="server" TargetControlID="txtFecha" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>                                                                                                            
				                </td>
				             </tr>				             				             
				             <tr>
				                <td>
				                    <asp:Label id="lblMontoOriginal" Runat="server" Text="Monto Original (*):"></asp:Label>	
				                </td>
				                <td>				                    
			                        <input id="txtMontoOriginal" type="text" runat="server"  style="border-color:Silver; border-bottom-width:1pt;" maxlength="8" onkeypress="return OnKeyPressTextDecimales(event)" class="textbox"/>			                                                                                    
				                </td>
				             </tr>				             
				             <tr>
				                <td>
				                    <asp:Label id="lblCuotaPactada" Runat="server" Text="n° Cuotas Pactadas (*):"></asp:Label>	
				                </td>
				                <td>				                    
			                      <input id="txtCuotaPactada" type="text" runat="server"  onkeypress="return OnKeyPressTextNumeros(event)" style="border-color:Silver; border-bottom-width:1pt;" maxlength="8" class="textbox"/>			                                                                                    
				                </td>
				             </tr>
				             <tr>
				                <td>
				                    <asp:Label id="lblCuotaPagada" Runat="server" Text="n° Cuotas Pagadas (*):"></asp:Label>	
				                </td>
				                <td>				                    
				                     <input id="txtCuotaPagada" type="text" runat="server" onkeypress="return OnKeyPressTextNumeros(event)" style="border-color:Silver; border-bottom-width:1pt;" maxlength="8" class="textbox"/>			                                                                                    
				                </td>
				             </tr>				             				             
				             <tr>
				                <td>
				                    <asp:Label id="lblFecCargo" Runat="server" Text="Fecha Cargo (*):"></asp:Label>	
				                </td>
				                <td>				                    				             				                    
                                    <asp:TextBox ID="txtFecCargo" runat="server" CssClass="textbox" onKeyUp = "this.value=formateafecha(this.value);" MaxLength="10"></asp:TextBox>				                                 			            
                                    <cc1:CalendarExtender ID="CalFecCargo" runat="server" TargetControlID="txtFecCargo" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>                                        				                    
                                    
				                </td>
				             </tr>				           				             
				             <tr>
				                <td>
				                    <asp:Label id="lblImporte" Runat="server" Text="Importe Cuota(*):"></asp:Label>	
				                </td>
				                <td>    			                    
    			                    <input id="txtImporteNew" type="text" runat="server"  style="border-color:Silver; border-bottom-width:1pt;" maxlength="8" onkeypress="return OnKeyPressTextDecimales(event)" class="textbox"/>			                                                                                    
				                </td>
				             </tr>				     				             
				             <tr>
				                <td>
				                    <asp:Label id="lblMensajeCampos" Runat="server" Text="Campos Obligatorios (*)" ForeColor="red"></asp:Label>	
				                </td>
				             </tr>
				             <tr align="center">				                
				                <td colspan="2">				                    				                    
                                    <input id="btnGrabarCliente" type="button" value="Guardar" onclick="validar();" runat="server" class="button"/>
				                    &nbsp;&nbsp
				                    <asp:Button ID="btnCerrar" runat="server" Text="Regresar"  CssClass="button"/>				                    
				                </td>				             
				             </tr>
				        </table>			
                    
                    </div> 
                     
                </div>        
            </div>
         </div> 
       
       
    </form>
</body>
</html>
