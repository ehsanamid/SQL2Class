/*
 * AUTHOR:
 * ----------------------------------------------------------------------------
 * Javascript developed by Paw Jershauge 
 *
 * Jan - Mar. 2008
 * 
 *
 * 
 * DISCLAME:
 * ----------------------------------------------------------------------------
 * Source code may only be used by employees working at Energi Danmark A/S
 * The source code is confidential, and may not be exportet from the company
 * unless direct permission has be given by the general manager of Energi Danmark A/S
 * 
 * 
 * 
 * LICENSE:
 * ----------------------------------------------------------------------------
 * This source code is coded by Paw Jershauge, all copyrights belong to Energi Danmark A/S
 * 
 * COPYRIGHT:
 * ----------------------------------------------------------------------------
 * Energi Danmark A/S
 * 2007-2009
 * 
 */
//--------------------------------------------------------------------------------------------------------------------------
//Site global variables
var contentshowing = true;
//--------------------------------------------------------------------------------------------------------------------------
function GetElement(strElement)
{
	if (document.getElementById)// IE5+, Netscape 6, Mozilla
		return (document.getElementById(strElement));
	else if (document.all)// IE4
		return (eval("document.all." + strElement));
	else if (document.layers)// Netscape 4
		return (eval("document." + strElement));
	else// Crap!
		return (null);
}
//--------------------------------------------------------------------------------------------------------------------------
function GetParentElement(strElement)
{
	if (document.getElementById)// IE5+, Netscape 6, Mozilla
		return (parent.document.getElementById(strElement));
	else if (document.all)// IE4
		return (eval("parent.document.all."+strElement));
	else if (boolNS4)// Netscape 4
		return (eval("document."+strElement));
	else// Crap!
		return (null);
}
//----------------------------------------------------------------------------------------------------------------------------------------------
function wo(url,target)
{
	var fr = new Date();
	if (url.indexOf("?") > -1){window.open(url + "&ForceRefresh=" + fr.getSeconds() + fr.getMinutes() + fr.getHours() + fr.getFullYear() + fr.getMonth() + fr.getDate(),target);}
	else{window.open(url + "?ForceRefresh=" + fr.getSeconds() + fr.getMinutes() + fr.getHours() + fr.getFullYear() + fr.getMonth() + fr.getDate(),target);}
	fr = null;
}
//--------------------------------------------------------------------------------------------------------------------------
function ChangeImage(ImageID,src)
{
objimg = GetElement(ImageID);
	if(objimg!=null)
		objimg.src = src;
}
//--------------------------------------------------------------------------------------------------------------------------
function divtgl(ID)
{
	obj = GetElement(ID);
	if (obj.style.display == 'none')
		obj.style.display = '';
	else
		obj.style.display = 'none';
}
//--------------------------------------------------------------------------------------------------------------------------
function codetgl(ID)
{
	obj = GetElement(ID);
	if (obj.style.display == 'none')
	{
		obj.style.display = '';
		ChangeImage(ID+'img','../objimg/Folder_open.png');
	}
	else
	{
		obj.style.display = 'none';
		ChangeImage(ID+'img','../objimg/Folder_closed.png');
	}
}
//--------------------------------------------------------------------------------------------------------------------------
function plusminustgl(ID)
{
	obj = GetElement(ID);
	if (obj.style.display == 'none')
	{
		obj.style.display = '';
		ChangeImage(ID+'img','../img/minus.gif');
	}
	else
	{
		obj.style.display = 'none';
		ChangeImage(ID+'img','../img/plus.gif');
	}
}
//--------------------------------------------------------------------------------------------------------------------------
function mainplusminustgl(ID)
{
	obj = GetElement(ID);
	if (obj.style.display == 'none')
	{
		obj.style.display = '';
		ChangeImage(ID+'img','img/minus.gif');
	}
	else
	{
		obj.style.display = 'none';
		ChangeImage(ID+'img','img/plus.gif');
	}
}
//--------------------------------------------------------------------------------------------------------------------------
function codetglsync()
{
	var idarr = new Array("cscode","vbcode","vjscode","vccode");
	obj = GetElement(idarr[0]);
	if (obj.style.display == 'none')
	{
		for(ix in idarr)
		{
			if(ShDiv(idarr[ix]))
				ChangeImage(idarr[ix]+'img','../objimg/Folder_open.png');
		}
	}
	else
	{
		for(ix in idarr)
		{
			if(HdDiv(idarr[ix]))
				ChangeImage(idarr[ix]+'img','../objimg/Folder_closed.png');
		}
	}
}
//--------------------------------------------------------------------------------------------------------------------------
function plusminustglcontentshowing()
{
	if(contentshowing)
		ChangeImage('collapse_expandimg','../img/plus.gif');
	else
		ChangeImage('collapse_expandimg','../img/minus.gif');
}
//--------------------------------------------------------------------------------------------------------------------------
function tglContent()
{
	var idarr = new Array("ConstructorBox","DestructorBox","MethodBox","FieldBox","ConstantsBox","PropertyBox","EventBox","EnumItemsBox","SeeAlso_RefBox");
	if(contentshowing)
	{
		for(ix in idarr)
		{
			if(HdDiv(idarr[ix]))
				ChangeImage(idarr[ix]+'Img','../img/plus.gif');
		}
		contentshowing = false;
	}
	else
	{
		for(ix in idarr)
		{
			if(ShDiv(idarr[ix]))
				ChangeImage(idarr[ix]+'Img','../img/minus.gif');
		}
		contentshowing = true;
	}
}
//--------------------------------------------------------------------------------------------------------------------------
function HdDiv(ID)
{
	objElem = GetElement(ID);
	if (objElem!=null)
	{
		objElem.style.display = 'none';
		return true;
	}
	else
		return false;
}
//--------------------------------------------------------------------------------------------------------------------------
function ShDiv(ID)
{
	objElem = GetElement(ID);
	if (objElem!=null)
	{
		objElem.style.display = '';
		return true;
	}
	else
		return false;
}
//--------------------------------------------------------------------------------------------------------------------------
function Set_Clsnm(ID,classname)
{
	objElem = GetElement(ID);
	if (objElem!=null)
		objElem.className = classname;
}

