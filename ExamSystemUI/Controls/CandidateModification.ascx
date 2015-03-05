<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CandidateModification.ascx.cs" Inherits="Controls_CandidateModification" %>
<script type="text/javascript" language="javascript">
    function check_uncheck(Val)
    {
      var ValChecked = Val.checked;
      var ValId = Val.id;
      var frm = document.forms[0];
            
      for (i = 0; i < frm.length; i++)
      {
                
        if (this != null)
        {
          if (ValId.indexOf('CheckAll') !=  - 1)
          {
            
            if (ValChecked)
            {  
              if(frm.elements[i].type.toString()=="checkbox")
              {
                frm.elements[i].checked = true;
              }
              
              
            }
            else
            {
              if(frm.elements[i].type.toString()=="checkbox")
              {
                frm.elements[i].checked = false;
              }
              
              
            }
          }
          else if (ValId.indexOf('deleteRec') !=  - 1)
          {
      
            if (frm.elements[i].checked == false)
              frm.elements[1].checked = false;
          }
        }
      } 
    } 
    
    function confirmMsg(frm)
    {
        var flag=false;
        var iIndexOfaRow=-1;
       
        for (i = 0; i < frm.length; i++)
        {
            if (frm.elements[i].name.indexOf("deleteRec") !=  - 1)
            {
                iIndexOfaRow=iIndexOfaRow+1;
                if (frm.elements[i].checked)
                {    
                    flag=true;
                }
            }
        }
        
        if(flag)
        {
            return confirm('Are you sure you want to delete these Candidates?');
        }
    } 
    
    

//    function CheckPhotoExtension(sender,args)
//    {
//        var sPhotoName = new String();
//        
//        var flag = true;
//        
//        sPhotoName = args.Value;
//       
//        if (sPhotoName.lastIndexOf(".gif",0) > 0 || sPhotoName.lastIndexOf(".png",0) > 0 || sPhotoName.lastIndexOf(".jpg",0) > 0 || sPhotoName.lastIndexOf(".jpeg",0) > 0 || sPhotoName.lastIndexOf(".bmp",0) > 0)
//        {

