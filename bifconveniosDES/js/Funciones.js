// Archivo JScript

function ColorearFila(obj)
{
	obj = document.getElementById(obj);
	obj.style.backgroundColor = '#B0D8FF';
}

function RestaurarFila(obj,Tipo)
{
	if (Tipo==1){	
		obj = document.getElementById(obj);
		obj.style.backgroundColor = '#FEFEFE';
	}else{
		obj = document.getElementById(obj);
		obj.style.backgroundColor = '#F3F5FB';
	}
}


function OnKeyPressTextLetras()		
{
	var e_k = event.keyCode;				
	if (e_k >= 97 && e_k <= 122 || e_k == 32 || e_k >= 65 && e_k <= 90 || e_k == 241 || e_k == 209)
	{				
		event.returnValue = true; //97-122 minusculas, 65-90 Mayusculas, 32-espacioblanco, 241-ñ, 209-Ñ
	}
	else
	{
		event.returnValue = false;
	}
}
		
function OnKeyPressTextNumeros()
{
	var e_k = event.keyCode;			
	if (e_k >= 48 && e_k <= 57) //||  e_k ==46) //48-57 es 0 al 9
	{				
		event.returnValue = true;
	}
	else
	{
		event.returnValue = false;
	}
}
    
    
function OnKeyPressTextLetrasConvierteMayusculas()		
{
	var e_k = event.keyCode;				
	if (e_k >= 97 && e_k <= 122 || e_k == 32 || e_k >= 65 && e_k <= 90 || e_k == 241 || e_k == 209)	
	{	
		if (e_k >= 97 && e_k <= 122)
		{
			event.keyCode = event.keyCode - 32;
		}			
		event.returnValue = true; //97-122 minusculas, 65-90 Mayusculas, 32-espacioblanco, 241-ñ, 209-Ñ
	}
	else
	{
		event.returnValue = false;
	}
}
        

function OnKeyPressTextDecimales(Text)
{
	var e_k = event.keyCode;						
	if (e_k >= 48 && e_k <= 57) //48-57 es 0 al 9
	{				
		event.returnValue = true;
	}
	else
	{	
		if (e_k == 46) 
		{						
			if (Text.value != "")
			{						
				indicepunto = Text.value.indexOf('.');					
				if ((indicepunto != -1))  //e_k=46 es el '.'
				{					
					event.returnValue = false;
				}
				else
				{
					event.returnValue = true;
				}
			}
			else
			{				
				event.returnValue = false;				
			}				
		}
		else
		{
			event.returnValue = false;
		}			
	}
}


function OnKeyPressTextDecimalesconComa(Text)
{

	var e_k = event.keyCode;						
	
	
	if (e_k >= 48 && e_k <= 57) //48-57 es 0 al 9
	{				
		event.returnValue = true;
	}
	else
	{	
		if (e_k == 44) 
		{						
			if (Text.value != "")
			{						
				indicepunto = Text.value.indexOf(',');					
				if ((indicepunto != -1))  //e_k=44 es el ','
				{					
					event.returnValue = false;
				}
				else
				{
					event.returnValue = true;
				}
			}
			else
			{				
				event.returnValue = false;				
			}				
		}
		else
		{
			event.returnValue = false;
		}			
	}
}


function OnKeyPressTextHora(Text)
{
	var e_k = event.keyCode;						
		if (e_k >= 48 && e_k <= 57) //48-57 es 0 al 9
	{				
		event.returnValue = true;
	}
	else
	{	
		if (e_k == 58) 
		
		{						
			if (Text.value != "")
			{						
				indicepunto = Text.value.indexOf(':');					
				if ((indicepunto != -1))  //e_k=46 es el '.'
				{					
					event.returnValue = false;
				}
				else
				{
					event.returnValue = true;
				}
			}
			else
			{				
				event.returnValue = false;				
			}				
		}
		else
		{
			event.returnValue = false;
		}			
	}
}


function OnKeyPressTextNumerosLetras(CaracterPermitido)
{
	var e_k = event.keyCode;						
	if (e_k >= 48 && e_k <= 57 || e_k >= 97 && e_k <= 122 || e_k >= 65 && e_k <= 90 || e_k == 241 || e_k == 209)	
	{				
		event.returnValue = true;
	}
	else
	{
		if (String.fromCharCode(e_k)==CaracterPermitido)
		{				
			event.returnValue = true;
		}
		else
		{	
			event.returnValue = false;
		}
	}
}


function OnKeyPressTextNumerosBinarios(Text)
{
	var e_k = event.keyCode;
	if (String.fromCharCode(e_k)=="0")
	{				
		event.returnValue = true;
	}
	else
	{	
		if (String.fromCharCode(e_k)=="1")
		{				
			event.returnValue = true;
		}
		else
			event.returnValue = false;
	}
}

function ValidaDecimal(Text)
{ 
	if (Text.value!="")
	{		
		indicepunto = Text.value.indexOf('.');					
		if (indicepunto != -1)
		{					
			if (Text.value.length <= 2)
			{
				alert("El Formato Decimal ingresado es incorrecto. Ej.50.22");
				Text.focus();
				return false;
			}
			else
			{
				if (Text.value.length-1==indicepunto) //El Punto Esta al Final
				{
					alert("El Formato Decimal ingresado es incorrecto. Ej.50.22");
					Text.focus();
					return false;
				}
				else
				{
					if (parseFloat(Text.value) == 0)
					{
						alert("El Valor ingresado no puede ser 0.");
						Text.focus();
						return false;
					}
				}
			}
				
		}
		else
		{
			if (parseFloat(Text.value) == 0)
			{
				alert("El Valor ingresado no puede ser 0.");
				Text.focus();
				return false;
			}
		}	
	}	
}	

function ValidaNroCaracteres(Text, Cantidad)
{	
	var e_k = event.keyCode;		
	if (Text.value.length>Cantidad-1)
	{		
		if ((e_k == 8) || (e_k >= 37 && e_k <= 40) || (e_k == 46))
			event.returnValue = true;
		else
			event.returnValue = false;
	}	
}

function ValidaCantidadCaracteres(Text, Cantidad)
{	
	if (Text.value.length>Cantidad-1)
	{		
		Text.value = Text.value.substring(0,Cantidad-1);
	}
}


