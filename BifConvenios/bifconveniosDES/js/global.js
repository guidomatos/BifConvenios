//Función para refrescar la cuenta de caracteres en un TEXTAREA
function fctRefrescarCuenta(strFormularioOrigen, strControlOrigen, strControlCuenta, intLongitud)
{	if(eval('document.' + strFormularioOrigen + '.' + strControlCuenta) != null && eval('document.' + strFormularioOrigen + '.' + strControlOrigen) != null)
    {   var strCadenaOrigen = new String(eval('document.' + strFormularioOrigen + '.' + strControlOrigen + '.value'));
	    var objControlCuenta = eval('document.' + strFormularioOrigen + '.' + strControlCuenta);
	    var objControlTexto = eval('document.' + strFormularioOrigen + '.' + strControlOrigen);
	
	    objControlCuenta.value = strCadenaOrigen.length;
    	
	    if (strCadenaOrigen.length > parseInt(intLongitud))
	    {	strCadenaOrigen = strCadenaOrigen.substring(0, parseInt(intLongitud));
		    objControlTexto.value = strCadenaOrigen;
    		
		    objControlCuenta.value = strCadenaOrigen.length;
	    }
    }
}

//Función para elminar los caracteres vacíos
function fctTrim(s) 
{   var m = s.match(/^\s*(\S+(\s+\S+)*)\s*$/);

    return (m == null) ? "" : m[1];
}


// funcion para abrir un elemento en una nueva ventana
function openPage( url, height , width ) {
	window.open(url,'','scrollbars=yes,width=' + width +  ',height=' + height );
}


function openDialog (url, height , width ) {
	//return window.showModalDialog(url, '', 'dialogHeight:' + height + 'px;dialogWidth:' + width + 'px;status:1;unadorned:1;help:0');

	if (window.showModalDialog) {
		window.showModalDialog(url, '', 'dialogHeight:' + height + 'px;dialogWidth:' + width + 'px;status:1;unadorned:1;help:0');
	} else {
		window.open(url, '',
			'height=' + height + 'px,width=' + width + 'px,toolbar=no,directories=no,status=no,continued from previous linemenubar = no, scrollbars = no, resizable = no, modal = yes');
	}

}


// funcion para establecer si es un numero 
function isnumber(name)
{	var ok = "yes";
	var temp;
	var valid = "0123456789.";
	var field = new String(name);
	for (var i=0; i<field.length; i++) 
	{	temp = "" + field.substring(i, i+1);
		if (valid.indexOf(temp) == "-1") ok = "no";
	}
	if (ok == "no") 
		return false;
   return true;
}


function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}




/*Funciones de seleccion del radio button*/
function getSelectedRadioValue(buttonGroup) {
	//retorna el valor del radio button seleccionado, o "" si no hay radio button seleccionado
		var i = getSelectedRadio(buttonGroup);
		if (i == -1) {
			return "";
		} else {
			if (buttonGroup[i]) { 
				return buttonGroup[i].value;
			} else { 
				return buttonGroup.value;
			}
		}
} 

function getSelectedRadio(buttonGroup) {
		// retorna la ubicacion en el array del radio button seleccionado o -1 si no hay boton seleccionado
		if (buttonGroup[0]) { 
			for (var i=0; i<buttonGroup.length; i++) {
				if (buttonGroup[i].checked) {
					return i
				}
			}
		} else {
			if (buttonGroup.checked) { return 0; } 
		}
		return -1;
} 

/*obtener un valor de una cadena con separadores*/

function getvalue(strData, intFieldNumber, separator)
{	var intCurrentField, intFoundPos, strValue, strNames;
	var bool = false;
	strNames = strData;
	intCurrentField = 0;
	while( (intCurrentField != intFieldNumber)&& !bool )
   	{
		intFoundPos = strNames.indexOf(separator);
		intCurrentField = intCurrentField + 1;
		if (intFoundPos != 0)
		{	strValue = strNames.substring(0,intFoundPos);
			strNames = strNames.substring(intFoundPos + 1, strNames.length);
		} 
		else
		{	if(intCurrentField == intFieldNumber)
				strValue = strNames;
			else
				strValue = "";

			bool = true;
		}
		
   	}
	if(strValue!="")
   		return strValue;
   	else
   		return strNames;
}

/*funciones para ocultar/mostrar objetos dentro de un DIV*/

function dummy(){
}
function ShowHide(obj){
	if(document.all(obj).className=='show'){
		document.all(obj).className='hide';
	}
	else{
		document.all(obj).className='show';
	}
}



/*****************************************************************/



function getSelectedCheckbox(buttonGroup) {
   // Go through all the check boxes. return an array of all the ones
   // that are selected (their position numbers). if no boxes were checked,
   // returned array will be empty (length will be zero)
   var retArr = new Array();
   var lastElement = 0;
   if (buttonGroup[0]) { // if the button group is an array (one check box is not an array)
      for (var i=0; i<buttonGroup.length; i++) {
         if (buttonGroup[i].checked) {
            retArr.length = lastElement;
            retArr[lastElement] = i;
            lastElement++;
         }
      }
   } else { // There is only one check box (it's not an array)
      if (buttonGroup.checked) { // if the one check box is checked
         retArr.length = lastElement;
         retArr[lastElement] = 0; // return zero as the only array value
      }
   }
   return retArr;
} // Ends the "getSelectedCheckbox" function

function getSelectedCheckboxValue(buttonGroup) {
   // return an array of values selected in the check box group. if no boxes
   // were checked, returned array will be empty (length will be zero)
   var retArr = new Array(); // set up empty array for the return values
   var selectedItems = getSelectedCheckbox(buttonGroup);
   if (selectedItems.length != 0) { // if there was something selected
      retArr.length = selectedItems.length;
      for (var i=0; i<selectedItems.length; i++) {
         if (buttonGroup[selectedItems[i]]) { // Make sure it's an array
            retArr[i] = buttonGroup[selectedItems[i]].value;
         } else { // It's not an array (there's just one check box and it's selected)
            retArr[i] = buttonGroup.value;// return that value
         }
      }
   }
   return retArr;
} // Ends the "getSelectedCheckBoxValue" function


// funcion para seleccionar todos los check de un grupo determinado 
/******************************** solo los checks habilitados *************************/

function setCheckBox (buttonGroup, bool) {
	if (buttonGroup[0]) { // if the button group is an array (one check box is not an array)
		for (var i=0; i<buttonGroup.length; i++) {
			if ( !buttonGroup[i].disabled) {
				buttonGroup[i].checked = bool;
			}
		}
	} 
	else { // There is only one check box (it's not an array)
		buttonGroup.checked = bool; // if the one check box is checked
	}
}

function waitMessage (img) {
	return '<center class="Subhead"><img src="' +img+ '" border=0><br><br>Procesando...</center>';
}