//        }
//        else
//        {
//            flag = false;
//            args.IsValid=false;
//        }
//        
//        if(flag)
//        {
//            args.IsValid=true;
//        }
//    
//    }
    function CheckSelectedCheckBox(frm)
    {
        var flagForValidPhoto=true;
        var flagForValidCV=true;
        var iIndexOfaRow=-1;
        
        //ctl00_CPH_Main_ctl00_Grid_Candidates_ctl02_fup_CandidatePhoto
        //ctl00_CPH_Main_ctl00_Grid_Candidates_ctl03_fup_CandidatePhoto


        //ctl00_CPH_Main_ctl00_Grid_Candidates_ctl02_fupNewCVName
        //ctl00_CPH_Main_ctl00_Grid_Candidates_ctl03_fupNewCVName
        
        var i=0;
        
        var iTwoForPhotoAndCV = 2;
        
        var iSum=0;
        
        var sIdStringForPhotoAndCV = new String();
        
        var sIdForPhoto = new String();
         
        var sIdForCV = new String();
        
        var obLebelErrorID = document.getElementById("ctl00_CPH_Main_ctl00_lbl_error");
        
        var sErrorsInPhotoAndCV = new String();
        sErrorsInPhotoAndCV="";
        
        //alert(obLebelErrorID);
        
        //alert(sErrorsInPhotoAndCV);
        
        for (i = 0; i < frm.length; i++)
        {
            if (frm.elements[i].name.indexOf("deleteRec") !=  - 1)
            {
                iIndexOfaRow=iIndexOfaRow+1;
                
                iSum = iIndexOfaRow +  iTwoForPhotoAndCV;
                    
                sIdStringForPhotoAndCV=iSum.toString(); 
                
                if (frm.elements[i].checked)
                {    
                    sIdForPhoto = "ctl00_CPH_Main_ctl00_Grid_Candidates_ctl0" + sIdStringForPhotoAndCV + "_fup_CandidatePhoto";
                    sIdForCV = "ctl00_CPH_Main_ctl00_Grid_Candidates_ctl0" + sIdStringForPhotoAndCV + "_fupNewCVName";
                    
                    var obIDPhoto = document.getElementById(sIdForPhoto);
                    var obIDCV = document.getElementById(sIdForCV);
                    
                    //alert(obIDPhoto);
                    //alert(obIDCV);
                    
                    var sPhotoName = new String();
                    var sCVName = new String();
                    
                    sPhotoName=obIDPhoto.value;
                    sCVName=obIDCV.value;
                    
                    var bAnyError = false;
                    
                    if(sPhotoName.length>0)
                    {
                        //alert(sPhotoName.lastIndexOf(".jpg",0));
                        
                        if (sPhotoName.lastIndexOf(".gif",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".png",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".jpg",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".jpeg",sPhotoName.length-1) > 0 || sPhotoName.lastIndexOf(".bmp",sPhotoName.length-1) > 0)
                        {
                                
                        }
                        else
                        {
                            flagForValidPhoto=false;
                            
                            bAnyError = true;
                            
                            //alert(sErrorsInPhotoAndCV);
                            sErrorsInPhotoAndCV= sErrorsInPhotoAndCV +"In valid photo extension in row "+iIndexOfaRow.toString()+".";
                            //alert(sErrorsInPhotoAndCV);
                        }
                    }
                    
                    if(sCVName.length>0)
                    {
                        if (sCVName.lastIndexOf(".htm",sCVName.length-1) > 0 || sCVName.lastIndexOf(".html",sCVName.length-1) > 0 || sCVName.lastIndexOf(".doc",sCVName.length-1) > 0 || sCVName.lastIndexOf(".pdf",sCVName.length-1) > 0 || sCVName.lastIndexOf(".rtf",sCVName.length-1) > 0)
                        {
                                
                        }
                        else
                        {
                            flagForValidCV=false;
                            
                            bAnyError = true;
                            
                            //alert(sErrorsInPhotoAndCV);
                            sErrorsInPhotoAndCV= sErrorsInPhotoAndCV + " In valid CV extension in row "+iIndexOfaRow.toString()+".";
                            //alert(sErrorsInPhotoAndCV);
                        }
                    }
                    
                    if(bAnyError)
                    {
                        //alert(sErrorsInPhotoAndCV);
                        sErrorsInPhotoAndCV=sErrorsInPhotoAndCV+"<br/>";
                        //alert(sErrorsInPhotoAndCV);
                    }
                }
            }
        }
        
        //alert(flagForValidPhoto);
        //alert(flagForValidCV);
        
        //alert(sErrorsInPhotoAndCV);
        
        if(flagForValidPhoto && flagForValidCV)
        {
            //alert(obLebelErrorID);
        }
        else
        {
            obLebelErrorID.visible=true;
            obLebelErrorID.innerHTML="<font color='red'>All Selected photo & cv extension must be valid."+"<br/>Valid CV Extension(.html,.htm,.doc,.pdf,.rtf)"+"<br/>Valid Photo Extension(.gif,.png,.jpg,.jpeg,.bmp)"+"<br/>"+sErrorsInPhotoAndCV+"</font>" ;  
            
            //alert(obLebelErrorID.innerHTML);
            
            return false;
        }
        
        return true;
    }
