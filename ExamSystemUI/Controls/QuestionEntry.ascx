<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionEntry.ascx.cs" Inherits="Controls_QuestionEntry" %>
<script type="text/javascript" language="javascript">

    var x=0;
    var TotalTextBoxes=0;
    var flag=false;
    var sID = new String();
    var iTextBoxNumber=0;
    
    function StoreTxtBoxCheckBoxValue(HiddenFieldID,lblErrorID,selectTypeID)
    {
        //alert(HiddenFieldID);
        
        var i=0;
        var oHiddenFieldID = document.getElementById(HiddenFieldID);
        var olblErrorID = document.getElementById(lblErrorID);
        var oselectTypeID = document.getElementById(selectTypeID);
        oHiddenFieldID.value="";
        olblErrorID.innerHTML="";
        
        for(i=0;i<=x;i++)
        {
             var oTextBoxID=document.getElementById(i.toString());
             var oChkBoxID =document.getElementById("CHE"+i.toString());
             
             if(oTextBoxID!=null && oChkBoxID!=null)
             {
                //alert(oTextBoxID+":"+oChkBoxID);
                oHiddenFieldID.value=oHiddenFieldID.value+oTextBoxID.value+":"+oChkBoxID.checked+"@";
             }
        }
        
        //alert(oHiddenFieldID.value);
    }
    
    function SelecObjOrDes(Obj,lblErrorID)
    {
        //alert(Obj);
        var IDOfSelect = document.getElementById(Obj);
        var olblErrorID = document.getElementById(lblErrorID);
        
        olblErrorID.innerHTML="";
        
        //alert(IDOfSelect.selectedIndex);
        if(IDOfSelect.selectedIndex==2)
        {
            AppendTextBox();
        }
        else
        {
           var cell = document.getElementById("TextBoxContainer");

            if ( cell.hasChildNodes() )
            {
                while ( cell.childNodes.length >= 1 )
                {
                    cell.removeChild( cell.firstChild );       
                } 
            }
        }
        //IDOfSelect.selectedIndex=0;
        return true;
    }
    

    
    function AppendTextBox()
    {
        var oTxt = document.createElement('input');
        oTxt.type = "text";
        //oTxt.value="abc";
        x=x+1;
        TotalTextBoxes=TotalTextBoxes+1;
        //alert("Total Created Boxes:"+x);     
        //alert("iTextBoxNumber:"+iTextBoxNumber);  
        var id =x.toString();
        oTxt.id = id;  
               
        oTxt.onclick =  function(e) 
        {
            
            sID=oTxt.id.toString();
            
            //alert("Clicked Text Box ID:"+sID);
            iTextBoxNumber = parseInt(sID); //clicked TextBox's ID
            //alert("Click Total Boxes:"+TotalTextBoxes);
            //alert("Clicked TextBox's ID:"+iTextBoxNumber.toString());
            AppendTextBox();
        }
                
        //if(x-iTextBoxNumber==1 || NoTextBoxfrom[iTextBoxNumber+1 to x])
        //NoTextBoxes(start,end);
        var start=iTextBoxNumber+1;
        var end=x;
        var flag=true;
        for(var i=start;i<end;i++)
        {
            var oTextBoxID=document.getElementById(i.toString());
            if(oTextBoxID!=null)
            {
                flag=false;
                break;
            }
            //alert(oTextBoxID +":"+ i);
            //alert(document.getElementById("TextBoxContainer").contains(oTextBoxID) +":"+i);
        }
        
        if(x-iTextBoxNumber==1 || flag)
        {
            //create x number of textboxes
            var oTxtContainer= document.getElementById("TextBoxContainer");
            
            oTxtContainer.appendChild(oTxt);
                                    
            var oSpan=document.createElement('span');
            oSpan.id="SPA"+x.toString();
            oSpan.innerHTML=" ";
            oTxtContainer.appendChild(oSpan);
            
            var oCheckBox=document.createElement('input');
            oCheckBox.id="CHE"+x.toString();
            oCheckBox.type="checkbox";
            oTxtContainer.appendChild(oCheckBox);
            
            var oSpanText=document.createElement('span');
            oSpanText.id="ISV"+x.toString();
            oSpanText.innerHTML=" Is Valid ";
            oTxtContainer.appendChild(oSpanText);
            
            var oButton=document.createElement('input');
            oButton.id="BTN"+x.toString();
            oButton.type="button";
            oButton.value="Delete";
            
            oButton.onclick = function(eButton)
            {
                var IDTextBoxContainer = document.getElementById("TextBoxContainer");
                
                var sButtonID = new String();
                sButtonID=oButton.id.toString();
                var iButtonID = parseInt(sButtonID.substring(3,sButtonID.length)); // Find the Button Number
                //alert(iButtonID);
                
                var IDTextBoxID = document.getElementById(iButtonID.toString()); // get the ID of the Text Box
                var IDSpaceID = document.getElementById("SPA"+iButtonID.toString()); //get the ID of Space
                var IDCheckBoxID = document.getElementById("CHE"+iButtonID.toString()); //get the ID of CheckBox
                var IDIsValidID = document.getElementById("ISV"+iButtonID.toString()); //get the ID of IsValid
                var IDButtonID = document.getElementById(sButtonID); // get the ID of the Button
                
                IDTextBoxContainer.removeChild(IDTextBoxID); //remove Text
                IDTextBoxContainer.removeChild(IDSpaceID);
                IDTextBoxContainer.removeChild(IDCheckBoxID);
                IDTextBoxContainer.removeChild(IDIsValidID);
                IDTextBoxContainer.removeChild(IDButtonID);  //remove Button
                
                //alert("Last Total Items:"+x);
                //alert("Button ID:"+iButtonID);
                
                
                
                
                //x=x-1;
                TotalTextBoxes=TotalTextBoxes-1;
                //alert("Delete Total Boxes:"+TotalTextBoxes);
                if(TotalTextBoxes==0)
                {
                    x=0;
                    iTextBoxNumber=0;
                }
                
               
                
            }
            
            oTxtContainer.appendChild(oButton); 
             
            CreateTable(); 
        }
        else
        {
            x=x-1;
            TotalTextBoxes=TotalTextBoxes-1;
        }

        //alert("Out:"+iTextBoxNumber.toString()+"="+x.toString());
       
    }
    /*function NoTextBoxes(start,end)
    {
        alert("Start:"+start+" End:"+end);
        var i=0;
        for(i=start;i<=end;i++)
        {
            alert(document.getElementById("TextBoxContainer").contains(document.getElementById(start.toString())));
            if(document.getElementById("TextBoxContainer").contains(document.getElementById(start.toString())))
            {
                break;
            }
        }
        
        if(i<=end)
        {
            return false;
        }
        
        return true;
    }*/
    function CreateTable()
    {
        //alert(x);
        var t=0;
        if(flag)
        {
            //remove previous table from TD=TextBoxContainer    
        }
        var oTable=document.createElement('table');
        oTable.setAttribute('cellspacing','0');
        oTable.setAttribute('cellpadding','0');
        oTable.setAttribute('border','0');
        
        //for(t=1;t<=x;t++)
        //{
            var oRow=document.createElement('tr');
            var otd=document.createElement('td');
            //otd1.innerHTML="new";
            oRow.appendChild(otd);
            oTable.appendChild(oRow);
        //}
        document.getElementById("TextBoxContainer").appendChild(oTable);
        
        
        flag=true; 
    }
    
    function checkMaxCharacterLength(sender,args)
    {
        var sDescription = new String();
        
        var flag = true;
        
        sDescription = args.Value;
       
        if (sDescription.length>1000)
        {
            flag = false;
            args.IsValid=false;
        }
        
        if(flag)
        {
            args.IsValid=true;
        }
    }
    
    function checkSelectCategory(sender,args)
    {
        var sCategoryName = new String();
        
        var flag = true;
        
        sCategoryName = args.Value;
        
        if(sCategoryName=="[Select One]")
        {
            flag = false;
            args.IsValid=false;
        }
        
        if(flag)
        {
            args.IsValid=true;
        }
    }
    
    function checkSelectLevel(sender,args)
    {
        var sLevelName = new String();
        
        var flag = true;
        
        sLevelName = args.Value;
        
        if(sLevelName=="[Select One]")
        {
            flag = false;
            args.IsValid=false;
        }
        
        if(flag)
        {
            args.IsValid=true;
        }
    }
    
    function checkSelectType(sender,args)
    {
        var sTypeName = new String();
        
        var flag = true;
        
        sTypeName = args.Value;
        
        if(sTypeName=="[Select One]")
        {
            flag = false;
            args.IsValid=false;
        }
        else if(sTypeName=="Objective")
        {
            var olblErrorID = document.getElementById("ctl00_CPH_Main_ctl00_lbl_error");
            
            var bAnyCheckBoxIsChecked=false;
            var bAnyOptionIsGiven=false;
            
            var bAnyCheckboxOrTextBoxIsExisted=false;
        
            var sErrorString = new String();
            
            var i=0;
            
            //alert(x);
        
            for(i=0;i<=x;i++)
            {
                 var oTextBoxID=document.getElementById(i.toString());
                 var oChkBoxID =document.getElementById("CHE"+i.toString());
                 
                 if(oTextBoxID!=null && oChkBoxID!=null)
                 {
                    bAnyCheckboxOrTextBoxIsExisted=true;
                    
                    if(oChkBoxID.checked)
                    {
                        bAnyCheckBoxIsChecked=true;
                    }
                    
                    if(oTextBoxID.value.length>0)
                    {
                        bAnyOptionIsGiven=true;
                    }
                 }
            }
            
            //alert(oHiddenFieldID.value);
            
            olblErrorID.innerHTML="";
            sErrorString="";
            
            //alert(x);
            
            if(bAnyCheckboxOrTextBoxIsExisted)
            {
                if(bAnyCheckBoxIsChecked==false || bAnyOptionIsGiven==false)
                {
                    if(bAnyCheckBoxIsChecked==false)
                    {
                        sErrorString=sErrorString+"<li>At least one check box must be checked.</li>";
                    }
                    
                    if(bAnyOptionIsGiven==false)
                    {
                        sErrorString=sErrorString+"<li>At least one objective answer must given.</li>";
                    }
                  
                    olblErrorID.innerHTML="<font color='red'><ul>"+sErrorString+"</ul></font>";
                
                    flag = false;
                    args.IsValid=false;
                }
            }
        }
        else if(sTypeName=="Descriptive")
        {
        
        }
        
        if(flag)
        {
            args.IsValid=true;
        }    
    }
    
    function CheckPhotoExtension(sender,args)
    {
        var sPhotoName = new String();
        
        var flag = true;
        
        sPhotoName = args.Value;
       
        if (sPhotoName.lastIndexOf(".gif",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".png",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".jpg",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".jpeg",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".bmp",sPhotoName.length-1) > 0)
        {

        }
        else
        {
            flag = false;
            args.IsValid=false;
        }
        
        if(flag)
        {
            args.IsValid=true;
        }
    
    }
    
    function ShowValidQuestionImage(oFup,olblErrorID)
    {
        var IDFup = document.getElementById(oFup);
        //var IDImg = document.getElementById(oImg);
        var IDlblError = document.getElementById(olblErrorID);
        
        //alert(IDImg+":"+oImg);
        
        //IDImg.visible = true;
        
        var sPhotoName = new String();
        
        sPhotoName=IDFup.value;
        
        if (sPhotoName.lastIndexOf(".gif",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".png",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".jpg",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".jpeg",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".bmp",sPhotoName.length-1) > 0)
        {
            //alert(sPhotoName+":"+IDImg);
            //IDImg.src="image001.jpg";
            
            //var imageSource= new Image();
            
            
            //alert(imageSource);
            
            //imageSource.src = IDFup.value;
            //alert(imageSource.src);
            
            //alert(IDImg.toString());
            
            //IDImg.src = imageSource.src; 
            
            //alert(IDImg.src);
            
           
           
        }
        else
        {
            //alert(sPhotoName);
            
            //IDlblError.innerHTML="";
            //IDlblError.innerHTML="<font color='red'><ul><li>valid question diagram extension(.gif,.png,.jpg,.jpeg,.bmp)</li></ul></font>";
        }
        
        
        
    }
    