</script>
<center>
    <table border="1" bgcolor="aliceblue" cellspacing="0" cellpadding="0" style="margin:none; border:1px solid #296488">
        <tr>
            <td style="width:20px; height:20px; border:none">
            </td>
            <td style="height:20px; border:none; text-align:left">
                <asp:Label ID="lbl_error" runat="server"></asp:Label>
            </td>
            <td style="width:20px; height:20px; border:none">
            
            </td>
        </tr>
        <tr>
            <td style="width:20px; border:none">
            </td>
            <td style=" border:none">
                <table id="tblCanModification" border="1" cellspacing="0" cellpadding="0" style="margin:none;border:1px solid green" runat="server">
                    <tr>
                        <td bgcolor="#b0e0e6" style="border:none; width:5px">
                        </td>
                        <td align="left" bgcolor="#b0e0e6" style="border:none;" valign="top">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Candidate Modification For Exam:"></asp:Label><asp:Label ID="lbl_ExamName" runat="server" Font-Bold="True" ForeColor="Desktop"></asp:Label></td>
                        <td bgcolor="#b0e0e6" style="border:none; width:10px">
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#b0e0e6" style="border:none; width:5px">
                        </td>
                        <td align="left" bgcolor="#b0e0e6" style="border:none;" valign="top">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Total number of candidates:"></asp:Label>
                            <asp:Label ID="lblTotalCandidates" runat="server" Font-Bold="True" ForeColor="Desktop"></asp:Label></td>
                        <td bgcolor="#b0e0e6" style="border:none; width:10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="border:none; width:5px">
                        
                        </td>
                        <td style="border:none">
                            <asp:Panel ID="pnl_Candidates" runat="server" ScrollBars="Horizontal" Width="1000px">
                                <asp:GridView ID="Grid_Candidates" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None"  AllowPaging="True" OnRowDataBound="Grid_Candidates_RowDataBound" OnPageIndexChanging="Grid_Candidates_PageIndexChanging" PageSize="10">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckAll" runat="server" onclick="return check_uncheck (this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="deleteRec" runat="server" onclick="return check_uncheck (this);" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Candidate ID">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblCandidateID" runat="server" Text='<%# Eval("CandidateForExamCandidate.CandidateCompositeID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Password">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNewPassword" Width="100px" runat="server" MaxLength="200" TextMode="Password"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNewEmail" Width="100px" runat="server" MaxLength="100" Text='<%# Eval("CandidateForExamCandidate.CandidateEmail") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNewName" Width="50px" runat="server" Text='<%# Eval("CandidateForExamCandidate.CandidateName") %>' MaxLength="100"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Result">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <table border="0" cellpadding="0" cellspacing="1" style="width:100%">
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList id="dr_SelectCGOrMark" runat="server" Width="70px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtLastResult" Width="40px" runat="server" Text='<%# Eval("CandidateForExamCandidate.CandidateLastResult") %>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label id="lblOutOf" runat="server" Text="Of"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox id="txtOutOf" runat="server" Width="30px" Text='<%# Eval("CandidateForExamCandidate.CandidateLastResultRange") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Institution">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLastInstitution" Width="70px" runat="server" Text='<%# Eval("CandidateForExamCandidate.CandiadteLastInstitution") %>' MaxLength="50"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PassingYear">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLastPassingYear" Width="80px" runat="server" Text='<%# Eval("CandidateForExamCandidate.CandidateLastPassingYear") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New Photo">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                 <%--<table border="0" cellpadding="0" cellspacing="1" style="width:100%">
                                                    <tr>
                                                        <td>--%>
                                                            <asp:FileUpload id="fup_CandidatePhoto" runat="server"></asp:FileUpload>        
                                                        <%--</td>
                                                        <td>
                                                            <asp:CustomValidator id="cusvPhoto" runat="server" SetFocusOnError="True" ErrorMessage="Valid Photo Extension(.gif,.png,.jpg,.jpeg,.bmp)" Display="None" ControlToValidate="fup_CandidatePhoto" ClientValidationFunction="CheckPhotoExtension"></asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                 </table>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Old CV Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hyperLinkOldCV" runat="server" Target="_self" Width="200px"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="New CV Name">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:FileUpload ID="fupNewCVName" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                        <td style="border:none; width:10px">
                        
                        </td>
                    </tr>
                    <tr>
                        <td style="border:none; width:5px">
                        
                        </td>
                        <td style="border:none; text-align:left">
                            <asp:Button ID="btn_Remove" runat="server" Text="Remove" OnClick="btn_Remove_Click" OnClientClick="return confirmMsg(this.form)" /> 
                            <asp:Button ID="btn_Update" runat="server" Text="Update" OnClick="btn_Update_Click" OnClientClick="return CheckSelectedCheckBox(this.form)"/></td>
                        <td style="border:none; width:10px">
                        
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
                
            </td>
            <td style="width:20px; height:20px; border:none">
            </td>
        </tr>
    </table>
</center>