</script>
<center>
    <table id="tblMain" border="1" style="margin:none;border:1px solid #296488" bgcolor="aliceblue" cellspacing="0" cellpadding="0">
        <tr>
            <td style="border:none; width: 20px;">
            </td>
            <td style="vertical-align: top;text-align: left; border:none;">
                <asp:ValidationSummary id="vsQuestionEntry" HeaderText="Following fields missing or invalid" runat="server"></asp:ValidationSummary>
            </td>
            <td style="width: 20px; border:none;">
            </td>
        </tr>
        <tr>
            <td style="width:20px; border:none; height:20px">
            
            </td>
            <td style="border:none; text-align:left; vertical-align:top;height:20px">
                <asp:Label ID="lbl_error" runat="server"></asp:Label>
            </td>
            <td style="width:20px; border:none;height:20px">
            
            </td>
        </tr>
        <tr>
            <td style="width:20px; border:none">
            
            </td>
            <td style=" border:none">
                <table id="tblQuestionEntry" border="1" style="margin:none;border:1px solid green" cellspacing="0" cellpadding="0" bgcolor="#e0ffff">
                    <tr>
                        <td style="border:none; text-align:left" valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" style="margin:0">
                                <tr>
                                    <td style="width: 5px"  bgcolor="#b0e0e6">
                                    </td>
                                    <td colspan="7" style="text-align: left" valign="top"  bgcolor="#b0e0e6">
                                         <asp:Label ID="Label8" runat="server" Text="Question Entry" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 10px"  bgcolor="#b0e0e6">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td style="text-align:left;" valign="top">
                                        <asp:Label ID="Label4" runat="server" Text="Category"></asp:Label></td>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dr_SaveCategory" runat="server" Width="150px">
                                        </asp:DropDownList><span style="color:Red; vertical-align:top">*</span><asp:CustomValidator
                                            ID="cusvSelectCategory" runat="server" ClientValidationFunction="checkSelectCategory"
                                            ControlToValidate="dr_SaveCategory" Display="None" ErrorMessage="Must select a category"
                                            SetFocusOnError="True"></asp:CustomValidator>
                                    </td>
                                    <td style="width:10px">
                                    
                                    </td>
                                    <td style="text-align:left" valign="top">
                                    
                                    </td>
                                    <td style="width:5px">
                                    
                                    </td>
                                    <td style="text-align:left" valign="top">
                                    
                                    </td>
                                    <td style="width:10px">
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label2" runat="server" Text="Question Level"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drDn_QuestionLevel" runat="server" Width="150px">
                                        </asp:DropDownList><span style="vertical-align: top; color: red">*</span><asp:CustomValidator
                                            ID="cusvLevel" runat="server" ClientValidationFunction="checkSelectLevel" ControlToValidate="drDn_QuestionLevel"
                                            Display="None" ErrorMessage="Must select a Level" SetFocusOnError="True"></asp:CustomValidator></td>
                                    <td style="width: 10px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label5" runat="server" Text="Question"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_SaveQuestion" runat="server" TextMode="MultiLine" MaxLength="1000" Width="300px" Height="150px"></asp:TextBox><span style="color:Red; vertical-align:top">*</span></td>
                                    <td style="width: 10px; vertical-align:top; text-align:left">
                                        <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ControlToValidate="txt_SaveQuestion"
                                            Display="None" ErrorMessage="Question required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cusvMaxLength" runat="server" ClientValidationFunction="checkMaxCharacterLength"
                                            ControlToValidate="txt_SaveQuestion" Display="None" ErrorMessage="Maximum character length is 1000 for question text"
                                            SetFocusOnError="True"></asp:CustomValidator></td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label6" runat="server" Text="Default Mark"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:TextBox ID="txt_SaveDefaultMark" runat="server" Width="40px"></asp:TextBox><span style="vertical-align: top; color: red">*</span></td>
                                    <td style="width: 10px; text-align:left; vertical-align:top">
                                        <asp:RequiredFieldValidator ID="rfvDefaulMark" runat="server" ControlToValidate="txt_SaveDefaultMark"
                                            Display="None" ErrorMessage="Default mark required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvDefaultMark" runat="server" ControlToValidate="txt_SaveDefaultMark"
                                            Display="None" ErrorMessage="Default mark must greater than zero" Operator="GreaterThan"
                                            SetFocusOnError="True" Type="Double" ValueToCompare="0"></asp:CompareValidator></td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <div id="ImageSourceContainer">
                                        </div>
                                    </td>
                                    <td style="vertical-align: top; width: 10px; text-align: left">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="vertical-align: top; width: 10px; text-align: left">
                                    </td>
                                </tr>
                               <%-- <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label3" runat="server" Text="Question Diagram(image)"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:FileUpload ID="fupQuestiondiagram" runat="server"/></td>
                                    <td style="vertical-align: top; width: 10px; text-align: left">
                                        <asp:RegularExpressionValidator ID="revQuestionPhoto" runat="server" ControlToValidate="fupQuestiondiagram"
                                            Display="None" ErrorMessage="Character  - and  '  is not allowed for question image"
                                            SetFocusOnError="True" ValidationExpression="([^'-]{1,400})"></asp:RegularExpressionValidator>
                                        <asp:CustomValidator ID="cusvPhoto" runat="server" ClientValidationFunction="CheckPhotoExtension"
                                            ControlToValidate="fupQuestiondiagram" Display="None" ErrorMessage="Valid image Extension(.gif,.png,.jpg,.jpeg,.bmp)"
                                            SetFocusOnError="True"></asp:CustomValidator></td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="vertical-align: top; width: 10px; text-align: left">
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label1" runat="server" Text="Possible Time"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left; vertical-align:top">
                                        <asp:TextBox ID="txt_PossibleAnswerTime" runat="server" Width="148px"></asp:TextBox><span style="color:Red; vertical-align:top">*</span>Minute</td>
                                    <td style="width: 10px; text-align:left; vertical-align:top">
                                        <asp:RequiredFieldValidator ID="rfvPossibleTime" runat="server" ControlToValidate="txt_PossibleAnswerTime"
                                            Display="None" ErrorMessage="Possible time required." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvPossibleTime" runat="server" ControlToValidate="txt_PossibleAnswerTime"
                                            Display="None" ErrorMessage="Possible time must greater than zero" Operator="GreaterThan"
                                            SetFocusOnError="True" Type="Double" ValueToCompare="0"></asp:CompareValidator></td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        <asp:Label ID="Label7" runat="server" Text="Type"></asp:Label></td>
                                    <td style="width: 5px">
                                    </td>
                                    <td>
                                    <select id="SelectQuestionSave" style="width: 150px" runat="server">
                                        <option id="SOne" selected="selected">[Select One]</option>
                                        <option id="SObjective">Descriptive</option>
                                        <option id="SDescriptive">Objective</option>
                                    </select><span style="color:Red; vertical-align:top">*<asp:CustomValidator ID="cusvQuestionType"
                                            runat="server" ClientValidationFunction="checkSelectType" ControlToValidate="SelectQuestionSave"
                                            Display="None" SetFocusOnError="True"></asp:CustomValidator></span>
                                    </td>
                                    <td style="width: 10px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td id="TextBoxContainer" style="text-align:left;" valign="top">
                                        
                                          
                                    </td>
                                    <td style="width: 10px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click" /></td>
                                    <td style="width: 10px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                        
                                    </td>
                                    <td style="width: 5px">
                                    </td>
                                    <td style="text-align: left" valign="top">
                                    </td>
                                    <td style="width: 10px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:20px; border:none">
            </td>
        </tr>
        <tr>
            <td style="width:20px; height:20px; border:none">
            
            </td>
            <td style="height:20px; border:none">
                <asp:HiddenField ID="HiddenStoreTxtBoxCheckBoxValue" runat="server"/>    
            </td>
            <td style="width:20px; height:20px; border:none">
            
            </td>
        </tr>
    </table>
</center>

